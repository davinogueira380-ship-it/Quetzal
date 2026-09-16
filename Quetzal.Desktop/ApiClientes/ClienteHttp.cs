// ============================================================
// Nome:         ClienteHttp.cs
// Objetivo:     Classe base para todos os clientes de API do projeto.
//               Centraliza configuracao do HttpClient, serializacao
//               JSON e tratamento de erros de comunicacao.
// Camada:       ApiClientes (infraestrutura de comunicacao)
// Participacao: Herdada por todos os ApiClientes especificos
//               (AuthApiCliente, FilmeApiCliente, etc.) para
//               reutilizar logica de requisicao HTTP.
// ============================================================

using System;                          // Necessario para Exception e Uri
using System.Net.Http;                 // Necessario para HttpClient e HttpContent
using System.Net.Http.Headers;         // Necessario para MediaTypeHeaderValue e AuthenticationHeaderValue
using System.Text;                     // Necessario para Encoding.UTF8
using System.Threading.Tasks;          // Necessario para Task e operacoes assincronas
using Newtonsoft.Json;                 // Necessario para serializar/desserializar JSON
using Quetzal.Desktop.Sessao;        // Necessario para acessar o token JWT da sessao

namespace Quetzal.Desktop.ApiClientes
{
    /// <summary>
    /// Classe base abstrata para todos os clientes HTTP do Quetzal Desktop.
    /// Fornece metodos reutilizaveis para GET, POST, PUT e DELETE com suporte a JWT.
    /// </summary>
    public abstract class ClienteHttp
    {
        // --------------------------------------------------------
        // Constantes e campos internos
        // --------------------------------------------------------

        // URL base da API REST do Quetzal (backend ASP.NET Core)
        private const string UrlBase = "http://localhost:5277";

        // Instancia compartilhada do HttpClient (boas praticas: reutilizar)
        // O handler ignora erros de certificado SSL em ambiente de desenvolvimento local
        private static readonly HttpClient _httpClient = CriarHttpClient();

        // --------------------------------------------------------
        // Criacao do HttpClient com handler de certificado customizado
        // --------------------------------------------------------

        /// <summary>
        /// Cria e configura o HttpClient para comunicacao com a API local.
        /// Ignora validacao de certificado SSL (apenas em desenvolvimento).
        /// </summary>
        private static HttpClient CriarHttpClient()
        {
            // Cria o handler que permite ignorar erros de certificado SSL autoassinado
            var handler = new HttpClientHandler
            {
                // Callback que sempre retorna true, ignorando erros de certificado
                // ATENCAO: Usar apenas em ambiente de desenvolvimento local
                ServerCertificateCustomValidationCallback = (mensagem, cert, chain, erros) => true
            };

            // Cria o HttpClient usando o handler customizado
            var cliente = new HttpClient(handler)
            {
                // Define a URL base para todas as requisicoes
                BaseAddress = new Uri(UrlBase)
            };

            // Define que o cliente aceita respostas no formato JSON
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Retorna o cliente configurado
            return cliente;
        }

        // --------------------------------------------------------
        // Metodo auxiliar para adicionar autenticacao JWT
        // --------------------------------------------------------

