using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Xls;
using System.Data;
using System.Windows.Forms;
using ECO_DX_For_PUR.DATA.COST_SYSTEM;
using ECO_DX_For_PUR.DATA.COST_SYSTEM.Repository;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using OfficeOpenXml;
using ExcelDataReader;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using ECO_DX_For_PUR.DATA.Bussiness.SQLHelper;
using NPOI.SS.UserModel;

namespace ECO_DX_For_PUR.Utils
{
    public class ExcelServices
    {
        IMPORT_INFO_Repository _importInfo = new IMPORT_INFO_Repository();

        WOHelper _woHelper = new WOHelper();
        private DataTable dataTable;
        public DataTable ImportExcel(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Read data from the Excel file
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Method 2: Reading data into a DataSet
                            var dataSet = reader.AsDataSet();
                            foreach (DataTable table in dataSet.Tables)
                            {
                                return table;
                            }
                        }
                    }
                }
                return null;
            }
            catch (IOException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
           

            //try
            //{
            //    // Create a workbook
            //    Workbook workbook = new Workbook();
            //    // Load the Excel file
            //    workbook.LoadFromFile(filePath/*@"C:\Users\u42107\Desktop\Book1.xlsx"*/);

            //    // Assume you are working with the first worksheet
            //    Worksheet sheet = workbook.Worksheets[0];

            //    // Create a DataTable
            //    dataTable = new DataTable();
            //    //dataTable.Columns.Add("STT");
            //    // Set up DataTable columns based on Excel columns
            //    for (int col = 1; col <= sheet.LastColumn; col++)
            //    {
            //        dataTable.Columns.Add(sheet[1, col].Text);
            //    }
            //    //int index = 1;
            //    // Read data from the worksheet and fill DataTable
            //    for (int row = 2; row <= sheet.LastRow; row++)
            //    {

            //        DataRow dataRow = dataTable.NewRow();
            //        //dataRow[0] = index;
            //        for (int col = 1; col <= sheet.LastColumn; col++)
            //        {
            //            // Access cell value using sheet[row, col].Text
            //            string cellValue = sheet[row, col].DisplayedText;
            //            dataRow[col - 1] = cellValue;
            //        }
            //        dataTable.Rows.Add(dataRow);
            //        //index++;
            //    }

            //    // Close the workbook
            //    workbook.Dispose();
               // return dataTable;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}          
        }

        public static string GetExelFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsb",
                Title = "Select an Excel File"               
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
        public DataTable GetColumnHeader(string filePath)
        {
            // Create a workbook
            Workbook workbook = new Workbook();

            // Load the Excel file
            workbook.LoadFromFile(filePath);

            // Assume you are working with the first worksheet
            Worksheet sheet = workbook.Worksheets[0];

            // Create a DataTable
            dataTable = new DataTable();
            //dataTable.Columns.Add("STT");
            // Set up DataTable columns based on Excel columns
            for (int col = 1; col <= sheet.LastColumn; col++)
            {
                dataTable.Columns.Add(sheet[1, col].Text);
            }
            // Close the workbook
            workbook.Dispose();
            return dataTable;
        }

        public void ImportZMM70(string filepath)
        {
            if (filepath != null)
            {
                //using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
                //{
                //    using (var reader = ExcelReaderFactory.CreateReader(stream))
                //    {
                //        var dataSet = reader.AsDataSet();
                //        foreach (DataTable table in dataSet.Tables)
                //        {
                            
                //        }
                //    }
                //}
                // Create a workbook
                Workbook workbook = new Workbook();
                // Load the Excel file
                workbook.LoadFromFile(filepath);

                // Assume you are working with the first worksheet
                Worksheet sheet = workbook.Worksheets[0];

                // Create a DataTable
                dataTable = new DataTable();
                dataTable.Columns.Add("PUR_INFO_REC", typeof(string));
                dataTable.Columns.Add("QUOTE_DATE", typeof(DateTime));
                dataTable.Columns.Add("PART_NO", typeof(string));
                dataTable.Columns.Add("RATE_UNIT", typeof(string));
                dataTable.Columns.Add("UNIT_PRICE", typeof(decimal));
                dataTable.Columns.Add("REF_NO", typeof(string));
                dataTable.Columns.Add("CREATE_USER", typeof(string));
                dataTable.Columns.Add("CREATE_DATE", typeof(DateTime));
                dataTable.Columns.Add("VALID_FROM", typeof(DateTime));
                dataTable.Columns.Add("VALID_TO", typeof(DateTime));
                dataTable.Columns.Add("SUPPLIER", typeof(string));
                dataTable.Columns.Add("SUPPLIER_NAME", typeof(string));
                dataTable.Columns.Add("FIXED_VENDOR", typeof(bool));
                dataTable.Columns.Add("MANUFACTURER_NAME", typeof(string));

                //if (!IsZMM70File(sheet))
                //{
                //    MessageBox.Show("ZMM70 file is not correct!");
                //    return;
                //}
                var refNo = DateTime.Now.ToString("yyMMddHHss");
                DataTable data = sheet.ExportDataTable();

                foreach(DataRow row in data.Rows)
                {
                    var infoRec = row.Field<object>("Purchasing Info Rec.");
                    var partNo = row.Field<object>("Internal P/N");
                    DateTime quoteDate;
                    if (!DateTime.TryParse(row.Field<string>("Quotation Create Date"), out quoteDate))
                    {
                        continue;
                    }
                    var price = decimal.Parse(row.Field<string>("Unit Price"), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                    DateTime dateParse;
                    DateTime? createDate = null;
                    if (DateTime.TryParse(row.Field<string>("Quotation Create Date"), out dateParse))
                    {
                        createDate = dateParse;
                    }
                    DateTime validFrom;
                    DateTime validTo;
                    if (!DateTime.TryParse(row.Field<string>("Valid-From Date"), out validFrom)) continue;
                    if (!DateTime.TryParse(row.Field<string>("Valid To Date"), out validTo)) continue;
                    var rateUnit = row.Field<object>("Rate unit (currency or percentage)");
                    if(rateUnit ==  null) continue;
                    var createUser = row.Field<object>("Record Creation");
                    var supplier = row.Field<object>("Supplier");
                    if (supplier == null) continue;
                    var supplierName = row.Field<object>("Supplier Name");
                    if(supplierName == null) continue;
                    var fixVendor = row.Field<string>("Fixed vendor");
                    var manufacturerName = row.Field<object>("Manufacturer Name");
                    if (manufacturerName == null) continue;
                    dataTable.Rows.Add(new object[] { infoRec, quoteDate, partNo, rateUnit, price,
                    refNo, createUser, createDate, validFrom,validTo,supplier,supplierName,!string.IsNullOrEmpty(fixVendor),manufacturerName });
                }
                
                using (DBContextBOM context = new DBContextBOM())
                {
                    var StaffCodeParam = new SqlParameter("@Data", dataTable)
                    {
                        TypeName = "dbo.Udt_PUR_PART_PRICE",
                        SqlDbType = SqlDbType.Structured
                    };
                    var window = new SqlParameter("@windowUser", "System");

                    context.Database.ExecuteSqlCommand("exec PurPartPrice_Update @Data , @windowUser", StaffCodeParam, window);
                }
                // luu vao Import Info
                IMPORT_INFO info = new IMPORT_INFO();
                info.FileName = filepath;
                info.Func = "fPurPartPrice";
                info.Hostname = System.Windows.Forms.SystemInformation.ComputerName;
                info.UpdateTime = DateTime.Now;
                _importInfo.SaveImportInfo(info);
            }          
        }

        public static bool ExportDataWORelationship(DataTable listData)
        {
            try
            {
                // Display SaveFileDialog to save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = DateTime.Now.Date.ToString("MM_dd_yyyy");
                saveFileDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save As Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = saveFileDialog.FileName;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        Workbook workbook = new Workbook();
                        // Get the first worksheet
                        Worksheet sheet = workbook.Worksheets[0];
                        sheet.Range["A1:I1"].Value = "Inser Wo Form";
                        sheet.Range["A1:I1"].Style.Font.Size = 30;
                        sheet.Range["A1:I1"].Style.Font.IsBold = true;
                        sheet.Range["A1:I1"].Style.VerticalAlignment = VerticalAlignType.Center;
                        sheet.Range["A1:I1"].Style.HorizontalAlignment = HorizontalAlignType.Center;

                        sheet.Range[2, 1].Value = "Lưu ý: - Không thay đổi định dạng!";
                        sheet.Range[3, 1].Value = "- Phải điền Wo (Order No) trùng với Wo AI hoặc SMT hoặc FAT";
                        sheet.Range[4, 1].Value = "- Phải có ít nhất một Wo SMT và FAT";
                        sheet.Range[5, 1].Value = "- Nếu có nhiều Wo FAT thì điền vào những ô sau cột FAT WO theo hàng ngang";
                        sheet.Range["A2:I2"].Style.Font.Size = 12;
                        sheet.Range["A3:I3"].Style.Font.Size = 12;
                        sheet.Range["A4:I4"].Style.Font.Size = 12;
                        sheet.Range["A5:I5"].Style.Font.Size = 12;

                        sheet.Range["A1:I1"].Merge();
                        sheet.Range["A2:I2"].Merge();
                        sheet.Range["A3:I3"].Merge();
                        sheet.Range["A4:I4"].Merge();
                        sheet.Range["A5:I5"].Merge();

                        // Set font color to red for the merged cells
                        Spire.Xls.CellStyle style1 = sheet.Range["A1:I1"].Style;
                        style1.Font.Color = System.Drawing.Color.Black;
                        Spire.Xls.CellStyle style2 = sheet.Range["A2:I2"].Style;
                        style2.Font.Color = System.Drawing.Color.Blue;
                        Spire.Xls.CellStyle style3 = sheet.Range["A3:I3"].Style;
                        style3.Font.Color = System.Drawing.Color.Blue;
                        Spire.Xls.CellStyle style4 = sheet.Range["A4:I4"].Style;
                        style4.Font.Color = System.Drawing.Color.Blue;
                        Spire.Xls.CellStyle style5 = sheet.Range["A5:I5"].Style;
                        style5.Font.Color = System.Drawing.Color.Blue;
                        sheet.InsertDataTable(listData, true, 6, 1);
                        // Get the header range
                        Spire.Xls.CellRange headerRange = sheet.Rows[5];
                        Spire.Xls.CellRange allRange = sheet.AllocatedRange;
                        // Set the background color of the header row to lime
                        headerRange.Style.Color = System.Drawing.Color.LimeGreen;
                        headerRange.Style.HorizontalAlignment = HorizontalAlignType.Center;
                        headerRange.Style.VerticalAlignment = VerticalAlignType.Center;
                        headerRange.AutoFitColumns();
                        headerRange.RowHeight = 40;
                        headerRange.Style.Font.IsBold = true;
                        headerRange.ColumnWidth = 14;
                        allRange.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
                        workbook.SaveToFile(fileName, ExcelVersion.Version2013);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ImportRelationWO()
        {
            try
            {
                var filePath = GetExelFilePath();
                if (filePath != null)
                {
                    // Create a workbook
                    Workbook workbook = new Workbook();

                    // Load the Excel file
                    workbook.LoadFromFile(filePath);

                    // Assume you are working with the first worksheet
                    Worksheet sheet = workbook.Worksheets[0];

                    if (IsFormatInsertWO(sheet))
                    {
                        List<WO_Relationship> wO_Relationships = new List<WO_Relationship>();
                        for (int row = 7; row <= sheet.LastRow; row++)
                        {
                            var dataInsert = GetWoFromRow(sheet, row);
                            if (dataInsert != null && dataInsert.Count() > 0)
                            {
                                wO_Relationships.AddRange(dataInsert);
                            }
                        }
                        if (_woHelper.InsertData(wO_Relationships))
                        {
                            return true;
                        }
                    }
                }
                MessageBox.Show("Sai định dạng file excel!");
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }           
        }

        private  IEnumerable<WO_Relationship> GetWoFromRow(Worksheet sheet, int row)
        {
            try
            {
                List<WO_Relationship> wO_Relationships = new List<WO_Relationship>();
                string order_NO = sheet[row, 3].DisplayedText;
                string AI = sheet[row, 5].DisplayedText;
                string SMTA = sheet[row, 6].DisplayedText;
                string SMTB = sheet[row, 7].DisplayedText;
                string SMT = sheet[row, 8].DisplayedText;
                List<string> FATlist = GetFATList(sheet, row);
                // phải có ít nhất một wo SMT và FAT thì mới chọn.
                if (IsOKWoRelationship(order_NO, AI, SMTA, SMTB, SMT, FATlist))
                {
                    var data = CreateBaseWoList( AI, SMTA, SMTB, SMT, FATlist);
                    if (data != null && data.Count()>0)
                    {
                        wO_Relationships.AddRange(data);
                    }
                }
                return wO_Relationships;
            }
            catch (Exception)
            {
                throw;
            }          
        }

        private static IEnumerable<WO_Relationship> CreateBaseWoList( string AI, string SMTA, string SMTB, string SMT, List<string> fATlist)
        {
            try
            {
                List<WO_Relationship> wO_Relationships = new List<WO_Relationship>();
                var orderBase = Guid.NewGuid();
                if (IsValidWo(AI))
                {
                    wO_Relationships.Add(GetSubWo(AI, orderBase, 0, "AI"));
                }
                if (IsValidWo(SMTA))
                {
                    wO_Relationships.Add(GetSubWo(SMTA, orderBase, 1, "SMTA"));
                }
                if (IsValidWo(SMTB))
                {
                    wO_Relationships.Add(GetSubWo(SMTB, orderBase, 2, "SMTB"));
                }
                if (IsValidWo(SMT))
                {
                    wO_Relationships.Add(GetSubWo(SMT, orderBase, 3, "SMT"));
                }
                if (fATlist.Count > 0)
                {
                    wO_Relationships.AddRange(GetSubWo(fATlist, orderBase, 4, "FAT"));
                }
                return wO_Relationships;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private static bool IsValidWo(string wo)
        {
            int result;
            if (!string.IsNullOrEmpty(wo))
            {
                if (wo.Length == 10)
                {
                    if(Int32.TryParse(wo, out result))
                    {
                        if (wo.Substring(0, 4) == "2000")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static List<WO_Relationship> GetSubWo(List<string> fATlist, Guid guid, int TypeID, string typeName)
        {
            try
            {
                List<WO_Relationship> list = new List<WO_Relationship>();
                foreach (var wo in fATlist)
                {
                    if (IsValidWo(wo))
                    {
                        WO_Relationship subWO = new WO_Relationship();
                        subWO.ORDER_NO = wo;
                        subWO.TYPE_ID = TypeID;
                        subWO.TYPE_NAME = typeName;
                        subWO.ORDER_BASE = guid;
                        subWO.QTY = null;
                        subWO.UPDATE_NAME = "";
                        subWO.HOST_NAME = System.Environment.MachineName;
                        subWO.UPDATE_TIME = DateTime.Now;
                        subWO.CHANGE_ID = null;
                        subWO.CHANGE_NAME = null;
                        list.Add(subWO);
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
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

        private  bool IsOKWoRelationship(string woWH,string AI,string SMTA, string SMTB, string SMT, List<string> fATlist)
        {
            try
            {
                List<string> listWo = new List<string>();
                listWo.Add(SMTA);
                listWo.Add(SMTB);
                listWo.Add(SMT);
                listWo.Add(AI);
                listWo.AddRange(fATlist);
                if ((IsValidWo(SMTA)|| IsValidWo(SMTB) || IsValidWo(SMT)) && IsValidWo(fATlist))
                {
                    // phải có ít nhất một wo SMT và FAT thì mới chọn.
                    if (fATlist.Count > 0 && (!string.IsNullOrEmpty(SMT) || !string.IsNullOrEmpty(SMTA) || !string.IsNullOrEmpty(SMTB)))
                    {
                        //phải có wo bất kì giống với woWH
                        if (AI == woWH || SMT == woWH || SMTA == woWH || SMTB == woWH || (fATlist.Where(w => w == woWH).Count() > 0))
                        {

                            if((string.IsNullOrEmpty(SMTA) && string.IsNullOrEmpty(SMTB)) || SMTA != SMTB)
                            {
                                ////phải không được trùng nhau
                                //List<string> filteredList = listWo.Where(item => !string.IsNullOrEmpty(item)).ToList();
                                //if (!filteredList.GroupBy(x => x).Any(g => g.Count() > 1))
                                //{
                                if (_woHelper.IsChangingWO(woWH)) // nếu là wo pending
                                {
                                    // neu ko phai Wo đã thêm rồi
                                    if (!_woHelper.IsFinishWO(woWH))
                                    {
                                        return true;
                                    }
                                }
                                //}
                            }
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

        private bool IsValidWo(List<string> fATlist)
        {
            foreach(var wo in fATlist)
            {
                if (IsValidWo(wo)) return true;
            }
            return false;
        }

        private static List<string> GetFATList(Worksheet sheet,int row)
        {
            try
            {
                List<string> FATlist = new List<string>();
                for (int column = 9; column < sheet.LastColumn + 1; column++) // từ cột thứ 9 là FAT
                {
                    var content = sheet[row, column].DisplayedText;
                    if (!string.IsNullOrEmpty(content))
                    {
                        FATlist.Add(content);
                    }
                }
                return FATlist;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private static bool IsFormatInsertWO(Worksheet sheet)
        {
            try
            {
                var rowHeader = sheet.Rows[5];
                if (rowHeader.CellList[0].DisplayedText != "Customer" || rowHeader.CellList[1].DisplayedText != "Model" || rowHeader.CellList[2].DisplayedText != "Order No" ||
                    rowHeader.CellList[3].DisplayedText != "Section" || rowHeader.CellList[4].DisplayedText != "AI WO" || rowHeader.CellList[5].DisplayedText != "SMTA WO" ||
                    rowHeader.CellList[6].DisplayedText != "SMTB WO" || rowHeader.CellList[7].DisplayedText != "SMT WO" || rowHeader.CellList[8].DisplayedText != "FAT WO")
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public List<ECOSchedule> GetdataECOSchedule(string filepath)
        {
            if (filepath != null)
            {
                // Create a workbook
                Workbook workbook = new Workbook();

                // Load the Excel file
                workbook.LoadFromFile(filepath);

                // Assume you are working with the first worksheet
                Worksheet sheet = workbook.Worksheets[0];

                if (!IsFormatECOSchedule(sheet))
                {
                    Common.CloseFormLoading();
                    MessageBox.Show("Sai định dạng cột!");
                    return null;
                }
                List<ECOSchedule> eCOSchedules = new List<ECOSchedule>();
                for (int row = 2; row <= sheet.LastRow; row++)
                {
                    if (IsNullRow(row, sheet)) continue;
                    ECOSchedule eCOSchedule = new ECOSchedule();
                    eCOSchedule.ECO_NO = sheet[row, 7].DisplayedText.ToUpper();
                    eCOSchedule.MODEL = sheet[row, 3].DisplayedText.ToUpper();
                    //if (string.IsNullOrEmpty(eCOSchedule.ECO_NO) || string.IsNullOrEmpty(eCOSchedule.MODEL)) continue;
                    eCOSchedule.RECEIVE_DOCUMENT_DATE = Common.ConvertDate(sheet[row, 1].DisplayedText);
                    eCOSchedule.IMPLEMENT_DATE = Common.ConvertDate(sheet[row, 2].DisplayedText);
                    eCOSchedule.CONTENT_CHANGE = sheet[row, 4].DisplayedText;
                    eCOSchedule.ECN_DCI_NO = sheet[row, 5].DisplayedText.ToUpper();
                    eCOSchedule.START_APPROVE_DATE = Common.ConvertDate(sheet[row, 6].DisplayedText);
                    eCOSchedule.REMARK = sheet[row, 8].DisplayedText;
                    eCOSchedules.Add(eCOSchedule);
                }
                return eCOSchedules;
            }
            return null;
        }

        private static bool IsNullRow(int row, Worksheet sheet)
        {
            for(int i  = 1; i<sheet.LastColumn; i++)
            {
                if (!string.IsNullOrEmpty(sheet[row, i].DisplayedText)) return false;
            }
            return true;
        }

        private bool IsFormatECOSchedule(Worksheet sheet)
        {
            var rowHeader = sheet.Rows[0];
            if (rowHeader.CellList[0].DisplayedText != "Ngày nhận tài liệu" || rowHeader.CellList[1].DisplayedText != "Ngày triển khai" || rowHeader.CellList[2].DisplayedText != "Model"||
                rowHeader.CellList[3].DisplayedText != "Nội dung thay đổi" || rowHeader.CellList[4].DisplayedText != "ECN, DCI No." || rowHeader.CellList[5].DisplayedText != "Ngày bắt đầu áp dụng" || rowHeader.CellList[6].DisplayedText != "UMC ECO No." || rowHeader.CellList[7].DisplayedText != "REMARK")
            {
                return false;
            }
            return true;
        }

        private bool IsZMM70File(Worksheet sheet)
        {
            var rowHeader = sheet.Rows[0];
            if(rowHeader.CellList[0].DisplayedText != "Purchasing Info Rec." || rowHeader.CellList[1].DisplayedText != "Internal P/N" || rowHeader.CellList[2].DisplayedText != "Material Number")
            {
                return false;
            }
            return true;
        }

        private DateTime ConvertDate(string displayedText)
        {
            DateTime dateTime;
            if(DateTime.TryParse(displayedText, out dateTime))
            {
                return System.Convert.ToDateTime(dateTime.Date.ToString("yyyy-MM-dd"));
            }
            return dateTime;
        }

        public static void ExportData(DataTable dataResult, string originPath, string newPathName)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(originPath);

            // Get the first worksheet
            Worksheet sheet = workbook.Worksheets[0];
            int startRow = sheet.LastRow + 1; // Start from the next row after the last used row

            // Insert data into the worksheet
            for (int i = 0; i < dataResult.Rows.Count; i++)
            {
                DataRow row = dataResult.Rows[i];

                for (int j = 0; j < dataResult.Columns.Count; j++)
                {
                    sheet.Range[startRow + i, j + 1].Text = row[j].ToString();
                    // Enable text wrapping for the cell
                    sheet.Range[startRow + i, j + 1].AutoFitColumns();
                }
            }

            // Save the modified workbook
            workbook.SaveToFile(newPathName, ExcelVersion.Version2013);
        }


        static Decimal? FindRowWithMaxTime(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                return null; // DataTable is empty
            }

            List<DataRow> rowListFixvender = new List<DataRow>();
            // lọc những hàng có fix vender
            foreach(DataRow row in dataTable.Rows)
            {
                if (!string.IsNullOrEmpty(row.Field<string>(16)))
                {
                    rowListFixvender.Add(row);
                }
            }
            DataRow maxTimeRow = rowListFixvender[0];
            string date = maxTimeRow.Field<string>(14);
            DateTime maxTime = DateTime.Parse(date);

            foreach (DataRow row in rowListFixvender)
            {
                DateTime currentTime = DateTime.Parse(row.Field<string>(14));

                if (currentTime > maxTime)
                {
                    maxTime = currentTime;
                    maxTimeRow = row;
                }
            }

            return decimal.Parse(maxTimeRow.Field<string>(10));
        }

        /// <summary>
        /// export data ecn finish
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="fileName"></param>
        public static bool ExportData(DataTable listData)
        {
            try
            {
                // Display SaveFileDialog to save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = DateTime.Now.Date.ToString("MM_dd_yyyy");
                saveFileDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save As Excel File";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {                 
                    var fileName = saveFileDialog.FileName;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        Workbook workbook = new Workbook();
                        // workbook.LoadFromFile(pathBase);

                        // Get the first worksheet
                        Worksheet sheet = workbook.Worksheets[0];
                        //listData.Columns.RemoveAt(0);
                        sheet.InsertDataTable(listData, true, 1, 1);
                        // Get the header range
                        Spire.Xls.CellRange headerRange = sheet.Rows[0];
                        Spire.Xls.CellRange allRange = sheet.AllocatedRange;
                        // Set the background color of the header row to lime
                        headerRange.Style.Color = System.Drawing.Color.LimeGreen;
                        headerRange.Style.HorizontalAlignment = HorizontalAlignType.Center;
                        headerRange.Style.VerticalAlignment = VerticalAlignType.Center;
                        headerRange.AutoFitColumns();
                        headerRange.RowHeight = 40;
                        headerRange.Style.Font.IsBold = true;
                        headerRange.ColumnWidth = 14;
                        allRange.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;
                        allRange.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
                        workbook.SaveToFile(fileName, ExcelVersion.Version2013);
                        return true;
                    }                   
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckFormat(string filePath)
        {
            try
            {
                // check gì gì đó........
                return true;
            }
            catch (Exception)
            {

                throw;
            }         
        }

        public void ImportLocationModel(string filepath)
        {
            if (filepath != null)
            {
                //// Create a workbook
                //Workbook workbook = new Workbook();

                //// Load the Excel file
                //workbook.LoadFromFile(filepath);

                //// Assume you are working with the first worksheet
                //Worksheet sheet = workbook.Worksheets[0];

                // Create a DataTable
                dataTable = new DataTable();
                dataTable.Columns.Add("MODEL_ID", typeof(string));
                dataTable.Columns.Add("PART_NO", typeof(string));
                dataTable.Columns.Add("ASSEMBLY_NO", typeof(string));
                dataTable.Columns.Add("RATIO", typeof(decimal));
                dataTable.Columns.Add("UNIT_QTY", typeof(decimal));
                dataTable.Columns.Add("CREATE_DATE", typeof(DateTime));
                dataTable.Columns.Add("RUNNING_CHANGE", typeof(bool));
                dataTable.Columns.Add("FOLLOW_UP", typeof(string));
                dataTable.Columns.Add("ALT_GROUP", typeof(string));
                dataTable.Columns.Add("LOCATION", typeof(string));

                // Check if the file exists



                //if (!IsZMM70File(sheet))
                //{
                //    MessageBox.Show("ZMM70 file is not correct!");
                //    return;
                //}
                // Check if the file exists


                // Check if the file exists
                if (File.Exists(filepath))
                {
                    // Read data from the Excel file
                    using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Choose one of the available methods to read data

                            // Method 1: Reading data using a DataReader
                            do
                            {
                                while (reader.Read())
                                {
                                    // Read data row by row
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write($"{reader.GetValue(i)} \t");
                                    }
                                    Console.WriteLine();
                                }
                            } while (reader.NextResult());

                            // Method 2: Reading data into a DataSet
                            var dataSet = reader.AsDataSet();
                            foreach (DataTable table in dataSet.Tables)
                            {
                                for(int row = 1; row < table.Rows.Count;row++)
                                {
                                    var modelId = table.Rows[row].ItemArray[0].ToString();
                                    var partNo = table.Rows[row].ItemArray[2].ToString();
                                    var assembly = table.Rows[row].ItemArray[1].ToString();
                                    decimal ratio = (decimal)0.01 * decimal.Parse(table.Rows[row].ItemArray[7].ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                                    if (ratio == 0)
                                    {
                                        ratio = 1;
                                    }
                                    var createDate = DateTime.Now;
                                    bool runingChange = !string.IsNullOrEmpty(table.Rows[row].ItemArray[9].ToString());
                                    var followUp = table.Rows[row].ItemArray[9].ToString();
                                    var altGroup = table.Rows[row].ItemArray[6].ToString();
                                    var location = table.Rows[row].ItemArray[10].ToString();
                                    var unitQty = decimal.Parse(table.Rows[row].ItemArray[4].ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);


                                    dataTable.Rows.Add(new object[] { modelId, partNo, assembly, ratio, unitQty,
                                        createDate, runingChange, followUp, altGroup,location });
                                }
                            }
                            using (DBContextBOM context = new DBContextBOM())
                            {
                                var StaffCodeParam = new SqlParameter("@Data", dataTable)
                                {
                                    TypeName = "dbo.Udt_BC_BOM_LIST1",
                                    SqlDbType = SqlDbType.Structured
                                };
                                var window = new SqlParameter("@windowUser", "System");

                                context.Database.ExecuteSqlCommand("exec BC_BOMLIST_Update @Data , @windowUser", StaffCodeParam, window);
                            }
                            // luu vao Import Info
                            //IMPORT_INFO info = new IMPORT_INFO();
                            //info.FileName = filepath;
                            //info.Func = "fBomList";
                            //info.Hostname = System.Windows.Forms.SystemInformation.ComputerName;
                            //info.UpdateTime = DateTime.Now;
                            //_importInfo.SaveImportInfo(info);
                        }
                    }
                }
                // Create a FileStream to read the Excel file

                //for (int row = 2; row <= sheet.LastRow; row++)
                //{
                //    var modelId = sheet[row, 1].DisplayedText;
                //    var partNo = sheet[row, 3].DisplayedText.Trim();
                //    if(partNo == "300-122-837")
                //    {
                //        Console.WriteLine("");
                //    }
                //    var assembly = sheet[row, 2].DisplayedText;
                //    decimal ratio = (decimal)0.01* decimal.Parse(sheet[row, 8].DisplayedText, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
                //    if((double)ratio != 0.00)
                //    {
                //        Console.WriteLine("");
                //    }
                //    var createDate = DateTime.Now;
                //    bool runingChange = !string.IsNullOrEmpty(sheet[row, 10].DisplayedText);
                //    var followUp = sheet[row, 10].DisplayedText;
                //    var altGroup = sheet[row, 7].DisplayedText;
                //    var location = sheet[row, 11].DisplayedText;
                //    var unitQty = decimal.Parse(sheet[row, 5].DisplayedText, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);


                //    dataTable.Rows.Add(new object[] { modelId, partNo, assembly, ratio, unitQty,
                //    createDate, runingChange, followUp, altGroup,location });
                //}
                //using (DBContextBOM context = new DBContextBOM())
                //{
                //    var StaffCodeParam = new SqlParameter("@Data", dataTable)
                //    {
                //        TypeName = "dbo.Udt_BC_BOM_LIST1",
                //        SqlDbType = SqlDbType.Structured            
                //    };
                //    var window = new SqlParameter("@windowUser", "System");

                //    context.Database.ExecuteSqlCommand("exec BC_BOMLIST_Update @Data , @windowUser", StaffCodeParam, window);
                //}
                //// luu vao Import Info
                //IMPORT_INFO info = new IMPORT_INFO();
                //info.FileName = filepath;
                //info.Func = "fBomList";
                //info.Hostname = System.Windows.Forms.SystemInformation.ComputerName;
                //info.UpdateTime = DateTime.Now;
                //_importInfo.SaveImportInfo(info);
            }
        }
    }
}
