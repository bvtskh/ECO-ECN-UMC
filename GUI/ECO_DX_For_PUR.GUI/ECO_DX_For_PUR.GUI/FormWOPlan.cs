using ADGV;
using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using ECO_DX_For_PUR.DATA.Entities;
using ECO_DX_For_PUR.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.Plan_Enum;

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormWOPlan : Form
    {
        WOHelper _woHelper = new WOHelper();
        DataTable dataTable;
        public FormWOPlan()
        {
            InitializeComponent();
            rdoPending.Checked = true;
            cbCustomer.Font = new Font("Tahoma", 11F, FontStyle.Regular);
            Common.SetHeaderColor(dgvPlan);
            InitTagTextbox();
        }

        private void FormWOPlan_Load(object sender, EventArgs e)
        {
            try
            {
                CustomRowColor(dgvPlan);
                cbCustomer.Items.AddRange(_woHelper.GetAreaList().Select(s=>s.AREA1).Distinct().ToArray());
                cbCustomer.SelectedIndex = 0;
                if (dgvStatusCustomer.RowCount > 0)
                {
                    dgvStatusCustomer.Rows[dgvStatusCustomer.RowCount - 1].DefaultCellStyle.BackColor = Color.CadetBlue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ResetFilter()
        {
            this.bindingSource1.Sort = null;
            this.bindingSource1.Filter = null;
        }     
        private void CustomRowColor(AdvancedDataGridView dgv)
        {
            try
            {
                if (dgv.RowCount > 0)
                {
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    var rowList = dgv.Rows.Cast<DataGridViewRow>().ToList();
                    rowList.Where(w => GetValueCell(w.Cells["SMT Status"].Value) == "OK" && GetValueCell(w.Cells["FAT Status"].Value) == "OK").ForEach(f => f.DefaultCellStyle.BackColor = Color.LimeGreen);
                    rowList.Where(w => GetValueCell(w.Cells["SMT Status"].Value) == "OK" && GetValueCell(w.Cells["FAT Status"].Value) == "Pending").ForEach(f => f.DefaultCellStyle.BackColor = Color.Yellow);
                }
                Common.CloseFormLoading();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private string GetValueCell(object dataGridViewCell)
        {
            return dataGridViewCell == null ? "" : dataGridViewCell.ToString();
        }

        private void ButonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ResetFilter();
                var objSender = (UIButton)sender;
                string columnName = GetColumnsName(objSender);
                List<string> searchString = new List<string>();
                searchString.Add(GetSearchString(objSender));
                ShowFindingData(objSender,columnName, searchString);
            }        
             catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void ShowFindingData(UIButton objSender, string columnName, List<string> searchString)
        {
            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(searchString[0]))
            {
                DataTable data;
                if ((int)objSender.GetTag(1) == (int)ButtonTag.customer) // nếu tìm theo customer
                {
                    if (cbCustomer.SelectedIndex != -1)
                    {
                        List<string> customerCode = _woHelper.GetAreaList().Where(w => w.AREA1.Contains(cbCustomer.Text)).Select(s => s.CUSTOMER).ToList();
                        data = _woHelper.FilterData(dataTable, columnName, customerCode);
                    }
                    else
                    {
                        RefreshData();
                        return;
                    }
                }
                else
                {
                    data = _woHelper.FilterData(dataTable, columnName, searchString);
                }

                this.bindingSource1.DataSource = data;
                dgvPlan.DataSource = this.bindingSource1.DataSource;
                lblStatus.Text = $"{dgvPlan.RowCount} Rows";
                CustomRowColor(dgvPlan);
                Common.CloseFormLoading();
            }
            else
            {
                RefreshData();
            }
        }

        private void RefreshData()
        {
            if (rdoAll.Checked)
            {
                rdoAll_CheckedChanged(null, null);
            }
            else
            {
                rdoPending_CheckedChanged(null,null);
            }
        }

        private string GetSearchString(UIButton objSender)
        {
            if (GetObject(objSender.GetTag(0)) is UITextBox)
            {
                UITextBox uITextBox = (UITextBox)objSender.GetTag(0);
                return uITextBox.Text;
            }
            else if (GetObject(objSender.GetTag(0)) is UIComboBox)
            {
                UIComboBox uIComboBox = (UIComboBox)objSender.GetTag(0);
                return uIComboBox.Text;
            }
            return null;
        }

        private string GetColumnsName(UIButton objSender)
        {
            if (GetObject(objSender.GetTag(0)) is UITextBox)
            {
                UITextBox uITextBox = (UITextBox)objSender.GetTag(0);
                return uITextBox.GetTag(0).ToString();
            }
            else if (GetObject(objSender.GetTag(0)) is UIComboBox)
            {
                UIComboBox uITextBox = (UIComboBox)objSender.GetTag(0);
                return uITextBox.GetTag(0).ToString();
            }
            return null;
        }
        private object GetObject(object tag)
        {
            if (tag is UITextBox)
            {
                return (UITextBox)tag;
            }
            if (tag is UIComboBox)
            {
                return (UIComboBox)tag;
            }
            return null;
        }

        private void InitTagTextbox()
        {
            btnCustomer.InitTag(cbCustomer, ButtonTag.customer);
            btnModel.InitTag(txtModel, ButtonTag.model);
            btnECO.InitTag(txtECO, ButtonTag.eco);
            btnWo.InitTag(txtWo, ButtonTag.Wo);
            txtModel.InitTag(TextboxTag.Model);
            txtECO.InitTag(TextboxTag.ECO_No);
            txtWo.InitTag(TextboxTag.Wo);
            cbCustomer.InitTag(TextboxTag.Customer);
        }

        private void pDFDetailECOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPDFfile(selectedRow);        
        }
        private void ShowPDFfile(DataGridViewRow selectedRow)
        {
            try
            {
                if (selectedRow == null || selectedRow.Index == -1)
                {
                    if (dgvPlan.RowCount > 0) selectedRow = dgvPlan.Rows[0];
                }
                if (selectedRow != null && selectedRow.Index != -1)
                {
                    object eco = selectedRow.Cells["ECO No"].Value;

                    if (eco != null)
                    {
                        string pdfLink = _woHelper.GetFullpathPDFfile(eco.ToString());
                        if (!string.IsNullOrEmpty(pdfLink))
                        {
                            FormViewPDF f = new FormViewPDF(pdfLink, eco.ToString());
                            f.Show();
                        }
                        else
                        {
                            MessageBox.Show($"PDF not found for USR: {eco}");
                        }
                    }
                }
                else
                {
                    FormViewPDF f = new FormViewPDF("", "");
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        DataGridViewRow selectedRow;
        private void dgvPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRow = dgvPlan.Rows[e.RowIndex];
            }
        }

        private void dgvPlan_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Sort = dgvPlan.SortString;
        }

        private void dgvPlan_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Filter = dgvPlan.FilterString;
        }

        private void rdoPending_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoPending.Checked)
                {
                    Common.StartFormLoading();
                    dataTable = _woHelper.GetAllWo(WO_Select.Pending);
                    Showdata(dataTable);
                }
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Showdata(DataTable dataTable)
        {
            ResetFilter();
            this.bindingSource1.DataSource = dataTable;
            dgvPlan.DataSource = this.bindingSource1;
            lblStatus.Text = $"{dgvPlan.Rows.Count} Rows";
            ShowStatusCustomer(dataTable);
            CustomRowColor(dgvPlan);
        }

        private void ShowStatusCustomer(DataTable dataTable)
        {
            try
            {
                DataTable data = _woHelper.GetCustomerFromDatatable(dataTable);             
                dgvStatusCustomer.DataSource = data;
                dgvStatusCustomer.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvStatusCustomer.Columns["Total"].Width = 80;
                dgvStatusCustomer.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (dgvStatusCustomer.RowCount > 0)
                {
                    dgvStatusCustomer.Rows[dgvStatusCustomer.RowCount - 1].DefaultCellStyle.BackColor = Color.CadetBlue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoAll.Checked)
                {
                    Common.StartFormLoading();
                    dataTable = _woHelper.GetAllWo(WO_Select.All);
                    Showdata(dataTable);
                }                    
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dgvStatusCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string customer = dgvStatusCustomer.Rows[e.RowIndex].Cells[0].Value == null ? "" : dgvStatusCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                    dgvPlan.DataSource = _woHelper.GetCustomerDataByName(dgvStatusCustomer, dataTable, customer);
                    lblStatus.Text = $"{dgvPlan.Rows.Count} Rows";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