        /// <summary>
        /// Adiciona o token Bearer JWT ao cabecalho da requisicao corrente.
        /// Deve ser chamado antes de qualquer requisicao autenticada.
        /// </summary>
        protected void AplicarAutenticacao()
        {
            // Recupera o token JWT da sessao do usuario logado
            var token = SessaoUsuario.Instancia.Token;

            // Verifica se o token existe antes de aplicar
            if (!string.IsNullOrWhiteSpace(token))
            {
                // Define o cabecalho Authorization com o esquema Bearer
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // --------------------------------------------------------
        // Metodo auxiliar para criar corpo JSON da requisicao
        // --------------------------------------------------------

        /// <summary>
        /// Serializa um objeto C# para JSON e cria o StringContent HTTP.
        /// </summary>
        /// <param name="corpo">Objeto a ser serializado em JSON.</param>
        /// <returns>StringContent com o JSON serializado e encoding UTF-8.</returns>
        private StringContent SerializarCorpo(object corpo)
        {
            // Serializa o objeto para string JSON usando Newtonsoft.Json
            var json = JsonConvert.SerializeObject(corpo);

            // Cria o conteudo HTTP com encoding UTF-8 e tipo application/json
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        // --------------------------------------------------------
        // Metodo auxiliar para desserializar a resposta da API
        // --------------------------------------------------------

        /// <summary>
        /// Le o corpo da resposta HTTP e desserializa para o tipo T.
        /// </summary>
        /// <typeparam name="T">Tipo esperado na resposta da API.</typeparam>
        /// <param name="resposta">Objeto HttpResponseMessage recebido da API.</param>
        /// <returns>Objeto do tipo T desserializado da resposta JSON.</returns>
        private async Task<T> DesserializarRespostaAsync<T>(HttpResponseMessage resposta)
        {
            // Le o conteudo da resposta como string assincrona
            var conteudo = await resposta.Content.ReadAsStringAsync();

            // Desserializa a string JSON para o tipo esperado e retorna
            return JsonConvert.DeserializeObject<T>(conteudo);
        }

        // --------------------------------------------------------
        // Metodos HTTP publicos para uso pelos filhos
        // --------------------------------------------------------

        /// <summary>
        /// Realiza uma requisicao POST autenticada e retorna a resposta desserializada.
        /// </summary>
        /// <typeparam name="TResposta">Tipo esperado no corpo da resposta.</typeparam>
        /// <param name="rota">Caminho relativo da API (ex: "/api/filmes").</param>
        /// <param name="corpo">Objeto a ser enviado no corpo da requisicao.</param>
        /// <returns>Objeto do tipo TResposta ou valor padrao em caso de erro.</returns>
        protected async Task<TResposta> PostAsync<TResposta>(string rota, object corpo)
        {
            try
            {
                // Aplica o token JWT no cabecalho da requisicao
                AplicarAutenticacao();

                // Serializa o corpo e envia a requisicao POST para a rota informada
                var resposta = await _httpClient.PostAsync(rota, SerializarCorpo(corpo));

                // Garante que a resposta indica sucesso (lanca excecao se status >= 400)
                resposta.EnsureSuccessStatusCode();

                // Desserializa e retorna a resposta
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                // Lanca excecao de aplicacao com mensagem amigavel
                throw new Exception($"Erro ao comunicar com a API (POST {rota}): {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Realiza uma requisicao POST sem autenticacao (ex: login).
        /// </summary>
        /// <typeparam name="TResposta">Tipo esperado no corpo da resposta.</typeparam>
        /// <param name="rota">Caminho relativo da API.</param>
        /// <param name="corpo">Objeto a ser enviado no corpo.</param>
        /// <returns>Objeto do tipo TResposta desserializado.</returns>
        protected async Task<TResposta> PostSemAutenticacaoAsync<TResposta>(string rota, object corpo)
        {
            try
            {
                // Remove cabecalho de autenticacao para requisicoes publicas
                _httpClient.DefaultRequestHeaders.Authorization = null;

                // Envia a requisicao POST sem token JWT
                var resposta = await _httpClient.PostAsync(rota, SerializarCorpo(corpo));

                // Garante que a resposta e de sucesso
                resposta.EnsureSuccessStatusCode();

                // Desserializa e retorna a resposta
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                // Propaga o erro com contexto adicional
                throw new Exception($"Erro ao comunicar com a API (POST publico {rota}): {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Realiza uma requisicao GET autenticada e retorna a resposta desserializada.
        /// </summary>
        /// <typeparam name="TResposta">Tipo esperado no corpo da resposta.</typeparam>
        /// <param name="rota">Caminho relativo da API incluindo query string se necessario.</param>
        /// <returns>Objeto do tipo TResposta desserializado.</returns>
        protected async Task<TResposta> GetAsync<TResposta>(string rota)
        {
            try
            {
                // Aplica autenticacao JWT antes da requisicao
                AplicarAutenticacao();

                // Envia a requisicao GET para a rota informada
                var resposta = await _httpClient.GetAsync(rota);

                // Garante que a resposta indica sucesso
                resposta.EnsureSuccessStatusCode();

                // Desserializa e retorna o resultado
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                // Propaga com contexto adicional
                throw new Exception($"Erro ao comunicar com a API (GET {rota}): {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Realiza uma requisicao PUT autenticada e retorna a resposta desserializada.
        /// </summary>
        /// <typeparam name="TResposta">Tipo esperado no corpo da resposta.</typeparam>
        /// <param name="rota">Caminho relativo da API com o ID do recurso.</param>
        /// <param name="corpo">Objeto com os dados atualizados.</param>
        /// <returns>Objeto do tipo TResposta desserializado.</returns>
        protected async Task<TResposta> PutAsync<TResposta>(string rota, object corpo)
        {
            try
            {
                // Aplica autenticacao JWT
                AplicarAutenticacao();

                // Envia a requisicao PUT com o corpo serializado
                var resposta = await _httpClient.PutAsync(rota, SerializarCorpo(corpo));

                // Garante que a resposta indica sucesso
                resposta.EnsureSuccessStatusCode();

                // Desserializa e retorna
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                // Propaga com contexto adicional
                throw new Exception($"Erro ao comunicar com a API (PUT {rota}): {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Realiza uma requisicao DELETE autenticada e retorna a resposta desserializada.
        /// </summary>
        /// <typeparam name="TResposta">Tipo esperado no corpo da resposta.</typeparam>
        /// <param name="rota">Caminho relativo da API com o ID do recurso.</param>
        /// <returns>Objeto do tipo TResposta desserializado.</returns>
        protected async Task<TResposta> DeleteAsync<TResposta>(string rota)
        {
            try
            {
                // Aplica autenticacao JWT
                AplicarAutenticacao();

                // Envia a requisicao DELETE para a rota informada
                var resposta = await _httpClient.DeleteAsync(rota);

                // Garante que a resposta indica sucesso
                resposta.EnsureSuccessStatusCode();

                // Desserializa e retorna
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                // Propaga com contexto adicional
                throw new Exception($"Erro ao comunicar com a API (DELETE {rota}): {ex.Message}", ex);
            }
        }

        protected async Task<TResposta> PostAsync<TResposta>(string rota, MultipartFormDataContent conteudo)
        {
            try
            {
                AplicarAutenticacao();
                var resposta = await _httpClient.PostAsync(rota, conteudo);
                resposta.EnsureSuccessStatusCode();
                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao comunicar com a API (POST Multipart {rota}): {ex.Message}", ex);
            }
        }
    }
}
