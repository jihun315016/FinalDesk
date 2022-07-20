using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DESK_MES
{
    public class ExcelUtil
    {        
        [DllImport("user32.dll")]     
        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
                
        public bool ExportList<T>(List<T> dataList, string fileName, string[] importColumn, string[] ColumnName)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
       
           
            try
            {                
                int c = 0;
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    foreach (string import in importColumn)
                    {
                        if (import.Equals(prop.Name))
                        {
                            
                             xlWorkSheet.Cells[1, c + 1] = prop.Name.Replace(prop.Name.ToString(), ColumnName[c]);
                             c++;
                        }
                    }
                }
       
                for (int r = 0; r < dataList.Count; r++)
                {
                    c = 0;
                    foreach (PropertyInfo prop in typeof(T).GetProperties())
                    {
                        foreach (string import in importColumn)
                        {
                            if (import.Equals(prop.Name))
                            {

                                if (prop.GetValue(dataList[r], null) != null)
                                    xlWorkSheet.Cells[r + 2, c + 1] = prop.GetValue(dataList[r]).ToString();
                                else
                                    xlWorkSheet.Cells[r + 2, c + 1] = "";
                                c++;
                            }
                        }
                    }
                }
               
                // 컬럼의 너비 자동 조정
                xlWorkSheet.Columns.AutoFit();
                

                // xls 확장자로 저장
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal);
                xlWorkBook.Close();
                xlApp.Quit();
               
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
            finally
            {
                uint processId;
                GetWindowThreadProcessId(new IntPtr(xlApp.Hwnd), out processId);
                Process p = Process.GetProcessById((int)processId);
                p.Kill();
            }
        }


        public bool ExportDataTable(DataTable dt, string fileName)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

            try
            {
                // 컬럼명을 엑셀파일의 첫번째 행에 제목으로 설정
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    xlWorkSheet.Cells[1, c + 1] = dt.Columns[c].ColumnName;
                }

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        xlWorkSheet.Cells[r + 2, c + 1] = dt.Rows[r][c].ToString();
                    }
                }

                // xls 확장자로 저장하는 경우
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal);
                xlWorkBook.Close();
                xlApp.Quit();                

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message); 
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public DataTable ImportDataTable(string filename)
        {
            try
            {
                string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"; //*.xls
                string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"; //*.xlsx

                //string.Format("안녕 {0}님 {1}살", "홍길동", 25);
                string ext = Path.GetExtension(filename);
                string connStr = string.Empty;
                if (ext == ".xls")
                    connStr = string.Format(Excel03ConString, filename, "Yes");
                else
                    connStr = string.Format(Excel07ConString, filename, "Yes");

                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();

                string sql = $"select * from [{sheetName}]";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }


        ////방향성에 따라 바뀌어야됨
        //public bool ExportList2<T>(List<T> dataList, List<T> dataList2, string fileName, string[] importColumn, string[] ColumnName)
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();
        //    Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

        //    Excel.Workbook xlWorkBook2 = xlApp.Workbooks.Add();
        //    Excel.Worksheet xlWorkSheet2 = xlWorkBook2.Worksheets.get_Item(2);

        //    try
        //    {
        //        //속성명을 엑셀파일의 첫번째 행에 제목으로 찍어준다.

        //        int c = 0;
        //        foreach (PropertyInfo prop in typeof(T).GetProperties())
        //        {
        //            foreach (string import in importColumn)
        //            {
        //                if (import.Contains(prop.Name))
        //                {

        //                    xlWorkSheet.Cells[1, c + 1] = prop.Name.Replace(prop.Name.ToString(), ColumnName[c]);
        //                    c++;
        //                }

        //            }

        //        }

        //        //데이터를 찍어준다.
        //        for (int r = 0; r < dataList.Count; r++)
        //        {
        //            c = 0;
        //            foreach (PropertyInfo prop in typeof(T).GetProperties())
        //            {
        //                foreach (string import in importColumn)
        //                {
        //                    if (import.Contains(prop.Name))
        //                    {

        //                        if (prop.GetValue(dataList[r], null) != null)
        //                            xlWorkSheet.Cells[r + 2, c + 1] = prop.GetValue(dataList[r]).ToString();
        //                        else
        //                            xlWorkSheet.Cells[r + 2, c + 1] = "";
        //                        c++;
        //                    }

        //                }

        //            }
        //        }

        //        //엑셀컬럼의 너비가 데이터길이에 따라서 자동 조정
        //        xlWorkSheet.Columns.AutoFit();

        //        //xls 확장자로 저장하는 경우
        //        xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal);

        //        //xlsx 확장자로 저장하는 경우
        //        //xlWorkBook.SaveCopyAs(dlg.FileName);
        //        //xlWorkBook.Saved = true;

        //        xlWorkBook.Close();
        //        xlApp.Quit();

        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        releaseObject(xlWorkSheet);
        //        releaseObject(xlWorkBook);
        //        releaseObject(xlApp);
        //    }
        //}

    }
}
