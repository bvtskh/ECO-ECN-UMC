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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.ECO_Schedule_Enum;

namespace ECO_DX_For_PUR.GUI
{
    public partial class FormECOSchedule : Form
    {
        DataTable _datatable;
        ECO_Schedule_Helper _schedule_Helper = new ECO_Schedule_Helper();
        bool firstShow = false;
        public FormECOSchedule()
        {
            InitializeComponent();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            FormInsertECOSchedule f = new FormInsertECOSchedule(ECO_Edit.Insert, null); ;
            f.Show();
        }

        private void FormECOSchedule_Load(object sender, EventArgs e)
        {
            dateFrom.Value = DateTime.Now.AddDays(-150);
            dateTo.Value = DateTime.Now;
            _schedule_Helper.UpdateECOScheduleFromBase();
        }

       
        public void ShowECOScheduleData()
        {
            this.bindingSource1.Sort = null;
            this.bindingSource1.Filter = null;
            try
            {
                Common.StartFormLoading();
                _datatable = _schedule_Helper.ToDatatable(checkboxPending.Checked,dateFrom.Value,dateTo.Value);

                this.bindingSource1.DataSource = _datatable;
                dgvECO_Schedule.DataSource = this.bindingSource1;
                lblStatus.Text = _datatable.Rows.Count + " Rows";
                dgvECO_Schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvECO_Schedule.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvECO_Schedule.Columns["Nội dung thay đổi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                Common.CloseFormLoading();
            }
            catch (Exception ex)
            {
                Common.CloseFormLoading();
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        DataGridViewRow selectRow;
        private void dgvECO_Schedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectRow = dgvECO_Schedule.Rows[e.RowIndex];
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectRow != null && selectRow.Index != -1)
                {
                    ECOSchedule eCOSchedule = _schedule_Helper.GetDataUpdate(selectRow);
                    FormInsertECOSchedule f = new FormInsertECOSchedule(ECO_Edit.Update, eCOSchedule);
                    f.Show();
                }
                else
                {
                    FormInsertECOSchedule f = new FormInsertECOSchedule(ECO_Edit.Insert, null);
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dgvECO_Schedule_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Sort = this.dgvECO_Schedule.SortString;
        }

        private void dgvECO_Schedule_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Filter = this.dgvECO_Schedule.FilterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var listData = _schedule_Helper.GetDataFromDGV(dgvECO_Schedule);
                if (ExcelServices.ExportData(listData))
                {                   
                    MessageBox.Show("Data saved to Excel successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
       
        private void checkboxPending_CheckedChanged(object sender, EventArgs e)
        {
            firstShow = true;
            ShowECOScheduleData();
        }

        private void date_ValueChanged(object sender, DateTime value)
        {
            if(firstShow)
            ShowECOScheduleData();
        }
    }
}
