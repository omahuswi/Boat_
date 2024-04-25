using boat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace boat
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (!chxSalesPerson.Checked){
                User user = new Customer(tbxLogin.Text, tbxPassword.Text, tbxEmail.Text, 
                    tbxPhone.Text, cbxDocument.Text , tbxIdNumber.Text, tbxFirstName.Text, tbxFamilyName.Text, dtpDateOfBirth.Value);
                Registration registration = new Registration(user);
                registration.GetRegistration();
                this.Close();
            }
        }
    }
}
