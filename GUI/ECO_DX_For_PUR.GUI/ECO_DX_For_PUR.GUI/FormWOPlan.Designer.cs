
namespace ECO_DX_For_PUR.GUI
{
    partial class FormWOPlan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWOPlan));
            this.dgvPlan = new ADGV.AdvancedDataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pDFDetailECOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiPanel6 = new Sunny.UI.UIPanel();
            this.txtWo = new Sunny.UI.UITextBox();
            this.btnWo = new Sunny.UI.UISymbolButton();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.lblStatus = new Sunny.UI.UILabel();
            this.uiPanel5 = new Sunny.UI.UIPanel();
            this.txtECO = new Sunny.UI.UITextBox();
            this.btnECO = new Sunny.UI.UISymbolButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.txtModel = new Sunny.UI.UITextBox();
            this.btnModel = new Sunny.UI.UISymbolButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.cbCustomer = new Sunny.UI.UIComboBox();
            this.btnCustomer = new Sunny.UI.UISymbolButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.rdoPending = new Sunny.UI.UIRadioButton();
            this.rdoAll = new Sunny.UI.UIRadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.uiPanel7 = new Sunny.UI.UIPanel();
            this.dgvStatusCustomer = new ADGV.AdvancedDataGridView();
            this.uiPanel8 = new Sunny.UI.UIPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiPanel6.SuspendLayout();
            this.uiPanel5.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.uiPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlan
            // 
            this.dgvPlan.AllowUserToAddRows = false;
            this.dgvPlan.AutoGenerateContextFilters = true;
            this.dgvPlan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlan.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPlan.DateWithTime = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlan.EnableHeadersVisualStyles = false;
            this.dgvPlan.Location = new System.Drawing.Point(289, 45);
            this.dgvPlan.Name = "dgvPlan";
            this.dgvPlan.RowHeadersVisible = false;
            this.dgvPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPlan.Size = new System.Drawing.Size(1402, 582);
            this.dgvPlan.TabIndex = 2;
            this.dgvPlan.TimeFilter = false;
            this.dgvPlan.SortStringChanged += new System.EventHandler(this.dgvPlan_SortStringChanged);
            this.dgvPlan.FilterStringChanged += new System.EventHandler(this.dgvPlan_FilterStringChanged);
            this.dgvPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlan_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pDFDetailECOToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 26);
            // 
            // pDFDetailECOToolStripMenuItem
            // 
            this.pDFDetailECOToolStripMenuItem.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.pdf1;
            this.pDFDetailECOToolStripMenuItem.Name = "pDFDetailECOToolStripMenuItem";
            this.pDFDetailECOToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pDFDetailECOToolStripMenuItem.Text = "PDF Detail ECO";
            this.pDFDetailECOToolStripMenuItem.Click += new System.EventHandler(this.pDFDetailECOToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.uiPanel2);
            this.panel1.Controls.Add(this.uiPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1691, 45);
            this.panel1.TabIndex = 3;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiPanel6);
            this.uiPanel2.Controls.Add(this.lblStatus);
            this.uiPanel2.Controls.Add(this.uiPanel5);
            this.uiPanel2.Controls.Add(this.uiPanel4);
            this.uiPanel2.Controls.Add(this.uiPanel3);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.FillColor = System.Drawing.Color.White;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(180, 0);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(1511, 45);
            this.uiPanel2.TabIndex = 90;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel6
            // 
            this.uiPanel6.Controls.Add(this.txtWo);
            this.uiPanel6.Controls.Add(this.btnWo);
            this.uiPanel6.Controls.Add(this.uiLabel4);
            this.uiPanel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel6.FillColor = System.Drawing.Color.White;
            this.uiPanel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel6.Location = new System.Drawing.Point(801, 0);
            this.uiPanel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel6.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel6.Name = "uiPanel6";
            this.uiPanel6.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
            this.uiPanel6.Size = new System.Drawing.Size(243, 45);
            this.uiPanel6.TabIndex = 94;
            this.uiPanel6.Text = null;
            this.uiPanel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWo
            // 
            this.txtWo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWo.Location = new System.Drawing.Point(46, 8);
            this.txtWo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWo.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtWo.Name = "txtWo";
            this.txtWo.Padding = new System.Windows.Forms.Padding(5);
            this.txtWo.ShowText = false;
            this.txtWo.Size = new System.Drawing.Size(159, 29);
            this.txtWo.TabIndex = 85;
            this.txtWo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtWo.Watermark = "";
            // 
            // btnWo
            // 
            this.btnWo.BackColor = System.Drawing.SystemColors.Control;
            this.btnWo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWo.FillColor = System.Drawing.Color.White;
            this.btnWo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnWo.ForeColor = System.Drawing.Color.Black;
            this.btnWo.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnWo.Location = new System.Drawing.Point(207, 8);
            this.btnWo.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnWo.Name = "btnWo";
            this.btnWo.Radius = 10;
            this.btnWo.Size = new System.Drawing.Size(30, 29);
            this.btnWo.TabIndex = 86;
            this.btnWo.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnWo.Click += new System.EventHandler(this.ButonSearch_Click);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(1, 12);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(46, 23);
            this.uiLabel4.TabIndex = 89;
            this.uiLabel4.Text = "WO";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblStatus.Location = new System.Drawing.Point(1397, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(111, 23);
            this.lblStatus.TabIndex = 93;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiPanel5
            // 
            this.uiPanel5.Controls.Add(this.txtECO);
            this.uiPanel5.Controls.Add(this.btnECO);
            this.uiPanel5.Controls.Add(this.uiLabel3);
            this.uiPanel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel5.FillColor = System.Drawing.Color.White;
            this.uiPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel5.Location = new System.Drawing.Point(558, 0);
            this.uiPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel5.Name = "uiPanel5";
            this.uiPanel5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
            this.uiPanel5.Size = new System.Drawing.Size(243, 45);
            this.uiPanel5.TabIndex = 92;
            this.uiPanel5.Text = null;
            this.uiPanel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtECO
            // 
            this.txtECO.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtECO.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECO.Location = new System.Drawing.Point(46, 8);
            this.txtECO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtECO.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtECO.Name = "txtECO";
            this.txtECO.Padding = new System.Windows.Forms.Padding(5);
            this.txtECO.ShowText = false;
            this.txtECO.Size = new System.Drawing.Size(159, 29);
            this.txtECO.TabIndex = 85;
            this.txtECO.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtECO.Watermark = "";
            // 
            // btnECO
            // 
            this.btnECO.BackColor = System.Drawing.SystemColors.Control;
            this.btnECO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnECO.FillColor = System.Drawing.Color.White;
            this.btnECO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnECO.ForeColor = System.Drawing.Color.Black;
            this.btnECO.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnECO.Location = new System.Drawing.Point(207, 8);
            this.btnECO.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnECO.Name = "btnECO";
            this.btnECO.Radius = 10;
            this.btnECO.Size = new System.Drawing.Size(30, 29);
            this.btnECO.TabIndex = 86;
            this.btnECO.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnECO.Click += new System.EventHandler(this.ButonSearch_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(1, 12);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(46, 23);
            this.uiLabel3.TabIndex = 89;
            this.uiLabel3.Text = "ECO";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.txtModel);
            this.uiPanel4.Controls.Add(this.btnModel);
            this.uiPanel4.Controls.Add(this.uiLabel2);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel4.FillColor = System.Drawing.Color.White;
            this.uiPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel4.Location = new System.Drawing.Point(264, 0);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
            this.uiPanel4.Size = new System.Drawing.Size(294, 45);
            this.uiPanel4.TabIndex = 91;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtModel
            // 
            this.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtModel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModel.Location = new System.Drawing.Point(59, 8);
            this.txtModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtModel.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtModel.Name = "txtModel";
            this.txtModel.Padding = new System.Windows.Forms.Padding(5);
            this.txtModel.ShowText = false;
            this.txtModel.Size = new System.Drawing.Size(198, 29);
            this.txtModel.TabIndex = 85;
            this.txtModel.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.txtModel.Watermark = "";
            // 
            // btnModel
            // 
            this.btnModel.BackColor = System.Drawing.SystemColors.Control;
            this.btnModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModel.FillColor = System.Drawing.Color.White;
            this.btnModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnModel.ForeColor = System.Drawing.Color.Black;
            this.btnModel.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnModel.Location = new System.Drawing.Point(259, 8);
            this.btnModel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnModel.Name = "btnModel";
            this.btnModel.Radius = 10;
            this.btnModel.Size = new System.Drawing.Size(30, 29);
            this.btnModel.TabIndex = 86;
            this.btnModel.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnModel.Click += new System.EventHandler(this.ButonSearch_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(4, 12);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(61, 23);
            this.uiLabel2.TabIndex = 89;
            this.uiLabel2.Text = "Model";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.cbCustomer);
            this.uiPanel3.Controls.Add(this.btnCustomer);
            this.uiPanel3.Controls.Add(this.uiLabel1);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel3.FillColor = System.Drawing.Color.White;
            this.uiPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel3.Location = new System.Drawing.Point(0, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
            this.uiPanel3.Size = new System.Drawing.Size(264, 45);
            this.uiPanel3.TabIndex = 90;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCustomer
            // 
            this.cbCustomer.DataSource = null;
            this.cbCustomer.FillColor = System.Drawing.Color.White;
            this.cbCustomer.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCustomer.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbCustomer.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbCustomer.Location = new System.Drawing.Point(56, 8);
            this.cbCustomer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCustomer.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbCustomer.Size = new System.Drawing.Size(171, 29);
            this.cbCustomer.TabIndex = 88;
            this.cbCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCustomer.Watermark = "";
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.btnCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomer.FillColor = System.Drawing.Color.White;
            this.btnCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCustomer.ForeColor = System.Drawing.Color.Black;
            this.btnCustomer.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnCustomer.Location = new System.Drawing.Point(229, 8);
            this.btnCustomer.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Radius = 10;
            this.btnCustomer.Size = new System.Drawing.Size(30, 29);
            this.btnCustomer.TabIndex = 86;
            this.btnCustomer.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnCustomer.Click += new System.EventHandler(this.ButonSearch_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(6, 12);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(47, 23);
            this.uiLabel1.TabIndex = 89;
            this.uiLabel1.Text = "Area";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.rdoPending);
            this.uiPanel1.Controls.Add(this.rdoAll);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(55)))));
            this.uiPanel1.Size = new System.Drawing.Size(180, 45);
            this.uiPanel1.TabIndex = 89;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdoPending
            // 
            this.rdoPending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoPending.Location = new System.Drawing.Point(12, 9);
            this.rdoPending.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdoPending.Name = "rdoPending";
            this.rdoPending.RadioButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(220)))), ((int)(((byte)(55)))));
            this.rdoPending.Size = new System.Drawing.Size(97, 29);
            this.rdoPending.TabIndex = 0;
            this.rdoPending.Text = "Pending";
            this.rdoPending.CheckedChanged += new System.EventHandler(this.rdoPending_CheckedChanged);
            // 
            // rdoAll
            // 
            this.rdoAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAll.Location = new System.Drawing.Point(114, 9);
            this.rdoAll.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.RadioButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(20)))), ((int)(((byte)(55)))));
            this.rdoAll.Size = new System.Drawing.Size(60, 29);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.Text = "All";
            this.rdoAll.CheckedChanged += new System.EventHandler(this.rdoAll_CheckedChanged);
            // 
            // uiPanel7
            // 
            this.uiPanel7.Controls.Add(this.dgvStatusCustomer);
            this.uiPanel7.Controls.Add(this.uiPanel8);
            this.uiPanel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel7.Location = new System.Drawing.Point(0, 45);
            this.uiPanel7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel7.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel7.Name = "uiPanel7";
            this.uiPanel7.Size = new System.Drawing.Size(289, 582);
            this.uiPanel7.TabIndex = 4;
            this.uiPanel7.Text = null;
            this.uiPanel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvStatusCustomer
            // 
            this.dgvStatusCustomer.AllowUserToAddRows = false;
            this.dgvStatusCustomer.AutoGenerateContextFilters = true;
            this.dgvStatusCustomer.BackgroundColor = System.Drawing.Color.White;
            this.dgvStatusCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatusCustomer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStatusCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatusCustomer.DateWithTime = false;
            this.dgvStatusCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatusCustomer.EnableHeadersVisualStyles = false;
            this.dgvStatusCustomer.Location = new System.Drawing.Point(0, 0);
            this.dgvStatusCustomer.Name = "dgvStatusCustomer";
            this.dgvStatusCustomer.RowHeadersVisible = false;
            this.dgvStatusCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvStatusCustomer.Size = new System.Drawing.Size(279, 582);
            this.dgvStatusCustomer.TabIndex = 3;
            this.dgvStatusCustomer.TimeFilter = false;
            this.dgvStatusCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStatusCustomer_CellClick);
            // 
            // uiPanel8
            // 
            this.uiPanel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiPanel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel8.Location = new System.Drawing.Point(279, 0);
            this.uiPanel8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel8.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel8.Name = "uiPanel8";
            this.uiPanel8.Size = new System.Drawing.Size(10, 582);
            this.uiPanel8.TabIndex = 4;
            this.uiPanel8.Text = null;
            this.uiPanel8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormWOPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1691, 627);
            this.Controls.Add(this.dgvPlan);
            this.Controls.Add(this.uiPanel7);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWOPlan";
            this.Text = "Plan";
            this.Load += new System.EventHandler(this.FormWOPlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlan)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel6.ResumeLayout(false);
            this.uiPanel5.ResumeLayout(false);
            this.uiPanel4.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.uiPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusCustomer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView dgvPlan;
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UIRadioButton rdoPending;
        private Sunny.UI.UIRadioButton rdoAll;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Sunny.UI.UISymbolButton btnCustomer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Sunny.UI.UISymbolButton btnECO;
        private Sunny.UI.UISymbolButton btnModel;
        private Sunny.UI.UITextBox txtECO;
        private Sunny.UI.UITextBox txtModel;
        private Sunny.UI.UIComboBox cbCustomer;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIPanel uiPanel5;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIPanel uiPanel4;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel lblStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pDFDetailECOToolStripMenuItem;
        private Sunny.UI.UIPanel uiPanel6;
        private Sunny.UI.UITextBox txtWo;
        private Sunny.UI.UISymbolButton btnWo;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIPanel uiPanel7;
        private ADGV.AdvancedDataGridView dgvStatusCustomer;
        private Sunny.UI.UIPanel uiPanel8;
    }
}