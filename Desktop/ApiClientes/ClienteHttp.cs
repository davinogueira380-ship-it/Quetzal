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

using Newtonsoft.Json;                 // Necessario para serializar/desserializar JSON
using System.Net.Http.Headers;         // Necessario para MediaTypeHeaderValue e AuthenticationHeaderValue
using System.Text;                     // Necessario para Encoding.UTF8

namespace Quetzal.Desktop.ApiClientes
{
    public abstract class ClienteHttp
    {
        private const string UrlBase = "http://localhost:5031";

        private static readonly HttpClient _httpClient = CriarHttpClient();

        // --------------------------------------------------------
        // Criacao do HttpClient com handler de certificado customizado
        // --------------------------------------------------------

        private static HttpClient CriarHttpClient()
        {
            // Cria o handler que permite ignorar erros de certificado SSL autoassinado
            var handler = new HttpClientHandler
            {
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

        private StringContent SerializarCorpo(object corpo)
        {
            // Serializa o objeto para string JSON usando Newtonsoft.Json
            var json = JsonConvert.SerializeObject(corpo);

            // Cria o conteudo HTTP com encoding UTF-8 e tipo application/json
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T> DesserializarRespostaAsync<T>(HttpResponseMessage resposta)
        {
            // Le o conteudo da resposta como string assincrona
            var conteudo = await resposta.Content.ReadAsStringAsync();

            // Desserializa a string JSON para o tipo esperado e retorna
            return JsonConvert.DeserializeObject<T>(conteudo);
        }

        protected async Task<TResposta> PostAsync<TResposta>(string rota, object corpo)
        {
            try
            {
                AplicarAutenticacao();

                var resposta = await _httpClient.PostAsync(rota, SerializarCorpo(corpo));

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

        protected async Task<TResposta> PostSemAutenticacaoAsync<TResposta>(string rota, object corpo)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;

                var resposta = await _httpClient.PostAsync(rota, SerializarCorpo(corpo));

                // Garante que a resposta e de sucesso
                resposta.EnsureSuccessStatusCode();

                return await DesserializarRespostaAsync<TResposta>(resposta);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao comunicar com a API (POST publico {rota}): {ex.Message}", ex);
            }
        }

        protected async Task<TResposta> GetAsync<TResposta>(string rota)
        {
            try
            {
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
