using System;
using System.Windows.Forms;
using SquareSoft.Blue.DataAccessLayer.Repository;
using Services;
using SquareSoft.Blue.DataAccessLayer.Data;
using SquareSoft.Blue.Models;
using System.Data.Entity;
using SquareSoft.Blue.UI.Staff;
using System.Linq;

namespace SquareSoft.Blue.UI
{
    public partial class Login : Form 
    {
        internal ApplicationDataContext context = new ApplicationDataContext();
        public DbSet<UsersReg> dbset;
        
        public Login(ApplicationDataContext _context)
        {
            this.context = _context;
            this.dbset = context.Set<UsersReg>();
        }

        public static string _username = "";
        public static string username
        {
            get;
            set;
        }

        public static void setUserName(string name)
        {
            username = name;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public static string DoLogin(string username, string password)
        {
            var valueReturn = LoginHandler.LoginUser(username, password);//calling Loginghandler.login, then applying encription.
            setUserName(username);
            return valueReturn;
           
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var x = DoLogin(txtUserName.Text, txtPassword.Text);
            if (x != "")
            {
                LandingPage ldp = new LandingPage();
                setUserName(txtUserName.Text);
                this.Hide();
                ldp.Show();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var userRepo = new UserRepo();
            
            if (userRepo.CheckForAdmin()==false)
            {
                StaffRegistration staff = new StaffRegistration();
                staff.ShowDialog();
                this.Hide();
            }
            //MakeDtFromList();

        }

        #region Mail
        public void MakeDtFromList()
        {
            ListtoDataTableConverter ltdtc = new ListtoDataTableConverter();

            using (var context = new ApplicationDataContext())
            {
                var source = context.Database.SqlQuery<PastWeekSales>("Get_PastWeekSales").ToList();
                if (source.Count > 0)
                {

                    var dataTable = ltdtc.ToDataTable(source);
                    if (DateTime.UtcNow.DayOfWeek.Equals("Monday"))
                    {
                        var x = dataTable.ExportDtToExcel(@"C:\SalesReport\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString());
                        MailServce.SendMail(@"C:\SalesReport\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + ".xlsx");
                    }
                    else
                    {
                        var dialog = MessageBox.Show("Do you want to Create New Excel File for last week sales?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            var x = dataTable.ExportDtToExcel(@"C:\SalesReport\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString());
                            MailServce.SendMail(@"C:\SalesReport\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + ".xlsx");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("No sales for the past week");
                }
            }
        }
        #endregion
    }
}
