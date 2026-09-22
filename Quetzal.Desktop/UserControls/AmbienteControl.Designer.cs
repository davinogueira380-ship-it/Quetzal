using Quetzal.Desktop.ApiClientes;
using System;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Guna.UI2.WinForms.Suite.Descriptions;



        namespace Quetzal.Desktop.UserControls
    {
    partial class AmbientesControl : System.Windows.Forms.UserControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            dgvAmbientes = new Guna.UI2.WinForms.Guna2DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNome = new DataGridViewTextBoxColumn();
            colDescricao = new DataGridViewTextBoxColumn();
            colAtivo = new DataGridViewCheckBoxColumn();
            pnlBusca = new Panel();
            btnAtualizar = new Guna.UI2.WinForms.Guna2Button();
            txtBusca = new Guna.UI2.WinForms.Guna2TextBox();
            lblBusca = new Label();
            pnlCardFormulario = new Guna.UI2.WinForms.Guna2Panel();
            lblStatusAtivo = new Label();
            swAtivo = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            btnDesativar = new Guna.UI2.WinForms.Guna2Button();
            btnSalvar = new Guna.UI2.WinForms.Guna2Button();
            btnNovo = new Guna.UI2.WinForms.Guna2Button();
            txtDescricao = new Guna.UI2.WinForms.Guna2TextBox();
            lblDescricao = new Label();
            txtNome = new Guna.UI2.WinForms.Guna2TextBox();
            lblNome = new Label();
            lblTituloCard = new Label();
            btnExcluir = new Guna.UI2.WinForms.Guna2Button();
            pnlPrincipal.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAmbientes).BeginInit();
            pnlBusca.SuspendLayout();
            pnlCardFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrincipal
            // 
            pnlPrincipal.BackColor = Color.Transparent;
            pnlPrincipal.Controls.Add(pnlGrid);
            pnlPrincipal.Controls.Add(pnlCardFormulario);
            pnlPrincipal.CustomizableEdges = customizableEdges21;
            pnlPrincipal.Dock = DockStyle.Fill;
            pnlPrincipal.Location = new Point(20, 20);
            pnlPrincipal.Name = "pnlPrincipal";
            pnlPrincipal.ShadowDecoration.CustomizableEdges = customizableEdges22;
            pnlPrincipal.Size = new Size(929, 444);
            pnlPrincipal.TabIndex = 0;
            // 
            // pnlGrid
            // 
            pnlGrid.BackColor = Color.Transparent;
            pnlGrid.BorderColor = Color.FromArgb(229, 188, 169);
            pnlGrid.BorderRadius = 12;
            pnlGrid.BorderThickness = 1;
            pnlGrid.Controls.Add(dgvAmbientes);
            pnlGrid.Controls.Add(pnlBusca);
            pnlGrid.CustomizableEdges = customizableEdges5;
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.FillColor = Color.White;
            pnlGrid.Location = new Point(345, 0);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(18);
            pnlGrid.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlGrid.Size = new Size(584, 444);
            pnlGrid.TabIndex = 1;
            // 
            // dgvAmbientes
            // 
            dgvAmbientes.AllowUserToAddRows = false;
            dgvAmbientes.AllowUserToDeleteRows = false;
            dgvAmbientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(254, 250, 245);
            dgvAmbientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(251, 234, 214);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 85, 70);
            dataGridViewCellStyle2.Padding = new Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(251, 234, 214);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(80, 85, 70);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAmbientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAmbientes.ColumnHeadersHeight = 40;
            dgvAmbientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvAmbientes.Columns.AddRange(new DataGridViewColumn[] { colId, colNome, colDescricao, colAtivo });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(60, 55, 50);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(240, 196, 203);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(60, 50, 50);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvAmbientes.DefaultCellStyle = dataGridViewCellStyle3;
            dgvAmbientes.Dock = DockStyle.Fill;
            dgvAmbientes.GridColor = Color.FromArgb(242, 232, 222);
            dgvAmbientes.Location = new Point(18, 73);
            dgvAmbientes.MultiSelect = false;
            dgvAmbientes.Name = "dgvAmbientes";
            dgvAmbientes.ReadOnly = true;
            dgvAmbientes.RowHeadersVisible = false;
            dgvAmbientes.RowTemplate.Height = 35;
            dgvAmbientes.Size = new Size(548, 353);
            dgvAmbientes.TabIndex = 1;
            dgvAmbientes.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(254, 250, 245);
            dgvAmbientes.ThemeStyle.GridColor = Color.FromArgb(242, 232, 222);
            dgvAmbientes.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(251, 234, 214);
            dgvAmbientes.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dgvAmbientes.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(80, 85, 70);
            dgvAmbientes.ThemeStyle.HeaderStyle.Height = 40;
            dgvAmbientes.ThemeStyle.ReadOnly = true;
            dgvAmbientes.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9.75F);
            dgvAmbientes.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(60, 55, 50);
            dgvAmbientes.ThemeStyle.RowsStyle.Height = 35;
            dgvAmbientes.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 196, 203);
            dgvAmbientes.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(60, 50, 50);
            dgvAmbientes.SelectionChanged += dgvAmbientes_SelectionChanged;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 25F;
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNome
            // 
            colNome.DataPropertyName = "Nome";
            colNome.FillWeight = 60F;
            colNome.HeaderText = "Ambiente";
            colNome.Name = "colNome";
            colNome.ReadOnly = true;
            // 
            // colDescricao
            // 
            colDescricao.DataPropertyName = "Descricao";
            colDescricao.FillWeight = 80F;
            colDescricao.HeaderText = "Descrição";
            colDescricao.Name = "colDescricao";
            colDescricao.ReadOnly = true;
            // 
            // colAtivo
            // 
            colAtivo.DataPropertyName = "Ativo";
            colAtivo.FillWeight = 25F;
            colAtivo.HeaderText = "Ativo";
            colAtivo.Name = "colAtivo";
            colAtivo.ReadOnly = true;
            // 
            // pnlBusca
            // 
            pnlBusca.BackColor = Color.Transparent;
            pnlBusca.Controls.Add(btnAtualizar);
            pnlBusca.Controls.Add(txtBusca);
            pnlBusca.Controls.Add(lblBusca);
            pnlBusca.Dock = DockStyle.Top;
            pnlBusca.Location = new Point(18, 18);
            pnlBusca.Name = "pnlBusca";
            pnlBusca.Size = new Size(548, 55);
            pnlBusca.TabIndex = 0;
            // 
            // btnAtualizar
            // 
            btnAtualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAtualizar.Animated = true;
            btnAtualizar.BorderRadius = 8;
            btnAtualizar.Cursor = Cursors.Hand;
            btnAtualizar.CustomizableEdges = customizableEdges1;
            btnAtualizar.FillColor = Color.FromArgb(251, 234, 214);
            btnAtualizar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnAtualizar.ForeColor = Color.FromArgb(80, 75, 70);
            btnAtualizar.HoverState.FillColor = Color.FromArgb(229, 188, 169);
            btnAtualizar.Location = new Point(443, 10);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAtualizar.Size = new Size(105, 34);
            btnAtualizar.TabIndex = 2;
            btnAtualizar.Text = "🔄 Atualizar";
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // txtBusca
            // 
            txtBusca.Animated = true;
            txtBusca.BorderColor = Color.FromArgb(229, 188, 169);
            txtBusca.BorderRadius = 8;
            txtBusca.Cursor = Cursors.IBeam;
            txtBusca.CustomizableEdges = customizableEdges3;
            txtBusca.DefaultText = "";
            txtBusca.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBusca.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBusca.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBusca.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBusca.FillColor = Color.FromArgb(251, 234, 214);
            txtBusca.FocusedState.BorderColor = Color.FromArgb(107, 117, 86);
            txtBusca.Font = new Font("Segoe UI", 9.5F);
            txtBusca.ForeColor = Color.FromArgb(50, 50, 45);
            txtBusca.HoverState.BorderColor = Color.FromArgb(200, 125, 135);
            txtBusca.Location = new Point(120, 10);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderForeColor = Color.FromArgb(165, 155, 145);
            txtBusca.PlaceholderText = "Filtrar por nome...";
            txtBusca.SelectedText = "";
            txtBusca.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtBusca.Size = new Size(260, 34);
            txtBusca.TabIndex = 1;
            txtBusca.TextChanged += txtBusca_TextChanged;
            // 
            // lblBusca
            // 
            lblBusca.AutoSize = true;
            lblBusca.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblBusca.ForeColor = Color.FromArgb(70, 65, 60);
            lblBusca.Location = new Point(0, 18);
            lblBusca.Name = "lblBusca";
            lblBusca.Size = new Size(114, 17);
            lblBusca.TabIndex = 0;
            lblBusca.Text = "Buscar Ambiente:";
            // 
            // pnlCardFormulario
            // 
            pnlCardFormulario.BackColor = Color.Transparent;
            pnlCardFormulario.BorderColor = Color.FromArgb(229, 188, 169);
            pnlCardFormulario.BorderRadius = 12;
            pnlCardFormulario.BorderThickness = 1;
            pnlCardFormulario.Controls.Add(lblStatusAtivo);
            pnlCardFormulario.Controls.Add(swAtivo);
            pnlCardFormulario.Controls.Add(btnDesativar);
            pnlCardFormulario.Controls.Add(btnSalvar);
            pnlCardFormulario.Controls.Add(btnNovo);
            pnlCardFormulario.Controls.Add(txtDescricao);
            pnlCardFormulario.Controls.Add(lblDescricao);
            pnlCardFormulario.Controls.Add(txtNome);
            pnlCardFormulario.Controls.Add(lblNome);
            pnlCardFormulario.Controls.Add(lblTituloCard);
            pnlCardFormulario.Controls.Add(btnExcluir);
            pnlCardFormulario.CustomizableEdges = customizableEdges19;
            pnlCardFormulario.Dock = DockStyle.Left;
            pnlCardFormulario.FillColor = Color.White;
            pnlCardFormulario.Location = new Point(0, 0);
            pnlCardFormulario.Name = "pnlCardFormulario";
            pnlCardFormulario.Padding = new Padding(20);
            pnlCardFormulario.ShadowDecoration.CustomizableEdges = customizableEdges20;
            pnlCardFormulario.Size = new Size(345, 444);
            pnlCardFormulario.TabIndex = 0;
            // 
            // lblStatusAtivo
            // 
            lblStatusAtivo.AutoSize = true;
            lblStatusAtivo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblStatusAtivo.ForeColor = Color.FromArgb(70, 65, 60);
            lblStatusAtivo.Location = new Point(75, 276);
            lblStatusAtivo.Name = "lblStatusAtivo";
            lblStatusAtivo.Size = new Size(103, 17);
            lblStatusAtivo.TabIndex = 6;
            lblStatusAtivo.Text = "Ambiente Ativo";
            // 
            // swAtivo
            // 
            swAtivo.Animated = true;
            swAtivo.Checked = true;
            swAtivo.CheckedState.BorderColor = Color.FromArgb(107, 117, 86);
            swAtivo.CheckedState.FillColor = Color.FromArgb(107, 117, 86);
            swAtivo.CheckedState.InnerBorderColor = Color.White;
            swAtivo.CheckedState.InnerColor = Color.White;
            swAtivo.Cursor = Cursors.Hand;
            swAtivo.CustomizableEdges = customizableEdges7;
            swAtivo.Location = new Point(20, 274);
            swAtivo.Name = "swAtivo";
            swAtivo.ShadowDecoration.CustomizableEdges = customizableEdges8;
            swAtivo.Size = new Size(45, 22);
            swAtivo.TabIndex = 5;
            swAtivo.UncheckedState.BorderColor = Color.FromArgb(229, 218, 208);
            swAtivo.UncheckedState.FillColor = Color.FromArgb(229, 218, 208);
            swAtivo.UncheckedState.InnerBorderColor = Color.White;
            swAtivo.UncheckedState.InnerColor = Color.White;
            // 
            // btnDesativar
            // 
            btnDesativar.Animated = true;
            btnDesativar.BorderRadius = 8;
            btnDesativar.Cursor = Cursors.Hand;
            btnDesativar.CustomizableEdges = customizableEdges9;
            btnDesativar.FillColor = Color.FromArgb(250, 230, 233);
            btnDesativar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnDesativar.ForeColor = Color.FromArgb(185, 90, 100);
            btnDesativar.HoverState.FillColor = Color.FromArgb(240, 196, 203);
            btnDesativar.Location = new Point(23, 355);
            btnDesativar.Name = "btnDesativar";
            btnDesativar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnDesativar.Size = new Size(305, 40);
            btnDesativar.TabIndex = 8;
            btnDesativar.Text = "🗑️ Desativar Ambiente";
            btnDesativar.Click += btnDesativar_Click;
            // 
            // btnSalvar
            // 
            btnSalvar.Animated = true;
            btnSalvar.BorderRadius = 8;
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.CustomizableEdges = customizableEdges11;
            btnSalvar.FillColor = Color.FromArgb(107, 117, 86);
            btnSalvar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.HoverState.FillColor = Color.FromArgb(92, 101, 74);
            btnSalvar.Location = new Point(23, 305);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnSalvar.Size = new Size(305, 44);
            btnSalvar.TabIndex = 7;
            btnSalvar.Text = "💾 Salvar Ambiente";
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnNovo
            // 
            btnNovo.Animated = true;
            btnNovo.BorderRadius = 8;
            btnNovo.Cursor = Cursors.Hand;
            btnNovo.CustomizableEdges = customizableEdges13;
            btnNovo.FillColor = Color.FromArgb(251, 234, 214);
            btnNovo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnNovo.ForeColor = Color.FromArgb(80, 75, 70);
            btnNovo.HoverState.FillColor = Color.FromArgb(240, 196, 203);
            btnNovo.Location = new Point(235, 17);
            btnNovo.Name = "btnNovo";
            btnNovo.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnNovo.Size = new Size(90, 30);
            btnNovo.TabIndex = 0;
            btnNovo.Text = "➕ Novo";
            btnNovo.Click += btnNovo_Click;
            // 
            // txtDescricao
            // 
            txtDescricao.Animated = true;
            txtDescricao.BorderColor = Color.FromArgb(229, 188, 169);
            txtDescricao.BorderRadius = 8;
            txtDescricao.Cursor = Cursors.IBeam;
            txtDescricao.CustomizableEdges = customizableEdges15;
            txtDescricao.DefaultText = "";
            txtDescricao.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtDescricao.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtDescricao.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtDescricao.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtDescricao.FocusedState.BorderColor = Color.FromArgb(107, 117, 86);
            txtDescricao.Font = new Font("Segoe UI", 9.5F);
            txtDescricao.ForeColor = Color.FromArgb(50, 50, 45);
            txtDescricao.HoverState.BorderColor = Color.FromArgb(200, 125, 135);
            txtDescricao.Location = new Point(20, 165);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.PlaceholderForeColor = Color.FromArgb(165, 155, 145);
            txtDescricao.PlaceholderText = "Ex: Espaço integrado com marcenaria sob medida...";
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.SelectedText = "";
            txtDescricao.ShadowDecoration.CustomizableEdges = customizableEdges16;
            txtDescricao.Size = new Size(305, 90);
            txtDescricao.TabIndex = 4;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblDescricao.ForeColor = Color.FromArgb(70, 65, 60);
            lblDescricao.Location = new Point(20, 142);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(68, 17);
            lblDescricao.TabIndex = 3;
            lblDescricao.Text = "Descrição:";
            // 
            // txtNome
            // 
            txtNome.Animated = true;
            txtNome.BorderColor = Color.FromArgb(229, 188, 169);
            txtNome.BorderRadius = 8;
            txtNome.Cursor = Cursors.IBeam;
            txtNome.CustomizableEdges = customizableEdges17;
            txtNome.DefaultText = "";
            txtNome.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNome.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNome.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNome.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNome.FocusedState.BorderColor = Color.FromArgb(107, 117, 86);
            txtNome.Font = new Font("Segoe UI", 10F);
            txtNome.ForeColor = Color.FromArgb(50, 50, 45);
            txtNome.HoverState.BorderColor = Color.FromArgb(200, 125, 135);
            txtNome.Location = new Point(20, 92);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderForeColor = Color.FromArgb(165, 155, 145);
            txtNome.PlaceholderText = "Ex: Cozinha, Sala de Estar, Quarto...";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges18;
            txtNome.Size = new Size(305, 36);
            txtNome.TabIndex = 2;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblNome.ForeColor = Color.FromArgb(70, 65, 60);
            lblNome.Location = new Point(20, 68);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(131, 17);
            lblNome.TabIndex = 1;
            lblNome.Text = "Nome do Ambiente:";
            // 
            // lblTituloCard
            // 
            lblTituloCard.AutoSize = true;
            lblTituloCard.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTituloCard.ForeColor = Color.FromArgb(107, 117, 86);
            lblTituloCard.Location = new Point(18, 20);
            lblTituloCard.Name = "lblTituloCard";
            lblTituloCard.Size = new Size(180, 25);
            lblTituloCard.TabIndex = 0;
            lblTituloCard.Text = "Dados do Ambiente";
            // 
            // btnExcluir
            // 
            btnExcluir.Animated = true;
            btnExcluir.BorderRadius = 8;
            btnExcluir.Cursor = Cursors.Hand;
            btnExcluir.CustomizableEdges = customizableEdges15;
            btnExcluir.FillColor = Color.FromArgb(235, 90, 95);
            btnExcluir.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.HoverState.FillColor = Color.FromArgb(215, 70, 75);
            btnExcluir.Location = new Point(20, 401);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnExcluir.Size = new Size(305, 40);
            btnExcluir.TabIndex = 9;
            btnExcluir.Text = "❌ Excluir Permanentemente";
            btnExcluir.Click += btnExcluir_Click;
            // 
            // AmbientesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(251, 247, 242);
            Controls.Add(pnlPrincipal);
            Name = "AmbientesControl";
            Padding = new Padding(20);
            Size = new Size(969, 484);
            pnlPrincipal.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAmbientes).EndInit();
            pnlBusca.ResumeLayout(false);
            pnlBusca.PerformLayout();
            pnlCardFormulario.ResumeLayout(false);
            pnlCardFormulario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlPrincipal;
        private Guna.UI2.WinForms.Guna2Panel pnlCardFormulario;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        private System.Windows.Forms.Label lblTituloCard;
        private System.Windows.Forms.Label lblNome;
        private Guna.UI2.WinForms.Guna2TextBox txtNome;
        private System.Windows.Forms.Label lblDescricao;
        private Guna.UI2.WinForms.Guna2TextBox txtDescricao;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swAtivo;
        private System.Windows.Forms.Label lblStatusAtivo;
        private Guna.UI2.WinForms.Guna2Button btnSalvar;
        private Guna.UI2.WinForms.Guna2Button btnNovo;
        private Guna.UI2.WinForms.Guna2Button btnDesativar;
        private System.Windows.Forms.Panel pnlBusca;
        private System.Windows.Forms.Label lblBusca;
        private Guna.UI2.WinForms.Guna2TextBox txtBusca;
        private Guna.UI2.WinForms.Guna2Button btnAtualizar;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAmbientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAtivo;
        private Guna.UI2.WinForms.Guna2Button btnExcluir; // Adicionado para exclusão permanente
        
    }
    }

