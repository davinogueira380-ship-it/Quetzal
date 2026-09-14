namespace Quetzal.Desktop.UserControls
{
    partial class ProjetoCControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvProjetos = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmbientes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlBusca = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            this.txtBusca = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblBusca = new System.Windows.Forms.Label();
            this.pnlCardFormulario = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDesativar = new Guna.UI2.WinForms.Guna2Button();
            this.btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            this.btnNovo = new Guna.UI2.WinForms.Guna2Button();
            this.lblStatusAtivo = new System.Windows.Forms.Label();
            this.swAtivo = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.grpGaleriaFotos = new Guna.UI2.WinForms.Guna2Panel();
            this.btnRemoverFoto = new Guna.UI2.WinForms.Guna2Button();
            this.lstFotos = new System.Windows.Forms.ListBox();
            this.btnAdicionarFoto = new Guna.UI2.WinForms.Guna2Button();
            this.picPreviewFoto = new Guna.UI2.WinForms.Guna2PictureBox();
            this.cmbAmbienteFoto = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblAmbienteFoto = new System.Windows.Forms.Label();
            this.lblTituloGaleria = new System.Windows.Forms.Label();
            this.clbAmbientes = new System.Windows.Forms.CheckedListBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNomeProjeto = new System.Windows.Forms.Label();
            this.txtNomeProjeto = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTituloCard = new System.Windows.Forms.Label();
            this.pnlPrincipal.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjetos)).BeginInit();
            this.pnlBusca.SuspendLayout();
            this.pnlCardFormulario.SuspendLayout();
            this.grpGaleriaFotos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreviewFoto)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrincipal.Controls.Add(this.pnlGrid);
            this.pnlPrincipal.Controls.Add(this.pnlCardFormulario);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(15, 15);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(920, 625);
            this.pnlPrincipal.TabIndex = 0;

            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.BorderRadius = 12;
            this.pnlGrid.Controls.Add(this.dgvProjetos);
            this.pnlGrid.Controls.Add(this.pnlBusca);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(440, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(18);
            this.pnlGrid.Size = new System.Drawing.Size(480, 625);
            this.pnlGrid.TabIndex = 1;

            // 
            // dgvProjetos
            // 
            this.dgvProjetos.AllowUserToAddRows = false;
            this.dgvProjetos.AllowUserToDeleteRows = false;
            this.dgvProjetos.AllowUserToResizeRows = false;
            this.dgvProjetos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProjetos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjetos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProjetos.ColumnHeadersHeight = 40;
            this.dgvProjetos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNome,
            this.colCliente,
            this.colAmbientes,
            this.colAtivo});
            this.dgvProjetos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProjetos.Location = new System.Drawing.Point(18, 72);
            this.dgvProjetos.MultiSelect = false;
            this.dgvProjetos.Name = "dgvProjetos";
            this.dgvProjetos.ReadOnly = true;
            this.dgvProjetos.RowHeadersVisible = false;
            this.dgvProjetos.RowTemplate.Height = 35;
            this.dgvProjetos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjetos.Size = new System.Drawing.Size(444, 535);
            this.dgvProjetos.TabIndex = 1;
            this.dgvProjetos.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvProjetos.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvProjetos.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvProjetos.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvProjetos.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvProjetos.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvProjetos.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.dgvProjetos.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvProjetos.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProjetos.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvProjetos.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvProjetos.ThemeStyle.HeaderStyle.Height = 40;
          //  this.dgvProjetos.ThemeStyle.HeaderStyle.SelectionBackColor = System.Drawing.Color.Empty;
         //   this.dgvProjetos.ThemeStyle.HeaderStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvProjetos.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvProjetos.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProjetos.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvProjetos.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            this.dgvProjetos.ThemeStyle.RowsStyle.Height = 35;
            this.dgvProjetos.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvProjetos.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            this.dgvProjetos.SelectionChanged += new System.EventHandler(this.dgvProjetos_SelectionChanged);

            // 
            // colId
            // 
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;

            // 
            // colNome
            // 
            this.colNome.HeaderText = "Projeto";
            this.colNome.Name = "colNome";
            this.colNome.ReadOnly = true;

            // 
            // colCliente
            // 
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.Name = "colCliente";
            this.colCliente.ReadOnly = true;

            // 
            // colAmbientes
            // 
            this.colAmbientes.HeaderText = "Ambientes";
            this.colAmbientes.Name = "colAmbientes";
            this.colAmbientes.ReadOnly = true;

            // 
            // colAtivo
            // 
            this.colAtivo.HeaderText = "Ativo";
            this.colAtivo.Name = "colAtivo";
            this.colAtivo.ReadOnly = true;

            // 
            // pnlBusca
            // 
            this.pnlBusca.Controls.Add(this.btnAtualizar);
            this.pnlBusca.Controls.Add(this.txtBusca);
            this.pnlBusca.Controls.Add(this.lblBusca);
            this.pnlBusca.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusca.Location = new System.Drawing.Point(18, 18);
            this.pnlBusca.Name = "pnlBusca";
            this.pnlBusca.Size = new System.Drawing.Size(444, 54);
            this.pnlBusca.TabIndex = 0;

            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BorderRadius = 8;
            this.btnAtualizar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAtualizar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAtualizar.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnAtualizar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(320, 15);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(110, 35);
            this.btnAtualizar.TabIndex = 2;
            this.btnAtualizar.Text = "🔄 Atualizar";
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);

            // 
            // txtBusca
            // 
            this.txtBusca.BorderRadius = 8;
            this.txtBusca.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBusca.DefaultText = "";
            this.txtBusca.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.txtBusca.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.txtBusca.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtBusca.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtBusca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtBusca.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBusca.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtBusca.Location = new System.Drawing.Point(105, 15);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.PlaceholderText = "Digite o nome do projeto...";
            this.txtBusca.SelectedText = "";
            this.txtBusca.Size = new System.Drawing.Size(205, 35);
            this.txtBusca.TabIndex = 1;
            this.txtBusca.TextChanged += new System.EventHandler(this.txtBusca_TextChanged);

            // 
            // lblBusca
            // 
            this.lblBusca.AutoSize = true;
            this.lblBusca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBusca.Location = new System.Drawing.Point(0, 24);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(96, 15);
            this.lblBusca.TabIndex = 0;
            this.lblBusca.Text = "Buscar Projeto:";

            // 
            // pnlCardFormulario
            // 
            this.pnlCardFormulario.AutoScroll = true;
            this.pnlCardFormulario.BackColor = System.Drawing.Color.White;
            this.pnlCardFormulario.BorderRadius = 12;
            this.pnlCardFormulario.Controls.Add(this.btnDesativar);
            this.pnlCardFormulario.Controls.Add(this.btnSalvar);
            this.pnlCardFormulario.Controls.Add(this.btnNovo);
            this.pnlCardFormulario.Controls.Add(this.lblStatusAtivo);
            this.pnlCardFormulario.Controls.Add(this.swAtivo);
            this.pnlCardFormulario.Controls.Add(this.grpGaleriaFotos);
            this.pnlCardFormulario.Controls.Add(this.clbAmbientes);
            this.pnlCardFormulario.Controls.Add(this.lblDescricao);
            this.pnlCardFormulario.Controls.Add(this.txtDescricao);
            this.pnlCardFormulario.Controls.Add(this.lblNomeProjeto);
            this.pnlCardFormulario.Controls.Add(this.txtNomeProjeto);
            this.pnlCardFormulario.Controls.Add(this.lblCliente);
            this.pnlCardFormulario.Controls.Add(this.cmbCliente);
            this.pnlCardFormulario.Controls.Add(this.lblTituloCard);
            this.pnlCardFormulario.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCardFormulario.Location = new System.Drawing.Point(0, 0);
            this.pnlCardFormulario.Name = "pnlCardFormulario";
            this.pnlCardFormulario.Padding = new System.Windows.Forms.Padding(18);
            this.pnlCardFormulario.Size = new System.Drawing.Size(440, 625);
            this.pnlCardFormulario.TabIndex = 0;

            // 
            // btnDesativar
            // 
            this.btnDesativar.BorderRadius = 8;
            this.btnDesativar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDesativar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDesativar.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnDesativar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnDesativar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDesativar.ForeColor = System.Drawing.Color.White;
            this.btnDesativar.Location = new System.Drawing.Point(18, 584);
            this.btnDesativar.Name = "btnDesativar";
            this.btnDesativar.Size = new System.Drawing.Size(390, 38);
            this.btnDesativar.TabIndex = 13;
            this.btnDesativar.Text = "🗑️ Desativar Projeto";
            this.btnDesativar.Click += new System.EventHandler(this.btnDesativar_Click);

            // 
            // btnSalvar
            // 
            this.btnSalvar.BorderRadius = 8;
            this.btnSalvar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSalvar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSalvar.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnSalvar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(16, 534);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(390, 44);
            this.btnSalvar.TabIndex = 12;
            this.btnSalvar.Text = "💾 Salvar Projeto e Fotos";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // 
            // btnNovo
            // 
            this.btnNovo.BorderRadius = 8;
            this.btnNovo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNovo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNovo.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnNovo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnNovo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNovo.ForeColor = System.Drawing.Color.White;
            this.btnNovo.Location = new System.Drawing.Point(318, 15);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(90, 30);
            this.btnNovo.TabIndex = 11;
            this.btnNovo.Text = "➕ Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            // 
            // lblStatusAtivo
            // 
            this.lblStatusAtivo.AutoSize = true;
            this.lblStatusAtivo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusAtivo.Location = new System.Drawing.Point(55, 512);
            this.lblStatusAtivo.Name = "lblStatusAtivo";
            this.lblStatusAtivo.Size = new System.Drawing.Size(82, 15);
            this.lblStatusAtivo.TabIndex = 10;
            this.lblStatusAtivo.Text = "Projeto Ativo";

            // 
            // swAtivo
            // 
            this.swAtivo.Checked = true;
            this.swAtivo.CheckedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.swAtivo.CheckedState.FillColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.swAtivo.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swAtivo.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swAtivo.Location = new System.Drawing.Point(18, 507);
            this.swAtivo.Name = "swAtivo";
            this.swAtivo.Size = new System.Drawing.Size(35, 20);
            this.swAtivo.TabIndex = 9;
            this.swAtivo.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
            this.swAtivo.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
            this.swAtivo.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swAtivo.UncheckedState.InnerColor = System.Drawing.Color.White;

            // 
            // grpGaleriaFotos
            // 
            this.grpGaleriaFotos.BackColor = System.Drawing.Color.FromArgb(248, 251, 255);
            this.grpGaleriaFotos.BorderColor = System.Drawing.Color.FromArgb(225, 235, 248);
            this.grpGaleriaFotos.BorderRadius = 10;
            this.grpGaleriaFotos.BorderThickness = 1;
            this.grpGaleriaFotos.Controls.Add(this.btnRemoverFoto);
            this.grpGaleriaFotos.Controls.Add(this.lstFotos);
            this.grpGaleriaFotos.Controls.Add(this.btnAdicionarFoto);
            this.grpGaleriaFotos.Controls.Add(this.picPreviewFoto);
            this.grpGaleriaFotos.Controls.Add(this.cmbAmbienteFoto);
            this.grpGaleriaFotos.Controls.Add(this.lblAmbienteFoto);
            this.grpGaleriaFotos.Controls.Add(this.lblTituloGaleria);
            this.grpGaleriaFotos.Location = new System.Drawing.Point(18, 295);
            this.grpGaleriaFotos.Name = "grpGaleriaFotos";
            this.grpGaleriaFotos.Padding = new System.Windows.Forms.Padding(12);
            this.grpGaleriaFotos.Size = new System.Drawing.Size(390, 205);
            this.grpGaleriaFotos.TabIndex = 8;

            // 
            // btnRemoverFoto
            // 
            this.btnRemoverFoto.BorderRadius = 6;
            this.btnRemoverFoto.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoverFoto.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoverFoto.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnRemoverFoto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnRemoverFoto.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRemoverFoto.ForeColor = System.Drawing.Color.White;
            this.btnRemoverFoto.Location = new System.Drawing.Point(260, 165);
            this.btnRemoverFoto.Name = "btnRemoverFoto";
            this.btnRemoverFoto.Size = new System.Drawing.Size(110, 28);
            this.btnRemoverFoto.TabIndex = 6;
            this.btnRemoverFoto.Text = "Remover Foto";
            this.btnRemoverFoto.Click += new System.EventHandler(this.btnRemoverFoto_Click);

            // 
            // lstFotos
            // 
            this.lstFotos.FormattingEnabled = true;
            this.lstFotos.ItemHeight = 15;
            this.lstFotos.Location = new System.Drawing.Point(155, 72);
            this.lstFotos.Name = "lstFotos";
            this.lstFotos.Size = new System.Drawing.Size(215, 79);
            this.lstFotos.TabIndex = 5;
            this.lstFotos.SelectedIndexChanged += new System.EventHandler(this.lstFotos_SelectedIndexChanged);

            // 
            // btnAdicionarFoto
            // 
            this.btnAdicionarFoto.BorderRadius = 6;
            this.btnAdicionarFoto.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdicionarFoto.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdicionarFoto.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnAdicionarFoto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnAdicionarFoto.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAdicionarFoto.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarFoto.Location = new System.Drawing.Point(155, 165);
            this.btnAdicionarFoto.Name = "btnAdicionarFoto";
            this.btnAdicionarFoto.Size = new System.Drawing.Size(100, 28);
            this.btnAdicionarFoto.TabIndex = 4;
            this.btnAdicionarFoto.Text = "📁 Anexar Foto...";
            this.btnAdicionarFoto.Click += new System.EventHandler(this.btnAdicionarFoto_Click);

            // 
            // picPreviewFoto
            // 
            this.picPreviewFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreviewFoto.ImageRotate = 0F;
            this.picPreviewFoto.Location = new System.Drawing.Point(15, 72);
            this.picPreviewFoto.Name = "picPreviewFoto";
            this.picPreviewFoto.Size = new System.Drawing.Size(125, 121);
            this.picPreviewFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreviewFoto.TabIndex = 3;
            this.picPreviewFoto.TabStop = false;

            // 
            // cmbAmbienteFoto
            // 
            this.cmbAmbienteFoto.BackColor = System.Drawing.Color.Transparent;
            this.cmbAmbienteFoto.BorderRadius = 6;
            this.cmbAmbienteFoto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAmbienteFoto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAmbienteFoto.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbAmbienteFoto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbAmbienteFoto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbAmbienteFoto.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.cmbAmbienteFoto.ItemHeight = 30;
            this.cmbAmbienteFoto.Location = new System.Drawing.Point(155, 36);
            this.cmbAmbienteFoto.Name = "cmbAmbienteFoto";
            this.cmbAmbienteFoto.Size = new System.Drawing.Size(215, 36);
            this.cmbAmbienteFoto.TabIndex = 2;

            // 
            // lblAmbienteFoto
            // 
            this.lblAmbienteFoto.AutoSize = true;
            this.lblAmbienteFoto.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAmbienteFoto.Location = new System.Drawing.Point(15, 47);
            this.lblAmbienteFoto.Name = "lblAmbienteFoto";
            this.lblAmbienteFoto.Size = new System.Drawing.Size(128, 13);
            this.lblAmbienteFoto.TabIndex = 1;
            this.lblAmbienteFoto.Text = "Ambiente desta imagem:";

            // 
            // lblTituloGaleria
            // 
            this.lblTituloGaleria.AutoSize = true;
            this.lblTituloGaleria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloGaleria.Location = new System.Drawing.Point(15, 12);
            this.lblTituloGaleria.Name = "lblTituloGaleria";
            this.lblTituloGaleria.Size = new System.Drawing.Size(297, 15);
            this.lblTituloGaleria.TabIndex = 0;
            this.lblTituloGaleria.Text = "📸 Fotos por Ambiente (Alimentam a Área do Cliente)";

            // 
            // clbAmbientes
            // 
            this.clbAmbientes.CheckOnClick = true;
            this.clbAmbientes.FormattingEnabled = true;
            this.clbAmbientes.Location = new System.Drawing.Point(21, 252);
            this.clbAmbientes.MultiColumn = true;
            this.clbAmbientes.Name = "clbAmbientes";
            this.clbAmbientes.Size = new System.Drawing.Size(390, 20);
            this.clbAmbientes.TabIndex = 7;
            this.clbAmbientes.MinimumSize = new System.Drawing.Size(20, 20);
            this.clbAmbientes.MaximumSize = new System.Drawing.Size(400, 400);

            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescricao.Location = new System.Drawing.Point(18, 185);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(65, 15);
            this.lblDescricao.TabIndex = 6;
            this.lblDescricao.Text = "Descrição:";

            // 
            // txtDescricao
            // 
            this.txtDescricao.BorderRadius = 8;
            this.txtDescricao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescricao.DefaultText = "";
            this.txtDescricao.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.txtDescricao.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.txtDescricao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtDescricao.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtDescricao.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtDescricao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescricao.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtDescricao.Location = new System.Drawing.Point(18, 203);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.PlaceholderText = "Ex: Reforma e decoração completa com estilo contemporâneo...";
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.SelectedText = "";
            this.txtDescricao.Size = new System.Drawing.Size(390, 45);
            this.txtDescricao.TabIndex = 5;

            // 
            // lblNomeProjeto
            // 
            this.lblNomeProjeto.AutoSize = true;
            this.lblNomeProjeto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNomeProjeto.Location = new System.Drawing.Point(18, 128);
            this.lblNomeProjeto.Name = "lblNomeProjeto";
            this.lblNomeProjeto.Size = new System.Drawing.Size(99, 15);
            this.lblNomeProjeto.TabIndex = 4;
            this.lblNomeProjeto.Text = "Nome do Projeto:";

            // 
            // txtNomeProjeto
            // 
            this.txtNomeProjeto.BorderRadius = 8;
            this.txtNomeProjeto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNomeProjeto.DefaultText = "";
            this.txtNomeProjeto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            this.txtNomeProjeto.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            this.txtNomeProjeto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtNomeProjeto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            this.txtNomeProjeto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtNomeProjeto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNomeProjeto.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtNomeProjeto.Location = new System.Drawing.Point(18, 146);
            this.txtNomeProjeto.Name = "txtNomeProjeto";
            this.txtNomeProjeto.PlaceholderText = "Ex: Apartamento Jardins - Design e Decoração";
            this.txtNomeProjeto.SelectedText = "";
            this.txtNomeProjeto.Size = new System.Drawing.Size(390, 36);
            this.txtNomeProjeto.TabIndex = 3;

            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCliente.Location = new System.Drawing.Point(18, 64);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 2;
            this.lblCliente.Text = "Cliente:";

            // 
            // cmbCliente
            // 
            this.cmbCliente.BackColor = System.Drawing.Color.Transparent;
            this.cmbCliente.BorderRadius = 8;
            this.cmbCliente.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbCliente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCliente.ForeColor = System.Drawing.Color.FromArgb(68, 88, 112);
            this.cmbCliente.ItemHeight = 30;
            this.cmbCliente.Location = new System.Drawing.Point(18, 82);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(390, 36);
            this.cmbCliente.TabIndex = 1;

            // 
            // lblTituloCard
            // 
            this.lblTituloCard.AutoSize = true;
            this.lblTituloCard.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloCard.Location = new System.Drawing.Point(18, 18);
            this.lblTituloCard.Name = "lblTituloCard";
            this.lblTituloCard.Size = new System.Drawing.Size(147, 25);
            this.lblTituloCard.TabIndex = 0;
            this.lblTituloCard.Text = "Dados do Projeto";

            // 
            // ProjetoCControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            this.Controls.Add(this.pnlPrincipal);
            this.Name = "ProjetoCControl";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(950, 655);

            this.pnlPrincipal.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjetos)).EndInit();
            this.pnlBusca.ResumeLayout(false);
            this.pnlBusca.PerformLayout();
            this.pnlCardFormulario.ResumeLayout(false);
            this.pnlCardFormulario.PerformLayout();
            this.grpGaleriaFotos.ResumeLayout(false);
            this.grpGaleriaFotos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreviewFoto)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlPrincipal;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        private Guna.UI2.WinForms.Guna2DataGridView dgvProjetos;
        private Guna.UI2.WinForms.Guna2Panel pnlBusca;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar;
        private Guna.UI2.WinForms.Guna2TextBox txtBusca;
        private System.Windows.Forms.Label lblBusca;
        private Guna.UI2.WinForms.Guna2Panel pnlCardFormulario;
        private Guna.UI2.WinForms.Guna2Button btnDesativar;
        private Guna.UI2.WinForms.Guna2Button btnSalvar;
        private Guna.UI2.WinForms.Guna2Button btnNovo;
        private System.Windows.Forms.Label lblStatusAtivo;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swAtivo;
        private Guna.UI2.WinForms.Guna2Panel grpGaleriaFotos;
        private Guna.UI2.WinForms.Guna2Button btnRemoverFoto;
        private System.Windows.Forms.ListBox lstFotos;
        private Guna.UI2.WinForms.Guna2Button btnAdicionarFoto;
        private Guna.UI2.WinForms.Guna2PictureBox picPreviewFoto;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAmbienteFoto;
        private System.Windows.Forms.Label lblAmbienteFoto;
        private System.Windows.Forms.Label lblTituloGaleria;
        public System.Windows.Forms.CheckedListBox clbAmbientes;
        private System.Windows.Forms.Label lblDescricao;
        private Guna.UI2.WinForms.Guna2TextBox txtDescricao;
        private System.Windows.Forms.Label lblNomeProjeto;
        private Guna.UI2.WinForms.Guna2TextBox txtNomeProjeto;
        private System.Windows.Forms.Label lblCliente;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCliente;
        private System.Windows.Forms.Label lblTituloCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmbientes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAtivo;
    }
}