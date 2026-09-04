using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiClientes
{
    public class PortifolioDto

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

    public class PortfolioApiCliente : ClienteHttp
    {
        private const string RotaBase = "/api/portfolio";

        public async Task<List<PortifolioDto>> ObterTodosAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<List<PortifolioDto>>>($"{RotaBase}/todos");
            return resposta?.Dados ?? new List<PortifolioDto>();
        }

        public async Task<PortifolioDto> ObterPorIdAsync(int id)
        {
            var resposta = await GetAsync<ApiRespostaSimples<PortifolioDto>>($"{RotaBase}/{id}");
            return resposta?.Dados;
        }

        public async Task<List<PortifolioDto>> BuscarAsync(string termo, int? categoriaId = null)
        {
            var url = $"{RotaBase}/buscar?termo={termo}";
            if (categoriaId.HasValue && categoriaId.Value > 0)
            {
                url += $"&categoriaId={categoriaId.Value}";
            }
            var resposta = await GetAsync<ApiRespostaSimples<List<PortifolioDto>>>(url);
            return resposta?.Dados ?? new List<PortifolioDto>();
        }

        // Metodos de escrita (criacao e atualizacao)

        public async Task<ApiRespostaSimples<PortifolioDto>> CadastrarAsync(PortifolioDto dados)
        {
            // Chama POST /api/portfolio enviando o DTO do novo filme no corpo
            return await PostAsync<ApiRespostaSimples<PortifolioDto>>(RotaBase, dados);
        }

        public async Task<ApiRespostaSimples<PortifolioDto>> AtualizarAsync(int id, PortifolioDto dados)
        {
            // Chama PUT /api/filmes/{id} com os dados atualizados no corpo
            return await PutAsync<ApiRespostaSimples<PortifolioDto>>($"{RotaBase}/{id}", dados);
        }

        // --------------------------------------------------------
        // Metodos de remocao (desativacao e exclusao)
        // --------------------------------------------------------

        public async Task<ApiRespostaSimples<object>> DesativarAsync(int id)
        {
            // Chama DELETE /api/filmes/{id}/desativar para desativacao logica
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}/desativar");
        }

        /// <summary>
        /// Remove permanentemente um filme do banco de dados.
        /// Operacao irreversivel. Requer perfil Admin exclusivamente.
        /// </summary>
        /// <param name="id">Identificador do filme a ser excluido definitivamente.</param>
        /// <returns>ApiRespostaSimples indicando sucesso ou falha.</returns>
        public async Task<ApiRespostaSimples<object>> ExcluirPermanentementeAsync(int id)
        {
            // Chama DELETE /api/filmes/{id}/permanente para exclusao fisca do banco
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}/permanente");
        }
    }


}
