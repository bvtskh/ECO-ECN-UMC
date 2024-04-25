using ECO_DX_For_PUR.DATA.Bussiness.Common;
using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using ECO_DX_For_PUR.DATA.Entities.UMC3000;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECO_DX_For_PUR.DATA.Bussiness.SQLHelper
{

    public class ECO_Schedule_Helper : IECO_SCHEDULE
    {
        DBContext _context = new DBContext();
        public bool InsertEcoSchedule(ECOSchedule ecoSchedule)
        {
            try
            {
                _context.ECOSchedules.Add(ecoSchedule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public List<ECOSchedule> GetAllDataECOSchedule()
        {
            using (DBContext context = new DBContext())
            {
                return context.ECOSchedules.ToList();
            }
        }

        public bool IsExistECOData(ECOSchedule ecoSchedule)
        {
            var ecoExist = _context.ECOSchedules.Where(w=> w.ECN_DCI_NO == ecoSchedule.ECN_DCI_NO && w.MODEL == ecoSchedule.MODEL).FirstOrDefault();
            if (ecoExist != null) return true;
            return false;
        }

        public void UpdateECOScheduleFromBase()
        {
            var baseECOList = GetUMC3000ECOList();
            using(var context = new DBContext())
            {
                var pendingSchedule = context.ECOSchedules.Where(w => string.IsNullOrEmpty(w.ECO_NO)).ToList();
                foreach (var item in pendingSchedule)
                {
                    string model = item.MODEL;
                    string ecn = item.ECN_DCI_NO;
                    string eco = baseECOList.Where(w => w.PART_NO == model && w.ATT_DOC == ecn).Select(s => s.ECO_NO).FirstOrDefault();
                    if (!string.IsNullOrEmpty(eco))
                    {
                        item.ECO_NO = eco;
                    }
                }
                context.SaveChanges();
            }          
        }

        private List<ENG_ECOHDRM> GetUMC3000ECOList()
        {
            using (var context = new UMC3000DBContext())
            {
                return context.ENG_ECOHDRM.ToList();
            }
        }

        public ECOSchedule GetExistECOData(ECOSchedule ecoSchedule)
        {
            var ecoExist = _context.ECOSchedules.Where(w => w.ECN_DCI_NO == ecoSchedule.ECN_DCI_NO && w.MODEL == ecoSchedule.MODEL).FirstOrDefault();
            if (ecoExist != null) return ecoExist;
            return null;
        }

        public DataTable ToDatatable(bool check,DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                List<ECOSchedule> data = new List<ECOSchedule>();
                if (check)
                {
                    data = GetAllDataECOSchedule().OrderByDescending(o => o.ID).Where(w => string.IsNullOrEmpty(w.ECO_NO)).OrderByDescending(o=>o.RECEIVE_DOCUMENT_DATE).ToList();
                }
                else
                {
                    data = GetAllDataECOSchedule().Where(w => w.RECEIVE_DOCUMENT_DATE >= dateFrom && w.RECEIVE_DOCUMENT_DATE <= dateTo).OrderByDescending(o => o.RECEIVE_DOCUMENT_DATE).ToList();
                }
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Ngày nhận tài liệu");
                dataTable.Columns.Add("Ngày triển khai");
                dataTable.Columns.Add("Model");
                dataTable.Columns.Add("Nội dung thay đổi");
                dataTable.Columns.Add("ECN, DCI No.");
                dataTable.Columns.Add("Ngày bắt đầu áp dụng");
                dataTable.Columns.Add("UMC ECO No.");
                dataTable.Columns.Add("REMARK");
                foreach (var item in data)
                {
                    var row = dataTable.NewRow();
                    row["Ngày nhận tài liệu"] = Utils.ConvertDate(item.RECEIVE_DOCUMENT_DATE);
                    row["Ngày triển khai"] = Utils.ConvertDate(item.IMPLEMENT_DATE);
                    row["Model"] = item.MODEL;
                    row["Nội dung thay đổi"] = item.CONTENT_CHANGE;
                    row["ECN, DCI No."] = item.ECN_DCI_NO;
                    row["Ngày bắt đầu áp dụng"] = Utils.ConvertDate(item.START_APPROVE_DATE);
                    row["UMC ECO No."] = item.ECO_NO;
                    row["REMARK"] = item.REMARK;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ECOSchedule GetDataUpdate(DataGridViewRow selectRow)
        {
            try
            {
                ECOSchedule eCOSchedule = new ECOSchedule();
                eCOSchedule.RECEIVE_DOCUMENT_DATE = Utils.ConvertDate(selectRow.Cells["Ngày nhận tài liệu"].Value);
                eCOSchedule.IMPLEMENT_DATE = Utils.ConvertDate(selectRow.Cells["Ngày triển khai"].Value);
                eCOSchedule.START_APPROVE_DATE = Utils.ConvertDate(selectRow.Cells["Ngày bắt đầu áp dụng"].Value);
                eCOSchedule.CONTENT_CHANGE = Utils.ConvertString(selectRow.Cells["Nội dung thay đổi"].Value);
                eCOSchedule.MODEL = Utils.ConvertString(selectRow.Cells["Model"].Value);
                eCOSchedule.ECN_DCI_NO = Utils.ConvertString(selectRow.Cells["ECN, DCI No."].Value);
                eCOSchedule.ECO_NO = Utils.ConvertString(selectRow.Cells["UMC ECO No."].Value);
                eCOSchedule.REMARK = Utils.ConvertString(selectRow.Cells["REMARK"].Value);
                return eCOSchedule;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable GetDataFromDGV(DataGridView dataGridView)
        {
            try
            {
                // Create a DataTable with the same columns as in the DataGridView
                var dataTable = new DataTable();

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    dataTable.Columns.Add(column.Name);
                }

                // Transfer data from DataGridView to DataTable
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }

                    dataTable.Rows.Add(dataRow);
                }
                return dataTable;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public bool UpdateECOSchedule(ECOSchedule dataUpdate, int id)
        {
             using(var transaction = _context.Database.BeginTransaction())
             {
                try
                {
                    var currentData = _context.ECOSchedules.Where(w => w.ID == id).FirstOrDefault();
                    currentData.RECEIVE_DOCUMENT_DATE = dataUpdate.RECEIVE_DOCUMENT_DATE;
                    currentData.IMPLEMENT_DATE = dataUpdate.IMPLEMENT_DATE;
                    currentData.START_APPROVE_DATE = dataUpdate.START_APPROVE_DATE;
                    currentData.ECN_DCI_NO = dataUpdate.ECN_DCI_NO;
                    currentData.ECO_NO = dataUpdate.ECO_NO;
                    currentData.MODEL = dataUpdate.MODEL;
                    currentData.CONTENT_CHANGE = dataUpdate.CONTENT_CHANGE;
                    currentData.REMARK = dataUpdate.REMARK;
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                    throw ex;
                }
            }            
        }

        public bool InsertEcoSchedule(List<ECOSchedule> ecoSchedule)
        {
            try
            {
                _context.ECOSchedules.AddRange(ecoSchedule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool InsertAndUpdateEcoSchedule(List<ECOSchedule> ecoSchedule)
        {
            try
            {
                foreach (var item in ecoSchedule)
                {
                    var existData = GetExistECOData(item);
                    if (existData != null)
                    {
                        existData.RECEIVE_DOCUMENT_DATE = item.RECEIVE_DOCUMENT_DATE;
                        existData.IMPLEMENT_DATE = item.IMPLEMENT_DATE;
                        existData.START_APPROVE_DATE = item.START_APPROVE_DATE;
                        existData.MODEL = item.MODEL;
                        existData.CONTENT_CHANGE = item.CONTENT_CHANGE;
                        existData.ECN_DCI_NO = item.ECN_DCI_NO;
                        existData.ECO_NO = item.ECO_NO;
                        existData.REMARK = item.REMARK;
                    }
                    else
                    {
                        _context.ECOSchedules.Add(item);
                    }
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
