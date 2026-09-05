using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Quetzal.Desktop.Infraestrutura
{
    public class ApiResposta<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }=string.Empty;
        public T Dados { get; set; }
    }


    public class ApiClienteDesktop
    {
        private readonly HttpClient _httpClient;

        public ApiClienteDesktop(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("QuetzalApi");
        }

        private void ConfigurarToken()
        {
            if (SessaoUsuario.EstaAutenticado)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessaoUsuario.Token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<ApiResposta<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                ConfigurarToken();
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResposta<T>>();
                    return result ?? new ApiResposta<T> { Sucesso = false, Mensagem = "Resposta nula." };
                }

                return new ApiResposta<T> { Sucesso = false, Mensagem = $"Erro HTTP {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResposta<T> { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public async Task<ApiResposta<T>> PostAsync<T, TBody>(string endpoint, TBody corpo)
        {
            try
            {
                ConfigurarToken();
                var response = await _httpClient.PostAsJsonAsync(endpoint, corpo);

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResposta<T>>();
                    return result ?? new ApiResposta<T> { Sucesso = false, Mensagem = "Resposta nula." };
                }

                return new ApiResposta<T> { Sucesso = false, Mensagem = $"Erro HTTP {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResposta<T> { Sucesso = false, Mensagem = ex.Message };
            }
        }
    }
}