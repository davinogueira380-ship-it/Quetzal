using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Quetzal.Desktop.ApiClientes;

namespace Quetzal.Desktop.UserControls
{
    public partial class AmbientesControl : System.Windows.Forms.UserControl
    {
        private string? _ambienteSelecionadoId;

        private readonly AmbienteApiUsuario _apiAmbiente;
        private List<AmbienteDto> _listaAmbientes = new List<AmbienteDto>();

        public AmbientesControl()
        {
            InitializeComponent();

            _apiAmbiente = new AmbienteApiUsuario();

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

        private async void btnSalvar_Click(object sender, EventArgs e)
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
                    await CriarAmbiente();
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

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                btnAtualizar.Enabled = false;

                await CarregarAmbientes();

                MessageBox.Show(
                    "Lista de ambientes atualizada com sucesso.",
                    "Atualizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao atualizar os ambientes.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnAtualizar.Enabled = true;
            }
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

        private async Task CriarAmbiente()
        {
            var dto = new CriarAmbienteDto
            {
                Nome = txtNome.Text.Trim(),
                Descricao = txtDescricao.Text.Trim()
            };

            var resposta = await _apiAmbiente.CadastrarAsync(dto);

            if (resposta != null && resposta.Sucesso)
            {
                MessageBox.Show(
                    resposta.Mensagem ?? "Ambiente cadastrado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimparFormulario();
                CarregarAmbientes();
            }
            else
            {
                MessageBox.Show(
                    resposta?.Mensagem ?? "Não foi possível cadastrar o ambiente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
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

        private async Task CarregarAmbientes()
        {
            try
            {
                dgvAmbientes.Enabled = false;

                _listaAmbientes = await _apiAmbiente.ObterTodasAsync();

                AtualizarGrid(_listaAmbientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível carregar os ambientes da API.\n\n{ex.Message}",
                    "Aviso de Comunicação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                dgvAmbientes.Enabled = true;
            }
        }
        
private void AtualizarGrid(List<AmbienteDto> dados)
        {
            dgvAmbientes.AutoGenerateColumns = false;

            // Limpa os dados atuais
            dgvAmbientes.DataSource = null;

            // Coloca os novos dados no Grid
            dgvAmbientes.DataSource = dados;

            // Remove a seleção automática
            dgvAmbientes.ClearSelection();
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