using MySql.Data.MySqlClient;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_t
{
	public class Shelf: List<Book>
	{
		// Syntax: SyntaxExample
		public string Topic { get; private set; }

		public Shelf(string topic) => Topic = topic;

		public void Fill()
		{
			string query = $"SELECT * FROM `books` WHERE Topic = '{Topic}';";
			var temp = SQL.ExecuteQuery(query);
			MySqlDataReader reader;
			if (temp.Item2)
			{
				reader = temp.Item1;
				DataTable dt = new DataTable();
				dt.Load(reader);
				foreach (DataRow row in dt.Rows)
				{
					string Topic = row["Topic"].ToString();
					string Author = row["Author"].ToString();
					int YearOFPublication = int.Parse(row["Author"].ToString());
					// Left Here
				}
			}
		}
	}
}
