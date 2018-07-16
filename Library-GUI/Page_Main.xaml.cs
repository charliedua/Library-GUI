using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using library_t;

namespace Library_GUI
{
	/// <summary>
	/// Interaction logic for Page_Main.xaml
	/// </summary>
	public partial class Page_Main : Page
	{
		private Library library;
		public Page_Main()
		{
			InitializeComponent();
			library = LoadLibraryFromDB();
		}

		private Library LoadLibraryFromDB()
		{
			
		}

		private void Btn_Registor_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			NavigationService.Navigate(new Uri("Page_RegistrationForm.xaml", UriKind.Relative));
		}
	}
}
