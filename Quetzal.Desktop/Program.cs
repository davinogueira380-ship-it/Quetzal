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
            Application.ThreadException += (s, e) => ShowError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex) ShowError(ex);
            };

            while (true)
            {
                using var login = new FormLogin();
                if (login.ShowDialog() != DialogResult.OK)
                
                    break;
                
                using var principal = new FormPrincipal();
                Application.Run(principal);
                

                // Se ao fechar o FormPrincipal a sessao estiver limpa (logout), reabre a tela de login
                if (string.IsNullOrEmpty(SessaoUsuario.Instancia.Token))
                {
                    continue;
                }

                break;
            }
        }
        private static void ShowError(Exception ex)
        {
            try
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { /* fallback silencioso */ }
        }
    }
}