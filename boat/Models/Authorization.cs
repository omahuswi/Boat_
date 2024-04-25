using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace boat.Models
{
    public class Authorization
    {
        public User user { get; set; }        

        public Authorization(User user)
        {
            this.user = user;
        }
        
        public int Pause(int loginCount)
        {           
            if (loginCount == 3)
            {
                Properties.Settings.Default.pauseCount++;
                if (Properties.Settings.Default.pauseCount == 1)
                {
                    MessageBox.Show("15сек");
                }
                else { MessageBox.Show("+20сек"); }               
                
                loginCount = 0;
            }            
            return loginCount;            
        }      
        
        public void Authorize()
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
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"select * from \"user\" where user_login = \'{user.Login}\' and user_password=\'{user.Password}\'");
                npgsqlCommand.Connection = conn;

                NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    user.Id = Convert.ToInt32(dataReader["user_id"]);
                    switch (dataReader["role_id"])
                    {
                        
                        case 1: user = new Customer(user.Login, user.Password, user.Id);
                            break;
                        case 2: 
                            break;
                        case 3:
                            user = new Admin(user.Login, user.Password, user.Id);                            
                            MainForm form = new MainForm(user);
                            form.ShowDialog();
                            break;
                    }                   
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль.Пожалуйста проверьте ещё раз введенные данные");
                    boat.Properties.Settings.Default.loginCount++;
                    boat.Properties.Settings.Default.loginCount = Pause(boat.Properties.Settings.Default.loginCount);
                }
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
    }
}
