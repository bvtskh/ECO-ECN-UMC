using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using ECO_DX_For_PUR.Utils;
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
    public partial class FormEditWoRelationship : Form
    {
        DataGridViewRow dataGridViewRow;
        Guid? OrderBase;
        WOHelper _woHelper = new WOHelper();
        public FormEditWoRelationship(DataGridViewRow selectRow)
        {
            InitializeComponent();
            dataGridViewRow = selectRow;
            OrderBase = _woHelper.GetOrderBase(dataGridViewRow);
        }

        private void FormEditWoRelationship_Load(object sender, EventArgs e)
        {
            foreach(DataGridViewColumn col in dataGridViewRow.DataGridView.Columns)
            {
                dgvEditWo.Columns.Add(col.Name,col.HeaderText);
            }
            dgvEditWo.Rows.Add();
            for(int data =0; data < dataGridViewRow.Cells.Count ; data++)
            {
                dgvEditWo.Rows[0].Cells[data].Value = dataGridViewRow.Cells[data].Value;
            }
            dgvEditWo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {            
            if (_woHelper.EditWoRow(dgvEditWo.Rows[0], OrderBase))
            {
                MessageBox.Show("Sửa thành công!");
                FormWORelationship formWORelationship = (FormWORelationship)Common.FindForm("FormWORelationship");
                if (formWORelationship != null)
                {
                    formWORelationship.ShowDataWo(DataStatus.Success);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thành công!");
            }

        }
    }
}
