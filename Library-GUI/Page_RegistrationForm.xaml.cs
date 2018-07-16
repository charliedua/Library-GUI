using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using SqlHelper;

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

			if (Regex.IsMatch(Text_FullName.Text, @"[A-Za-z ]{3,25}"))
			{
				FullName = Text_FullName.Text;
				Label_NameHelper.Visibility = Visibility.Hidden;
			}
			else
			{
				Label_NameHelper.Visibility = Visibility.Visible;
				Label_NameHelper.Content = "Please Enter a Valid name";
				Submit = false;
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

			if (Regex.IsMatch(Text_Email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
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
					"email varchar(255) NOT NULL," +
					"Age int NOT NULL" +
					");";
				SQL.ExecuteNonQuery(query);
				query = $"SELECT email FROM members WHERE email LIKE '{email}'";
				Tuple<MySqlDataReader, bool> result_temp = SQL.ExecuteQuery(query);
				if (result_temp.Item2)
				{
					MySqlDataReader reader = result_temp.Item1;
					DataTable dt = new DataTable();
					dt.Load(reader);
					if (dt.Rows.Count > 0)
					{
						Label_EmailHelper.Content = "Your email already exists";
						Label_EmailHelper.Visibility = Visibility.Visible;
					}
					else
					{
						Label_EmailHelper.Visibility = Visibility.Hidden;
						query = $"INSERT INTO members (email, Name, Age) VALUES ('{email}', '{FullName}', {Age});";
						if (!SQL.ExecuteNonQuery(query))
						{
							Label_SubmitHelper.Content = "The query failed please try again.";
							Label_SubmitHelper.Visibility = Visibility.Visible;
						}
						else
						{
							MessageBox.Show("Membership has been successfully activated", "Activation", MessageBoxButton.OK, MessageBoxImage.Information);
							Label_SubmitHelper.Visibility = Visibility.Hidden;
						}
					}
				}
			}
		}


	}
}
