using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using ECO_DX_For_PUR.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.PE_DM_Enum;

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormPEDM : Form
    {
        ExcelServices _excelService = new ExcelServices();
        ECO_Helper _ecoHelper = new ECO_Helper();
        DataTable dataResult;
        bool IsCheckUpdate;

        public FormPEDM()
        {
            InitializeComponent();
        }

        private void FormPEDM_Load(object sender, EventArgs e)
        {
            SetUpSelectSearchType();
        }

        private void SetUpSelectSearchType()
        {
            cbSelectTypeSearch.SelectedIndex = (int)SelectType.ECN;
            cbIssue.SelectedIndex = (int)Issue.PURCHASE;
            dateTo.Value = DateTime.Now;
            dateFrom.Value = dateTo.Value.AddDays(-180);
        }

        bool btnaddClick = false;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!btnaddClick)
            {
                panelShowFormAdd.Visible = true;
                btnAdd.Image = Properties.Resources.minus;
                Common.AddFormToPanel(new FormInsertControlSheet(), panelShowFormAdd);
            }
            else
            {
                panelShowFormAdd.Visible = false;
                btnAdd.Image = Properties.Resources.add;
                Common.CloseForm("FormInsertControlSheet");
            }
            btnaddClick = !btnaddClick;

        }
        private void dgvPEDM_FilterStringChanged(object sender, EventArgs e)
        {
            try
            {
                this.bindingSourceECO.Filter = this.dgvPEDM.FilterString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvPEDM_SortStringChanged(object sender, EventArgs e)
        {
            try
            {
                this.bindingSourceECO.Sort = this.dgvPEDM.SortString;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        } 

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }
        DataGridViewRow selectedRow;
        private void dgvPEDM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = dgvPEDM.Rows[e.RowIndex];                       
            }
        }

        private void uptoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ecoHelper.UpdateDataECOControlSheet(selectedRow))
            {
                MessageBox.Show("Update Success");
                return;
            }           
        }

        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ecoHelper.RemoveControlsheetData(selectedRow))
            {
                MessageBox.Show("Delete Success");
                return;
            }
        }
        bool isAdvanced = false;
        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (!isAdvanced)
            {
                panelAdvanced.Visible = true;
            }
            else
            {
                panelAdvanced.Visible = false;
            }
            isAdvanced = !isAdvanced;
        }
       
        private void contextSelectRow_Opening(object sender, CancelEventArgs e)
        {
            if (IsCheckUpdate)
            {
                contextSelectRow.Items[0].Visible = false;
                contextSelectRow.Items[1].Visible = false;
                contextSelectRow.Items[2].Visible = true;
            }
            else
            {
                contextSelectRow.Items[0].Visible = true;
                contextSelectRow.Items[1].Visible = true;
                contextSelectRow.Items[2].Visible = false;
            }
        }
    
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void SearchData()
        {
            try
            {
                IsCheckUpdate = false;
                ClearFilterBindingSource();
                Common.StartFormLoading();
                int indexType = cbSelectTypeSearch.SelectedIndex;
                if (indexType != -1)
                {
                    if (indexType == (int)SelectType.Duplicate_ECO)
                    {
                        ShowDuplicateUSR();
                        return;
                    }
                    else
                    {
                        if (cbIssue.SelectedIndex == (int)Issue.PURCHASE)
                        {
                            IEnumerable<ECO_ControlSheet> dataIssueToPur = _ecoHelper.GetDataIssueToPur(dateFrom.Value,dateTo.Value);
                            if (string.IsNullOrEmpty(txtSearchContent.Text))
                            {
                                dataResult = _ecoHelper.GetDataControlSheet(dataIssueToPur, intBox.Value);
                                this.bindingSourceECO.DataSource = dataResult;
                                dgvPEDM.DataSource = this.bindingSourceECO.DataSource;
                            }
                            else
                            {
                                dgvPEDM.DataSource = _ecoHelper.GetDataECOcontrolsheetSearch(indexType, txtSearchContent.Text);
                            }
                        }
                        else if (cbIssue.SelectedIndex == (int)Issue.ALL)
                        {
                            if (string.IsNullOrEmpty(txtSearchContent.Text))
                            {
                                dataResult = _ecoHelper.GetDataControlSheet(intBox.Value, dateFrom.Value, dateTo.Value);
                                this.bindingSourceECO.DataSource = dataResult;
                                dgvPEDM.DataSource = this.bindingSourceECO.DataSource;
                            }
                            else
                            {
                                dgvPEDM.DataSource = _ecoHelper.GetDataECOcontrolsheetSearch(indexType, txtSearchContent.Text);
                            }
                        }
                    }                          
                }
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                CustomDgv();
                Common.CloseFormLoading();
            }
        }

        private void ShowDuplicateUSR()
        {
            dataResult = _ecoHelper.GetDuplicateECO(dateFrom.Value, dateTo.Value);
            this.bindingSourceECO.DataSource = dataResult;
            dgvPEDM.DataSource = this.bindingSourceECO.DataSource;;
            Common.CloseFormLoading();
            CustomDgv();
            MessageBox.Show($"{dataResult.Rows.Count} duplicate record");
        }

        private void ClearFilterBindingSource()
        {
            this.bindingSourceECO.Sort = null;
            this.bindingSourceECO.Filter = null;
        }

        private void CustomDgv()
        {
            try
            {
                dgvPEDM.Columns["Id"].Visible = false;
                dgvPEDM.Columns["ECN_ReceiveDate"].HeaderText = "Receive Date";
                dgvPEDM.Columns["ECN_ERI_No"].HeaderText = "ECN\\ERI No";
                dgvPEDM.Columns["VE_Follows_ECN_CVN"].HeaderText = "VE theo ECN\\CVN";
                dgvPEDM.Columns["ECO_No"].HeaderText = "ECO No";
                dgvPEDM.Columns["OldPartCode"].HeaderText = "Old Part";
                dgvPEDM.Columns["NewPartCode"].HeaderText = "New Part";
                dgvPEDM.Columns["ContentOfChange"].HeaderText = "Content Of Change";
                dgvPEDM.Columns["ECO_Issue"].HeaderText = "ECO Issue";
                dgvPEDM.Columns["FAT_Implement"].HeaderText = "FAT Implement";
                dgvPEDM.Columns["ImplementDate"].HeaderText = "Implement Date";
                dgvPEDM.Columns["ShippingDate"].HeaderText = "Shipping Date";
                dgvPEDM.Columns["ECO_Status"].HeaderText = "ECO Status";
                dgvPEDM.Columns["Issue_To"].HeaderText = "Issue To";
                dgvPEDM.Columns["FATContentInformation"].HeaderText = "Nội dung FAT thực hiện";
                dgvPEDM.Columns["TVP_No"].HeaderText = "TVP No";
                dgvPEDM.Columns["TVPResult"].HeaderText = "Kết quả TVP";
                dgvPEDM.Columns["TVP_RecieveResultDate"].HeaderText = "Ngày nhận kết quả TVP";
                dgvPEDM.Columns["BOM_Change_When_ECO_Implement"].HeaderText = "Thay đổi BOM khi ECO thực hiện";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void txtSearchContent_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SearchData();
            }
        }
    }
}
