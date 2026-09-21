using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quetzal.Desktop.ApiClientes;

namespace Quetzal.Desktop.Formularios
{
    public partial class FormAmbientes : Form
    {
        private readonly AmbienteApiUsuario _apiAmbiente;
        private List<AmbienteDto> _listaAmbientes = new List<AmbienteDto>();
        private int? _ambienteSelecionadoId = null;

        public FormAmbientes()
        {
            InitializeComponent();
            _apiAmbiente = new AmbienteApiUsuario();
        }

        private async void FormAmbientes_Load(object sender, EventArgs e)
        {
            await CarregarAmbientesAsync();
        }

        private async Task CarregarAmbientesAsync()
        {
            try
            {
                dgvAmbientes.Enabled = false;//dgv data grid view
                _listaAmbientes = await _apiAmbiente.ObterTodasAsync();
                AtualizarGrid(_listaAmbientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível carregar a lista de ambientes da API: {ex.Message}",
                    "Aviso de Comunicação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            finally
            {
                dgvAmbientes.Enabled = true;
            }
        }

        private void AtualizarGrid(List<AmbienteDto> dados)
        {
            dgvAmbientes.AutoGenerateColumns = false;
            dgvAmbientes.DataSource = null;
            dgvAmbientes.DataSource = dados;
            dgvAmbientes.ClearSelection();
        }

        private void dgvAmbientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAmbientes.SelectedRows.Count == 0) return;

            var linha = dgvAmbientes.SelectedRows[0];
            if (linha.DataBoundItem is AmbienteDto item)
            {
                _ambienteSelecionadoId = item.Id;
                txtNome.Text = item.Nome;
                txtDescricao.Text = item.Descricao;
                swAtivo.Checked = item.Ativo;
                btnDesativar.Enabled = true;
                btnDesativar.Text = item.Ativo ? "🗑️ Desativar Ambiente" : "🔄 Reativar Ambiente";
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            _ambienteSelecionadoId = null;
            txtNome.Clear();
            txtDescricao.Clear();
            swAtivo.Checked = true;
            dgvAmbientes.ClearSelection();
            btnDesativar.Enabled = false;
            btnDesativar.Text = "🗑️ Desativar Ambiente";
            txtNome.Focus();
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            var nome = txtNome.Text.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome do ambiente (ex: Sala de Estar, Cozinha, Quarto Master).", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                return;
            }

            try
            {
                btnSalvar.Enabled = false;

                var dto = new CriarAmbienteDto
                {
                    Nome = nome,
                    Descricao = txtDescricao.Text.Trim(),
                };

                if (_ambienteSelecionadoId == null || _ambienteSelecionadoId == 0)
                {
                    // Cadastro novo
                    var resposta = await _apiAmbiente.CadastrarAsync(dto);
                    if (resposta != null && resposta.Sucesso)
                    {
                        MessageBox.Show("Ambiente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Atualização
                    var resposta = await _apiAmbiente.AtualizarAsync(_ambienteSelecionadoId.Value, dto);
                    if (resposta != null && resposta.Sucesso)
                    {
                        MessageBox.Show("Ambiente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                await CarregarAmbientesAsync();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar ambiente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSalvar.Enabled = true;
            }
        }

        private async void btnDesativar_Click(object sender, EventArgs e)
        {
            if (_ambienteSelecionadoId == null) return;

            var confirmacao = MessageBox.Show(
                "Deseja alternar a ativação deste ambiente?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    await _apiAmbiente.DesativarAsync(_ambienteSelecionadoId.Value);
                    MessageBox.Show("Situação do ambiente alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CarregarAmbientesAsync();
                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao alterar situação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            var termo = txtBusca.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(termo))
            {
                AtualizarGrid(_listaAmbientes);
            }
            else
            {
                var filtrados = _listaAmbientes
                    .Where(a => (a.Nome?.ToLower().Contains(termo) ?? false) ||
                                (a.Descricao?.ToLower().Contains(termo) ?? false))
                    .ToList();
                AtualizarGrid(filtrados);
            }
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            await CarregarAmbientesAsync();
        }
    }
}
