using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using MySql.Data.MySqlClient;

namespace Library_GUI
{
	/// <summary>
	/// Interaction logic for Page_RegistrationForm.xaml
	/// </summary>
	public partial class Page_RegistrationForm : Page
	{
		public Page_RegistrationForm()
		{
			InitializeComponent();
		}

		private void Btn_Submit_Click(object sender, RoutedEventArgs e)
		{
			string email = "";
			bool Submit = true;
			string FullName = "";

			if (!Regex.IsMatch(FullName, @"[A-Za-z ]{3,25}"))
			{
				Label_NameHelper.Visibility = Visibility.Visible;
				Label_NameHelper.Content = "Please Enter a Valid name";
				Submit = false;
			}
			else
			{
				Label_NameHelper.Visibility = Visibility.Hidden;
			}

			if (!int.TryParse(Text_Age.Text, out int Age))
			{
				Label_AgeHelper.Visibility = Visibility.Visible;
				Label_AgeHelper.Content = "Please enter an integer";
				Submit = false;
			}
			else
			{
				Label_AgeHelper.Visibility = Visibility.Hidden;
			}

			if (Regex.IsMatch(Text_Email.Text, @" ^ ([\w\.\-] +)@([\w\-] +)((\.(\w){ 2,3})+)$"))
			{
				email = Text_Email.Text;
			}
			else
			{
				Label_EmailHelper.Content = "Please Enter a valid email";
				Label_EmailHelper.Visibility = Visibility.Visible;
				Submit = false;
			}

			if (Submit)
			{
				string query = "CREATE TABLE IF NOT EXISTS members(" +
					"membership_id int NOT NULL AUTO_INCREMENT PRIMARY KEY," +
					"Name varchar(25) NOT NULL," +
					"email varchar(100) NOT NULL" +
					");";
				ExecuteNonQuery(query);
				query = $"SELECT email FROM members WHERE email LIKE {email}";
				Tuple<MySqlDataReader, bool> result_temp = ExecuteQuery(query);
				if (result_temp.Item2)
				{
					MySqlDataReader reader = result_temp.Item1;
					DataTable dt = new DataTable();
					dt.Load(reader);
					if (dt.Rows.Count > 0)
					{
						Submit = false;
						Label_EmailHelper.Content = "Your email already exists";
						Label_EmailHelper.Visibility = Visibility.Visible;
					}
					else
					{
						Label_EmailHelper.Visibility = Visibility.Hidden;
						query = $"INSERT INTO members (email, Name, Age) VALUES ({email}, '{Name}', {Age});";
						ExecuteNonQuery(query);
					}
				}
			}
		}

		private static void ExecuteNonQuery(string query)
		{
			string connectionString = @"datasource=127.0.0.1;port=3306;database=Library_GUI;username=root;Password=;SslMode=none";
			MySqlConnection conn = new MySqlConnection(connectionString);
			try
			{
				conn.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Connection cant be opend! \n error is {ex.Message.ToString()}",
					@"Can't connect", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
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
				return;
			}
			conn.Close();
		}
		
		private static Tuple<MySqlDataReader, bool> ExecuteQuery(string query)
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

		private static Tuple<MySqlConnection, bool> DB_Connect()
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
