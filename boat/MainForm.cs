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
    public partial class MainForm : Form
    {
        User User { get; set; }
        public MainForm(User user)
        {
            InitializeComponent();
            this.User = user;            
        }

        #region Вывод данных 

        /// <summary>
        /// Вывод списка аксессуаров
        /// </summary>
        public void GetAccessories()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            if (npgsqlConnection.State == ConnectionState.Open)
            {
                npgsqlConnection.Close();
            }
            npgsqlConnection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * FROM accessories");
            cmd.Connection = npgsqlConnection;
            cmd.ExecuteNonQuery();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            npgsqlDataAdapter.Fill(ds);
            dgvData.DataSource = ds.Tables[0];
            npgsqlConnection.Close();
        }

        /// <summary>
        /// Вывод списка пользователей
        /// </summary>
        public void GetUsers()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            if (npgsqlConnection.State == ConnectionState.Open)
            {
                npgsqlConnection.Close();
            }
            npgsqlConnection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * FROM users");
            cmd.Connection = npgsqlConnection;
            cmd.ExecuteNonQuery();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            npgsqlDataAdapter.Fill(ds);
            dgvData.DataSource = ds.Tables[0];
            npgsqlConnection.Close();
        }

        /// <summary>
        /// Вывод списка лодок
        /// </summary>
        public void GetBoats()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            if (npgsqlConnection.State == ConnectionState.Open)
            {
                npgsqlConnection.Close();
            }
            npgsqlConnection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * FROM boats");
            cmd.Connection = npgsqlConnection;
            cmd.ExecuteNonQuery();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            npgsqlDataAdapter.Fill(ds);
            dgvData.DataSource = ds.Tables[0];
            npgsqlConnection.Close();
        }
        #endregion



        private void btnUsers_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Пользователи";
            GetUsers();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Лодки";
            GetBoats();
        }

        private void btnAccessory_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Аксессуары";
            GetAccessories();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
