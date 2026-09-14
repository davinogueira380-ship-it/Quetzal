namespace Quetzal.Desktop.Formularios
{
    partial class FormClientes
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvClientes = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelefone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtivo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlBusca = new System.Windows.Forms.Panel();
            this.btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            this.txtBusca = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblBusca = new System.Windows.Forms.Label();
            this.pnlCardFormulario = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlAvisoRegra = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAvisoRegra = new System.Windows.Forms.Label();
            this.btnAlternarAtivacao = new Guna.UI2.WinForms.Guna2Button();
            this.btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            this.lblPerfilAtivo = new System.Windows.Forms.Label();
            this.swPerfilAtivo = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.txtTelefone = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtNomeCompleto = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNomeCompleto = new System.Windows.Forms.Label();
            this.lblTituloCard = new System.Windows.Forms.Label();
            this.pnlPrincipal.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.pnlBusca.SuspendLayout();
            this.pnlCardFormulario.SuspendLayout();
            this.pnlAvisoRegra.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrincipal.Controls.Add(this.pnlGrid);
            this.pnlPrincipal.Controls.Add(this.pnlCardFormulario);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(20, 20);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(910, 615);
            this.pnlPrincipal.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlGrid.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.pnlGrid.BorderRadius = 12;
            this.pnlGrid.BorderThickness = 1;
            this.pnlGrid.Controls.Add(this.dgvClientes);
            this.pnlGrid.Controls.Add(this.pnlBusca);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.FillColor = System.Drawing.Color.White;
            this.pnlGrid.Location = new System.Drawing.Point(365, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(18);
            this.pnlGrid.Size = new System.Drawing.Size(545, 615);
            this.pnlGrid.TabIndex = 1;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.dgvClientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(85)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(85)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvClientes.ColumnHeadersHeight = 40;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNome,
            this.colEmail,
            this.colTelefone,
            this.colAtivo});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(55)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(196)))), ((int)(((byte)(203)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClientes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.EnableHeadersVisualStyles = false;
            this.dgvClientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(232)))), ((int)(((byte)(222)))));
            this.dgvClientes.Location = new System.Drawing.Point(18, 73);
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.RowTemplate.Height = 35;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(509, 524);
            this.dgvClientes.TabIndex = 1;
            this.dgvClientes.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.dgvClientes.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvClientes.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvClientes.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvClientes.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvClientes.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvClientes.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(232)))), ((int)(((byte)(222)))));
            this.dgvClientes.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            this.dgvClientes.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvClientes.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dgvClientes.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(85)))), ((int)(((byte)(70)))));
            this.dgvClientes.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvClientes.ThemeStyle.ReadOnly = true;
            this.dgvClientes.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvClientes.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvClientes.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dgvClientes.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(55)))), ((int)(((byte)(50)))));
            this.dgvClientes.ThemeStyle.RowsStyle.Height = 35;
            this.dgvClientes.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(196)))), ((int)(((byte)(203)))));
            this.dgvClientes.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.dgvClientes.SelectionChanged += new System.EventHandler(this.dgvClientes_SelectionChanged);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.FillWeight = 25F;
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colNome
            // 
            this.colNome.DataPropertyName = "NomeCompleto";
            this.colNome.FillWeight = 70F;
            this.colNome.HeaderText = "Nome do Cliente";
            this.colNome.Name = "colNome";
            this.colNome.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.FillWeight = 65F;
            this.colEmail.HeaderText = "E-mail";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colTelefone
            // 
            this.colTelefone.DataPropertyName = "Telefone";
            this.colTelefone.FillWeight = 40F;
            this.colTelefone.HeaderText = "Telefone";
            this.colTelefone.Name = "colTelefone";
            this.colTelefone.ReadOnly = true;
            // 
            // colAtivo
            // 
            this.colAtivo.DataPropertyName = "Ativo";
            this.colAtivo.FillWeight = 25F;
            this.colAtivo.HeaderText = "Perfil Ativo";
            this.colAtivo.Name = "colAtivo";
            this.colAtivo.ReadOnly = true;
            // 
            // pnlBusca
            // 
            this.pnlBusca.BackColor = System.Drawing.Color.Transparent;
            this.pnlBusca.Controls.Add(this.btnAtualizar);
            this.pnlBusca.Controls.Add(this.txtBusca);
            this.pnlBusca.Controls.Add(this.lblBusca);
            this.pnlBusca.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusca.Location = new System.Drawing.Point(18, 18);
            this.pnlBusca.Name = "pnlBusca";
            this.pnlBusca.Size = new System.Drawing.Size(509, 55);
            this.pnlBusca.TabIndex = 0;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtualizar.Animated = true;
            this.btnAtualizar.BorderRadius = 8;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAtualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(70)))));
            this.btnAtualizar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.btnAtualizar.Location = new System.Drawing.Point(404, 10);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(105, 34);
            this.btnAtualizar.TabIndex = 2;
            this.btnAtualizar.Text = "🔄 Atualizar";
            //this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // txtBusca
            // 
            this.txtBusca.Animated = true;
            this.txtBusca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.txtBusca.BorderRadius = 8;
            this.txtBusca.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBusca.DefaultText = "";
            this.txtBusca.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBusca.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBusca.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBusca.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBusca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.txtBusca.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBusca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(45)))));
            this.txtBusca.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(125)))), ((int)(((byte)(135)))));
            this.txtBusca.Location = new System.Drawing.Point(110, 10);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.PasswordChar = '\0';
            this.txtBusca.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(155)))), ((int)(((byte)(145)))));
            this.txtBusca.PlaceholderText = "Filtrar por nome ou e-mail...";
            this.txtBusca.SelectedText = "";
            this.txtBusca.Size = new System.Drawing.Size(270, 34);
            this.txtBusca.TabIndex = 1;
          //  this.txtBusca.TextChanged += new System.EventHandler(this.txtBusca_TextChanged);
            // 
            // lblBusca
            // 
            this.lblBusca.AutoSize = true;
            this.lblBusca.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBusca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.lblBusca.Location = new System.Drawing.Point(0, 18);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(94, 17);
            this.lblBusca.TabIndex = 0;
            this.lblBusca.Text = "Buscar Cliente:";
            // 
            // pnlCardFormulario
            // 
            this.pnlCardFormulario.BackColor = System.Drawing.Color.Transparent;
            this.pnlCardFormulario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.pnlCardFormulario.BorderRadius = 12;
            this.pnlCardFormulario.BorderThickness = 1;
            this.pnlCardFormulario.Controls.Add(this.pnlAvisoRegra);
            this.pnlCardFormulario.Controls.Add(this.btnAlternarAtivacao);
            this.pnlCardFormulario.Controls.Add(this.btnSalvar);
            this.pnlCardFormulario.Controls.Add(this.lblPerfilAtivo);
            this.pnlCardFormulario.Controls.Add(this.swPerfilAtivo);
            this.pnlCardFormulario.Controls.Add(this.txtTelefone);
            this.pnlCardFormulario.Controls.Add(this.lblTelefone);
            this.pnlCardFormulario.Controls.Add(this.txtEmail);
            this.pnlCardFormulario.Controls.Add(this.lblEmail);
            this.pnlCardFormulario.Controls.Add(this.txtNomeCompleto);
            this.pnlCardFormulario.Controls.Add(this.lblNomeCompleto);
            this.pnlCardFormulario.Controls.Add(this.lblTituloCard);
            this.pnlCardFormulario.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCardFormulario.FillColor = System.Drawing.Color.White;
            this.pnlCardFormulario.Location = new System.Drawing.Point(0, 0);
            this.pnlCardFormulario.Name = "pnlCardFormulario";
            this.pnlCardFormulario.Padding = new System.Windows.Forms.Padding(20);
            this.pnlCardFormulario.Size = new System.Drawing.Size(345, 615);
            this.pnlCardFormulario.TabIndex = 0;
            // 
            // pnlAvisoRegra
            // 
            this.pnlAvisoRegra.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.pnlAvisoRegra.BorderRadius = 8;
            this.pnlAvisoRegra.BorderThickness = 1;
            this.pnlAvisoRegra.Controls.Add(this.lblAvisoRegra);
            this.pnlAvisoRegra.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            this.pnlAvisoRegra.Location = new System.Drawing.Point(20, 260);
            this.pnlAvisoRegra.Name = "pnlAvisoRegra";
            this.pnlAvisoRegra.Padding = new System.Windows.Forms.Padding(10);
            this.pnlAvisoRegra.Size = new System.Drawing.Size(305, 70);
            this.pnlAvisoRegra.TabIndex = 7;
            // 
            // lblAvisoRegra
            // 
            this.lblAvisoRegra.BackColor = System.Drawing.Color.Transparent;
            this.lblAvisoRegra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvisoRegra.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAvisoRegra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.lblAvisoRegra.Location = new System.Drawing.Point(10, 10);
            this.lblAvisoRegra.Name = "lblAvisoRegra";
            this.lblAvisoRegra.Size = new System.Drawing.Size(285, 50);
            this.lblAvisoRegra.TabIndex = 0;
            this.lblAvisoRegra.Text = "ℹ️ Regra de Acesso: O cliente só consegue visualizar as fotos dos seus projetos n" +
    "a Área do Cliente se este perfil estiver marcado como ATIVO.";
            // 
            // btnAlternarAtivacao
            // 
            this.btnAlternarAtivacao.Animated = true;
            this.btnAlternarAtivacao.BorderRadius = 8;
            this.btnAlternarAtivacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlternarAtivacao.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(196)))), ((int)(((byte)(203)))));
            this.btnAlternarAtivacao.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAlternarAtivacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.btnAlternarAtivacao.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.btnAlternarAtivacao.Location = new System.Drawing.Point(20, 455);
            this.btnAlternarAtivacao.Name = "btnAlternarAtivacao";
            this.btnAlternarAtivacao.Size = new System.Drawing.Size(305, 40);
            this.btnAlternarAtivacao.TabIndex = 9;
            this.btnAlternarAtivacao.Text = "⚡ Alternar Ativação do Perfil";
            this.btnAlternarAtivacao.Click += new System.EventHandler(this.btnAlternarAtivacao_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Animated = true;
            this.btnSalvar.BorderRadius = 8;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(101)))), ((int)(((byte)(74)))));
            this.btnSalvar.Location = new System.Drawing.Point(20, 400);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(305, 44);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "💾 Salvar Alterações do Cliente";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblPerfilAtivo
            // 
            this.lblPerfilAtivo.AutoSize = true;
            this.lblPerfilAtivo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPerfilAtivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.lblPerfilAtivo.Location = new System.Drawing.Point(75, 350);
            this.lblPerfilAtivo.Name = "lblPerfilAtivo";
            this.lblPerfilAtivo.Size = new System.Drawing.Size(235, 17);
            this.lblPerfilAtivo.TabIndex = 7;
            this.lblPerfilAtivo.Text = "Perfil Ativo (Permite Acesso às Fotos)";
            // 
            // swPerfilAtivo
            // 
            this.swPerfilAtivo.Animated = true;
            this.swPerfilAtivo.Checked = true;
            this.swPerfilAtivo.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.swPerfilAtivo.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.swPerfilAtivo.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swPerfilAtivo.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swPerfilAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swPerfilAtivo.Location = new System.Drawing.Point(20, 348);
            this.swPerfilAtivo.Name = "swPerfilAtivo";
            this.swPerfilAtivo.Size = new System.Drawing.Size(45, 22);
            this.swPerfilAtivo.TabIndex = 6;
            this.swPerfilAtivo.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(218)))), ((int)(((byte)(208)))));
            this.swPerfilAtivo.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(218)))), ((int)(((byte)(208)))));
            this.swPerfilAtivo.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swPerfilAtivo.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Animated = true;
            this.txtTelefone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.txtTelefone.BorderRadius = 8;
            this.txtTelefone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTelefone.DefaultText = "";
            this.txtTelefone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTelefone.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTelefone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTelefone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTelefone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.txtTelefone.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTelefone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(45)))));
            this.txtTelefone.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(125)))), ((int)(((byte)(135)))));
            this.txtTelefone.Location = new System.Drawing.Point(20, 215);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.PasswordChar = '\0';
            this.txtTelefone.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(155)))), ((int)(((byte)(145)))));
            this.txtTelefone.PlaceholderText = "(11) 98765-4321";
            this.txtTelefone.SelectedText = "";
            this.txtTelefone.Size = new System.Drawing.Size(305, 36);
            this.txtTelefone.TabIndex = 5;
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTelefone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.lblTelefone.Location = new System.Drawing.Point(20, 192);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(61, 17);
            this.lblTelefone.TabIndex = 4;
            this.lblTelefone.Text = "Telefone:";
            // 
            // txtEmail
            // 
            this.txtEmail.Animated = true;
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(45)))));
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(125)))), ((int)(((byte)(135)))));
            this.txtEmail.Location = new System.Drawing.Point(20, 150);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(155)))), ((int)(((byte)(145)))));
            this.txtEmail.PlaceholderText = "cliente@exemplo.com";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(305, 36);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.lblEmail.Location = new System.Drawing.Point(20, 127);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(48, 17);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "E-mail:";
            // 
            // txtNomeCompleto
            // 
            this.txtNomeCompleto.Animated = true;
            this.txtNomeCompleto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(188)))), ((int)(((byte)(169)))));
            this.txtNomeCompleto.BorderRadius = 8;
            this.txtNomeCompleto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNomeCompleto.DefaultText = "";
            this.txtNomeCompleto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNomeCompleto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNomeCompleto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNomeCompleto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNomeCompleto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.txtNomeCompleto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNomeCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(45)))));
            this.txtNomeCompleto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(125)))), ((int)(((byte)(135)))));
            this.txtNomeCompleto.Location = new System.Drawing.Point(20, 85);
            this.txtNomeCompleto.Name = "txtNomeCompleto";
            this.txtNomeCompleto.PasswordChar = '\0';
            this.txtNomeCompleto.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(155)))), ((int)(((byte)(145)))));
            this.txtNomeCompleto.PlaceholderText = "Nome do cliente...";
            this.txtNomeCompleto.SelectedText = "";
            this.txtNomeCompleto.Size = new System.Drawing.Size(305, 36);
            this.txtNomeCompleto.TabIndex = 1;
            // 
            // lblNomeCompleto
            // 
            this.lblNomeCompleto.AutoSize = true;
            this.lblNomeCompleto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNomeCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            this.lblNomeCompleto.Location = new System.Drawing.Point(20, 62);
            this.lblNomeCompleto.Name = "lblNomeCompleto";
            this.lblNomeCompleto.Size = new System.Drawing.Size(113, 17);
            this.lblNomeCompleto.TabIndex = 0;
            this.lblNomeCompleto.Text = "Nome Completo:";
            // 
            // lblTituloCard
            // 
            this.lblTituloCard.AutoSize = true;
            this.lblTituloCard.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTituloCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(117)))), ((int)(((byte)(86)))));
            this.lblTituloCard.Location = new System.Drawing.Point(18, 20);
            this.lblTituloCard.Name = "lblTituloCard";
            this.lblTituloCard.Size = new System.Drawing.Size(155, 25);
            this.lblTituloCard.TabIndex = 0;
            this.lblTituloCard.Text = "Dados do Cliente";
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(950, 655);
            this.Controls.Add(this.pnlPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormClientes";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "FormClientes";
            this.Load += new System.EventHandler(this.FormClientes_Load);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.pnlBusca.ResumeLayout(false);
            this.pnlBusca.PerformLayout();
            this.pnlCardFormulario.ResumeLayout(false);
            this.pnlCardFormulario.PerformLayout();
            this.pnlAvisoRegra.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlPrincipal;
        private Guna.UI2.WinForms.Guna2Panel pnlCardFormulario;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        private System.Windows.Forms.Label lblTituloCard;
        private System.Windows.Forms.Label lblNomeCompleto;
        private Guna.UI2.WinForms.Guna2TextBox txtNomeCompleto;
        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private Guna.UI2.WinForms.Guna2TextBox txtTelefone;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swPerfilAtivo;
        private System.Windows.Forms.Label lblPerfilAtivo;
        private Guna.UI2.WinForms.Guna2Button btnSalvar;
        private Guna.UI2.WinForms.Guna2Button btnAlternarAtivacao;
        private Guna.UI2.WinForms.Guna2Panel pnlAvisoRegra;
        private System.Windows.Forms.Label lblAvisoRegra;
        private System.Windows.Forms.Panel pnlBusca;
        private System.Windows.Forms.Label lblBusca;
        private Guna.UI2.WinForms.Guna2TextBox txtBusca;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar;
        private Guna.UI2.WinForms.Guna2DataGridView dgvClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelefone;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAtivo;
    }
}
