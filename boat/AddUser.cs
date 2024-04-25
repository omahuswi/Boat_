using boat.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boat
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void rbnSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            pnlInfo.Visible = false;
        }

        private void rbnCustomer_CheckedChanged(object sender, EventArgs e)
        {
            pnlInfo.Visible = true;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            DBStatement dbConn = DBStatement.Instance;
            NpgsqlConnection conn = dbConn.GetConnection();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                NpgsqlCommand cmd = null;
                if (rbnCustomer.Checked == true)
                {
                    cmd = new NpgsqlCommand($"select add_new_customer(\'{tbxLogin.Text}\', \'{tbxPassword.Text}\', \'" +
                    $"{tbxFirstName.Text}\', \'{tbxFamilyName.Text}\', \'{dtpDateOfBirth.Text:dd.MM.yyyy}\', \'{tbxEmail.Text}\', \'" +
                    $"{tbxPhone.Text}\', \'{tbxIdNumber.Text}\', \'{cbxDocument.Text}\')");
                }
                else if (rbnSalesPerson.Checked == true)
                    cmd = new NpgsqlCommand($"select add_new_sales_person(\'{tbxLogin.Text}\', \'{tbxPassword.Text}\', \'" +
                    $"{tbxFirstName.Text}\', \'{tbxFamilyName.Text}\')");

                else cmd = new NpgsqlCommand($"select add_new_admin(\'{tbxLogin.Text}\', \'{tbxPassword.Text}\')");

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            
            
        }
    }
}
