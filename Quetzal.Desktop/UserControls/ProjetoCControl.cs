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
    public partial class ProjetoCControl : UserControl
    {
        private readonly ProjetoCApiCliente _apiProjeto;
        private readonly UsuarioApiCliente _apiUsuario;
        private readonly AmbienteApiUsuario _apiAmbiente;

        private List<ProjetoCDto> _listaProjetos = new List<ProjetoCDto>();
        private List<UsuarioDto> _listaClientes = new List<UsuarioDto>();
        private List<AmbienteDto> _listaAmbientes = new List<AmbienteDto>();
        private List<FotoItemModel> _fotosDoProjeto = new List<FotoItemModel>();

        private int? _projetoSelecionadoId = null;

        private bool _dadosCarregados = false;

        public class ItemCombo
        {
            public string Id { get; set; } = string.Empty;
            public string Texto { get; set; } = string.Empty;

            public override string ToString() => Texto;
        }

        public class FotoItemModel
        {
            public int AmbienteId { get; set; }
            public string AmbienteNome { get; set; } = string.Empty;
            public string CaminhoArquivo { get; set; } = string.Empty;
            public string Base64 { get; set; } = string.Empty;
            public Image? Imagem { get; set; }

            public override string ToString()
                => $"[{AmbienteNome}] {Path.GetFileName(CaminhoArquivo)}";
        }

        public ProjetoCControl()
        {
            InitializeComponent();

            _apiProjeto = new ProjetoCApiCliente();
            _apiUsuario = new UsuarioApiCliente();
            _apiAmbiente = new AmbienteApiUsuario();

            this.Load += ProjetoCControl_Load;
        }

        private async void ProjetoCControl_Load(object sender, EventArgs e)
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
                // 1. Carrega Clientes
                try
                {
                    _listaClientes = await _apiUsuario.ObterTodosAsync();

                    cmbCliente.Items.Clear();

                    cmbCliente.Items.Add(
                        new ItemCombo
                        {
                            Id = "",
                            Texto = "-- Selecione o Cliente --"
                        });

                    foreach (var c in _listaClientes)
                    {
                        var status = c.Ativo ? "" : " (Inativo)";

                        cmbCliente.Items.Add(
                            new ItemCombo
                            {
                                Id = c.Id,
                                Texto = $"{c.NomeCompleto}{status}"
                            });
                    }

                    if (cmbCliente.Items.Count > 0)
                        cmbCliente.SelectedIndex = 0;
                }
                catch
                {
                    // Mantém o comportamento original.
                }

                // 2. Carrega Ambientes
                try
                {
                    _listaAmbientes = await _apiAmbiente.ObterTodasAsync();

                    clbAmbientes.Items.Clear();
                    cmbAmbienteFoto.Items.Clear();

                    foreach (var amb in _listaAmbientes)
                    {
                        clbAmbientes.Items.Add(
                            new ItemCombo
                            {
                                Id = amb.Id.ToString(),
                                Texto = amb.Nome
                            });

                        cmbAmbienteFoto.Items.Add(
                            new ItemCombo
                            {
                                Id = amb.Id.ToString(),
                                Texto = amb.Nome
                            });
                    }

                    if (cmbAmbienteFoto.Items.Count > 0)
                        cmbAmbienteFoto.SelectedIndex = 0;
                }
                catch
                {
                    // Mantém o comportamento original.
                }

                // 3. Carrega Projetos
                await CarregarProjetosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar dados do formulário: {ex.Message}",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async Task CarregarProjetosAsync()
        {
            try
            {
                dgvProjetos.Enabled = false;

                _listaProjetos =
                    await _apiProjeto.ObterTodosAsync(incluirInativos: true);

                AtualizarGrid(_listaProjetos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível carregar a lista de projetos: {ex.Message}",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                dgvProjetos.Enabled = true;
            }
        }

        private void AtualizarGrid(List<ProjetoCDto> dados)
        {
            dgvProjetos.AutoGenerateColumns = false;
            dgvProjetos.DataSource = null;
            dgvProjetos.DataSource = dados;
            dgvProjetos.ClearSelection();
        }

        private void dgvProjetos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProjetos.SelectedRows.Count == 0)
                return;

            var linha = dgvProjetos.SelectedRows[0];

            if (linha.DataBoundItem is ProjetoCDto item)
            {
                _projetoSelecionadoId = item.Id;

                txtNomeProjeto.Text = item.Nome;
                txtDescricao.Text = item.Descricao;
                swAtivo.Checked = item.Ativo;

                // Seleciona Cliente
                for (int i = 0; i < cmbCliente.Items.Count; i++)
                {
                    if (cmbCliente.Items[i] is ItemCombo combo &&
                        combo.Id == item.UsuarioId)
                    {
                        cmbCliente.SelectedIndex = i;
                        break;
                    }
                }

                // Marca Ambientes associados
                for (int i = 0; i < clbAmbientes.Items.Count; i++)
                {
                    if (clbAmbientes.Items[i] is ItemCombo combo)
                    {
                        var marcado =
                            (item.AmbientesIds != null &&
                             item.AmbientesIds.Contains(int.Parse(combo.Id)))
                            ||
                            item.AmbienteId.ToString() == combo.Id;

                        clbAmbientes.SetItemChecked(i, marcado);
                    }
                }

                // Carrega Fotos existentes
                _fotosDoProjeto.Clear();

                if (item.Fotos != null)
                {
                    foreach (var f in item.Fotos)
                    {
                        _fotosDoProjeto.Add(
                            new FotoItemModel
                            {
                                AmbienteId = f.AmbienteId,
                                AmbienteNome = f.AmbienteNome,
                                CaminhoArquivo = f.CaminhoOuBase64,
                                Base64 = f.CaminhoOuBase64
                            });
                    }
                }

                AtualizarListaFotos();

                btnDesativar.Enabled = true;

                btnDesativar.Text =
                    item.Ativo
                        ? "🗑️ Desativar Projeto"
                        : "🔄 Reativar Projeto";
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            _projetoSelecionadoId = null;

            txtNomeProjeto.Clear();
            txtDescricao.Clear();

            if (cmbCliente.Items.Count > 0)
                cmbCliente.SelectedIndex = 0;

            swAtivo.Checked = true;

            for (int i = 0; i < clbAmbientes.Items.Count; i++)
            {
                clbAmbientes.SetItemChecked(i, false);
            }

            _fotosDoProjeto.Clear();

            AtualizarListaFotos();

            picPreviewFoto.Image = null;

            dgvProjetos.ClearSelection();

            btnDesativar.Enabled = false;
            btnDesativar.Text = "🗑️ Desativar Projeto";

            txtNomeProjeto.Focus();
        }

        private void btnAdicionarFoto_Click(object sender, EventArgs e)
        {
            if (cmbAmbienteFoto.SelectedItem is not ItemCombo ambienteSelecionado ||
                string.IsNullOrEmpty(ambienteSelecionado.Id))
            {
                MessageBox.Show(
                    "Selecione qual ambiente esta foto representa antes de anexar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var ofd = new OpenFileDialog
            {
                Title = "Selecionar Foto do Ambiente",
                Filter =
                    "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.webp;*.bmp|" +
                    "Todos os Arquivos|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var bytes = File.ReadAllBytes(ofd.FileName);
                    var base64 = Convert.ToBase64String(bytes);

                    using var ms = new MemoryStream(bytes);

                    var img = Image.FromStream(ms);

                    var foto = new FotoItemModel
                    {
                        AmbienteId = int.Parse(ambienteSelecionado.Id),
                        AmbienteNome = ambienteSelecionado.Texto,
                        CaminhoArquivo = ofd.FileName,
                        Base64 = base64,
                        Imagem = (Image)img.Clone()
                    };

                    _fotosDoProjeto.Add(foto);

                    AtualizarListaFotos();

                    lstFotos.SelectedIndex =
                        _fotosDoProjeto.Count - 1;
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

        private void AtualizarListaFotos()
        {
            lstFotos.Items.Clear();

            foreach (var foto in _fotosDoProjeto)
            {
                lstFotos.Items.Add(foto);
            }
        }

        private void lstFotos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFotos.SelectedItem is FotoItemModel foto)
            {
                if (foto.Imagem != null)
                {
                    picPreviewFoto.Image = foto.Imagem;
                }
                else if (!string.IsNullOrEmpty(foto.CaminhoArquivo) &&
                         File.Exists(foto.CaminhoArquivo))
                {
                    try
                    {
                        picPreviewFoto.Image =
                            Image.FromFile(foto.CaminhoArquivo);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    picPreviewFoto.Image = null;
                }
            }
        }

        private void btnRemoverFoto_Click(object sender, EventArgs e)
        {
            if (lstFotos.SelectedIndex >= 0 &&
                lstFotos.SelectedIndex < _fotosDoProjeto.Count)
            {
                _fotosDoProjeto.RemoveAt(lstFotos.SelectedIndex);

                picPreviewFoto.Image = null;

                AtualizarListaFotos();
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            var nome = txtNomeProjeto.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show(
                    "Informe o nome do projeto.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNomeProjeto.Focus();

                return;
            }

            var clienteCombo =
                cmbCliente.SelectedItem as ItemCombo;

            var clienteId =
                clienteCombo?.Id ?? "";

            if (string.IsNullOrWhiteSpace(clienteId))
            {
                MessageBox.Show(
                    "Selecione a qual cliente este projeto pertence.",
                    "Campo Obrigatório",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCliente.Focus();

                return;
            }

            // Ambientes selecionados
            var ambientesIds = new List<int>();

            int primeiroAmbienteId = 0;
            string primeiroAmbienteNome = "";

            for (int i = 0; i < clbAmbientes.Items.Count; i++)
            {
                if (clbAmbientes.GetItemChecked(i) &&
                    clbAmbientes.Items[i] is ItemCombo combo)
                {
                    if (int.TryParse(combo.Id, out int ambId))
                    {
                        ambientesIds.Add(ambId);

                        if (primeiroAmbienteId == 0)
                        {
                            primeiroAmbienteId = ambId;
                            primeiroAmbienteNome = combo.Texto;
                        }
                    }
                }
            }

            try
            {
                btnSalvar.Enabled = false;

                var fotosDto =
                    _fotosDoProjeto
                        .Select(f => new FotoAmbienteDto
                        {
                            AmbienteId = f.AmbienteId,
                            AmbienteNome = f.AmbienteNome,
                            CaminhoOuBase64 = f.Base64,
                            Descricao =
                                $"Foto do ambiente {f.AmbienteNome}"
                        })
                        .ToList();

                var dto = new ProjetoCDto
                {
                    Id = _projetoSelecionadoId ?? 0,
                    Nome = nome,
                    Descricao = txtDescricao.Text.Trim(),
                    UsuarioId = clienteId,
                    UsuarioNome = clienteCombo?.Texto ?? "",
                    AmbienteId = primeiroAmbienteId,
                    AmbienteNome = primeiroAmbienteNome,
                    AmbientesIds = ambientesIds,
                    Fotos = fotosDto,
                    Ativo = swAtivo.Checked,
                    DataCadastro = DateTime.Now
                };

                if (_projetoSelecionadoId == null ||
                    _projetoSelecionadoId == 0)
                {
                    await _apiProjeto.CadastrarAsync(dto);

                    MessageBox.Show(
                        "Projeto do cliente cadastrado com sucesso com seus ambientes e galeria de fotos!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    await _apiProjeto.AtualizarAsync(
                        _projetoSelecionadoId.Value,
                        dto);

                    MessageBox.Show(
                        "Projeto do cliente atualizado com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                await CarregarProjetosAsync();

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar projeto: {ex.Message}",
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
            if (_projetoSelecionadoId == null)
                return;

            var confirmacao = MessageBox.Show(
                "Deseja alternar a ativação deste projeto?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    await _apiProjeto.DesativarAsync(
                        _projetoSelecionadoId.Value);

                    MessageBox.Show(
                        "Situação do projeto alterada com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await CarregarProjetosAsync();

                    LimparCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Erro ao alterar situação: {ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            var termo =
                txtBusca.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(termo))
            {
                AtualizarGrid(_listaProjetos);
            }
            else
            {
                var filtrados =
                    _listaProjetos
                        .Where(p =>
                            (p.Nome?.ToLower().Contains(termo) ?? false) ||
                            (p.UsuarioNome?.ToLower().Contains(termo) ?? false) ||
                            (p.AmbienteNome?.ToLower().Contains(termo) ?? false))
                        .ToList();

                AtualizarGrid(filtrados);
            }
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            await CarregarProjetosAsync();
        }
    }
}