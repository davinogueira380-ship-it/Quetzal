using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quetzal.Desktop.ApiClientes;
using Quetzal.Desktop.Sessao;

namespace Quetzal.Desktop.Formularios
{
    public partial class FormLogin : Form
    {
        private readonly AuthApiCliente _apiAuth;

        public FormLogin()
        {
            InitializeComponent();
            _apiAuth = new AuthApiCliente();
        }

        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            lblMensagemErro.Visible = false;
            var email = txtEmail.Text.Trim();
            var senha = txtSenha.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                ExibirErro("Por favor, informe o seu e-mail de acesso.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                ExibirErro("Por favor, digite a sua senha.");
                txtSenha.Focus();
                return;
            }

            try
            {
                btnEntrar.Enabled = false;
                btnEntrar.Text = "Acessando...";

                var resposta = await _apiAuth.LoginAsync(email, senha);

                if (resposta != null && resposta.Sucesso && resposta.Dados != null)
                {
                    // Registra dados na sessao global do usuario
                    var sessao = SessaoUsuario.Instancia;
                    sessao.Token = resposta.Dados.Token;
                    sessao.NomeUsuario = resposta.Dados.NomeUsuario ?? email;
                    sessao.Email = resposta.Dados.Email ?? email;
                    sessao.Perfis = resposta.Dados.Perfis ?? new List<string>();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var msg = resposta?.Mensagem ?? "Credenciais inválidas. Verifique seu e-mail e senha.";
                    ExibirErro(msg);
                }
            }
            catch (Exception ex)
            {
                ExibirErro($"Falha na comunicação: {ex.Message}");
            }
            finally
            {
                btnEntrar.Enabled = true;
                btnEntrar.Text = "Entrar no Painel";
            }
        }

        private void ExibirErro(string mensagem)
        {
            lblMensagemErro.Text = $"⚠️ {mensagem}";
            lblMensagemErro.Visible = true;
        }

        private void txtCampos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnEntrar_Click(sender, EventArgs.Empty);
            }
        }

        private void pnlConteudoLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        //private async Task CriarAdminAsync()
        //{
        //    var api = new UsuarioApiCliente();

        //    var adminDto = new UsuarioDto
        //    {
        //        NomeCompleto = "Administrador",
        //        Email = "admin@exemplo.local",
        //        Telefone = "",
        //        Ativo = true,
        //        Perfis = new List<string> { "Admin" } // ajuste conforme roles do backend
        //    };

        //    var resposta = await api.CriarAsync(adminDto);

        //    // Verifique a estrutura de ApiRespostaSimples para checar sucesso/erros
        //    if (resposta != null)
        //    {
        //        MessageBox.Show("Admin criado (verifique se foi criado no servidor).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
    }
}
