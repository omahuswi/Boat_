using boat.Models;
using Microsoft.Extensions.Logging;
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
        static DBStatement dbConn = DBStatement.Instance;
        NpgsqlConnection conn = dbConn.GetConnection();
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
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * FROM accessories");
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                npgsqlDataAdapter.Fill(ds);
                dgvData.DataSource = ds.Tables[0];                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            
        }

        /// <summary>
        /// Вывод списка пользователей
        /// </summary>
        public void GetUsers()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * FROM users");
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                npgsqlDataAdapter.Fill(ds);
                dgvData.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Вывод списка лодок
        /// </summary>
        public void GetBoats()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * FROM boats");
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                npgsqlDataAdapter.Fill(ds);
                dgvData.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }                       
            
        }
        #endregion


        #region Изменение данных

        /// <summary>
        /// Изменение цены на определенный процент
        /// </summary>
        /// <param name="table">таблица</param>
        /// <param name="per">процент</param>
        /// <param name="id">id продукции</param>
        /// <param name="state">Значение повышения или понижения цены</param>
        private void UpdatePrice(string table, int per, int id, string state)
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select update_{table}_price_percente({id}, {per}, \'{state}\')", conn);
                cmd.ExecuteNonQuery();                
            }
            catch(Exception ex) { 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        #endregion


        #region События
        private void btnUsers_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Пользователи";
            GetUsers();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Лодки";
            GetBoats();
            dgvData.Columns[0].Visible = false;
        }

        private void btnAccessory_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Аксессуары";
            GetAccessories();
            dgvData.Columns[0].Visible = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            User.UpdateLastVisit();
        }

       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                using (NewPrice secondForm = new NewPrice())
                {
                    if (secondForm.ShowDialog() == DialogResult.OK)
                    {
                        int per = secondForm.price;
                        string state = secondForm.state;
                        for (int i = 0; i < dgvData.SelectedRows.Count; i++)
                        {
                            int Id = Convert.ToInt32((dgvData.SelectedRows[i].Cells[0].Value));
                            switch (lblTitle.Text)
                            {                                
                                case "Лодки":
                                    UpdatePrice("boat", per, Id, state);
                                    break;
                                case "Аксессуары":
                                    UpdatePrice("accessory", per, Id, state);
                                    break;
                            }
                            
                        }
                    }
                }
                
            }
            
        }
        #endregion
    }
}
