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
    public partial class NewPrice : Form
    {
        enum ButtonState
        {
            UP,
            DOWN
        }

        public int price;
        public string state;
        public NewPrice()
        {
            InitializeComponent();
            button1.Tag = ButtonState.UP.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            price = Convert.ToInt32(textBox1.Text);
            state = button1.Tag.ToString();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Tag.ToString() == "UP")
            {
                button1.Tag = ButtonState.DOWN;
                button1.Text = "↓";
            }
            else
            {
                button1.Tag = ButtonState.UP;
                button1.Text = "↑";
            }
        }
    }
}
