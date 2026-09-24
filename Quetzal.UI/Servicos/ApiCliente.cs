using System.Net.Http.Headers;
using System.Net.Http.Json;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Servicos;

public class ApiCliente
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiCliente(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("QuetzalAPI");
        _httpContextAccessor = httpContextAccessor;
    }

    // Monta UMA requisição isolada, com o token do usuário DESTA requisição.
    // Nada é gravado no _httpClient, então não há vazamento entre usuários.
    private HttpRequestMessage CriarRequisicao(HttpMethod metodo, string endpoint)
    {
        var requisicao = new HttpRequestMessage(metodo, endpoint);

        var token = _httpContextAccessor.HttpContext?.Request.Cookies["quetzal_token"];
        if (!string.IsNullOrEmpty(token))
        {
            requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return requisicao;
    }

    // Todo o tratamento de resposta ficava repetido 5 vezes.
    // Agora mora num lugar só.
    private async Task<ApiResposta<T>> EnviarAsync<T>(HttpRequestMessage requisicao)
    {
        try
        {
            var resposta = await _httpClient.SendAsync(requisicao);

            // 401 = token expirou ou é inválido. Vale uma mensagem específica,
            // senão o usuário vê "Erro HTTP: Unauthorized" e não entende nada.
            if (resposta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return ApiResposta<T>.Falha("Sua sessão expirou. Faça login novamente.");
            }

            // A API devolve o corpo padronizado tanto no sucesso quanto no
            // BadRequest (erros de validação), então desserializamos os dois.
            if (resposta.IsSuccessStatusCode ||
                resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var resultado = await resposta.Content.ReadFromJsonAsync<ApiResposta<T>>();
                return resultado ?? ApiResposta<T>.Falha("Resposta vazia da API.");
            }

            return ApiResposta<T>.Falha($"Erro HTTP: {(int)resposta.StatusCode} {resposta.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResposta<T>.Falha($"Erro de conexão: {ex.Message}");
        }
    }

    public Task<ApiResposta<T>> GetAsync<T>(string endpoint)
        => EnviarAsync<T>(CriarRequisicao(HttpMethod.Get, endpoint));

    public Task<ApiResposta<T>> DeleteAsync<T>(string endpoint)
        => EnviarAsync<T>(CriarRequisicao(HttpMethod.Delete, endpoint));

    public Task<ApiResposta<T>> PostAsync<T, TBody>(string endpoint, TBody corpo)
    {
        var requisicao = CriarRequisicao(HttpMethod.Post, endpoint);
        requisicao.Content = JsonContent.Create(corpo);
        return EnviarAsync<T>(requisicao);
    }

    public Task<ApiResposta<T>> PutAsync<T, TBody>(string endpoint, TBody corpo)
    {
        var requisicao = CriarRequisicao(HttpMethod.Put, endpoint);
        requisicao.Content = JsonContent.Create(corpo);
        return EnviarAsync<T>(requisicao);
    }

    public Task<ApiResposta<T>> PostMultipartAsync<T>(string endpoint, MultipartFormDataContent conteudo)
    {
        var requisicao = CriarRequisicao(HttpMethod.Post, endpoint);
        requisicao.Content = conteudo;
        return EnviarAsync<T>(requisicao);
    }

    public Task<ApiResposta<T>> PutAsync<T>(string endpoint)
    {
        var requisicao = CriarRequisicao(HttpMethod.Put, endpoint);
        return EnviarAsync<T>(requisicao);
    }
}