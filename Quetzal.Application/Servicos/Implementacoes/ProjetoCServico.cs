using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;

namespace Quetzal.Application.Servicos.Implementacoes
{
    public class ProjetoCServico : IProjetoCServico
    {
        private readonly IProjetoCRepositorio _repositorio;
        private readonly IMapper _mapper;


    public ProjetoCServico(
        IProjetoCRepositorio repositorio,
        IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<ApiResposta<IEnumerable<ProjetoCDto>>> ObterTodosAsync(
            bool incluirInativos = false)
        {
            try
            {
                var projetosC = await _repositorio.ObterTodosAsync(incluirInativos);

                var dtos = _mapper.Map<IEnumerable<ProjetoCDto>>(projetosC);

                return ApiResposta<IEnumerable<ProjetoCDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<ProjetoCDto>>.Falha(
                    $"Erro ao obter projetos: {ex.Message}");
            }
        }

        public async Task<ApiResposta<ProjetoCDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var projetoC = await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                    return ApiResposta<ProjetoCDto>.Falha(
                        "Projeto nao encontrado.");

                var dto = _mapper.Map<ProjetoCDto>(projetoC);

                return ApiResposta<ProjetoCDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao obter o projeto: {ex.Message}");
            }
        }

        public async Task<ApiResposta<ProjetoCDto>> CadastrarAsync(
            CriarProjetoCDto dto)
        {
            try
            {
                var projetoC = _mapper.Map<ProjetoC>(dto);

                // Define o UsuarioId enviado pelo cliente.
                // Esse campo representa a FK para ApplicationUser.
                if (!string.IsNullOrWhiteSpace(dto.UsuarioId))
                {
                    projetoC.UsuarioId = dto.UsuarioId;
                }

                var projetoCAdicionado =
                    await _repositorio.AdicionarAsync(projetoC);

                var projetoCDto =
                    _mapper.Map<ProjetoCDto>(projetoCAdicionado);

                return ApiResposta<ProjetoCDto>.Ok(
                    projetoCDto,
                    "Projeto do cliente cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao cadastrar o projeto do cliente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<ProjetoCDto>> AtualizarAsync(
            int id,
            AtualizarProjetoCDto dto)
        {
            try
            {
                var projetoCExistente =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoCExistente == null)
                    return ApiResposta<ProjetoCDto>.Falha(
                        "Projeto do cliente não encontrado.");

                // Atualiza os campos simples do projeto.
                _mapper.Map(dto, projetoCExistente);

                // Atualiza o usuário proprietário somente
                // quando um UsuarioId válido for enviado.
                if (!string.IsNullOrWhiteSpace(dto.UsuarioId))
                {
                    projetoCExistente.UsuarioId = dto.UsuarioId;
                }

                await _repositorio.AtualizarAsync(projetoCExistente);

                var projetoCDto =
                    _mapper.Map<ProjetoCDto>(projetoCExistente);

                return ApiResposta<ProjetoCDto>.Ok(
                    projetoCDto,
                    "Projeto do cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao atualizar o projeto do cliente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> DesativarAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");

                await _repositorio.DesativarAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Projeto do cliente desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao desativar o projeto do cliente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");

                await _repositorio.ExcluirPermanentementeAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Projeto do cliente excluído permanentemente com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao excluir permanentemente o projeto do cliente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ReativarAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");

                if (projetoC.Ativo)
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente já está ativo.");

                await _repositorio.ReativarAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Projeto do cliente reativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao reativar o projeto do cliente: {ex.Message}");
            }
        }
    }

}
