using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.Infraestrutura
{
    public static class SessaoUsuario
    {
        public static string Token { get; set; } = string.Empty;
        public static string NomeUsuario { get; set; } = string.Empty;
        public static string Email { get; set; } = string.Empty;
        public static List<string> Perfis { get; set; } = new List<string>();

        public static bool EstaAutenticado => !string.IsNullOrEmpty(Token);

        public static bool TemPermissaoAdmin()
        {
            return Perfis.Contains("Admin") || Perfis.Contains("Operador");
        }

        public static void LimparSessao()
        {
            Token = string.Empty;
            NomeUsuario = string.Empty;
            Email = string.Empty;
            Perfis.Clear();
        }


    }
}
