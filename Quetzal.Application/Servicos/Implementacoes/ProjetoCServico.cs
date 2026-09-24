using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        // =========================================================
        // OBTER TODOS
        // =========================================================

        public async Task<ApiResposta<IEnumerable<ProjetoCDto>>> ObterTodosAsync(
            bool incluirInativos = false)
        {
            try
            {
                var projetosC =
                    await _repositorio.ObterTodosAsync(incluirInativos);

                var dtos =
                    _mapper.Map<IEnumerable<ProjetoCDto>>(projetosC);

                return ApiResposta<IEnumerable<ProjetoCDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<ProjetoCDto>>.Falha(
                    $"Erro ao obter projetos: {ex.Message}");
            }
        }

        // =========================================================
        // OBTER POR ID
        // =========================================================

        public async Task<ApiResposta<ProjetoCDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                {
                    return ApiResposta<ProjetoCDto>.Falha(
                        "Projeto nao encontrado.");
                }

                var dto =
                    _mapper.Map<ProjetoCDto>(projetoC);

                return ApiResposta<ProjetoCDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao obter o projeto: {ex.Message}");
            }
        }

        // =========================================================
        // CADASTRAR
        // =========================================================

        public async Task<ApiResposta<ProjetoCDto>> CadastrarAsync(
            CriarProjetoCDto dto)
        {
            try
            {
                var projetoC =
                    _mapper.Map<ProjetoC>(dto);

                // Define o usuário/cliente proprietário do projeto.
                if (!string.IsNullOrWhiteSpace(dto.UsuarioId))
                {
                    projetoC.UsuarioId = dto.UsuarioId;
                }

                // =================================================
                // FOTOS
                // =================================================
                // O DTO recebe List<string>.
                //
                // Aqui cada string é transformada em uma entidade
                // ProjetoCFoto para ser armazenada na tabela
                // ProjetoCFotos.
                // =================================================

                projetoC.Fotos = (dto.Fotos ?? new List<string>())
                    .Select((foto, indice) => new ProjetoCFoto
                    {
                        ProjetoC = projetoC,
                        Foto = foto,
                        Ordem = indice + 1
                    })
                    .ToList();

                var projetoCAdicionado =
                    await _repositorio.AdicionarAsync(projetoC);

                var projetoCDto =
                    _mapper.Map<ProjetoCDto>(projetoCAdicionado);

                return ApiResposta<ProjetoCDto>.Ok(
                    projetoCDto,
                    "Projeto do cliente cadastrado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao cadastrar o projeto do cliente no banco: {DetalharExcecao(ex)}");
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao cadastrar o projeto do cliente: {DetalharExcecao(ex)}");
            }
        }

        // =========================================================
        // ATUALIZAR
        // =========================================================

        public async Task<ApiResposta<ProjetoCDto>> AtualizarAsync(
            int id,
            AtualizarProjetoCDto dto)
        {
            try
            {
                var projetoCExistente =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoCExistente == null)
                {
                    return ApiResposta<ProjetoCDto>.Falha(
                        "Projeto do cliente não encontrado.");
                }

                // Atualiza os campos simples do projeto.
                _mapper.Map(dto, projetoCExistente);

                // Atualiza o usuário/cliente proprietário somente
                // quando um UsuarioId válido for enviado.
                if (!string.IsNullOrWhiteSpace(dto.UsuarioId))
                {
                    projetoCExistente.UsuarioId = dto.UsuarioId;
                }

                //// =================================================
                //// ATUALIZA AS FOTOS
                //// =================================================
                //// Converte novamente a List<string> recebida pelo
                //// DTO para a coleção de ProjetoCFoto do Domain.
                //// =================================================

                //projetoCExistente.Fotos =
                //    (dto.Fotos ?? new List<string>())
                //    .Select((foto, indice) => new ProjetoCFoto
                //    {
                //        ProjetoCId = projetoCExistente.Id,
                //        Foto = foto,
                //        Ordem = indice + 1
                //    })
                //    .ToList();

                // =================================================
                // ATUALIZA AS FOTOS
                // =================================================

                var fotosExistentes = projetoCExistente.Fotos.ToList();

                // Remove as fotos antigas da coleção rastreada
                foreach (var fotoExistente in fotosExistentes)
                {
                    projetoCExistente.Fotos.Remove(fotoExistente);
                }

                // Adiciona as novas fotos
                var novasFotos = dto.Fotos ?? new List<string>();

                for (int i = 0; i < novasFotos.Count; i++)
                {
                    projetoCExistente.Fotos.Add(
                        new ProjetoCFoto
                        {
                            ProjetoCId = projetoCExistente.Id,
                            Foto = novasFotos[i],
                            Ordem = i + 1
                        });
                }

                await _repositorio.AtualizarAsync(projetoCExistente);

                var projetoCDto =
                    _mapper.Map<ProjetoCDto>(projetoCExistente);

                return ApiResposta<ProjetoCDto>.Ok(
                    projetoCDto,
                    "Projeto do cliente atualizado com sucesso.");
            }
            //catch (Exception ex)
            //{
            //    return ApiResposta<ProjetoCDto>.Falha(
            //        $"Erro ao atualizar o projeto do cliente: {ex.Message}");
            //}

            catch (DbUpdateException ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao atualizar o projeto do cliente no banco: {DetalharExcecao(ex)}");
            }
            catch (Exception ex)
            {
                return ApiResposta<ProjetoCDto>.Falha(
                    $"Erro ao atualizar o projeto do cliente: {DetalharExcecao(ex)}");
            }
        }

        // =========================================================
        // DESATIVAR
        // =========================================================

        public async Task<ApiResposta<bool>> DesativarAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");
                }

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

        // =========================================================
        // EXCLUIR PERMANENTEMENTE
        // =========================================================

        public async Task<ApiResposta<bool>> ExcluirPermanentementeAsync(
            int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");
                }

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

        // =========================================================
        // REATIVAR
        // =========================================================

        private static string DetalharExcecao(Exception ex)
        {
            var mensagens = new List<string>();
            var atual = ex;

            while (atual != null)
            {
                if (!string.IsNullOrWhiteSpace(atual.Message) &&
                    !mensagens.Contains(atual.Message))
                {
                    mensagens.Add(atual.Message);
                }

                atual = atual.InnerException;
            }

            return string.Join(" | ", mensagens);
        }

        public async Task<ApiResposta<bool>> ReativarAsync(int id)
        {
            try
            {
                var projetoC =
                    await _repositorio.ObterPorIdAsync(id);

                if (projetoC == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente não encontrado.");
                }

                if (projetoC.Ativo)
                {
                    return ApiResposta<bool>.Falha(
                        "Projeto do cliente já está ativo.");
                }

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