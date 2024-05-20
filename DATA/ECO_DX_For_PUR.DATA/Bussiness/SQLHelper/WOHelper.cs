using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Entities;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using ECO_DX_For_PUR.DATA.Entities.PI_BASE;
using ECO_DX_For_PUR.DATA.USAPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.Plan_Enum;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.Relationship_Enum;

namespace ECO_DX_For_PUR.DATA.Bussiness.SQLHelper
{
    public class WOHelper
    {
        PI_BASEHelper _base = new PI_BASEHelper();
        DBContext _context = new DBContext();
        USAPService.USAPWebServiceSoapClient _usap = new USAPWebServiceSoapClient();
        static DataTable dataTable;
        public List<Area> GetAreaList()
        {
            return _context.Areas.ToList();
        }

        public List<string> GetCustomer()
        {

            throw new NotImplementedException();
        }

        public string GetECONoByOrderNo(IGrouping<Guid?, WO_Relationship> group)
        {

            foreach (var item in group)
            {
                var eco = _context.WoChangings.Where(w => w.ORDER_NO == item.ORDER_NO).Select(s => s.ECO_NO).FirstOrDefault();
                if (!string.IsNullOrEmpty(eco)) return eco;
            }
            return null;
        }

        public Guid? GetOrderBase(DataGridViewRow dataGridViewRow)
        {
            List<string> woList = GetWoList(dataGridViewRow);
            using(var context = new DBContext())
            {
                var orderBase = context.WO_Relationship
                .Where(w => woList.All(s => context.WO_Relationship.Any(a => a.ORDER_BASE == w.ORDER_BASE && a.ORDER_NO == s)))
                .Select(ss => ss.ORDER_BASE)
                .FirstOrDefault();
                return orderBase;
            }
        }

        private List<string> GetWoList(DataGridViewRow dataGridViewRow)
        {
            List<string> woList = new List<string>();
            woList.Add(GetValueWoRow(dataGridViewRow.Cells["AI WO"].Value));
            woList.Add(GetValueWoRow(dataGridViewRow.Cells["SMTA WO"].Value));
            woList.Add(GetValueWoRow(dataGridViewRow.Cells["SMTB WO"].Value));
            woList.Add(GetValueWoRow(dataGridViewRow.Cells["SMT WO"].Value));
            woList.AddRange(GetFATwo(dataGridViewRow));
            woList.RemoveAll(r => string.IsNullOrEmpty(r));
            return woList;
        }

        public bool EditWoRow(DataGridViewRow dataGridViewRow,Guid? orderBase)
        {
            string AI = GetValueWoRow(dataGridViewRow.Cells["AI WO"].Value);
            string SMTA = GetValueWoRow(dataGridViewRow.Cells["SMTA WO"].Value);
            string SMTB = GetValueWoRow(dataGridViewRow.Cells["SMTB WO"].Value);
            string SMT = GetValueWoRow(dataGridViewRow.Cells["SMT WO"].Value);
            List<string> FAT = GetFATwo(dataGridViewRow);
            List<string> woList = GetWoList(dataGridViewRow);
            if (SMTA == SMTB) return false;
            string pendingWo = GetPendingWo(woList);
            if(pendingWo != null)
            {
                if ((!string.IsNullOrEmpty(SMT) || !string.IsNullOrEmpty(SMTA) || !string.IsNullOrEmpty(SMTB)) && FAT.Count > 0) // neu co it nhat 1 smt va 1 fat
                {
                    if (IsValidWo(woList))
                    {
                        if (UpdateWoRelationship(orderBase,AI,SMT,SMTA,SMTB,FAT)) return true;
                    }
                }
            }
            return false;
        }

