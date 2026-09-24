using System;
using System.Collections.Generic;

namespace Quetzal.Desktop.ApiClientes
{
    // Representa uma foto do ProjetoC escolhida
    // para ser publicada no Portfolio.
    public class PortfolioFotoDto
    {
        public int ProjetoCFotoId { get; set; }

        public string Foto { get; set; } = string.Empty;

        public int Ordem { get; set; }
    }

    public class PortifolioDto
    {
        public int Id { get; set; }

        public string NomeProjeto { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        // Mantido temporariamente durante a transição.
        public string ImagemUpload { get; set; } = string.Empty;

        public int AmbienteId { get; set; }

        public string AmbienteNome { get; set; } = string.Empty;

        public int? ProjetoCId { get; set; }

        public string ProjetoCNome { get; set; } = string.Empty;

        // IDs das fotos escolhidas para publicação.
        public List<int> ProjetoCFotosIds { get; set; }
            = new List<int>();

        // Fotos selecionadas devolvidas pela API.
        public List<PortfolioFotoDto> FotosSelecionadas { get; set; }
            = new List<PortfolioFotoDto>();

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
            var resposta =
                await GetAsync<ApiRespostaSimples<List<PortifolioDto>>>(
                    $"{RotaBase}/todos");

            return resposta?.Dados ??
                   new List<PortifolioDto>();
        }

        public async Task<PortifolioDto?> ObterPorIdAsync(int id)
        {
            var resposta =
                await GetAsync<ApiRespostaSimples<PortifolioDto>>(
                    $"{RotaBase}/{id}");

            return resposta?.Dados;
        }

        public async Task<List<PortifolioDto>> BuscarAsync(
            string termo,
            int? ambienteId = null)
        {
            var url =
                $"{RotaBase}/buscar?termo={Uri.EscapeDataString(termo)}";

            if (ambienteId.HasValue && ambienteId.Value > 0)
            {
                url += $"&ambienteId={ambienteId.Value}";
            }

            var resposta =
                await GetAsync<ApiRespostaSimples<List<PortifolioDto>>>(
                    url);

            return resposta?.Dados ??
                   new List<PortifolioDto>();
        }

        public async Task<ApiRespostaSimples<PortifolioDto>>
            CadastrarAsync(PortifolioDto dados)
        {
            return await PostAsync<ApiRespostaSimples<PortifolioDto>>(
                RotaBase,
                dados);
        }

        public async Task<ApiRespostaSimples<PortifolioDto>>
            AtualizarAsync(
                int id,
                PortifolioDto dados)
        {
            return await PutAsync<ApiRespostaSimples<PortifolioDto>>(
                $"{RotaBase}/{id}/Atualizar",
                dados);
        }

        public async Task<ApiRespostaSimples<object>>
            DesativarAsync(int id)
        {
            return await DeleteAsync<ApiRespostaSimples<object>>(
                $"{RotaBase}/{id}/desativar");
        }

        public async Task<ApiRespostaSimples<object>>
            ReativarAsync(int id)
        {
            return await PutAsync<ApiRespostaSimples<object>>(
                $"{RotaBase}/{id}/reativar",
                null);
        }

        public async Task<ApiRespostaSimples<object>>
            ExcluirPermanentementeAsync(int id)
        {
            return await DeleteAsync<ApiRespostaSimples<object>>(
                $"{RotaBase}/{id}/permanente");
        }
    }
}