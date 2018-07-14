using System;
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
			bool Submit = true;
			string FullName = Text_FullName.Text;
			DateTime DOB = new DateTime();

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

			if (Regex.IsMatch(Text_Email.Text, "^(? (\")(\".+? (?< !\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$))"))
			{
				string email = Text_Email.Text;
			}
			else
			{
				Label_EmailHelper.Content = "Please Enter a valid email";
				Label_EmailHelper.Visibility = Visibility.Visible;
				Submit = false;
			}

			if (Submit)
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
				string query = "CREATE TABLE IF NOT EXISTS members(" +
					"membership_id int NOT NULL AUTO_INCREMENT PRIMARY KEY," +
					"DOB date NOT NULL," +
					"Name varchar(25) NOT NULL," +
					"email varchar(100) NOT NULL" +
					");";
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
				
				query = $"INSERT INTO members (DOB, Name, Age) VALUES ({DOB.ToString("YYYY-MM-DD")}, '{Name}', {Age});";
				command = new MySqlCommand(query, conn);
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
			
		}
	}
}
