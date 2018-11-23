using System.Collections.Generic;
using System.Net.Mail;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Threading;

namespace Services
{
    public static class MailServce
    {
        public static int SendMail(string attached)
        {
            try
            {
                MailMessage mail = new MailMessage("akwaoyo@gmail.com", "adeoluwagbemiga@gmail.com", "Weekly Sales Record", "This is the sales record excel file of IT Zone");
                Attachment attachment = new Attachment(attached);
                mail.Attachments.Add(attachment);
                SmtpClient mailClient = new SmtpClient("smtp.gmail.com");//cross check this
                                                                         //mailClient.Host = "smtp.yandex.com";
                mailClient.Port = 587;
                mailClient.EnableSsl = true;
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailClient.UseDefaultCredentials = false;
                mailClient.Timeout = 40000;
                mailClient.Credentials = new System.Net.NetworkCredential("akwaoyo@gmail.com", "123@Algorithm321");
                //mailClient.SendCompleted += SMTP_SendCompleted;

                mailClient.Send(mail);

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
            
        }
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        public static int ExportDtToExcel(this DataTable tbl, string excelFilePath = null)
        {
            try
            {
                int oyo = 0;
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Excel._Worksheet workSheet = excelApp.ActiveSheet;
                workSheet.Name = "SalesReport";

                // column headings
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                    }
                }

                // check file path
                if (!System.IO.File.Exists(excelFilePath))
                {
                    try
                    {
                        
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                        Thread.Sleep(3000);
                        oyo = 1;
                    }
                    catch (Exception ex)
                    {
                        oyo = 0;
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                    excelApp.Quit();
                                            Thread.Sleep(3000);
                    oyo = 1;
                }
                return oyo;
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception("ExportToExcel: \n" + ex.Message);               
            }
        }
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);

            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }

    public class PastWeekSales
    {
        public string product_name { get; set; }
        public int quantity { get; set; }
        public decimal amount_paid { get; set; }
        public DateTime sales_date { get; set; }
        public decimal original_price { get; set; }
    }
}
