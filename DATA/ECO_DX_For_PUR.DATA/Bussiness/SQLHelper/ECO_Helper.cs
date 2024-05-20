using ECO_DX_For_PUR.DATA.Bussiness.Common;
using ECO_DX_For_PUR.DATA.Bussiness.EnumDefine;
using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.COST_SYSTEM;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using ECO_DX_For_PUR.DATA.USAPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECO_DX_For_PUR.DATA.Bussiness.SQLHelper
{
    public class ECO_Helper
    {
        DBContext _context = new DBContext();
        USAPWebServiceSoapClient _usap = new USAPWebServiceSoapClient();

        public string GetPDFlink(string eco)
        {
            return _usap.GetFullPathDocument(eco);
        }
        public List<ECO_ControlSheet> GetDataByListString(List<string> ecnResult, DateTime datefrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public List<ECO_ControlSheet> GetDataExist(string ecn, string eco, string model, string newpart)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDuplicateECO(DateTime datefrom, DateTime dateTo)
        {
            DataTable dataTable;
            var dataDuplicate = _context.ECO_ControlSheet
                .Where(e => e.ECO_No != null && e.ECN_ReceiveDate >= datefrom && e.ECN_ReceiveDate <= dateTo)
                .GroupBy(g => g.ECO_No)
                    .Where(w => w.Count() > 1).SelectMany(group => group).OrderByDescending(o => o.ECN_ReceiveDate).ToList().AsQueryable();
            return dataTable = ToDataTable(dataDuplicate);
        }
       
        static DataTable ToDataTable<T>(IEnumerable<T> query)
        {
            DataTable dataTable = new DataTable();

            // Create columns based on the entity properties
            foreach (var property in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(property.Name);
            }

            // Populate the DataTable with data from the query
            foreach (var item in query)
            {
                DataRow row = dataTable.NewRow();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (item is ECO_ControlSheet)
                    {
                        var controlSheet = item as ECO_ControlSheet;
                        string date = ConvertDate(controlSheet.ECN_ReceiveDate);
                        if (property.Name == "ECN_ReceiveDate")
                        {
                            row[property.Name] = date;
                        }
                        else
                        {
                            row[property.Name] = property.GetValue(item);
                        }
                    }
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        private static string ConvertDate(DateTime? eCN_ReceiveDate)
        {
            DateTime result;
            if (eCN_ReceiveDate != null)
            {
                if (DateTime.TryParse(eCN_ReceiveDate.ToString(), out result))
                {
                    return result.Date.ToString("MM/dd/yyyy");
                }
            }
            return null;
        }

        public List<ECO_ControlSheet> GetECNDataList(string eCNText)
        {
            throw new NotImplementedException();
        }

        public List<ECO_ControlSheet> GetECNlist()
        {
            throw new NotImplementedException();
        }

        public List<ECO_ControlSheet> GetECNPending()
        {
            return _context.ECO_ControlSheet
                    .Where(t2 => !_context.Purchase_Action.Any(t1 => t1.ECN == t2.ECN_ERI_No))
                    .Select(t2 => t2).Where(w => w.Issue_To.ToLower() == "pur").ToList()
                    .ToList();
        }

        public ECO_ControlSheet GetECOcontrolSheetByID(int? id)
        {
            using(var context = new DBContext())
            {
                return _context.ECO_ControlSheet.AsNoTracking().Where(w => w.Id == id).FirstOrDefault();
            }
        }

        public ECO_ControlSheet GetLastControlSheetUpdate()
        {
            var id = _context.HistoryUpdateControlSheets.OrderByDescending(o => o.Id).First().Last_Update_Id;
            return _context.ECO_ControlSheet.Where(w => w.Id == id).FirstOrDefault();
        }

        public string GetLocationByID(int id)
        {
            throw new NotImplementedException();
        }

        public string GetModelNameByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetModelNameIsBOMExist()
        {
            throw new NotImplementedException();
        }

        public List<string> GetModelNameNOTBOM()
        {
            using(var context = new DBContextBOM())
            {
                var controlsheetModel = _context.ECO_ControlSheet.Where(w => w.ModelFull != null).Select(s => s.ModelFull).Distinct();
                var result = context.BC_BOM_LIST
                .GroupBy(x => x.MODEL_ID)
                .Where(group => group.All(item => string.IsNullOrEmpty(item.LOCATION)))
                .Select(group => group.Key).Distinct()
                .ToList();
                foreach (var model in controlsheetModel)
                {
                    var value = context.BC_BOM_LIST
                    .Where(x => x.MODEL_ID == model)
                    .All(item => string.IsNullOrEmpty(item.LOCATION));
                    if (value) result.Add(model);
                }
                return result;
            }       
        }

        public bool InsertControlsheetNewUpdate(List<ECO_ControlSheet> controlSheetList)
        {
            try
            {
                _context.ECO_ControlSheet.AddRange(controlSheetList);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool InsertData(ECO_ControlSheet ecoControlSheet)
        {
            try
            {
                _context.ECO_ControlSheet.Add(ecoControlSheet);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool IsLastRow(ECO_ControlSheet controlSheetLastUpdate, DataRow row)
        {
            try
            {
                int? No = Utils.ConvertINT(row.Field<object>(0));
                DateTime? ECnReceiveDate = Utils.ConvertDate(row.Field<object>(1));
                string ECN = Utils.ConvertString(row.Field<object>(2));
                string ECO = Utils.ConvertString(row.Field<object>(8));
                string Model = Utils.ConvertString(row.Field<object>(9));
                if (No == controlSheetLastUpdate.No && ECnReceiveDate == controlSheetLastUpdate.ECN_ReceiveDate && ECN == controlSheetLastUpdate.ECN_ERI_No && ECO == controlSheetLastUpdate.ECO_No && Model == controlSheetLastUpdate.ModelName)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertData(List<ECO_ControlSheet> controlSheetList)
        {
            try
            {
                _context.ECO_ControlSheet.AddRange(controlSheetList);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void InsertHistoryUpdateControlSheet(int id)
        {
            try
            {
                HistoryUpdateControlSheet history = new HistoryUpdateControlSheet();
                history.Last_Update_Id = id;
                history.UpdateTime = DateTime.Now;
                _context.HistoryUpdateControlSheets.Add(history);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExistedECN(string Eco)
        {
            if (Eco != null)
            {
                var result = _context.ECO_ControlSheet.Where(w => w.ECO_No.Trim() == Eco.Trim()).FirstOrDefault();
                if (result != null)
                    return true;
            }
            return false;
        }

        public bool RemoveControlsheetData(DataGridViewRow dataGridViewRow)
        {
            try
            {
                if (dataGridViewRow != null && dataGridViewRow.Index != -1)
                {
                    int? id = Utils.ConvertINT(dataGridViewRow.Cells["Id"].Value);
                    if (id != null)
                    {
                        var controlSheet = _context.ECO_ControlSheet.Where(w => w.Id == id).FirstOrDefault();
                        if (controlSheet != null)
                        {
                            _context.ECO_ControlSheet.Remove(controlSheet);
                            _context.SaveChanges();
                            return true;
                        }                        
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private ECO_ControlSheet CreateDataEcoControlsheet(DataGridViewRow selectedRow)
        {
            try
            {
                if (selectedRow != null && selectedRow.Index != -1)
                {
                    int? id = Utils.ConvertINT(selectedRow.Cells["Id"].Value);
                    if (id != null)
                    {
                        ECO_ControlSheet controlsheet = new ECO_ControlSheet();
                        controlsheet.Id = (int)id;
                        controlsheet.No = Utils.ConvertINT(selectedRow.Cells["No"].Value);
                        controlsheet.ECN_ReceiveDate = Utils.ConvertDate(selectedRow.Cells["ECN_ReceiveDate"].Value);
                        controlsheet.ECN_ERI_No = Utils.ConvertString(selectedRow.Cells["ECN_ERI_No"].Value);
                        controlsheet.History = Utils.ConvertString(selectedRow.Cells["History"].Value);
                        controlsheet.Ver_EE = Utils.ConvertString(selectedRow.Cells["Ver_EE"].Value);
                        controlsheet.Ver_EA = Utils.ConvertString(selectedRow.Cells["Ver_EA"].Value);
                        controlsheet.Apply = Utils.ConvertString(selectedRow.Cells["Apply"].Value);
                        controlsheet.VE_Follows_ECN_CVN = Utils.ConvertString(selectedRow.Cells["VE_Follows_ECN_CVN"].Value);
                        controlsheet.ECO_No = Utils.ConvertString(selectedRow.Cells["ECO_No"].Value);
                        controlsheet.ModelName = Utils.ConvertString(selectedRow.Cells["ModelName"].Value);
                        controlsheet.OldPartCode = Utils.ConvertString(selectedRow.Cells["OldPartCode"].Value);
                        controlsheet.NewPartCode = Utils.ConvertString(selectedRow.Cells["NewPartCode"].Value);
                        controlsheet.ContentOfChange = Utils.ConvertString(selectedRow.Cells["ContentOfChange"].Value);
                        controlsheet.Location = Utils.ConvertString(selectedRow.Cells["Location"].Value);
                        controlsheet.ProcessForAssembly = Utils.ConvertString(selectedRow.Cells["ProcessForAssembly"].Value);
                        controlsheet.ECO_Issue = Utils.ConvertString(selectedRow.Cells["ECO_Issue"].Value);
                        controlsheet.FAT_Implement = Utils.ConvertString(selectedRow.Cells["FAT_Implement"].Value);
                        controlsheet.ImplementDate = Utils.ConvertString(selectedRow.Cells["ImplementDate"].Value);
                        controlsheet.ShippingDate = Utils.ConvertString(selectedRow.Cells["ShippingDate"].Value);
                        controlsheet.ECO_Status = Utils.ConvertString(selectedRow.Cells["ECO_Status"].Value);
                        controlsheet.Confirm = Utils.ConvertString(selectedRow.Cells["Confirm"].Value);
                        controlsheet.Issue_To = Utils.ConvertString(selectedRow.Cells["Issue_To"].Value);
                        controlsheet.FATContentInformation = Utils.ConvertString(selectedRow.Cells["FATContentInformation"].Value);
                        controlsheet.ModelFull = Utils.ConvertString(selectedRow.Cells["ModelFull"].Value);
                        controlsheet.TVP_No = Utils.ConvertString(selectedRow.Cells["TVP_No"].Value);
                        controlsheet.TVPResult = Utils.ConvertString(selectedRow.Cells["TVPResult"].Value);
                        controlsheet.TVP_RecieveResultDate = Utils.ConvertString(selectedRow.Cells["TVP_RecieveResultDate"].Value);
                        controlsheet.BOM_Change_When_ECO_Implement = Utils.ConvertString(selectedRow.Cells["BOM_Change_When_ECO_Implement"].Value);
                        return controlsheet;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool UpdateDataECOControlSheet(DataGridViewRow dataGridViewRow)
        {
            try
            {
                var update = CreateDataEcoControlsheet(dataGridViewRow);
                if(update != null)
                {
                    var allDataECOcontrolsheet = _context.Database.SqlQuery<ECO_ControlSheet>("exec GetAllDataECOControlSheet").ToList();
                    ECO_ControlSheet updateControlSheet = allDataECOcontrolsheet.Where(w => w.Id == update.Id).FirstOrDefault();
                    updateControlSheet = update;
                    _context.Set<ECO_ControlSheet>().AddOrUpdate(updateControlSheet);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex )
            {
                return false;
                throw ex;
            }
        }

        public IEnumerable<ECO_ControlSheet> GetDataIssueToPur(DateTime datefrom, DateTime dateTo)
        {
            var allData = GetAllControlSheetData();
            return allData.Where(w => w.ECN_ReceiveDate >= datefrom && w.ECN_ReceiveDate <= dateTo).OrderByDescending(o => o.ECN_ReceiveDate).Where(w => w.Issue_To.ToLower().Trim() == "pur").Select(e => e);
        }

        public DataTable GetDataControlSheet(IEnumerable<ECO_ControlSheet> dataIssueToPur, int value)
        {
            DataTable dataTable = ToDataTable(dataIssueToPur.Take(value));
            return dataTable;
        }

        public DataTable GetDataECOcontrolsheetSearch(int selectedIndex, string searchString)
        {
            DataTable dataTable;
            if (selectedIndex == (int)PE_DM_Enum.SelectType.ECN)
            {
                var query = _context.ECO_ControlSheet.OrderByDescending(o => o.ECN_ReceiveDate).Where(w => w.ECN_ERI_No.Contains(searchString)).ToList().AsQueryable();
                return dataTable = ToDataTable(query);
            }
            else if (selectedIndex == (int)PE_DM_Enum.SelectType.ECO)
            {
                return dataTable = ToDataTable(_context.ECO_ControlSheet.OrderByDescending(o => o.ECN_ReceiveDate).Where(w => w.ECO_No.Contains(searchString)).ToList().AsQueryable());
            }
            else if (selectedIndex == (int)PE_DM_Enum.SelectType.Model)
            {
                return dataTable = ToDataTable(_context.ECO_ControlSheet.OrderByDescending(o => o.ECN_ReceiveDate).Where(w => w.ModelName.Contains(searchString)).ToList().AsQueryable());
            }
            else if (selectedIndex == (int)PE_DM_Enum.SelectType.NO)
            {
                return dataTable = ToDataTable(_context.ECO_ControlSheet.OrderByDescending(o => o.ECN_ReceiveDate).Where(w => w.No.ToString().Contains(searchString)).ToList().AsQueryable());
            }

            return null;
        }

        public DataTable GetDataControlSheet(int record, DateTime datefrom, DateTime dateTo)
        {
            var query = GetAllControlSheetData().Where(w => w.ECN_ReceiveDate >= datefrom && w.ECN_ReceiveDate <= dateTo).OrderByDescending(o => o.ECN_ReceiveDate).Take(record).Select(e => e);
            DataTable dataTable = ToDataTable(query);
            return dataTable;
        }

        public List<ECO_ControlSheet> GetAllControlSheetData()
        {
            return _context.Database.SqlQuery<ECO_ControlSheet>("exec GetAllDataECOControlSheet").ToList();
        }

        public List<Purchase_Action> GetDataPurchaseAction(DateTime datefrom, DateTime dateTo)
        {
            return _context.Purchase_Action.AsNoTracking().Where(w => w.ReleaseDate >= datefrom && w.ReleaseDate <= dateTo).OrderByDescending(o => o.Id).ToList();
        }
        public DataTable CheckUpdateECOcontrolsheetFromExcelFile(DataTable dataControlSheetDM)
        {
            try
            {
                dataControlSheetDM = ReverseDataTableRows(dataControlSheetDM);
                ECO_ControlSheet controlSheetLastUpdate = GetLastControlSheetUpdate();
                var resultData = dataControlSheetDM.Clone();
                foreach (DataRow row in dataControlSheetDM.Rows)
                {
                    DateTime? date = Utils.ConvertDate(row.Field<object>(1));
                    if (date == null || row.Field<object>(2) == null ) continue;
                    else
                    {
                        if (IsLastRow(controlSheetLastUpdate, row))
                        {
                            return resultData;
                        }
                        else
                        {
                            resultData.ImportRow(row);
                            if (resultData.Rows.Count >= dataControlSheetDM.Rows.Count)
                            {
                                return null;
                            }
                        }
                    }
                }
                return resultData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void InsertEcoControlsheetNewUpdate(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                dataTable = ReverseDataTableRows(dataTable);
                List<ECO_ControlSheet> controlSheetList = new List<ECO_ControlSheet>();
                foreach (DataRow row in dataTable.Rows)
                {
                    ECO_ControlSheet controlSheet = new ECO_ControlSheet();
                    controlSheet.No = Utils.ConvertINT(row.Field<object>(0));
                    controlSheet.ECN_ReceiveDate = Utils.ConvertDate(row.Field<object>(1));
                    controlSheet.ECN_ERI_No = Utils.ConvertString(row.Field<object>(2));
                    if (controlSheet.ECN_ReceiveDate == null || controlSheet.ECN_ERI_No == null) continue;
                    controlSheet.History = Utils.ConvertString(row.Field<object>(3));
                    controlSheet.Ver_EE = Utils.ConvertString(row.Field<object>(4));
                    controlSheet.Ver_EA = Utils.ConvertString(row.Field<object>(5));
                    controlSheet.Apply = Utils.ConvertString(row.Field<object>(6));
                    controlSheet.VE_Follows_ECN_CVN = Utils.ConvertString(row.Field<object>(7));
                    controlSheet.ECO_No = Utils.ConvertString(row.Field<object>(8));
                    controlSheet.ModelName = Utils.ConvertString(row.Field<object>(9));
                    controlSheet.OldPartCode = Utils.ConvertString(row.Field<object>(10));
                    controlSheet.NewPartCode = Utils.ConvertString(row.Field<object>(11));
                    controlSheet.ContentOfChange = Utils.ConvertString(row.Field<object>(12));
                    controlSheet.Location = Utils.ConvertString(row.Field<object>(13));
                    controlSheet.ProcessForAssembly = Utils.ConvertString(row.Field<object>(14));
                    controlSheet.ECO_Issue = Utils.ConvertString(row.Field<object>(15));
                    controlSheet.FAT_Implement = Utils.ConvertString(row.Field<object>(16));
                    controlSheet.ImplementDate = Utils.ConvertString(row.Field<object>(17));
                    controlSheet.ShippingDate = Utils.ConvertString(row.Field<object>(18));
                    controlSheet.ECO_Status = Utils.ConvertString(row.Field<object>(19));
                    controlSheet.Confirm = Utils.ConvertString(row.Field<object>(20));
                    controlSheet.Issue_To = Utils.ConvertString(row.Field<object>(21));
                    controlSheet.FATContentInformation = Utils.ConvertString(row.Field<object>(22));
                    controlSheet.ModelFull = Utils.ConvertString(row.Field<object>(23));
                    controlSheet.TVP_No = Utils.ConvertString(row.Field<object>(24));
                    controlSheet.TVPResult = Utils.ConvertString(row.Field<object>(25));
                    controlSheet.TVP_RecieveResultDate = Utils.ConvertString(row.Field<object>(26));
                    controlSheet.BOM_Change_When_ECO_Implement = Utils.ConvertString(row.Field<object>(27));
                    controlSheetList.Add(controlSheet);
                }
                if (controlSheetList.Count > 0)
                {
                    if (InsertControlsheetNewUpdate(controlSheetList))
                    {
                        InsertHistoryUpdateControlSheet(controlSheetList.Last().Id);
                    }
                }
            }
        }
        private DataTable ReverseDataTableRows(DataTable originalDataTable)
        {
            try
            {
                // Clone the structure of the original DataTable
                DataTable reversedDataTable = originalDataTable.Clone();

                // Add rows in reverse order
                for (int i = originalDataTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow originalRow = originalDataTable.Rows[i];
                    reversedDataTable.ImportRow(originalRow);
                }

                return reversedDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return null;
            }
        }
    }
}
