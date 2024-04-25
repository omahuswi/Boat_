using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace boat.Models
{
    
    public class Query
    {
        static DBStatement dbConn = DBStatement.Instance;
        static NpgsqlConnection conn = dbConn.GetConnection();

        #region Изменение данных
        /// <summary>
        /// Изменение цены на определенный процент
        /// </summary>
        /// <param name="table">таблица</param>
        /// <param name="per">процент</param>
        /// <param name="id">id продукции</param>
        /// <param name="state">Значение повышения или понижения цены</param>
        public static void UpdatePrice(string table, int per, int id, string state)
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select update_{table}_price_percente({id}, {per}, \'{state}\')", conn);
                cmd.ExecuteNonQuery();
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

        public static void RemoveDate(string table, int id)
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select delete_{table}({id})", conn);
                cmd.ExecuteNonQuery();
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
