using boat.Models;
using Npgsql;
using System;
using System.Data;
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
            lblTitle.Text = "Пользователи";
            GetUsers();
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
                dgvData.Columns[0].Visible = false;
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
                dgvData.Columns[0].Visible = false;
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
                dgvData.Columns[0].Visible = false;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            User.UpdateLastVisit();
        }          

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0 && lblTitle.Text!="Пользователи")//бьбьбьббь
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
                                    Query.UpdatePrice("boat", per, Id, state);
                                    break;
                                case "Аксессуары":
                                    Query.UpdatePrice("accessory", per, Id, state);
                                    break;
                            }
                            
                        }
                    }
                }
                
            }
            
        }
       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch (lblTitle.Text)
            {
                case "Пользователи":
                    AddUser form = new AddUser();
                    form.ShowDialog();
                    break;
                case "Лодки":
                    MessageBox.Show("В разработке");
                    break;
                case "Аксессуары":
                    MessageBox.Show("В разработке");
                    break;
            }
        }           
 
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows != null)
            {
                var result = MessageBox.Show("Точно удалить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvData.SelectedRows.Count; i++)
                    {
                        int Id = Convert.ToInt32((dgvData.SelectedRows[i].Cells[0].Value));
                        switch (lblTitle.Text)
                        {
                            case "Пользователи":
                                Query.RemoveDate("user", Id);
                                break;
                            case "Лодки":
                                Query.RemoveDate("boat", Id);
                                break;
                            case "Аксессуары":
                                Query.RemoveDate("accessory", Id);
                                break;
                        }

                    }
                }
            }
        }
        #endregion
    }
}
