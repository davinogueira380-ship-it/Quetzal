using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Quetzal.Desktop.UserControls
{
    public partial class AmbientesControl : System.Windows.Forms.UserControl
    {
        private string? _ambienteSelecionadoId;

        public AmbientesControl()
        {
            InitializeComponent();

            ConfigurarGrid();
            LimparFormulario();
        }

        private void ConfigurarGrid()
        {
            dgvAmbientes.AutoGenerateColumns = false;
            dgvAmbientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAmbientes.MultiSelect = false;
            dgvAmbientes.ReadOnly = true;
            dgvAmbientes.AllowUserToAddRows = false;
            dgvAmbientes.AllowUserToDeleteRows = false;
            dgvAmbientes.AllowUserToResizeRows = false;
            dgvAmbientes.RowHeadersVisible = false;
        }

        private void LimparFormulario()
        {
            _ambienteSelecionadoId = null;

            txtNome.Clear();
            txtDescricao.Clear();
            swAtivo.Checked = true;

            dgvAmbientes.ClearSelection();

            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show(
                    "Informe o nome do ambiente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNome.Focus();
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(_ambienteSelecionadoId))
                {
                    CriarAmbiente();
                }
                else
                {
                    AtualizarAmbiente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocorreu um erro ao salvar o ambiente.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_ambienteSelecionadoId))
            {
                MessageBox.Show(
                    "Selecione um ambiente para desativar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Deseja realmente desativar este ambiente?",
                "Confirmar desativação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                DesativarAmbiente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocorreu um erro ao desativar o ambiente.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarAmbientes();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            FiltrarAmbientes();
        }

        private void dgvAmbientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAmbientes.SelectedRows.Count == 0)
                return;

            DataGridViewRow linha = dgvAmbientes.SelectedRows[0];

            if (linha.Cells["colId"].Value == null)
                return;

            _ambienteSelecionadoId =
                linha.Cells["colId"].Value.ToString();

            txtNome.Text =
                linha.Cells["colNome"].Value?.ToString() ?? string.Empty;

            txtDescricao.Text =
                linha.Cells["colDescricao"].Value?.ToString() ?? string.Empty;

            if (linha.Cells["colAtivo"].Value != null)
            {
                swAtivo.Checked =
                    Convert.ToBoolean(linha.Cells["colAtivo"].Value);
            }
        }

        private void CriarAmbiente()
        {
            // A integração com a API será colocada aqui.
            //
            // Neste momento estamos mantendo o UserControl
            // independente da implementação da API.

            MessageBox.Show(
                "Ambiente pronto para ser cadastrado.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimparFormulario();
        }

        private void AtualizarAmbiente()
        {
            // A integração com a API será colocada aqui.

            MessageBox.Show(
                "Ambiente pronto para ser atualizado.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            CarregarAmbientes();
        }

        private void DesativarAmbiente()
        {
            // A integração com a API será colocada aqui.

            MessageBox.Show(
                "Ambiente pronto para ser desativado.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            CarregarAmbientes();
            LimparFormulario();
        }

        private void CarregarAmbientes()
        {
            // A integração com a API será colocada aqui.
            //
            // Depois vamos preencher o dgvAmbientes
            // com os ambientes retornados pela API.
        }

        private void FiltrarAmbientes()
        {
            string busca = txtBusca.Text.Trim();

            if (string.IsNullOrWhiteSpace(busca))
            {
                CarregarAmbientes();
                return;
            }

            // O filtro definitivo será feito sobre os dados
            // retornados pela API.
        }
    }
}