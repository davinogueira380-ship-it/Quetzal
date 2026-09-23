using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quetzal.Desktop.ApiClientes;

namespace Quetzal.Desktop.UserControls
{
    public partial class PortfolioControl : System.Windows.Forms.UserControl
    {
        private readonly PortfolioApiCliente _apiPortfolio;
        private readonly AmbienteApiUsuario _apiAmbiente;

        private List<PortifolioDto> _listaPortfolio = new List<PortifolioDto>();
        private List<AmbienteDto> _listaAmbientes = new List<AmbienteDto>();

        private int? _portfolioSelecionadoId = null;
        private string? _imagemBase64OuCaminho = null;

        private bool _dadosCarregados = false;

        public class ItemAmbienteCombo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;

            public override string ToString() => Nome;
        }

        public PortfolioControl()
        {
            InitializeComponent();

            _apiPortfolio = new PortfolioApiCliente();
            _apiAmbiente = new AmbienteApiUsuario();

            this.Load += PortfolioControl_Load;
        }

        private async void PortfolioControl_Load(object sender, EventArgs e)
        {
            if (_dadosCarregados)
                return;

            _dadosCarregados = true;

            await CarregarDadosIniciaisAsync();
        }

        private async Task CarregarDadosIniciaisAsync()
        {
            try
            {
                // 1. Carrega Ambientes para o ComboBox
                try
                {
                    _listaAmbientes = await _apiAmbiente.ObterTodasAsync();

                    cmbAmbiente.Items.Clear();

                    cmbAmbiente.Items.Add(
                        new ItemAmbienteCombo
                        {
                            Id = 0,
                            Nome = "-- Selecione o Ambiente --"
                        });

                    foreach (var amb in _listaAmbientes)
                    {
                        cmbAmbiente.Items.Add(
                            new ItemAmbienteCombo
                            {
                                Id = amb.Id,
                                Nome = amb.Nome
                            });
                    }

                    if (cmbAmbiente.Items.Count > 0)
                        cmbAmbiente.SelectedIndex = 0;
                }
                catch
                {
                    // Mantém o comportamento original
                }

                // 2. Carrega lista do portfólio
                await CarregarPortfolioAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar dados de portfólio: {ex.Message}",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async Task CarregarPortfolioAsync()
        {
            try
            {
                dgvPortfolio.Enabled = false;

                _listaPortfolio = await _apiPortfolio.ObterTodosAsync();

                AtualizarGrid(_listaPortfolio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível carregar o portfólio da API: {ex.Message}",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                dgvPortfolio.Enabled = true;
            }
        }

        private void AtualizarGrid(List<PortifolioDto> dados)
        {
            dgvPortfolio.AutoGenerateColumns = false;
            dgvPortfolio.DataSource = null;
            dgvPortfolio.DataSource = dados;
            dgvPortfolio.ClearSelection();
        }

        private void dgvPortfolio_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPortfolio.SelectedRows.Count == 0)
                return;

            var linha = dgvPortfolio.SelectedRows[0];

            if (linha.DataBoundItem is PortifolioDto item)
            {
                _portfolioSelecionadoId = item.Id;

                txtNomeProjeto.Text = item.NomeProjeto;
                txtDescricao.Text = item.Descricao;
                swAtivo.Checked = item.Ativo;

                _imagemBase64OuCaminho = item.ImagemUpload;

                // Seleciona o ambiente correspondente
                for (int i = 0; i < cmbAmbiente.Items.Count; i++)
                {
                    if (cmbAmbiente.Items[i] is ItemAmbienteCombo combo &&
                        combo.Id == item.AmbienteId)
                    {
                        cmbAmbiente.SelectedIndex = i;
                        break;
                    }
                }

                // Carrega preview da imagem se existir
                CarregarPreviewImagem(item.ImagemUpload);

                
                btnDesativar.Enabled = true;
                btnExcluir.Enabled = true;



                btnDesativar.Text = item.Ativo
                    ? "🗑️ Desativar do Site"
                    : "🔄 Reativar no Site";
            }
        }

        private void CarregarPreviewImagem(string? imagem)
        {
            if (string.IsNullOrWhiteSpace(imagem))
            {
                picImagem.Image = null;
                return;
            }

            try
            {
                if (File.Exists(imagem))
                {
                    picImagem.Image = Image.FromFile(imagem);
                    return;
                }

                // Tenta base64
                var bytes = Convert.FromBase64String(imagem);

                using var ms = new MemoryStream(bytes);

                picImagem.Image = Image.FromStream(ms);
            }
            catch
            {
                picImagem.Image = null;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            _portfolioSelecionadoId = null;
            _imagemBase64OuCaminho = null;

            txtNomeProjeto.Clear();
            txtDescricao.Clear();

            if (cmbAmbiente.Items.Count > 0)
                cmbAmbiente.SelectedIndex = 0;

            swAtivo.Checked = true;

            picImagem.Image = null;

            dgvPortfolio.ClearSelection();

           
           // btnDesativar.Enabled = false;
            //btnDesativar.Text = "🗑️ Desativar do Site";

            btnExcluir.Enabled = false;


            txtNomeProjeto.Focus();
        }

        private void btnSelecionarImagem_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Selecionar Imagem de Destaque para o Portfólio",
                Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.webp;*.bmp|Todos os Arquivos|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var bytes = File.ReadAllBytes(ofd.FileName);

                    _imagemBase64OuCaminho =
                        Convert.ToBase64String(bytes);

                    using var ms = new MemoryStream(bytes);

                    picImagem.Image =
                        (Image)Image.FromStream(ms).Clone();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Erro ao carregar imagem: {ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            var titulo = txtNomeProjeto.Text.Trim();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show(
                    "Informe o título do projeto público para o portfólio.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNomeProjeto.Focus();

                return;
            }

            var ambienteSelecionado =
                cmbAmbiente.SelectedItem as ItemAmbienteCombo;

            var ambienteId =
                ambienteSelecionado?.Id ?? 0;

            if (ambienteId <= 0)
            {
                MessageBox.Show(
                    "Selecione qual ambiente este projeto representa.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbAmbiente.Focus();

                return;
            }

            try
            {
                btnSalvar.Enabled = false;

                var dto = new PortifolioDto
                {
                    Id = _portfolioSelecionadoId ?? 0,

                    NomeProjeto = titulo,

                    AmbienteId = ambienteId,

                    AmbienteNome =
                        ambienteSelecionado?.Nome ?? "",

                    Descricao =
                        txtDescricao.Text.Trim(),

                    ImagemUpload =
                        _imagemBase64OuCaminho ?? string.Empty,

                    Ativo =
                        swAtivo.Checked,

                    DataCadastro =
                        DateTime.Now
                };

                if (_portfolioSelecionadoId == null ||
                    _portfolioSelecionadoId == 0)
                {
                    await _apiPortfolio.CadastrarAsync(dto);

                    MessageBox.Show(
                        "Projeto adicionado ao portfólio público com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    await _apiPortfolio.AtualizarAsync(
                        _portfolioSelecionadoId.Value,
                        dto);

                    MessageBox.Show(
                        "Item do portfólio atualizado com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                await CarregarPortfolioAsync();

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar no portfólio: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSalvar.Enabled = true;
            }
        }
        private async void btnDesativar_Click(object sender, EventArgs e)
        {
            if (_portfolioSelecionadoId == null)
            {
                MessageBox.Show(
                    "Selecione um projeto do portfólio.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Recupera o projeto selecionado na grade
            var itemSelecionado = dgvPortfolio.SelectedRows.Count > 0
                ? dgvPortfolio.SelectedRows[0].DataBoundItem as PortifolioDto
                : null;

            if (itemSelecionado == null)
            {
                MessageBox.Show(
                    "Não foi possível identificar o projeto selecionado.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool estaAtivo = itemSelecionado.Ativo;

            // Define a mensagem de acordo com a situação atual
            string mensagem = estaAtivo
                ? "Deseja realmente desativar este projeto?\n\nEle deixará de aparecer no site."
                : "Deseja realmente reativar este projeto?\n\nEle voltará a aparecer no site.";

            string titulo = estaAtivo
                ? "Desativar Projeto"
                : "Reativar Projeto";

            var confirmacao = MessageBox.Show(
                mensagem,
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            try
            {
                btnDesativar.Enabled = false;

                if (estaAtivo)
                {
                    // DESATIVA
                    await _apiPortfolio.DesativarAsync(
                        _portfolioSelecionadoId.Value);

                    MessageBox.Show(
                        "Projeto desativado com sucesso!\n\nEle não será mais exibido no site.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // REATIVA
                    await _apiPortfolio.ReativarAsync(
                        _portfolioSelecionadoId.Value);

                    MessageBox.Show(
                        "Projeto reativado com sucesso!\n\nEle voltará a ser exibido no site.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                // Recarrega a grade
                await CarregarPortfolioAsync();

                // Limpa os campos
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao alterar a situação do projeto: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnDesativar.Enabled = _portfolioSelecionadoId != null;
            }
        }

        //private async void btnDesativar_Click(object sender, EventArgs e)
        //{
        //    if (_portfolioSelecionadoId == null)
        //        return;

        //    var confirmacao = MessageBox.Show(
        //        "Deseja alternar a exibição deste projeto no site?",
        //        "Confirmação",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question);

        //    if (confirmacao == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            await _apiPortfolio.DesativarAsync(
        //                _portfolioSelecionadoId.Value);

        //            MessageBox.Show(
        //                "Visibilidade do item no site atualizada com sucesso!",
        //                "Sucesso",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);

        //            await CarregarPortfolioAsync();

        //            LimparCampos();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(
        //                $"Erro ao alterar visibilidade: {ex.Message}",
        //                "Erro",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            var termo = txtBusca.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(termo))
            {
                AtualizarGrid(_listaPortfolio);
            }
            else
            {
                var filtrados = _listaPortfolio
                    .Where(p =>
                        (p.NomeProjeto?.ToLower().Contains(termo) ?? false) ||
                        (p.AmbienteNome?.ToLower().Contains(termo) ?? false) ||
                        (p.Descricao?.ToLower().Contains(termo) ?? false))
                    .ToList();

                AtualizarGrid(filtrados);
            }
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            await CarregarPortfolioAsync();
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtDescricao_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void picImagem_Click(object sender, EventArgs e)
        {
        }
        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verifica se existe um projeto selecionado
            if (_portfolioSelecionadoId == null)
            {
                MessageBox.Show(
                    "Selecione um projeto do portfólio para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Recupera o projeto selecionado na grade
            var itemSelecionado = dgvPortfolio.SelectedRows.Count > 0
                ? dgvPortfolio.SelectedRows[0].DataBoundItem as PortifolioDto
                : null;

            if (itemSelecionado == null)
            {
                MessageBox.Show(
                    "Não foi possível identificar o projeto selecionado.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Primeira confirmação
            var confirmacao = MessageBox.Show(
                $"Deseja realmente excluir permanentemente o projeto:\n\n" +
                $"\"{itemSelecionado.NomeProjeto}\"?\n\n" +
                "Esta operação não poderá ser desfeita.",
                "Excluir Permanentemente",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao != DialogResult.Yes)
                return;

            // Segunda confirmação para evitar exclusão acidental
            var confirmacaoFinal = MessageBox.Show(
                "ATENÇÃO!\n\n" +
                "O projeto será removido definitivamente do banco de dados.\n\n" +
                "Depois da exclusão não será possível reativá-lo.\n\n" +
                "Confirma a exclusão permanente?",
                "Confirmar Exclusão Definitiva",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacaoFinal != DialogResult.Yes)
                return;

            try
            {
                // Desabilita o botão enquanto a operação é executada
                btnExcluir.Enabled = false;

                // Chama a API para excluir definitivamente
                await _apiPortfolio.ExcluirPermanentementeAsync(
                    _portfolioSelecionadoId.Value);

                MessageBox.Show(
                    $"O projeto \"{itemSelecionado.NomeProjeto}\" foi excluído permanentemente com sucesso!",
                    "Projeto Excluído",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpa a seleção antes de recarregar
                _portfolioSelecionadoId = null;

                // Recarrega a grade
                await CarregarPortfolioAsync();

                // Limpa os campos do formulário
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao excluir permanentemente o projeto:\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Só habilita novamente se houver um projeto selecionado
                btnExcluir.Enabled = _portfolioSelecionadoId != null;
            }
        }
    }
}