// ============================================================
// Nome:         CategoriaApiCliente.cs
// Objetivo:     Realizar todas as chamadas HTTP relacionadas ao
//               gerenciamento de categorias na API do SenacFlix.
// Camada:       ApiClientes (infraestrutura de comunicacao)
// Participacao: Utilizado pelos UserControls de categorias e
//               pelo formulario de cadastro de filmes para
//               preencher o combo de generos.
// ============================================================


using Quetzal.Desktop.ApiClientes;
using System.Collections.Generic;  // Necessario para List<CategoriaDto>
using System.Threading.Tasks;      // Necessario para operacoes assincronas

namespace Quetzal.Desktop.ApiClientes;

// ============================================================
// DTO da Categoria utilizado em toda a camada Desktop
// ============================================================

/// <summary>
/// Objeto de transferencia de dados da Categoria.
/// Representa os campos enviados e recebidos pela API REST.
/// </summary>
public class AmbienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCastro { get; set; }
    public int TotalProjetos { get; set; }
    public string? ImagemUpload { get; set; }
}
    // ============================================================
    // Cliente HTTP especializado em operacoes de categorias
    // ============================================================

    /// <summary>
    /// Classe responsavel por toda comunicacao HTTP relativa a categorias.
    /// Herda de ClienteHttp para reutilizar autenticacao e serializacao.
    /// </summary>
    public class AmbienteApiUsuario : ClienteHttp
{
    // --------------------------------------------------------
    // Prefixo base das rotas de categorias
    // --------------------------------------------------------

    // Prefixo comum a todos os endpoints de categorias
    private const string RotaBase = "/api/ambiente";

    // --------------------------------------------------------
    // Metodos de consulta (leitura)
    // --------------------------------------------------------

    public async Task<List<AmbienteDto>> ObterTodasAsync()
    {
        var resposta = await GetAsync<ApiRespostaSimples<List<AmbienteDto>>>(RotaBase);
        return resposta?.Dados ?? new List<AmbienteDto>();
    }

    public async Task<AmbienteDto> ObterPorIdAsync(int id)
    {
        var resposta = await GetAsync<ApiRespostaSimples<AmbienteDto>>($"{RotaBase}/{id}");
        return resposta?.Dados;
    }

    public async Task<ApiRespostaSimples<AmbienteDto>> CadastrarAsync(AmbienteDto dados)
    {
        // Chama POST /api/ambiente enviando o DTO da novo ambiente no corpo
        return await PostAsync<ApiRespostaSimples<AmbienteDto>>(RotaBase, dados);
    }

    public async Task<ApiRespostaSimples<AmbienteDto>> AtualizarAsync(int id, AmbienteDto dados)
    {
        // Chama PUT /api/ambiente/{id} com os dados atualizados no corpo
        return await PutAsync<ApiRespostaSimples<AmbienteDto>>($"{RotaBase}/{id}", dados);
    }

    // --------------------------------------------------------
    // Metodo de desativacao (remocao logica)
    // --------------------------------------------------------

    /// <summary>
    /// Desativa logicamente uma categoria sem excluir do banco de dados.
    /// Categorias desativadas nao aparecem no combo de cadastro de filmes.
    /// Requer perfil Admin ou Operador.
    /// </summary>
    /// <param name="id">Identificador da categoria a ser desativada.</param>
    /// <returns>ApiRespostaSimples indicando sucesso ou falha.</returns>
    public async Task<ApiRespostaSimples<object>> DesativarAsync(int id)
    {
        // Chama DELETE /api/Ambiente/{id}/desativar para desativacao logica
        return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}/desativar");
    }
}