        private bool UpdateWoRelationship(Guid? orderBase, string AI, string SMT, string SMTA, string SMTB, List<string> FAT)
        {
            using(var context = new DBContext())
            {
                using(var transaction  = context.Database.BeginTransaction())
                {
                    try
                    {
                        var baselist = context.WO_Relationship.Where(w => w.ORDER_BASE == orderBase).ToList();
                        context.WO_Relationship.RemoveRange(baselist);
                        var orderbaseNew = Guid.NewGuid();
                        if (!string.IsNullOrEmpty(AI)) context.WO_Relationship.Add(GetSubWo(AI, orderbaseNew, 0, "AI"));
                        if (!string.IsNullOrEmpty(SMTA)) context.WO_Relationship.Add(GetSubWo(SMTA, orderbaseNew, 1, "SMTA"));
                        if (!string.IsNullOrEmpty(SMTB))  context.WO_Relationship.Add(GetSubWo(SMTB, orderbaseNew, 2, "SMTB"));
                        if (!string.IsNullOrEmpty(SMT)) context.WO_Relationship.Add(GetSubWo(SMT, orderbaseNew, 3, "SMT"));
                        foreach (var item in FAT)
                        {
                            if (!string.IsNullOrEmpty(item)) context.WO_Relationship.Add(GetSubWo(item, orderbaseNew, 4, "FAT"));
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                        throw;
                    }                    
                }
            }
        }
        private static WO_Relationship GetSubWo(string Wo, Guid guid, int TypeID, string typeName)
        {
            try
            {
                WO_Relationship subWO = new WO_Relationship();
                subWO.ORDER_NO = Wo;
                subWO.TYPE_ID = TypeID;
                subWO.TYPE_NAME = typeName;
                subWO.ORDER_BASE = guid;
                subWO.QTY = null;
                subWO.UPDATE_NAME = "";
                subWO.HOST_NAME = System.Environment.MachineName;
                subWO.UPDATE_TIME = DateTime.Now;
                subWO.CHANGE_ID = null;
                subWO.CHANGE_NAME = null;
                return subWO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetWaitingTime(object value)
        {
            return int.Parse(value.ToString());
        }

        private string GetPendingWo(List<string> woList)
        {
            foreach(var item in woList)
            {
                var data = _context.WoChangings.Where(w => w.ORDER_NO == item).FirstOrDefault();
                if (data != null) return data.ORDER_NO;
            }
            return null;
        }

        private bool IsValidWo(List<string> woList)
        {
            int result;
            foreach(var wo in woList)
            {
                if (!string.IsNullOrEmpty(wo))
                {
                    if (wo.Length == 10)
                    {
                        if (Int32.TryParse(wo, out result))
                        {
                            if (wo.Substring(0, 4) == "2000")
                            {
                                return true;
                            }
                        }
                    }
                }
            }          
            return false;
        }
        private List<string> GetFATwo(DataGridViewRow dataGridViewRow)
        {
            List<string> listFATwo = new List<string>();
            for (int i = dataGridViewRow.Cells["SMT WO"].ColumnIndex + 1; i < dataGridViewRow.Cells.Count; i++)
            {
                var wo = GetValueWoRow(dataGridViewRow.Cells[i].Value);
                if (wo != null) listFATwo.Add(wo);
            }
            return listFATwo;
        }

        private string GetValueWoRow(object value)
        {
            if (value != null) return value.ToString();
            return null;
        }

        public List<WoChanging> GetPendingWO()
        {
            using (var context = new DBContext())
            {
                return context.Database.SqlQuery<WoChanging>("exec GetPendingWoChanging")
                .Where(e2 => !context.WO_Relationship.Select(e1 => e1.ORDER_NO).Contains(e2.ORDER_NO)).ToList()
                .GroupBy(e => e.ORDER_NO)
                .Select(g => g.FirstOrDefault()).ToList();
            }
        }

        // UsapServices.USAPWebServiceSoapClient service = new UsapServices.USAPWebServiceSoapClient();
        public List<WoChanging> GetWOChanging()
        {
            return _context.WoChangings.ToList();
        }
        private static DataTable createatable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ORDER_NO");
            dataTable.Columns.Add("ECO_NO");
            dataTable.Columns.Add("DEPT_ORD");
            dataTable.Columns.Add("DEPT_NAME");
            dataTable.Columns.Add("TYPE_NAME");
            dataTable.Columns.Add("DUE_DATE");
            dataTable.Columns.Add("TYPE_ID");
            dataTable.Columns.Add("MODEL");
            dataTable.Columns.Add("CUSTOMER");
            return dataTable;
        }
        enum ColumnExist
        {
            No,
            Yes
        }
        public DataTable GetDataFromDGV(DataGridView dgvWO, bool active)
        {
            try
            {
                List<string> columnWo = new List<string> { "AI WO", "SMTA WO", "SMTB WO", "SMT WO", "FAT WO" };
                // Create a DataTable with the same columns as in the DataGridView
                var dataTable = new DataTable();

                foreach (DataGridViewColumn column in dgvWO.Columns)
                {
                    dataTable.Columns.Add(column.Name);
                }

                // Transfer data from DataGridView to DataTable
                foreach (DataGridViewRow row in dgvWO.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }

                    dataTable.Rows.Add(dataRow);
                }
                if (active)
                {
                    List<string> Column = ExistedColumn(dataTable, columnWo, ColumnExist.Yes);
                    AddColumn(dataTable, Column);
                }
                else
                {
                    List<string> Column = ExistedColumn(dataTable, columnWo, ColumnExist.No);
                    RemoveColum(dataTable, Column);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return null;
            }
        }
        private void RemoveColum(DataTable data, List<string> column)
        {
            foreach (var col in column)
            {
                data.Columns.Remove(col);
            }
        }

        private void AddColumn(DataTable data, List<string> column)
        {
            foreach (var col in column)
            {
                data.Columns.Add(col);
            }
        }
        private List<string> ExistedColumn(DataTable data, List<string> columnWo, ColumnExist exist)
        {
            List<string> existColumn = new List<string>();
            List<string> notExistColumn = new List<string>();
            foreach (var column in columnWo)
            {
                if (data.Columns.Cast<DataColumn>().Where(w => w.ColumnName == column) == null)
                {
                    notExistColumn.Add(column);
                }
                else
                {
                    existColumn.Add(column);
                }
            }
            if (exist == ColumnExist.Yes) return existColumn;
            if (exist == ColumnExist.No) return notExistColumn;
            return null;
        }

        public static List<WOPending> GetWOPlaning(WO_Select check)
        {
            using (var context = new DBContext())
            {
                if (check ==  WO_Select.All)// all
                {
                    return context.Database.SqlQuery<WOPending>("exec GetAllWOChanging").ToList();
                }
                else if (check == WO_Select.Pending) //pending
                {
                    DataTable dataTable = createatable();
                    return context.Database.SqlQuery<WOPending>("exec GetAllECOPending").ToList();
                }
                return null;
            }
        }
        public static void UpdatePendingWoPlan()
        {
            try
            {
                List<WOPending> dataWoChangingBeforUpdate = GetWOPlaning(WO_Select.All).ToList();
                USAPWebServiceSoapClient _usap = new USAPWebServiceSoapClient();
                dataTable = CreateTableUpdate();
                GetDataUpdate(dataWoChangingBeforUpdate, _usap, dataTable);
                using (var context = new DBContext())
                {
                    var dataParam = new SqlParameter("@Data", dataTable)
                    {
                        TypeName = "udt_UpdateWoPlan",
                        SqlDbType = SqlDbType.Structured
                    };
                    context.Database.ExecuteSqlCommand("exec UpdatePlan @Data", dataParam);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void GetDataUpdate(List<WOPending> dataWoChanging, USAPWebServiceSoapClient _usap,DataTable dataTable)
        {
            using (var context = new DBContext())
            {
                var ecoList = dataWoChanging.Select(s => s.ECO_NO).Distinct().ToList();
                foreach (var eco in ecoList)
                {
                    var dataUsap = _usap.GetECO(eco);
                    if (dataUsap == null) continue;
                    AddRowToTable(dataUsap, dataTable);
                    var dataChange = dataWoChanging.Where(w => w.ECO_NO == eco).FirstOrDefault();
                    var infoExist = context.WochangingInfoAdvanced.Where(w => w.ECO_NO == eco && w.MODEL == dataUsap.PART_NO && w.CUSTOMER == dataUsap.CUS_CODE).FirstOrDefault();
                    if (infoExist == null)
                    {
                        WochangingInfoAdvanced wochangingInfoAdvanced = new WochangingInfoAdvanced();
                        wochangingInfoAdvanced.ECO_NO = eco;
                        wochangingInfoAdvanced.MODEL = dataUsap.PART_NO;
                        wochangingInfoAdvanced.CUSTOMER = dataUsap.CUS_CODE;
                        context.WochangingInfoAdvanced.Add(wochangingInfoAdvanced);
                        context.SaveChanges();
                    }
                    else
                    {
                        infoExist.ECO_NO = eco;
                        infoExist.MODEL = dataUsap.PART_NO;
                        infoExist.CUSTOMER = dataUsap.CUS_CODE;
                        context.Entry(infoExist).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }

                    //dataChange.CUSTOMER = dataUsap.CUS_CODE;
                    //dataChange.MODEL = dataUsap.PART_NO;
                    //wOPendings.Add(dataChange);
                }
            }
        }

        private static void AddRowToTable(ENG_ECOHDRMEntity dataUsap, DataTable dataTable)
        {
            dataTable.Rows.Add(dataUsap.ECO_NO, dataUsap.ORD_NO, dataUsap.DEPT_CODE);
        }

        private static DataTable CreateTableUpdate()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ECO_NO", typeof(string));
            dataTable.Columns.Add("DEPT_ORD", typeof(string));
            dataTable.Columns.Add("DEPT_NAME", typeof(string));
            return dataTable;
        }

        public DataTable GetAllWo(WO_Select select)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var stateParam = new SqlParameter("@state", (int)select);
                    var dataTable = new DataTable();
                    using (var connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "ShowPlanWOChanging"; // Thay tên stored procedure của bạn

                            // Thêm tham số vào command
                            command.Parameters.Add(stateParam);

                            connection.Open();

                            using (var reader = command.ExecuteReader())
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                    return dataTable;
                }
            }

            //List<WOPending> dataWoChangingAfterUpdate = GetWOPlaning(select).OrderBy(o => o.ORDER_NO).ToList();
            //if (select == WO_Select.Pending)
            //{
            //    dataWoChangingAfterUpdate.RemoveAll(r => r.DEPT_ORD >= 15);
            //    dataWoChangingAfterUpdate = dataWoChangingAfterUpdate.OrderBy(o => o.DEPT_ORD).ThenBy(t => t.TYPE_ID).ThenBy(t1 => t1.ORDER_NO).ToList();
            //}

            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Customer");
            //dataTable.Columns.Add("Model");
            //dataTable.Columns.Add("WO");
            //dataTable.Columns.Add("ECO No");
            //dataTable.Columns.Add("Section");
            //dataTable.Columns.Add("Kitting Date");
            //dataTable.Columns.Add("SMT Status");
            //dataTable.Columns.Add("FAT Status");
            //var customerList = _base.GetCustomer();
            //using (var context = new DBContext())
            //{
            //    foreach (var item in dataWoChangingAfterUpdate)
            //    {                       
            //        var eco = wOPendings.FirstOrDefault(m => m.ECO_NO == item.ECO_NO);
            //        if (eco == null) continue;                       
            //        DataRow row = dataTable.NewRow();
            //        row["Customer"] = ConvertShortCustomerName(eco.CUSTOMER, customerList);
            //        row["Model"] = eco == null ? "" : eco.MODEL;
            //        row["WO"] = item.ORDER_NO;
            //        row["ECO No"] = item.ECO_NO;
            //if (IsROMwo(item.ORDER_NO))
            //        {
            //row["Section"] = GetType(item.TYPE_ID) + "(ROM)";
            //        }
            //        else
            //        {
            //            row["Section"] = GetType(item.TYPE_ID);
            //        }
            //        row["Kitting Date"] = item.DUE_DATE.ToString("MM/dd/yyyy");
            //       row["SMT Status"] = GetStatusWO(item.DEPT_ORD, "SMT"); // 14 => ok
            //        row["FAT Status"] = GetStatusWO(item.DEPT_ORD, "FAT"); //15 => ok
            //        dataTable.Rows.Add(row);
            //    }
            //    return dataTable;
            //}


            catch (Exception ex)
            {
                return null;
            }
        }

        private bool IsROMwo(string oRDER_NO)
        {
            using(var context  = new DBContext())
            {
                var existData = context.WoChangings.Where(w => w.ORDER_NO == oRDER_NO && w.TYPE_ID == 5 && context.WO_Relationship.Where(w1=>w1.ORDER_NO == oRDER_NO).FirstOrDefault() != null).FirstOrDefault();
                if(existData!=null)
                return true;
            }
            return false;
        }

        private string ConvertShortCustomerName(string cUSTOMER,List<Customer> customerList)
        { 
            return customerList.Where(w => cUSTOMER.StartsWith(w.CUSTOMER_ID)).Select(s => s.CUSTOMER_NAME).FirstOrDefault() ?? cUSTOMER;
        }

        public DataTable GetCustomerFromDatatable(DataTable dataTable)
        {
            DataTable data = new DataTable();
            data.Columns.Add("Customer");
            data.Columns.Add("Total");
            Dictionary<string, int> customerDetail = new Dictionary<string, int>();

            // Loop through each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                string name = row["Customer"].ToString();

                // If name is already in dictionary, increment count
                if (customerDetail.ContainsKey(name))
                {
                    customerDetail[name]++;
                }
                // If name is not in dictionary, add it with count 1
                else
                {
                    customerDetail.Add(name, 1);
                }
            }
            int count = 0;
            foreach (var item in customerDetail.OrderByDescending(o => o.Value))
            {
                data.Rows.Add(item.Key, item.Value);
                count += item.Value;
            }
            data.Rows.Add("Total", count);
            return data;
        }

        public List<WO_Relationship> GetWOFATByBase(IGrouping<Guid?, WO_Relationship> group, WOTYPE wo_Type)
        {
            return group.Where(w => w.TYPE_ID == (int)wo_Type).ToList();
        }

        public DataTable GetCustomerDataByName(DataGridView dgvStatusCustomer, DataTable dataTable,string customer)
        {           
            if (customer != "Total")
            {
                var query = from DataRow row in dataTable.Rows
                        where row.Field<string>("Customer") == customer
                        select row;
                // Copy filtered rows to the new DataTable
                DataTable data = new DataTable();
                data = dataTable.Clone();
                foreach (DataRow row in query)
                {
                    data.Rows.Add(row.ItemArray);
                }
                return data;
            }
            else
            {
                return dataTable;
            }          
        }

        public object GetWOByBase(IGrouping<Guid?, WO_Relationship> group, WOTYPE wo_Type)
        {
            return group.Where(w => w.TYPE_ID == (int)wo_Type).Select(s => s.ORDER_NO).FirstOrDefault();
        }

        public static object GetType(int tYPE_ID)
        {
            switch (tYPE_ID)
            {
                case 0:
                    {
                        return "AI";
                    }
                case 1:
                    {
                        return "SMTA";
                    }
                case 2:
                    {
                        return "SMTB";
                    }
                case 3:
                    {
                        return "SMT";
                    }
                case 4:
                    {
                        return "FAT";
                    }
                case 5:
                    {
                        return "SMT(ROM)";
                    }
                default: return null;
            }
        }

        public DataTable GetWoRowSearching(DataTable dataTable ,string txt)
        {
            // New DataTable to store rows containing "hello"
            DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the original DataTable

            // LINQ query to filter rows containing "hello"
            var query = from DataRow row in dataTable.Rows
                        let values = row.ItemArray.Select(x => x.ToString())
                        where values.Any(value => value.ToLower().Contains(txt.ToLower()))
                        select row;

            // Copy filtered rows to the new DataTable
            foreach (DataRow row in query)
            {
                filteredDataTable.Rows.Add(row.ItemArray);
            }
            return filteredDataTable;
        }

        private string GetStatusWO(int? dept_order, string code)
        {
            if (code == "SMT") //SMT
            {
                if (dept_order >= 14)
                {
                    return "OK";
                }
                else if (dept_order < 14)
                {
                    return "Pending";
                }
                else return "";
            }
            if (code == "FAT") //FAT
            {
                if (dept_order >= 15)
                {
                    return "OK";
                }
                else if (dept_order < 15)
                {
                    return "Pending";
                }
                else return "";
            }
            return "";
        }

        public DataTable ToDataWORelationship()
        {
            try
            {               
                var dataList = GetWORelationList().OrderByDescending(o => o.ID).ToList();
                var groupData = dataList.GroupBy(g => g.ORDER_BASE);
                int maximumFatWo = GetMaximumFatWo(groupData);

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Customer");
                dataTable.Columns.Add("Model");
                dataTable.Columns.Add("AI WO");
                dataTable.Columns.Add("SMTA WO");
                dataTable.Columns.Add("SMTB WO");
                dataTable.Columns.Add("SMT WO");

                for(int i = 1; i < maximumFatWo+1; i++)
                {
                    dataTable.Columns.Add($"FAT WO {i}");
                }
                            
                var customerList = _base.GetCustomer();
                foreach (var group in groupData) // tách thành group theo key.
                {
                    var dataUsap = _usap.GetECO(GetECONoByOrderNo(group));
                    if (dataUsap == null) continue;
                    var row = dataTable.NewRow();
                    row["Customer"] = ConvertShortCustomerName(dataUsap.CUS_CODE, customerList);
                    row["Model"] = dataUsap.PART_NO;
                    row["AI WO"] = GetWOByBase(group, WOTYPE.AI);
                    row["SMTA WO"] = GetWOByBase(group, WOTYPE.SMTA);
                    row["SMTB WO"] = GetWOByBase(group, WOTYPE.SMTB);
                    row["SMT WO"] = GetWOByBase(group, WOTYPE.SMT);
                    List<WO_Relationship> woFAT = GetWOFATByBase(group, WOTYPE.FAT);
                    int index = 6;
                    foreach (var item in woFAT)
                    {
                        row[index] = item.ORDER_NO;
                        index ++;
                    }
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private int GetMaximumFatWo(IEnumerable<IGrouping<Guid?, WO_Relationship>> groupData)
        {
            int max = int.MinValue;
            foreach(var group in groupData)
            {
                var number = group.Where(w => w.TYPE_ID == 4).Count();
                if(number >max)
                {
                    max = number;
                }
            }
            return max;
        }

        public DataTable ToDataTableWOPending()
        {
            try
            {
                var data = GetPendingWO().OrderBy(o => o.UPD_DATE).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Customer");
                dataTable.Columns.Add("Model");
                dataTable.Columns.Add("Order No");
                dataTable.Columns.Add("Section");
                dataTable.Columns.Add("Update Time");
                dataTable.Columns.Add("Confirm Waiting Time (h)");
                var customerList = _base.GetCustomer();
                foreach (var item in data)
                {
                    var dataUsap = _usap.GetECO(item.ECO_NO);
                    DataRow row = dataTable.NewRow();
                    row["Customer"] = ConvertShortCustomerName(dataUsap.CUS_CODE, customerList);
                    row["Model"] = dataUsap == null ? "" : dataUsap.PART_NO;
                    row["Order No"] = item.ORDER_NO;
                    row["Section"] = GetType(item.TYPE_ID);
                    row["Update Time"] = item.UPD_DATE;
                    row["Confirm Waiting Time (h)"] = (int)(DateTime.Now - item.UPD_DATE).TotalHours;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //private List<WoChanging> UpdateWochanging(List<WoChanging> dataWoChanging)
        //{
        //    foreach (var item in dataWoChanging)
        //    {
        //        _context.Database.ExecuteSqlCommand("exec UpdateWOChanging", item.ECO_NO, item.DEPT_ORD, item)
        //    }
        //}

        public List<WO_Relationship> GetWORelationList()
        {
            using (var context = new DBContext())
            {
                return context.WO_Relationship.ToList();
            }
        }

        public bool InsertData(List<WO_Relationship> dataInsert)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //// luu vao wochanging
                    //List<WoChanging> woChangings = new List<WoChanging>();
                    //List<WoChanging> ecoList = _context.WoChangings.Where(w => w.ORDER_NO == woPending).ToList();
                    //foreach (var item in dataInsert)
                    //{                                                
                    //    foreach(var subECO in ecoList)
                    //    {
                    //        if (item.TYPE_NAME == subECO.TYPE_NAME) continue;
                    //        WoChanging woChanging = new WoChanging();
                    //        woChanging.ID = Guid.NewGuid();
                    //        woChanging.ORDER_NO = item.ORDER_NO;
                    //        woChanging.ECO_NO = subECO.ECO_NO;
                    //        woChanging.QUANTITY = item.QTY ?? 0;
                    //        woChanging.DUE_DATE = DateTime.Now.Date;
                    //        woChanging.UPD_DATE = DateTime.Now;
                    //        woChanging.UPDATER_NAME = "System";
                    //        woChanging.HOSTNAME = System.Environment.MachineName;
                    //        woChanging.TYPE_ID = item.TYPE_ID ?? -1;
                    //        woChanging.TYPE_NAME = item.TYPE_NAME;
                    //        woChanging.DEPT_ORD = subECO.DEPT_ORD;
                    //        woChanging.DEPT_NAME = subECO.DEPT_NAME;
                    //        woChanging.PE_OPTION = subECO.PE_OPTION;
                    //        woChangings.Add(woChanging);
                    //    }
                    //}
                    //_context.WoChangings.AddRange(woChangings);
                    _context.WO_Relationship.AddRange(dataInsert);
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

        public bool IsChangingWO(string wo)
        {
            WOPending woChangingExist = GetWOPlaning(WO_Select.Pending).Where(w => w.ORDER_NO == wo).FirstOrDefault();
            if (woChangingExist != null) return true;
            return false;
        }

        public bool IsFinishWO(string wo)
        {
            WO_Relationship wO_Relationship = _context.WO_Relationship.Where(w => w.ORDER_NO == wo).FirstOrDefault();
            if (wO_Relationship != null) return true;
            return false;
        }      
        public DataTable FilterData(DataTable dataTable, string columnName, List<string> searchStringList)
        {
            try
            {
                DataTable dataResult = dataTable.Clone();
                var customerList = _base.GetCustomer();
                foreach (DataRow row in dataTable.Rows)
                {
                    if (columnName == TextboxTag.ECO_No.ToString()) columnName = "ECO No";
                    var valueRow = row.Field<object>(columnName);                   
                    if(columnName == "Customer")
                    {
                        foreach (var item in searchStringList)
                        {
                            if (GetIDCustomer(customerList,valueRow, item))
                            {
                                dataResult.Rows.Add(row.ItemArray);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in searchStringList)
                        {
                            if (valueRow != null && valueRow.ToString().ToLower().Contains(item.ToLower()))
                            {
                                dataResult.Rows.Add(row.ItemArray);
                            }
                        }
                    }
                }
                return dataResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool GetIDCustomer(List<Customer> customerList,object valueRow, string item)
        {
            if (string.IsNullOrEmpty(item.Trim()))
            {
                return true;
            }
            if (valueRow != null)
            {
                var customerID = customerList.Where(w => w.CUSTOMER_NAME == valueRow.ToString()).Select(s => s.CUSTOMER_ID).FirstOrDefault();
                if (customerID != null && customerID == item) return true;
            }
            return false;
        }

        public string GetFullpathPDFfile(string eco)
        {            
             return _usap.GetFullPathDocument(eco);
        }
    }
}
