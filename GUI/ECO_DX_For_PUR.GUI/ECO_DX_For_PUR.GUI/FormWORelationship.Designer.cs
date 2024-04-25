
namespace ECO_DX_For_PUR.GUI
{
    partial class FormWORelationship
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWORelationship));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.txtWo = new Sunny.UI.UITextBox();
            this.btnWo = new Sunny.UI.UISymbolButton();
            this.lbRowNumber = new System.Windows.Forms.Label();
            this.cbCustomer = new Sunny.UI.UIComboBox();
            this.checkBoxPending = new Sunny.UI.UISwitch();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.btnExport = new Sunny.UI.UISymbolButton();
            this.dgvWO = new ADGV.AdvancedDataGridView();
            this.ctxWO = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixWoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.uiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWO)).BeginInit();
            this.ctxWO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.txtWo);
            this.uiPanel1.Controls.Add(this.btnWo);
            this.uiPanel1.Controls.Add(this.lbRowNumber);
            this.uiPanel1.Controls.Add(this.cbCustomer);
            this.uiPanel1.Controls.Add(this.checkBoxPending);
            this.uiPanel1.Controls.Add(this.label1);
            this.uiPanel1.Controls.Add(this.btnSearch);
            this.uiPanel1.Controls.Add(this.uiSymbolButton1);
            this.uiPanel1.Controls.Add(this.btnExport);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(1414, 38);
            this.uiPanel1.TabIndex = 1;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWo
            // 
            this.txtWo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWo.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWo.Location = new System.Drawing.Point(667, 5);
            this.txtWo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWo.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtWo.Name = "txtWo";
            this.txtWo.Padding = new System.Windows.Forms.Padding(5);
            this.txtWo.ShowText = false;
            this.txtWo.Size = new System.Drawing.Size(215, 29);
            this.txtWo.TabIndex = 96;
            this.txtWo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtWo.Watermark = "";
            this.txtWo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWo_KeyDown);
            // 
            // btnWo
            // 
            this.btnWo.BackColor = System.Drawing.SystemColors.Control;
            this.btnWo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWo.FillColor = System.Drawing.Color.White;
            this.btnWo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnWo.ForeColor = System.Drawing.Color.Black;
            this.btnWo.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnWo.Location = new System.Drawing.Point(884, 5);
            this.btnWo.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnWo.Name = "btnWo";
            this.btnWo.Radius = 10;
            this.btnWo.Size = new System.Drawing.Size(30, 29);
            this.btnWo.TabIndex = 97;
            this.btnWo.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnWo.Click += new System.EventHandler(this.btnWo_Click);
            // 
            // lbRowNumber
            // 
            this.lbRowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRowNumber.AutoSize = true;
            this.lbRowNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.lbRowNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRowNumber.Location = new System.Drawing.Point(1255, 7);
            this.lbRowNumber.Name = "lbRowNumber";
            this.lbRowNumber.Size = new System.Drawing.Size(0, 24);
            this.lbRowNumber.TabIndex = 95;
            // 
            // cbCustomer
            // 
            this.cbCustomer.DataSource = null;
            this.cbCustomer.FillColor = System.Drawing.Color.White;
            this.cbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbCustomer.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbCustomer.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbCustomer.Location = new System.Drawing.Point(95, 4);
            this.cbCustomer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCustomer.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbCustomer.Size = new System.Drawing.Size(154, 29);
            this.cbCustomer.TabIndex = 94;
            this.cbCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbCustomer.Watermark = "";
            // 
            // checkBoxPending
            // 
            this.checkBoxPending.Active = true;
            this.checkBoxPending.ActiveColor = System.Drawing.Color.DodgerBlue;
            this.checkBoxPending.ActiveText = "Pending";
            this.checkBoxPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBoxPending.InActiveColor = System.Drawing.Color.ForestGreen;
            this.checkBoxPending.InActiveText = "Success";
            this.checkBoxPending.Location = new System.Drawing.Point(351, 4);
            this.checkBoxPending.MinimumSize = new System.Drawing.Size(1, 1);
            this.checkBoxPending.Name = "checkBoxPending";
            this.checkBoxPending.Size = new System.Drawing.Size(113, 29);
            this.checkBoxPending.TabIndex = 93;
            this.checkBoxPending.Text = "uiSwitch1";
            this.checkBoxPending.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.checkBoxPending_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 92;
            this.label1.Text = "Customer:";
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FillColor = System.Drawing.Color.AntiqueWhite;
            this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.view1;
            this.btnSearch.Location = new System.Drawing.Point(252, 4);
            this.btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Radius = 10;
            this.btnSearch.Size = new System.Drawing.Size(91, 29);
            this.btnSearch.TabIndex = 89;
            this.btnSearch.Text = "Search";
            this.btnSearch.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.BackColor = System.Drawing.SystemColors.Control;
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(104)))), ((int)(((byte)(149)))));
            this.uiSymbolButton1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(179)))), ((int)(((byte)(255)))));
            this.uiSymbolButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiSymbolButton1.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.exel;
            this.uiSymbolButton1.Location = new System.Drawing.Point(569, 5);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Radius = 10;
            this.uiSymbolButton1.Size = new System.Drawing.Size(92, 29);
            this.uiSymbolButton1.TabIndex = 88;
            this.uiSymbolButton1.Text = "Upload";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiSymbolButton1.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(174)))), ((int)(((byte)(239)))));
            this.btnExport.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(179)))), ((int)(((byte)(255)))));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExport.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.exel;
            this.btnExport.Location = new System.Drawing.Point(471, 5);
            this.btnExport.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Radius = 10;
            this.btnExport.Size = new System.Drawing.Size(92, 29);
            this.btnExport.TabIndex = 88;
            this.btnExport.Text = "Export";
            this.btnExport.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvWO
            // 
            this.dgvWO.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(233)))), ((int)(((byte)(244)))));
            this.dgvWO.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWO.AutoGenerateContextFilters = true;
            this.dgvWO.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWO.ContextMenuStrip = this.ctxWO;
            this.dgvWO.DateWithTime = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWO.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWO.EnableHeadersVisualStyles = false;
            this.dgvWO.Location = new System.Drawing.Point(0, 38);
            this.dgvWO.Name = "dgvWO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWO.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWO.RowHeadersVisible = false;
            this.dgvWO.Size = new System.Drawing.Size(1414, 749);
            this.dgvWO.TabIndex = 3;
            this.dgvWO.TimeFilter = false;
            this.dgvWO.SortStringChanged += new System.EventHandler(this.advancedDataGridView1_SortStringChanged);
            this.dgvWO.FilterStringChanged += new System.EventHandler(this.advancedDataGridView1_FilterStringChanged);
            this.dgvWO.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWO_CellClick);
            this.dgvWO.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvWO_CellFormatting);
            // 
            // ctxWO
            // 
            this.ctxWO.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertWOToolStripMenuItem,
            this.fixWoToolStripMenuItem});
            this.ctxWO.Name = "ctxWO";
            this.ctxWO.Size = new System.Drawing.Size(127, 48);
            this.ctxWO.Opening += new System.ComponentModel.CancelEventHandler(this.ctxWO_Opening);
            // 
            // insertWOToolStripMenuItem
            // 
            this.insertWOToolStripMenuItem.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.adds;
            this.insertWOToolStripMenuItem.Name = "insertWOToolStripMenuItem";
            this.insertWOToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.insertWOToolStripMenuItem.Text = "Insert WO";
            this.insertWOToolStripMenuItem.Click += new System.EventHandler(this.insertWOToolStripMenuItem_Click);
            // 
            // fixWoToolStripMenuItem
            // 
            this.fixWoToolStripMenuItem.Image = global::ECO_DX_For_PUR.GUI.Properties.Resources.setup;
            this.fixWoToolStripMenuItem.Name = "fixWoToolStripMenuItem";
            this.fixWoToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.fixWoToolStripMenuItem.Text = "Edit";
            this.fixWoToolStripMenuItem.Click += new System.EventHandler(this.fixWoToolStripMenuItem_Click);
            // 
            // FormWORelationship
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1414, 787);
            this.Controls.Add(this.dgvWO);
            this.Controls.Add(this.uiPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWORelationship";
            this.Text = "Relationship";
            this.Load += new System.EventHandler(this.FormWORelationship_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWO)).EndInit();
            this.ctxWO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private ADGV.AdvancedDataGridView dgvWO;
        private Sunny.UI.UISymbolButton btnSearch;
        private Sunny.UI.UISymbolButton btnExport;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ContextMenuStrip ctxWO;
        private System.Windows.Forms.ToolStripMenuItem insertWOToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UISwitch checkBoxPending;
        private Sunny.UI.UIComboBox cbCustomer;
        private System.Windows.Forms.Label lbRowNumber;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UITextBox txtWo;
        private Sunny.UI.UISymbolButton btnWo;
        private System.Windows.Forms.ToolStripMenuItem fixWoToolStripMenuItem;
    }
}