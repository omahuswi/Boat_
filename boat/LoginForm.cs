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

namespace boat
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User currentUser = new User(tbxLogin.Text, tbxPassword.Text);
            this.Hide();
            Authorization  authorization = new Authorization(currentUser);
            authorization.Authorize();            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegForm form = new RegForm();
            form.ShowDialog();
        }
    }
}
