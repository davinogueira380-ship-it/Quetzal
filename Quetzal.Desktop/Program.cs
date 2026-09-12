using System;
using System.Windows.Forms;
using Quetzal.Desktop.Formularios;
using Quetzal.Desktop.Sessao;

namespace Quetzal.Desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                using var login = new FormLogin();
                if (login.ShowDialog() != DialogResult.OK)
                {
                    break;
                }

                Application.Run(new FormPrincipal());

                // Se ao fechar o FormPrincipal a sessao estiver limpa (logout), reabre a tela de login
                if (string.IsNullOrEmpty(SessaoUsuario.Instancia.Token))
                {
                    continue;
                }

                break;
            }
        }
    }
}