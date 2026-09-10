using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.Sessao
{
    public class SessaoUsuario
    {
        private static SessaoUsuario _instancia;

        private static readonly object _trava = new object();


        private SessaoUsuario()
        {
            // Inicializa a lista de perfis como lista vazia para evitar NullReference
            Perfis = new List<string>();
        }

        public static SessaoUsuario Instancia
        {
            get
            {
                // Verifica se a instancia ja foi criada (verificacao dupla para performance)
                if (_instancia == null)
                {
                    // Entra em regiao critica para evitar criacao duplicada em multithread
                    lock (_trava)
                    {
                        // Verifica novamente dentro do lock para garantir unicidade
                        if (_instancia == null)
                        {
                            // Cria a unica instancia da sessao
                            _instancia = new SessaoUsuario();
                        }
                    }
                }
                return _instancia;
            }
        }

        public string Token { get; set; }

        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        public List<string> Perfis { get; set; }

        public bool EhAdmin => Perfis != null && Perfis.Contains("Admin");

        public bool EhOperador => (Perfis != null && Perfis.Contains("Operador")) || EhAdmin;

        public void Limpar()
        {
            Token = null;

            NomeUsuario = null;

            Email = null;

            Perfis = new List<string>();
        }

    }

}
