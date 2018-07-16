using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SqlHelper
{
    public class SQL
    {
		// Helper Methods For mysql connections
		// Handles Heavy lifting
		public static bool ExecuteNonQuery(string query)
		{
			var conn_temp = DB_Connect();
			MySqlConnection conn = null;
			if (conn_temp.Item2)
			{
				conn = conn_temp.Item1;
			}
			else
			{
				return false;
			}
			MySqlCommand command = new MySqlCommand(query, conn);
			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error executing query \n{ex.ToString()}", "error",
					MessageBoxButton.OK, MessageBoxImage.Error);
				conn.Close();
				return false;
			}
			conn.Close();
			return true;
		}
		public static Tuple<MySqlDataReader, bool> ExecuteQuery(string query)
		{
			Tuple<MySqlConnection, bool> db_temp = DB_Connect();
			MySqlConnection conn = null;
			MySqlDataReader reader = null;
			if (db_temp.Item2)
			{
				conn = db_temp.Item1;
				MySqlCommand command = new MySqlCommand(query, conn);
				try
				{
					reader = command.ExecuteReader();
					return Tuple.Create(reader, true);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error executing query \n{ex.ToString()}", "error",
						MessageBoxButton.OK, MessageBoxImage.Error);
					conn.Close();
					return Tuple.Create(reader, false);
				}
			}
			else
			{
				return Tuple.Create(reader, false);
			}
		}
		public static Tuple<MySqlConnection, bool> DB_Connect()
		{
			string connectionString = @"datasource=127.0.0.1;port=3306;database=Library_GUI;username=root;Password=;SslMode=none";
			MySqlConnection conn = new MySqlConnection(connectionString);
			try
			{
				conn.Open();
				return Tuple.Create(conn, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Connection cant be opend! \n error is {ex.Message.ToString()}",
					@"Can't connect", MessageBoxButton.OK, MessageBoxImage.Error);
				return Tuple.Create(conn, false);
			}
		}
	}
}
