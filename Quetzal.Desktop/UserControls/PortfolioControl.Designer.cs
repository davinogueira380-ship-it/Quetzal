namespace Quetzal.Desktop.UserControls
{
    partial class PortfolioControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows UserControl Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesPrincipal1 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesPrincipal2 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesGrid1 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesGrid2 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesCard1 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edgesCard2 = new();

            Guna.UI2.WinForms.Suite.CustomizableEdges edges1 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges2 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges3 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges4 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges5 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges6 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges7 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges8 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges9 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges10 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges11 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges12 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges13 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges14 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges15 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges16 = new();

            DataGridViewCellStyle dataGridViewCellStyle1 = new();
            DataGridViewCellStyle dataGridViewCellStyle2 = new();
            DataGridViewCellStyle dataGridViewCellStyle3 = new();

            pnlPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            dgvPortfolio = new Guna.UI2.WinForms.Guna2DataGridView();

            colId = new DataGridViewTextBoxColumn();
            colNomeProjeto = new DataGridViewTextBoxColumn();
            colProjetoOrigem = new DataGridViewTextBoxColumn();
            colAmbiente = new DataGridViewTextBoxColumn();
            colDescricao = new DataGridViewTextBoxColumn();
            colAtivo = new DataGridViewCheckBoxColumn();

            pnlBusca = new Panel();
            btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            txtBusca = new Guna.UI2.WinForms.Guna2TextBox();
            lblBusca = new Label();

            pnlCardFormulario = new Guna.UI2.WinForms.Guna2Panel();

            lblTituloCard = new Label();
            btnNovo = new Guna.UI2.WinForms.Guna2Button();

            lblProjeto = new Label();
            cmbProjeto = new Guna.UI2.WinForms.Guna2ComboBox();

            lblFotoProjeto = new Label();
            clbFotosProjeto = new CheckedListBox();

            lblAmbiente = new Label();
            cmbAmbiente = new Guna.UI2.WinForms.Guna2ComboBox();

            lblNomeProjeto = new Label();
            txtNomeProjeto = new Guna.UI2.WinForms.Guna2TextBox();

            lblDescricao = new Label();
            txtDescricao = new Guna.UI2.WinForms.Guna2TextBox();

            lblImagem = new Label();
            picImagem = new Guna.UI2.WinForms.Guna2PictureBox();

            swAtivo = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblStatusAtivo = new Label();

            btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            btnDesativar = new Guna.UI2.WinForms.Guna2Button();
            btnExcluir = new Guna.UI2.WinForms.Guna2Button();

            pnlPrincipal.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPortfolio).BeginInit();
            pnlBusca.SuspendLayout();
            pnlCardFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picImagem).BeginInit();
            SuspendLayout();

            // pnlPrincipal
            pnlPrincipal.BackColor = Color.Transparent;
            pnlPrincipal.Controls.Add(pnlGrid);
            pnlPrincipal.Controls.Add(pnlCardFormulario);
            pnlPrincipal.CustomizableEdges = edgesPrincipal1;
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(15, 15);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.ShadowDecoration.CustomizableEdges = edgesPrincipal2;
            pnlPrincipal.Size = new Size(961, 573);
            pnlPrincipal.TabIndex = 0;

            // pnlGrid
            pnlGrid.BackColor = Color.Transparent;
            pnlGrid.BorderRadius = 12;
            pnlGrid.Controls.Add(dgvPortfolio);
            pnlGrid.Controls.Add(pnlBusca);
            pnlGrid.CustomizableEdges = edgesGrid1;
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.FillColor = Color.White;
            pnlGrid.Location = new Point(385, 0);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(15);
            pnlGrid.ShadowDecoration.CustomizableEdges = edgesGrid2;
            pnlGrid.Size = new Size(576, 573);
            pnlGrid.TabIndex = 1;

            // dgvPortfolio
            dgvPortfolio.AllowUserToAddRows = false;
            dgvPortfolio.AllowUserToDeleteRows = false;
            dgvPortfolio.AllowUserToResizeRows = false;

            dataGridViewCellStyle1.BackColor =
                Color.FromArgb(248, 250, 252);

            dgvPortfolio.AlternatingRowsDefaultCellStyle =
                dataGridViewCellStyle1;

            dataGridViewCellStyle2.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor =
                Color.FromArgb(238, 242, 246);
            dataGridViewCellStyle2.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor =
                Color.FromArgb(40, 50, 65);
            dataGridViewCellStyle2.Padding = new Padding(4);
            dataGridViewCellStyle2.SelectionBackColor =
                Color.FromArgb(238, 242, 246);
            dataGridViewCellStyle2.SelectionForeColor =
                Color.FromArgb(40, 50, 65);
            dataGridViewCellStyle2.WrapMode =
                DataGridViewTriState.True;

            dgvPortfolio.ColumnHeadersDefaultCellStyle =
                dataGridViewCellStyle2;

            dgvPortfolio.ColumnHeadersHeight = 38;
            dgvPortfolio.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dgvPortfolio.Columns.AddRange(
                new DataGridViewColumn[]
                {
                    colId,
                    colNomeProjeto,
                    colProjetoOrigem,
                    colAmbiente,
                    colDescricao,
                    colAtivo
                });

            dataGridViewCellStyle3.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font =
                new Font("Segoe UI", 9.25F);
            dataGridViewCellStyle3.ForeColor =
                Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle3.SelectionBackColor =
                Color.FromArgb(225, 238, 250);
            dataGridViewCellStyle3.SelectionForeColor =
                Color.FromArgb(10, 40, 70);
            dataGridViewCellStyle3.WrapMode =
                DataGridViewTriState.False;

            dgvPortfolio.DefaultCellStyle =
                dataGridViewCellStyle3;
            dgvPortfolio.Dock = DockStyle.Fill;
            dgvPortfolio.GridColor =
                Color.FromArgb(235, 238, 242);
            dgvPortfolio.Location = new Point(15, 67);
            dgvPortfolio.MultiSelect = false;
            dgvPortfolio.Name = "dgvPortfolio";
            dgvPortfolio.ReadOnly = true;
            dgvPortfolio.RowHeadersVisible = false;
            dgvPortfolio.RowTemplate.Height = 33;
            dgvPortfolio.Size = new Size(546, 491);
            dgvPortfolio.TabIndex = 1;

            dgvPortfolio.ThemeStyle.AlternatingRowsStyle.BackColor =
                Color.FromArgb(248, 250, 252);
            dgvPortfolio.ThemeStyle.GridColor =
                Color.FromArgb(235, 238, 242);
            dgvPortfolio.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(238, 242, 246);
            dgvPortfolio.ThemeStyle.HeaderStyle.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvPortfolio.ThemeStyle.HeaderStyle.ForeColor =
                Color.FromArgb(40, 50, 65);
            dgvPortfolio.ThemeStyle.HeaderStyle.Height = 38;
            dgvPortfolio.ThemeStyle.ReadOnly = true;
            dgvPortfolio.ThemeStyle.RowsStyle.Font =
                new Font("Segoe UI", 9.25F);
            dgvPortfolio.ThemeStyle.RowsStyle.ForeColor =
                Color.FromArgb(40, 40, 40);
            dgvPortfolio.ThemeStyle.RowsStyle.Height = 33;
            dgvPortfolio.ThemeStyle.RowsStyle.SelectionBackColor =
                Color.FromArgb(225, 238, 250);
            dgvPortfolio.ThemeStyle.RowsStyle.SelectionForeColor =
                Color.FromArgb(10, 40, 70);

            dgvPortfolio.SelectionChanged +=
                dgvPortfolio_SelectionChanged;

            // colId
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;

            // colNomeProjeto
            colNomeProjeto.DataPropertyName = "NomeProjeto";
            colNomeProjeto.FillWeight = 55F;
            colNomeProjeto.HeaderText = "Título";
            colNomeProjeto.Name = "colNomeProjeto";
            colNomeProjeto.ReadOnly = true;

            // colProjetoOrigem
            colProjetoOrigem.DataPropertyName = "ProjetoCNome";
            colProjetoOrigem.FillWeight = 50F;
            colProjetoOrigem.HeaderText = "Projeto";
            colProjetoOrigem.Name = "colProjetoOrigem";
            colProjetoOrigem.ReadOnly = true;

            // colAmbiente
            colAmbiente.DataPropertyName = "AmbienteNome";
            colAmbiente.FillWeight = 40F;
            colAmbiente.HeaderText = "Ambiente";
            colAmbiente.Name = "colAmbiente";
            colAmbiente.ReadOnly = true;

            // colDescricao
            colDescricao.DataPropertyName = "Descricao";
            colDescricao.FillWeight = 60F;
            colDescricao.HeaderText = "Descrição";
            colDescricao.Name = "colDescricao";
            colDescricao.ReadOnly = true;

            // colAtivo
            colAtivo.DataPropertyName = "Ativo";
            colAtivo.FillWeight = 25F;
            colAtivo.HeaderText = "Site";
            colAtivo.Name = "colAtivo";
            colAtivo.ReadOnly = true;

            // pnlBusca
            pnlBusca.BackColor = Color.Transparent;
            pnlBusca.Controls.Add(btnAtualizar);
            pnlBusca.Controls.Add(txtBusca);
            pnlBusca.Controls.Add(lblBusca);
            pnlBusca.Dock = DockStyle.Top;
            pnlBusca.Location = new Point(15, 15);
            pnlBusca.Name = "pnlBusca";
            pnlBusca.Size = new Size(546, 52);
            pnlBusca.TabIndex = 0;

            // lblBusca
            lblBusca.AutoSize = true;
            lblBusca.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblBusca.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblBusca.Location = new Point(3, 11);
            lblBusca.Name = "lblBusca";
            lblBusca.Size = new Size(99, 15);
            lblBusca.TabIndex = 0;
            lblBusca.Text = "Buscar Portfólio:";

            // txtBusca
            txtBusca.Animated = true;
            txtBusca.BorderRadius = 7;
            txtBusca.Cursor = Cursors.IBeam;
            txtBusca.CustomizableEdges = edges1;
            txtBusca.DefaultText = "";
            txtBusca.FillColor =
                Color.FromArgb(251, 234, 214);
            txtBusca.FocusedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            txtBusca.Font =
                new Font("Segoe UI", 9F);
            txtBusca.ForeColor =
                Color.FromArgb(50, 60, 75);
            txtBusca.Location = new Point(105, 6);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderForeColor = Color.Gray;
            txtBusca.PlaceholderText =
                "Título, projeto, ambiente...";
            txtBusca.SelectedText = "";
            txtBusca.ShadowDecoration.CustomizableEdges = edges2;
            txtBusca.Size = new Size(250, 32);
            txtBusca.TabIndex = 1;
            txtBusca.TextChanged += txtBusca_TextChanged;

            // btnAtualizar
            btnAtualizar.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;
            btnAtualizar.Animated = true;
            btnAtualizar.BorderRadius = 7;
            btnAtualizar.Cursor = Cursors.Hand;
            btnAtualizar.CustomizableEdges = edges3;
            btnAtualizar.FillColor =
                Color.FromArgb(251, 234, 214);
            btnAtualizar.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            btnAtualizar.ForeColor =
                Color.FromArgb(50, 60, 75);
            btnAtualizar.HoverState.FillColor =
                Color.FromArgb(240, 220, 198);
            btnAtualizar.Location = new Point(443, 6);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.ShadowDecoration.CustomizableEdges = edges4;
            btnAtualizar.Size = new Size(100, 32);
            btnAtualizar.TabIndex = 2;
            btnAtualizar.Text = "🔄 Atualizar";
            btnAtualizar.Click += btnAtualizar_Click;

            // pnlCardFormulario
            pnlCardFormulario.AutoScroll = true;
            pnlCardFormulario.BackColor = Color.Transparent;
            pnlCardFormulario.BorderRadius = 12;

            pnlCardFormulario.Controls.Add(btnExcluir);
            pnlCardFormulario.Controls.Add(btnDesativar);
            pnlCardFormulario.Controls.Add(btnSalvar);

            pnlCardFormulario.Controls.Add(lblStatusAtivo);
            pnlCardFormulario.Controls.Add(swAtivo);

            pnlCardFormulario.Controls.Add(picImagem);
            pnlCardFormulario.Controls.Add(lblImagem);

            pnlCardFormulario.Controls.Add(txtDescricao);
            pnlCardFormulario.Controls.Add(lblDescricao);

            pnlCardFormulario.Controls.Add(txtNomeProjeto);
            pnlCardFormulario.Controls.Add(lblNomeProjeto);

            pnlCardFormulario.Controls.Add(cmbAmbiente);
            pnlCardFormulario.Controls.Add(lblAmbiente);

            pnlCardFormulario.Controls.Add(clbFotosProjeto);
            pnlCardFormulario.Controls.Add(lblFotoProjeto);

            pnlCardFormulario.Controls.Add(cmbProjeto);
            pnlCardFormulario.Controls.Add(lblProjeto);

            pnlCardFormulario.Controls.Add(btnNovo);
            pnlCardFormulario.Controls.Add(lblTituloCard);

            pnlCardFormulario.CustomizableEdges = edgesCard1;
            pnlCardFormulario.Dock = DockStyle.Left;
            pnlCardFormulario.FillColor = Color.White;
            pnlCardFormulario.Location = new Point(0, 0);
            pnlCardFormulario.Name = "pnlCardFormulario";
            pnlCardFormulario.Padding = new Padding(15);
            pnlCardFormulario.ShadowDecoration.CustomizableEdges = edgesCard2;
            pnlCardFormulario.Size = new Size(385, 573);
            pnlCardFormulario.TabIndex = 0;

            // lblTituloCard
            lblTituloCard.AutoSize = true;
            lblTituloCard.Font =
                new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTituloCard.ForeColor =
                Color.FromArgb(25, 35, 50);
            lblTituloCard.Location = new Point(15, 14);
            lblTituloCard.Name = "lblTituloCard";
            lblTituloCard.Size = new Size(203, 21);
            lblTituloCard.TabIndex = 0;
            lblTituloCard.Text = "Portfólio Público do Site";

            // btnNovo
            btnNovo.Animated = true;
            btnNovo.BorderRadius = 7;
            btnNovo.Cursor = Cursors.Hand;
            btnNovo.CustomizableEdges = edges5;
            btnNovo.FillColor =
                Color.FromArgb(251, 234, 214);
            btnNovo.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            btnNovo.ForeColor =
                Color.FromArgb(50, 60, 75);
            btnNovo.HoverState.FillColor =
                Color.FromArgb(240, 220, 198);
            btnNovo.Location = new Point(286, 10);
            btnNovo.Name = "btnNovo";
            btnNovo.ShadowDecoration.CustomizableEdges = edges6;
            btnNovo.Size = new Size(80, 28);
            btnNovo.TabIndex = 1;
            btnNovo.Text = "➕ Novo";
            btnNovo.Click += btnNovo_Click;

            // lblProjeto
            lblProjeto.AutoSize = true;
            lblProjeto.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblProjeto.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblProjeto.Location = new Point(17, 48);
            lblProjeto.Name = "lblProjeto";
            lblProjeto.Size = new Size(104, 15);
            lblProjeto.TabIndex = 2;
            lblProjeto.Text = "Projeto de Origem:";

            // cmbProjeto
            cmbProjeto.Animated = true;
            cmbProjeto.BackColor = Color.Transparent;
            cmbProjeto.BorderRadius = 7;
            cmbProjeto.CustomizableEdges = edges7;
            cmbProjeto.DrawMode = DrawMode.OwnerDrawFixed;
            cmbProjeto.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cmbProjeto.FillColor =
                Color.FromArgb(251, 234, 214);
            cmbProjeto.FocusedColor =
                Color.FromArgb(107, 117, 86);
            cmbProjeto.FocusedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            cmbProjeto.Font =
                new Font("Segoe UI", 9F);
            cmbProjeto.ForeColor =
                Color.FromArgb(68, 88, 112);
            cmbProjeto.ItemHeight = 22;
            cmbProjeto.Location = new Point(17, 65);
            cmbProjeto.Name = "cmbProjeto";
            cmbProjeto.ShadowDecoration.CustomizableEdges = edges8;
            cmbProjeto.Size = new Size(349, 28);
            cmbProjeto.TabIndex = 3;
            cmbProjeto.SelectedIndexChanged +=
                cmbProjeto_SelectedIndexChanged;

            // lblFotoProjeto
            lblFotoProjeto.AutoSize = true;
            lblFotoProjeto.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblFotoProjeto.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblFotoProjeto.Location = new Point(17, 99);
            lblFotoProjeto.Name = "lblFotoProjeto";
            lblFotoProjeto.Size = new Size(140, 15);
            lblFotoProjeto.TabIndex = 4;
            lblFotoProjeto.Text = "Fotos para Publicar:";

            // clbFotosProjeto
            clbFotosProjeto.BackColor =
                Color.FromArgb(251, 234, 214);
            clbFotosProjeto.BorderStyle =
                BorderStyle.FixedSingle;
            clbFotosProjeto.CheckOnClick = true;
            clbFotosProjeto.Enabled = false;
            clbFotosProjeto.Font =
                new Font("Segoe UI", 9F);
            clbFotosProjeto.ForeColor =
                Color.FromArgb(68, 88, 112);
            clbFotosProjeto.FormattingEnabled = true;
            clbFotosProjeto.IntegralHeight = false;
            clbFotosProjeto.Location = new Point(17, 116);
            clbFotosProjeto.Name = "clbFotosProjeto";
            clbFotosProjeto.Size = new Size(349, 68);
            clbFotosProjeto.TabIndex = 5;
            clbFotosProjeto.SelectedIndexChanged +=
                clbFotosProjeto_SelectedIndexChanged;

            // lblAmbiente
            lblAmbiente.AutoSize = true;
            lblAmbiente.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblAmbiente.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblAmbiente.Location = new Point(17, 190);
            lblAmbiente.Name = "lblAmbiente";
            lblAmbiente.Size = new Size(130, 15);
            lblAmbiente.TabIndex = 6;
            lblAmbiente.Text = "Ambiente Relacionado:";

            // cmbAmbiente
            cmbAmbiente.Animated = true;
            cmbAmbiente.BackColor = Color.Transparent;
            cmbAmbiente.BorderRadius = 7;
            cmbAmbiente.CustomizableEdges = edges9;
            cmbAmbiente.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAmbiente.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cmbAmbiente.FillColor =
                Color.FromArgb(251, 234, 214);
            cmbAmbiente.FocusedColor =
                Color.FromArgb(107, 117, 86);
            cmbAmbiente.FocusedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            cmbAmbiente.Font =
                new Font("Segoe UI", 9F);
            cmbAmbiente.ForeColor =
                Color.FromArgb(68, 88, 112);
            cmbAmbiente.ItemHeight = 22;
            cmbAmbiente.Location = new Point(17, 207);
            cmbAmbiente.Name = "cmbAmbiente";
            cmbAmbiente.ShadowDecoration.CustomizableEdges = edges10;
            cmbAmbiente.Size = new Size(349, 28);
            cmbAmbiente.TabIndex = 7;

            // lblNomeProjeto
            lblNomeProjeto.AutoSize = true;
            lblNomeProjeto.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblNomeProjeto.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblNomeProjeto.Location = new Point(17, 241);
            lblNomeProjeto.Name = "lblNomeProjeto";
            lblNomeProjeto.Size = new Size(144, 15);
            lblNomeProjeto.TabIndex = 8;
            lblNomeProjeto.Text = "Título Público do Projeto:";

            // txtNomeProjeto
            txtNomeProjeto.Animated = true;
            txtNomeProjeto.BorderRadius = 7;
            txtNomeProjeto.Cursor = Cursors.IBeam;
            txtNomeProjeto.CustomizableEdges = edges11;
            txtNomeProjeto.DefaultText = "";
            txtNomeProjeto.FillColor =
                Color.FromArgb(251, 234, 214);
            txtNomeProjeto.FocusedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            txtNomeProjeto.Font =
                new Font("Segoe UI", 9F);
            txtNomeProjeto.ForeColor =
                Color.FromArgb(50, 60, 75);
            txtNomeProjeto.Location = new Point(17, 258);
            txtNomeProjeto.Name = "txtNomeProjeto";
            txtNomeProjeto.PlaceholderText =
                "Ex: Living Moderno Integrado";
            txtNomeProjeto.SelectedText = "";
            txtNomeProjeto.ShadowDecoration.CustomizableEdges = edges12;
            txtNomeProjeto.Size = new Size(349, 28);
            txtNomeProjeto.TabIndex = 9;

            // lblDescricao
            lblDescricao.AutoSize = true;
            lblDescricao.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblDescricao.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblDescricao.Location = new Point(17, 292);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(59, 15);
            lblDescricao.TabIndex = 10;
            lblDescricao.Text = "Descrição:";

            // txtDescricao
            txtDescricao.Animated = true;
            txtDescricao.BorderRadius = 7;
            txtDescricao.Cursor = Cursors.IBeam;
            txtDescricao.CustomizableEdges = edges13;
            txtDescricao.DefaultText = "";
            txtDescricao.FillColor =
                Color.FromArgb(251, 234, 214);
            txtDescricao.FocusedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            txtDescricao.Font =
                new Font("Segoe UI", 9F);
            txtDescricao.ForeColor =
                Color.FromArgb(50, 60, 75);
            txtDescricao.Location = new Point(17, 309);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.PlaceholderText =
                "Descrição que será exibida no site...";
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.SelectedText = "";
            txtDescricao.ShadowDecoration.CustomizableEdges = edges14;
            txtDescricao.Size = new Size(349, 45);
            txtDescricao.TabIndex = 11;
            txtDescricao.TextChanged +=
                txtDescricao_TextChanged_1;

            // lblImagem
            lblImagem.AutoSize = true;
            lblImagem.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblImagem.ForeColor =
                Color.FromArgb(60, 70, 85);
            lblImagem.Location = new Point(17, 360);
            lblImagem.Name = "lblImagem";
            lblImagem.Size = new Size(136, 15);
            lblImagem.TabIndex = 12;
            lblImagem.Text = "Prévia da Foto Selecionada:";

            // picImagem
            picImagem.BackColor = Color.Transparent;
            picImagem.BorderRadius = 7;
            picImagem.CustomizableEdges = edges15;
            picImagem.FillColor =
                Color.FromArgb(251, 234, 214);
            picImagem.ImageRotate = 0F;
            picImagem.Location = new Point(17, 378);
            picImagem.Name = "picImagem";
            picImagem.ShadowDecoration.CustomizableEdges = edges16;
            picImagem.Size = new Size(349, 70);
            picImagem.SizeMode = PictureBoxSizeMode.Zoom;
            picImagem.TabIndex = 13;
            picImagem.TabStop = false;
            picImagem.Click += picImagem_Click;

            // swAtivo
            swAtivo.Animated = true;
            swAtivo.Checked = true;
            swAtivo.CheckedState.BorderColor =
                Color.FromArgb(107, 117, 86);
            swAtivo.CheckedState.FillColor =
                Color.FromArgb(107, 117, 86);
            swAtivo.CheckedState.InnerBorderColor =
                Color.White;
            swAtivo.CheckedState.InnerColor =
                Color.White;
            swAtivo.Cursor = Cursors.Hand;
            swAtivo.Location = new Point(17, 458);
            swAtivo.Name = "swAtivo";
            swAtivo.Size = new Size(42, 20);
            swAtivo.TabIndex = 14;
            swAtivo.UncheckedState.BorderColor =
                Color.FromArgb(180, 190, 200);
            swAtivo.UncheckedState.FillColor =
                Color.FromArgb(180, 190, 200);
            swAtivo.UncheckedState.InnerBorderColor =
                Color.White;
            swAtivo.UncheckedState.InnerColor =
                Color.White;

            // lblStatusAtivo
            lblStatusAtivo.AutoSize = true;
            lblStatusAtivo.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusAtivo.ForeColor =
                Color.FromArgb(40, 50, 65);
            lblStatusAtivo.Location = new Point(65, 460);
            lblStatusAtivo.Name = "lblStatusAtivo";
            lblStatusAtivo.Size = new Size(209, 15);
            lblStatusAtivo.TabIndex = 15;
            lblStatusAtivo.Text =
                "Exibir no Portfólio Público (Ativo)";

            // btnSalvar
            btnSalvar.Animated = true;
            btnSalvar.BorderRadius = 7;
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.FillColor =
                Color.FromArgb(107, 117, 86);
            btnSalvar.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.HoverState.FillColor =
                Color.FromArgb(90, 100, 72);
            btnSalvar.Location = new Point(17, 487);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(349, 30);
            btnSalvar.TabIndex = 16;
            btnSalvar.Text = "💾 Salvar no Portfólio";
            btnSalvar.Click += btnSalvar_Click;

            // btnDesativar
            btnDesativar.Animated = true;
            btnDesativar.BorderRadius = 7;
            btnDesativar.Cursor = Cursors.Hand;
            btnDesativar.Enabled = false;
            btnDesativar.FillColor =
                Color.FromArgb(254, 235, 237);
            btnDesativar.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            btnDesativar.ForeColor =
                Color.FromArgb(190, 40, 50);
            btnDesativar.HoverState.FillColor =
                Color.FromArgb(250, 220, 223);
            btnDesativar.Location = new Point(17, 523);
            btnDesativar.Name = "btnDesativar";
            btnDesativar.Size = new Size(170, 28);
            btnDesativar.TabIndex = 17;
            btnDesativar.Text = "🗑️ Desativar do Site";
            btnDesativar.Click += btnDesativar_Click;

            // btnExcluir
            btnExcluir.Animated = true;
            btnExcluir.BorderRadius = 7;
            btnExcluir.Cursor = Cursors.Hand;
            btnExcluir.Enabled = false;
            btnExcluir.FillColor =
                Color.FromArgb(235, 90, 95);
            btnExcluir.Font =
                new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.HoverState.FillColor =
                Color.FromArgb(190, 40, 50);
            btnExcluir.Location = new Point(196, 523);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(170, 28);
            btnExcluir.TabIndex = 18;
            btnExcluir.Text = "❌ Excluir Definitivamente";
            btnExcluir.Click += btnExcluir_Click;

            // PortfolioControl
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 245, 249);
            Controls.Add(pnlPrincipal);
            Name = "PortfolioControl";
            Padding = new Padding(15);
            Size = new Size(991, 603);

            pnlPrincipal.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)dgvPortfolio)
                .EndInit();

            pnlBusca.ResumeLayout(false);
            pnlBusca.PerformLayout();

            pnlCardFormulario.ResumeLayout(false);
            pnlCardFormulario.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)picImagem)
                .EndInit();

            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlPrincipal;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        internal Guna.UI2.WinForms.Guna2Panel pnlCardFormulario;

        private Label lblTituloCard;
        private Guna.UI2.WinForms.Guna2Button btnNovo;

        private Label lblProjeto;
        private Guna.UI2.WinForms.Guna2ComboBox cmbProjeto;

        private Label lblFotoProjeto;
        private CheckedListBox clbFotosProjeto;

        private Label lblAmbiente;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAmbiente;

        private Label lblNomeProjeto;
        private Guna.UI2.WinForms.Guna2TextBox txtNomeProjeto;

        private Label lblDescricao;
        private Guna.UI2.WinForms.Guna2TextBox txtDescricao;

        private Label lblImagem;
        private Guna.UI2.WinForms.Guna2PictureBox picImagem;

        private Guna.UI2.WinForms.Guna2ToggleSwitch swAtivo;
        private Label lblStatusAtivo;

        private Guna.UI2.WinForms.Guna2Button btnSalvar;
        private Guna.UI2.WinForms.Guna2Button btnDesativar;
        private Guna.UI2.WinForms.Guna2Button btnExcluir;

        private Panel pnlBusca;
        private Label lblBusca;
        private Guna.UI2.WinForms.Guna2TextBox txtBusca;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar;

        private Guna.UI2.WinForms.Guna2DataGridView dgvPortfolio;

        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNomeProjeto;
        private DataGridViewTextBoxColumn colProjetoOrigem;
        private DataGridViewTextBoxColumn colAmbiente;
        private DataGridViewTextBoxColumn colDescricao;
        private DataGridViewCheckBoxColumn colAtivo;
    }
}