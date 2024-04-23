using Npgsql;
using System;
using System.Data;
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
                else { MessageBox.Show("20сек"); }               
                
                loginCount = 0;
            }            
            return loginCount;            
        }

        public void Authorize()
        {            
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            try
            {
                if (npgsqlConnection.State == ConnectionState.Open)
                {
                    npgsqlConnection.Close();
                }
                npgsqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"select * from \"user\" where user_login = \'{user.Login}\' and user_password=\'{user.Password}\'");
                npgsqlCommand.Connection = npgsqlConnection;

                NpgsqlDataReader dataReader = npgsqlCommand.ExecuteReader();
                if (dataReader.Read())
                {                   
                   MainForm form = new MainForm();
                   form.ShowDialog();
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
                npgsqlConnection.Close();
            }
            
        }
    }
}
