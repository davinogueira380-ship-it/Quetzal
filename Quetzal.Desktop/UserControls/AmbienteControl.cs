using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Quetzal.Desktop.ApiClientes;

namespace Quetzal.Desktop.UserControls
{
    public partial class AmbientesControl : System.Windows.Forms.UserControl
    {
        private int? _ambienteSelecionadoId;

        private readonly AmbienteApiUsuario _apiAmbiente;
        private List<AmbienteDto> _listaAmbientes = new List<AmbienteDto>();

        public AmbientesControl()
        {
            InitializeComponent();

            _apiAmbiente = new AmbienteApiUsuario();

            ConfigurarGrid();
            LimparFormulario();

            Load += AmbientesControl_Load;
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
// campo: usar inteiro anulável para representar "nenhum selecionado"

        private void LimparFormulario()
        {
            _ambienteSelecionadoId = null;

            txtNome.Clear();
            txtDescricao.Clear();
            swAtivo.Checked = true;
            btnDesativar.Text = "🗑️ Desativar Ambiente";

            dgvAmbientes.ClearSelection();

            txtNome.Focus();
        }

        private async void AmbientesControl_Load(object? sender, EventArgs e)
        {
            await CarregarAmbientes();
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
                if (_ambienteSelecionadoId == null)
                {
                    await CriarAmbiente();
                }
                else
                {
                    await AtualizarAmbienteAsync();
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

        private async void btnDesativar_Click(object sender, EventArgs e)
        {
            if (_ambienteSelecionadoId == null)
            {
                MessageBox.Show(
                    "Selecione um ambiente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool estaAtivo = swAtivo.Checked;
            string acao = estaAtivo ? "desativar" : "reativar";
            string titulo = estaAtivo ? "Confirmar desativação" : "Confirmar reativação";

            DialogResult resultado = MessageBox.Show(
                $"Deseja realmente {acao} este ambiente?",
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                btnDesativar.Enabled = false;

                var resposta = estaAtivo
                    ? await _apiAmbiente.DesativarAsync(_ambienteSelecionadoId.Value)
                    : await _apiAmbiente.ReativarAsync(_ambienteSelecionadoId.Value);

                if (resposta?.Sucesso == true)
                {
                    MessageBox.Show(
                        resposta.Mensagem ?? (estaAtivo
                            ? "Ambiente desativado com sucesso!"
                            : "Ambiente reativado com sucesso!"),
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await CarregarAmbientes();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show(
                        resposta?.Mensagem ?? (estaAtivo
                            ? "Não foi possível desativar o ambiente."
                            : "Não foi possível reativar o ambiente."),
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocorreu um erro ao {(estaAtivo ? "desativar" : "reativar")} o ambiente.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnDesativar.Enabled = true;
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
                Convert.ToInt32(linha.Cells["colId"].Value);

            txtNome.Text =
                linha.Cells["colNome"].Value?.ToString() ?? string.Empty;

            txtDescricao.Text =
                linha.Cells["colDescricao"].Value?.ToString() ?? string.Empty;

            if (linha.Cells["colAtivo"].Value != null)
            {
                swAtivo.Checked =
                    Convert.ToBoolean(linha.Cells["colAtivo"].Value);
            }

            btnDesativar.Text = swAtivo.Checked
                ? "🗑️ Desativar Ambiente"
                : "♻️ Reativar Ambiente";
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

        private async Task AtualizarAmbienteAsync()
        {
            if (_ambienteSelecionadoId == null)
                return;

            var dto = new CriarAmbienteDto
            {
                Nome = txtNome.Text.Trim(),
                Descricao = txtDescricao.Text.Trim()
            };

            try
            {
                var resposta = await _apiAmbiente.AtualizarAsync(
                    _ambienteSelecionadoId.Value,
                    dto);

                if (resposta?.Sucesso == true)
                {
                    MessageBox.Show(
                        resposta.Mensagem ?? "Ambiente atualizado com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await CarregarAmbientes();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show(
                        resposta?.Mensagem ?? "Não foi possível atualizar o ambiente.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocorreu um erro ao atualizar o ambiente.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_ambienteSelecionadoId == null)
            {
                MessageBox.Show(
                    "Selecione um ambiente para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirma = MessageBox.Show(
                "Deseja realmente excluir este ambiente permanentemente? Esta ação não pode ser desfeita.",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirma != DialogResult.Yes)
                return;

            try
            {
                btnExcluir.Enabled = false;

                // Chamada à API (adapte o nome do método se diferente)
                var resposta = await _apiAmbiente.ExcluirPermanentementeAsync(_ambienteSelecionadoId.Value);

                if (resposta != null && resposta.Sucesso)
                {
                    MessageBox.Show(
                        resposta.Mensagem ?? "Ambiente excluído permanentemente.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await CarregarAmbientes();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show(
                        resposta?.Mensagem ?? "Não foi possível excluir o ambiente.",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocorreu um erro ao excluir o ambiente.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnExcluir.Enabled = true;
            }
        }
    }

}