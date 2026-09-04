using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiClientes
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public string NomeProjeto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ImagemUpload { get; set; } = string.Empty;
        // Dados Ambiente
        public int AmbienteId { get; set; }
        public string AmbienteNome { get; set; } = string.Empty;

        // Dados da situação do projeto
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
    public class PortifolioApiCliente : UsuarioHttp
    {
        private const string RotaBase = "/api/portfolio";

        public async Task<List<PortfolioDto>> ObterTodosAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<List<PortfolioDto>>>($"{RotaBase}/todos");
            return resposta?.Dados ?? new List<PortfolioDto>();
        }
        public async Task<PortfolioDto> ObterPorIdAsync(int id)
        {
            var resposta = await GetAsync<ApiRespostaSimples<PortfolioDto>>($"{RotaBase}/{id}");
            return resposta?.Dados;
        }

        public async Task<List<PortfolioDto>> BuscarAsync(string termo, int? ambienteId = null)
        {
            var url = $"{RotaBase}/buscar?termo={termo}";
            if (ambienteId.HasValue && ambienteId.Value > 0)
            {
                url += $"&ambienteId={ambienteId.Value}";
            }
            var resposta = await GetAsync<ApiRespostaSimples<List<PortfolioDto>>>(url);
            return resposta?.Dados ?? new List<PortfolioDto>();
        }

        public async Task<ApiRespostaSimples<PortfolioDto>> CadastrarAsync(PortfolioDto dados)
        {
            // Chama POST /api/portfolio enviando o DTO do novo portfolio no corpo
            return await PostAsync<ApiRespostaSimples<PortfolioDto>>(RotaBase, dados);
        }

        public async Task<ApiRespostaSimples<PortfolioDto>> AtualizarAsync(int id, PortfolioDto dados)
        {
            // Chama PUT /api/portfolio/{id} com os dados atualizados no corpo
            return await PutAsync<ApiRespostaSimples<PortfolioDto>>($"{RotaBase}/{id}", dados);
        }
        public async Task<ApiRespostaSimples<object>> DesativarAsync(int id)
        {
            // Chama DELETE /api/portfolio/{id}/desativar para desativacao logica
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}/desativar");
        }

        public async Task<ApiRespostaSimples<object>> ExcluirPermanentementeAsync(int id)
        {
            // Chama DELETE /api/portfolio/{id}/permanente para exclusao fisca do banco
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}/permanente");
        }



    }



}
