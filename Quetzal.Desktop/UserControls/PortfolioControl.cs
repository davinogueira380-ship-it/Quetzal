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
        private readonly ProjetoCApiCliente _apiProjetoC;

        private List<PortifolioDto> _listaPortfolio =
            new List<PortifolioDto>();

        private List<AmbienteDto> _listaAmbientes =
            new List<AmbienteDto>();

        private List<ProjetoCDto> _listaProjetos =
            new List<ProjetoCDto>();

        private int? _portfolioSelecionadoId = null;
        private int? _projetoSelecionadoId = null;

        private bool _dadosCarregados = false;
        private bool _carregandoSelecao = false;

        public class ItemAmbienteCombo
        {
            public int Id { get; set; }

            public string Nome { get; set; } = string.Empty;

            public override string ToString() => Nome;
        }

        public class ItemProjetoCombo
        {
            public int Id { get; set; }

            public string Nome { get; set; } = string.Empty;

            public override string ToString() => Nome;
        }

        public class ItemFotoLista
        {
            public int Id { get; set; }

            public int Ordem { get; set; }

            public string Foto { get; set; } = string.Empty;

            public override string ToString()
            {
                return $"Foto {Ordem}";
            }
        }

        public PortfolioControl()
        {
            InitializeComponent();

            _apiPortfolio = new PortfolioApiCliente();
            _apiAmbiente = new AmbienteApiUsuario();
            _apiProjetoC = new ProjetoCApiCliente();

            Load += PortfolioControl_Load;
        }

        private async void PortfolioControl_Load(
            object sender,
            EventArgs e)
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
                await CarregarAmbientesAsync();
                await CarregarProjetosAsync();
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

        private async Task CarregarAmbientesAsync()
        {
            _listaAmbientes =
                await _apiAmbiente.ObterTodasAsync();

            cmbAmbiente.Items.Clear();

            cmbAmbiente.Items.Add(
                new ItemAmbienteCombo
                {
                    Id = 0,
                    Nome = "-- Selecione o Ambiente --"
                });

            foreach (var ambiente in _listaAmbientes)
            {
                cmbAmbiente.Items.Add(
                    new ItemAmbienteCombo
                    {
                        Id = ambiente.Id,
                        Nome = ambiente.Nome
                    });
            }

            if (cmbAmbiente.Items.Count > 0)
            {
                cmbAmbiente.SelectedIndex = 0;
            }
        }

        private async Task CarregarProjetosAsync()
        {
            _listaProjetos =
                await _apiProjetoC.ObterTodosAsync(false);

            cmbProjeto.Items.Clear();

            cmbProjeto.Items.Add(
                new ItemProjetoCombo
                {
                    Id = 0,
                    Nome = "-- Selecione o Projeto --"
                });

            foreach (var projeto in _listaProjetos
                         .Where(p => p.Ativo)
                         .OrderBy(p => p.Nome))
            {
                cmbProjeto.Items.Add(
                    new ItemProjetoCombo
                    {
                        Id = projeto.Id,
                        Nome = projeto.Nome
                    });
            }

            if (cmbProjeto.Items.Count > 0)
            {
                cmbProjeto.SelectedIndex = 0;
            }

            LimparFotosProjeto();
        }

        private async Task CarregarPortfolioAsync()
        {
            try
            {
                dgvPortfolio.Enabled = false;

                _listaPortfolio =
                    await _apiPortfolio.ObterTodosAsync();

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

        private void AtualizarGrid(
            List<PortifolioDto> dados)
        {
            dgvPortfolio.AutoGenerateColumns = false;
            dgvPortfolio.DataSource = null;
            dgvPortfolio.DataSource = dados;
            dgvPortfolio.ClearSelection();
        }

        private async void dgvPortfolio_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (_carregandoSelecao)
                return;

            if (dgvPortfolio.SelectedRows.Count == 0)
                return;

            var linha =
                dgvPortfolio.SelectedRows[0];

            if (linha.DataBoundItem is not PortifolioDto item)
                return;

            try
            {
                _carregandoSelecao = true;

                _portfolioSelecionadoId = item.Id;

                txtNomeProjeto.Text =
                    item.NomeProjeto;

                txtDescricao.Text =
                    item.Descricao;

                swAtivo.Checked =
                    item.Ativo;

                SelecionarAmbiente(item.AmbienteId);

                await SelecionarProjetoAsync(
                    item.ProjetoCId,
                    item.ProjetoCFotosIds);

                if (item.FotosSelecionadas != null &&
                    item.FotosSelecionadas.Count > 0)
                {
                    var primeiraFoto =
                        item.FotosSelecionadas
                            .OrderBy(f => f.Ordem)
                            .FirstOrDefault();

                    if (primeiraFoto != null)
                    {
                        CarregarPreviewImagem(
                            primeiraFoto.Foto);
                    }
                }
                else
                {
                    picImagem.Image = null;
                }

                btnDesativar.Enabled = true;
                btnExcluir.Enabled = true;

                btnDesativar.Text =
                    item.Ativo
                        ? "🗑️ Desativar do Site"
                        : "🔄 Reativar no Site";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar o item selecionado: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _carregandoSelecao = false;
            }
        }

        private void SelecionarAmbiente(int ambienteId)
        {
            cmbAmbiente.SelectedIndex = 0;

            for (int i = 0;
                 i < cmbAmbiente.Items.Count;
                 i++)
            {
                if (cmbAmbiente.Items[i]
                        is ItemAmbienteCombo item &&
                    item.Id == ambienteId)
                {
                    cmbAmbiente.SelectedIndex = i;
                    break;
                }
            }
        }

        private async Task SelecionarProjetoAsync(
            int? projetoId,
            List<int>? fotosIds)
        {
            _projetoSelecionadoId = projetoId;

            cmbProjeto.SelectedIndex = 0;

            if (!projetoId.HasValue ||
                projetoId.Value <= 0)
            {
                LimparFotosProjeto();
                return;
            }

            for (int i = 0;
                 i < cmbProjeto.Items.Count;
                 i++)
            {
                if (cmbProjeto.Items[i]
                        is ItemProjetoCombo item &&
                    item.Id == projetoId.Value)
                {
                    cmbProjeto.SelectedIndex = i;
                    break;
                }
            }

            await CarregarFotosDoProjetoAsync(
                projetoId.Value,
                fotosIds);
        }

        private async void cmbProjeto_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (_carregandoSelecao)
                return;

            if (cmbProjeto.SelectedItem
                is not ItemProjetoCombo projeto ||
                projeto.Id <= 0)
            {
                _projetoSelecionadoId = null;

                LimparFotosProjeto();

                return;
            }

            _projetoSelecionadoId = projeto.Id;

            await CarregarFotosDoProjetoAsync(
                projeto.Id,
                null);
        }

        private async Task CarregarFotosDoProjetoAsync(
            int projetoId,
            List<int>? fotosIdsSelecionadas)
        {
            try
            {
                clbFotosProjeto.Enabled = false;
                clbFotosProjeto.Items.Clear();
                picImagem.Image = null;

                var projeto =
                    await _apiProjetoC.ObterPorIdAsync(
                        projetoId);

                if (projeto == null)
                {
                    MessageBox.Show(
                        "O projeto selecionado não foi encontrado.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    LimparFotosProjeto();

                    return;
                }

                var fotos =
                    projeto.FotosDetalhadas
                        .OrderBy(f => f.Ordem)
                        .ToList();

                if (fotos.Count == 0)
                {
                    clbFotosProjeto.Items.Add(
                        "Projeto sem fotos");

                    clbFotosProjeto.Enabled = false;

                    return;
                }

                var idsMarcados =
                    fotosIdsSelecionadas ??
                    new List<int>();

                foreach (var foto in fotos)
                {
                    var itemFoto =
                        new ItemFotoLista
                        {
                            Id = foto.Id,
                            Ordem = foto.Ordem,
                            Foto = foto.Foto
                        };

                    var deveMarcar =
                        idsMarcados.Contains(foto.Id);

                    clbFotosProjeto.Items.Add(
                        itemFoto,
                        deveMarcar);
                }

                clbFotosProjeto.Enabled = true;

                if (clbFotosProjeto.Items.Count > 0)
                {
                    clbFotosProjeto.SelectedIndex = 0;

                    if (clbFotosProjeto.Items[0]
                        is ItemFotoLista primeiraFoto)
                    {
                        CarregarPreviewImagem(
                            primeiraFoto.Foto);
                    }
                }
            }
            catch (Exception ex)
            {
                LimparFotosProjeto();

                MessageBox.Show(
                    $"Erro ao carregar as fotos do projeto: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void clbFotosProjeto_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (_carregandoSelecao)
                return;

            if (clbFotosProjeto.SelectedItem
                is not ItemFotoLista foto)
            {
                picImagem.Image = null;
                return;
            }

            CarregarPreviewImagem(
                foto.Foto);
        }

        private List<int> ObterFotosMarcadas()
        {
            return clbFotosProjeto
                .CheckedItems
                .OfType<ItemFotoLista>()
                .Select(f => f.Id)
                .Distinct()
                .ToList();
        }

        private void LimparFotosProjeto()
        {
            clbFotosProjeto.Items.Clear();
            clbFotosProjeto.Enabled = false;

            picImagem.Image = null;
        }

        private void CarregarPreviewImagem(
            string? imagem)
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
                    using var imagemArquivo =
                        Image.FromFile(imagem);

                    picImagem.Image =
                        (Image)imagemArquivo.Clone();

                    return;
                }

                var conteudoBase64 = imagem;

                if (conteudoBase64.Contains(","))
                {
                    conteudoBase64 =
                        conteudoBase64.Substring(
                            conteudoBase64.IndexOf(",") + 1);
                }

                var bytes =
                    Convert.FromBase64String(
                        conteudoBase64);

                using var ms =
                    new MemoryStream(bytes);

                using var imagemMemoria =
                    Image.FromStream(ms);

                picImagem.Image =
                    (Image)imagemMemoria.Clone();
            }
            catch
            {
                picImagem.Image = null;
            }
        }

        private void btnNovo_Click(
            object sender,
            EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            _carregandoSelecao = true;

            try
            {
                _portfolioSelecionadoId = null;
                _projetoSelecionadoId = null;

                txtNomeProjeto.Clear();
                txtDescricao.Clear();

                if (cmbProjeto.Items.Count > 0)
                {
                    cmbProjeto.SelectedIndex = 0;
                }

                LimparFotosProjeto();

                if (cmbAmbiente.Items.Count > 0)
                {
                    cmbAmbiente.SelectedIndex = 0;
                }

                swAtivo.Checked = true;

                picImagem.Image = null;

                dgvPortfolio.ClearSelection();

                btnDesativar.Enabled = false;
                btnDesativar.Text =
                    "🗑️ Desativar do Site";

                btnExcluir.Enabled = false;

                txtNomeProjeto.Focus();
            }
            finally
            {
                _carregandoSelecao = false;
            }
        }

        private void btnSelecionarImagem_Click(
            object sender,
            EventArgs e)
        {
            MessageBox.Show(
                "As fotos do portfólio devem ser marcadas na lista de fotos do projeto selecionado.",
                "Selecionar Fotos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private async void btnSalvar_Click(
            object sender,
            EventArgs e)
        {
            var titulo =
                txtNomeProjeto.Text.Trim();

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

            var projetoSelecionado =
                cmbProjeto.SelectedItem
                    as ItemProjetoCombo;

            var projetoId =
                projetoSelecionado?.Id ?? 0;

            if (projetoId <= 0)
            {
                MessageBox.Show(
                    "Selecione o projeto que será publicado no portfólio.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProjeto.Focus();

                return;
            }

            var fotosSelecionadas =
                ObterFotosMarcadas();

            if (fotosSelecionadas.Count == 0)
            {
                MessageBox.Show(
                    "Marque pelo menos uma foto do projeto para publicar no portfólio.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                clbFotosProjeto.Focus();

                return;
            }

            var ambienteSelecionado =
                cmbAmbiente.SelectedItem
                    as ItemAmbienteCombo;

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

                var dto =
                    new PortifolioDto
                    {
                        Id =
                            _portfolioSelecionadoId ?? 0,

                        NomeProjeto =
                            titulo,

                        Descricao =
                            txtDescricao.Text.Trim(),

                        AmbienteId =
                            ambienteId,

                        AmbienteNome =
                            ambienteSelecionado?.Nome ??
                            string.Empty,

                        ProjetoCId =
                            projetoId,

                        ProjetoCNome =
                            projetoSelecionado?.Nome ??
                            string.Empty,

                        ProjetoCFotosIds =
                            fotosSelecionadas,

                        ImagemUpload =
                            string.Empty,

                        Ativo =
                            swAtivo.Checked,

                        DataCadastro =
                            DateTime.Now
                    };

                if (_portfolioSelecionadoId == null ||
                    _portfolioSelecionadoId == 0)
                {
                    await _apiPortfolio
                        .CadastrarAsync(dto);

                    MessageBox.Show(
                        "Projeto adicionado ao portfólio público com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    await _apiPortfolio
                        .AtualizarAsync(
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

        private async void btnDesativar_Click(
            object sender,
            EventArgs e)
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

            var itemSelecionado =
                dgvPortfolio.SelectedRows.Count > 0
                    ? dgvPortfolio.SelectedRows[0]
                        .DataBoundItem as PortifolioDto
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

            bool estaAtivo =
                itemSelecionado.Ativo;

            string mensagem =
                estaAtivo
                    ? "Deseja realmente desativar este projeto?\n\nEle deixará de aparecer no site."
                    : "Deseja realmente reativar este projeto?\n\nEle voltará a aparecer no site.";

            string titulo =
                estaAtivo
                    ? "Desativar Projeto"
                    : "Reativar Projeto";

            var confirmacao =
                MessageBox.Show(
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
                    await _apiPortfolio
                        .DesativarAsync(
                            _portfolioSelecionadoId.Value);

                    MessageBox.Show(
                        "Projeto desativado com sucesso!\n\nEle não será mais exibido no site.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    await _apiPortfolio
                        .ReativarAsync(
                            _portfolioSelecionadoId.Value);

                    MessageBox.Show(
                        "Projeto reativado com sucesso!\n\nEle voltará a ser exibido no site.",
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
                    $"Erro ao alterar a situação do projeto: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnDesativar.Enabled =
                    _portfolioSelecionadoId != null;
            }
        }

        private void txtBusca_TextChanged(
            object sender,
            EventArgs e)
        {
            var termo =
                txtBusca.Text
                    .Trim()
                    .ToLower();

            if (string.IsNullOrEmpty(termo))
            {
                AtualizarGrid(
                    _listaPortfolio);
            }
            else
            {
                var filtrados =
                    _listaPortfolio
                        .Where(p =>
                            (p.NomeProjeto?
                                .ToLower()
                                .Contains(termo) ??
                             false) ||
                            (p.ProjetoCNome?
                                .ToLower()
                                .Contains(termo) ??
                             false) ||
                            (p.AmbienteNome?
                                .ToLower()
                                .Contains(termo) ??
                             false) ||
                            (p.Descricao?
                                .ToLower()
                                .Contains(termo) ??
                             false))
                        .ToList();

                AtualizarGrid(filtrados);
            }
        }

        private async void btnAtualizar_Click(
            object sender,
            EventArgs e)
        {
            await CarregarProjetosAsync();
            await CarregarPortfolioAsync();

            LimparCampos();
        }

        private void txtDescricao_TextChanged(
            object sender,
            EventArgs e)
        {
        }

        private void txtDescricao_TextChanged_1(
            object sender,
            EventArgs e)
        {
        }

        private void picImagem_Click(
            object sender,
            EventArgs e)
        {
        }

        private async void btnExcluir_Click(
            object sender,
            EventArgs e)
        {
            if (_portfolioSelecionadoId == null)
            {
                MessageBox.Show(
                    "Selecione um projeto do portfólio para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var itemSelecionado =
                dgvPortfolio.SelectedRows.Count > 0
                    ? dgvPortfolio.SelectedRows[0]
                        .DataBoundItem as PortifolioDto
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

            var confirmacao =
                MessageBox.Show(
                    $"Deseja realmente excluir permanentemente o projeto:\n\n" +
                    $"\"{itemSelecionado.NomeProjeto}\"?\n\n" +
                    "Esta operação não poderá ser desfeita.",
                    "Excluir Permanentemente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (confirmacao != DialogResult.Yes)
                return;

            var confirmacaoFinal =
                MessageBox.Show(
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
                btnExcluir.Enabled = false;

                await _apiPortfolio
                    .ExcluirPermanentementeAsync(
                        _portfolioSelecionadoId.Value);

                MessageBox.Show(
                    $"O projeto \"{itemSelecionado.NomeProjeto}\" foi excluído permanentemente com sucesso!",
                    "Projeto Excluído",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _portfolioSelecionadoId = null;

                await CarregarPortfolioAsync();

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
                btnExcluir.Enabled =
                    _portfolioSelecionadoId != null;
            }
        }
    }
}