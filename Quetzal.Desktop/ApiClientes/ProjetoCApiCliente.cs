namespace Quetzal.Desktop.ApiClientes
{
    public class ProjetoCFotoDto
    {
        public int Id { get; set; }

        public string Foto { get; set; } = string.Empty;

        public int Ordem { get; set; }
    }

    public class ProjetoCDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string? ImagemUpload { get; set; }

        public string UsuarioId { get; set; } = string.Empty;

        public string? UsuarioNome { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? DataExclusao { get; set; }

        public List<string> Fotos { get; set; } =
            new List<string>();

        public List<ProjetoCFotoDto> FotosDetalhadas { get; set; } =
            new List<ProjetoCFotoDto>();
    }

    public class ProjetoCApiCliente : ClienteHttp
    {
        private const string RotaBase = "/api/projetoc";

        public async Task<List<ProjetoCDto>> ObterTodosAsync(
            bool incluirInativos = true)
        {
            var rota =
                incluirInativos
                    ? $"{RotaBase}/todas"
                    : RotaBase;

            var resposta =
                await GetAsync<ApiRespostaSimples<List<ProjetoCDto>>>(
                    rota);

            return resposta?.Dados ??
                   new List<ProjetoCDto>();
        }

        public async Task<ProjetoCDto?> ObterPorIdAsync(int id)
        {
            var resposta =
                await GetAsync<ApiRespostaSimples<ProjetoCDto>>(
                    $"{RotaBase}/{id}");

            return resposta?.Dados;
        }

        public async Task<ApiRespostaSimples<ProjetoCDto>>
            CadastrarAsync(ProjetoCDto dados)
        {
            return await PostAsync<ApiRespostaSimples<ProjetoCDto>>(
                RotaBase,
                dados);
        }

        public async Task<ApiRespostaSimples<ProjetoCDto>>
            AtualizarAsync(
                int id,
                ProjetoCDto dados)
        {
            return await PutAsync<ApiRespostaSimples<ProjetoCDto>>(
                $"{RotaBase}/{id}/atualizar",
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
                new { });
        }

        public async Task<ApiRespostaSimples<object>>
            ExcluirPermanentementeAsync(int id)
        {
            return await DeleteAsync<ApiRespostaSimples<object>>(
                $"{RotaBase}/{id}/permanente");
        }
    }
}