using SquareSoft.Blue.DataAccessLayer.Data;
using SquareSoft.Blue.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SquareSoft.Blue.Models;
using Blue.Models;

namespace SquareSoft.Blue.UI.Vendor
{
    public partial class AddVendor : Form
    {
        SqlDataAdapter dat;
        SqlCommandBuilder scb;
        DataTable dt;
        internal ApplicationDataContext con = new ApplicationDataContext();
        public DbSet<Vendors> dbset;

        public AddVendor()
        {
            
            InitializeComponent();
            FillGrid();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var regUser = VendorRepo.AddVendor(txtCompName.Text, txtMngName.Text, txtAddress.Text, System.DateTime.UtcNow.Date, txtTel.Text);
                FillGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void FillGrid()
        {
            VendorRepo vd = new VendorRepo(con);
            var datasource = new BindingSource();
            datasource.DataSource = vd.GetVendors();
            dgvVendors.DataSource = datasource;
        }

        private void dgvVendors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int columnIndext = e.ColumnIndex;
                if (columnIndext != 0 && columnIndext != 5)
                {
                    txtTextToUpdate.Text = dgvVendors.CurrentRow.Cells[columnIndext].Value.ToString();
                    lblNoticEdit.Text = this.dgvVendors.CurrentCell.OwningColumn.Name.ToString() + " :";
                    //lblColValue.Text = "(" + this.dgvVendors.CurrentRow.Cells[0].Value.ToString() + ")";
                }
                else
                {
                    MessageBox.Show("Cannot Edit UserRole or userName", "Warning", MessageBoxButtons.OK);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string colName = this.dgvVendors.CurrentCell.OwningColumn.Name.ToString();
                string id = this.dgvVendors.CurrentRow.Cells[0].Value.ToString();

                int isUpdated = VendorRepo.UpdateUserRecord(new Guid(id), colName, txtTextToUpdate.Text);
                MessageBox.Show(colName.ToString() + " Updated Successfully");
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Substring(0, 20));
            }
        }
    }
}
