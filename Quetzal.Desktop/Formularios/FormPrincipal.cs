using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Quetzal.Desktop.Formularios;
using Quetzal.Desktop.Sessao;

namespace Quetzal.Desktop
{
    public partial class FormPrincipal : Form
    {
        private Form? _formularioAtivo = null;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Atualiza informacoes do usuario caso haja sessao ativa
            var sessao = SessaoUsuario.Instancia;
            if (!string.IsNullOrWhiteSpace(sessao.NomeUsuario))
            {
                lblUsuarioLogado.Text = $"👤 {sessao.NomeUsuario} | Designer Admin";
            }

            // Inicia exibindo a tela de Ambientes
            btnAmbientes.Checked = true;
            btnAmbientes_Click(btnAmbientes, EventArgs.Empty);
        }

        public void AbrirFormularioFilho(Form formFilho, string titulo, Guna2Button botaoMenu)
        {
            if (_formularioAtivo != null)
            {
                _formularioAtivo.Close();
                _formularioAtivo.Dispose();
            }

            botaoMenu.Checked = true;
            _formularioAtivo = formFilho;
            lblTituloModulo.Text = titulo;

            formFilho.TopLevel = false;
            formFilho.FormBorderStyle = FormBorderStyle.None;
            formFilho.Dock = DockStyle.Fill;

            pnlConteudo.Controls.Clear();
            pnlConteudo.Controls.Add(formFilho);
            pnlConteudo.Tag = formFilho;

            formFilho.BringToFront();
            formFilho.Show();
        }

        private void btnAmbientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioFilho(new FormAmbientes(), "🛋️ Gerenciamento de Ambientes", btnAmbientes);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioFilho(new FormClientes(), "👥 Gestão de Clientes e Ativação de Acesso", btnClientes);
        }

        private void btnProjetos_Click(object sender, EventArgs e)
        {
            AbrirFormularioFilho(new FormProjetos(), "📁 Projetos de Clientes e Galeria por Ambiente", btnProjetos);
        }

        private void btnPortfolio_Click(object sender, EventArgs e)
        {
            AbrirFormularioFilho(new FormPortfolio(), "🖼️ Portfólio Público para o Site", btnPortfolio);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var confirmacao = MessageBox.Show(
                "Deseja realmente sair do painel administrativo?",
                "Confirmar Saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao == DialogResult.Yes)
            {
                SessaoUsuario.Instancia.Limpar();
                this.Close();
            }
        }

        private void pnlConteudo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
