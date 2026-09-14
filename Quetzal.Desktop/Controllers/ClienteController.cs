//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Quetzal.Desktop.ApiClientes;
//namespace Quetzal.Desktop.Controllers
//{
//    public class ClientesController
//    {
//        private readonly UsuarioApiCliente _apiUsuario;
//        public ClientesController()
//        {
//            _apiUsuario = new UsuarioApiCliente();
//        }
//        //===== // LISTAR CLIENTES // =========================================================
//        public async Task<List<UsuarioDto>> ObterTodosAsync()
//        {
//            return await _apiUsuario.ObterTodosAsync();
//        }
//        public async Task AtualizarAsync(string id, string nomeCompleto, string email, string telefone, bool ativo)

//        {
//            if (string.IsNullOrWhiteSpace(id))
//            {
//                throw new ArgumentException("Nenhum cliente foi selecionado.");
//            }
//            if (string.IsNullOrWhiteSpace(nomeCompleto))
//            {
//                throw new ArgumentException("O nome do cliente não pode ficar em branco.");
//            }
//            var cliente = new UsuarioDto
//            {
//                Id = id,
//                NomeCompleto = nomeCompleto.Trim(),
//                Email = email?.Trim(),
//                Telefone = telefone?.Trim(),
//                Ativo = ativo
//            };
//            await _apiUsuario.AtualizarAsync(id, cliente);

//        }
//        //======  ATIVAR CLIENTE // ===============
//        public async Task AtivarAsync(string id)
//        {
//            if (string.IsNullOrWhiteSpace(id))
//            {
//                throw new ArgumentException("Nenhum cliente foi selecionado.");
//            }
//            await _apiUsuario.AtivarAsync(id);
//        } // DESATIVAR CLIENTE 
//        public async Task DesativarAsync(string id)
//        {
//            if (string.IsNullOrWhiteSpace(id))
//            {
//                throw new ArgumentException("Nenhum cliente foi selecionado.");
//            }
//            await _apiUsuario.DesativarAsync(id);
//        }
//        //ALTERNAR ATIVAÇÃO
//        public async Task AlternarAtivacaoAsync(string id, bool novoStatus)
//        {
//            if (string.IsNullOrWhiteSpace(id))
//            {
//                throw new ArgumentException("Nenhum cliente foi selecionado.");
//            }
//            if (novoStatus)
//            {
//                await _apiUsuario.AtivarAsync(id);
//            }
//            else
//            {
//                await _apiUsuario.DesativarAsync(id);
//            }
//        }
//        // FILTRAR CLIENTES

//        public List<UsuarioDto> Filtrar(List<UsuarioDto> clientes, string termo)
//        {
//            if (clientes == null)
//            {
//                return new List<UsuarioDto>();
//            }
//            if (string.IsNullOrWhiteSpace(termo))
//            {
//                return clientes;
//            }
//            termo = termo.Trim();

//            return clientes.Where(c => (c.NomeCompleto?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false) ||
//            (c.Email?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false) || (c.Telefone?.Contains(termo,
//            StringComparison.OrdinalIgnoreCase) ?? false)).ToList();

//        }
//    }
//}