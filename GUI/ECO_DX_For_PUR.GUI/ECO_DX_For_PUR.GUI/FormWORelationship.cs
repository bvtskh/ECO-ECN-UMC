using ADGV;
using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using ECO_DX_For_PUR.DATA.USAPService;
using ECO_DX_For_PUR.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.Relationship_Enum;

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormWORelationship : Form
    {
        WOHelper _woHelper = new WOHelper();
        DataTable dataWO;

        public FormWORelationship()
        {
            InitializeComponent();
            cbCustomer.Font = new Font("Arial",12f, FontStyle.Bold);
        }         

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchStr = cbCustomer.SelectedItem as string;
                string columnName = "Customer";
                var codeCustomer = Common.CreateCustomer().Where(w => w.Value == searchStr).Select(s => s.Key).ToList();
                if (!string.IsNullOrEmpty(searchStr))
                {
                    dgvWO.DataSource = _woHelper.FilterData(dataWO, columnName, codeCustomer);
                    lbRowNumber.Text = $"{dgvWO.RowCount} Rows";
                }
                else
                {
                    checkBoxPending_ValueChanged(null, false);
                }
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Common.CloseFormLoading();
            }
        }

        private void txtInputINT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {               
                if (dgvWO.RowCount > 0)
                {
                    var data =_woHelper.GetDataFromDGV(dgvWO, checkBoxPending.Active);
                    data.Columns.Remove("Update Time");
                    data.Columns.Remove("Confirm Waiting Time (h)");
                    if (ExcelServices.ExportDataWORelationship(data)) MessageBox.Show("Export Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
   
        private void FormWORelationship_Load(object sender, EventArgs e)
        {
            ShowDataWo(DataStatus.Pending);
            cbCustomer.Items.AddRange(Common.CreateCustomer().Select(w => w.Value).Distinct().ToArray());
        }     
    
        private void advancedDataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Sort = this.dgvWO.SortString;
        }

        private void advancedDataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Filter = this.dgvWO.FilterString;
        }

        private void insertWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkBoxPending.Active)
            {
                try
                {
                    if(selectedRow != null && selectedRow.Index != -1)
                    {
                        var orderNo = selectedRow.Cells["Order No"].Value;
                        FormInsertWO f = new FormInsertWO(orderNo);
                        f.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        DataGridViewRow selectedRow;
        private void dgvWO_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.RowIndex >= 0)
            {
                selectedRow = dgvWO.Rows[e.RowIndex];
            }
            else
            {
                selectedRow = null;
            }
        }

        private void checkBoxPending_ValueChanged(object sender, bool value)
        {
            this.bindingSource1.Sort = null;
            this.bindingSource1.Filter = null;
            var status = checkBoxPending.Active ? DataStatus.Pending : DataStatus.Success;
            ShowDataWo(status);
        }

        public void ShowDataWo(DataStatus status)
        {
            try
            {
                Common.StartFormLoading();
                if (status == DataStatus.Pending)
                {
                    dataWO = _woHelper.ToDataTableWOPending();
                }
                else if (status == DataStatus.Success)
                {
                    dataWO = _woHelper.ToDataWORelationship();
                }
                this.bindingSource1.DataSource = dataWO;
                dgvWO.DataSource = this.bindingSource1.DataSource;
                dgvWO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvWO.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                lbRowNumber.Text = $"{dgvWO.RowCount} Rows";
                Common.CloseFormLoading();
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show(ex.Message);
            }          
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelServices excelServices = new ExcelServices();
                if (excelServices.ImportRelationWO())
                {
                    ShowDataWo(DataStatus.Pending);
                    MessageBox.Show("Import Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnWo_Click(object sender, EventArgs e)
        {
            string txt = txtWo.Text;
            if(dataWO != null)
            {
                dgvWO.DataSource =_woHelper.GetWoRowSearching(dataWO,txt);
            }
        }

        private void txtWo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnWo_Click(null,null);
            }
        }

        private void fixWoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkBoxPending.Active)
            {
                try
                {
                    if (selectedRow != null && selectedRow.Index != -1)
                    {
                        FormEditWoRelationship f = new FormEditWoRelationship(selectedRow);
                        f.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void ctxWO_Opening(object sender, CancelEventArgs e)
        {
            if (checkBoxPending.Active)
            {
                ctxWO.Items[0].Visible = true;
                ctxWO.Items[1].Visible = false;
            }
            else if (!checkBoxPending.Active)
            {
                ctxWO.Items[0].Visible = false;
                ctxWO.Items[1].Visible = true;
            }
        }

        private void dgvWO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(dgvWO.Columns[e.ColumnIndex].Name == "Confirm Waiting Time (h)")
            {
                AlarmRowPendingOver();
            }
        }

        private void AlarmRowPendingOver()
        {
            dgvWO.Rows.Cast<DataGridViewRow>().Where(w => _woHelper.GetWaitingTime(w.Cells["Confirm Waiting Time (h)"].Value) >= 24).ForEach(f => f.DefaultCellStyle.BackColor = Color.Yellow);
            dgvWO.Columns["Confirm Waiting Time (h)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvWO.Columns["Confirm Waiting Time (h)"].DefaultCellStyle.Font = new Font("Arial", 15, FontStyle.Bold);
        }
    }
}
