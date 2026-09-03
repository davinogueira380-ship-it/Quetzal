using Quetzal.Desktop.ApiClientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiUsuario
{
    public class AtualizarPerfilDto
     
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
    }

    public class AlterarSenhaDto
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }


    public class PerfilApiUsuario : UsuarioHttp
    {
        private const string RotaBase = "/api/perfil";

        public async Task<UsuarioDto> ObterPerfilAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<UsuarioDto>>(RotaBase);
            return resposta?.Dados;
        }

        public async Task<ApiRespostaSimples<object>> AtualizarPerfilAsync(AtualizarPerfilDto dados)
        {
            return await PutAsync<ApiRespostaSimples<object>>(RotaBase, dados);
        }

        public async Task<ApiRespostaSimples<object>> AlterarSenhaAsync(AlterarSenhaDto dados)
        {
            return await PutAsync<ApiRespostaSimples<object>>($"{RotaBase}/senha", dados);
        }

        public async Task<ApiRespostaSimples<string>> UploadFotoAsync(string filePath)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                var fileStreamContent = new StreamContent(System.IO.File.OpenRead(filePath));
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + System.IO.Path.GetExtension(filePath).TrimStart('.'));

                multipartFormContent.Add(fileStreamContent, name: "arquivo", fileName: System.IO.Path.GetFileName(filePath));

                return await PostAsync<ApiRespostaSimples<string>>($"{RotaBase}/foto", multipartFormContent);
            }
        }
    }
}
