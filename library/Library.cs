using MySql.Data.MySqlClient;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace library_t
{
	public class Library
	{
		public List<Shelf> Shelves { get; set; }

		private List<string> Topics { get; set; }

		private List<Member> Members { get; set; }

		public Library()
		{
			LoadResources();
		}

		private void LoadShelves()
		{
			Shelf shelf;
			foreach (string topic in Topics)
			{
				shelf = new Shelf(topic);
				Shelves.Add(shelf);
			}
		}

		private void LoadResources()
		{
			LoadMembers();
			LoadTopics();
			LoadShelves();
		}

		private void LoadTopics()
		{
			if (TopicsInitialize())
			{

				string query = "SELECT * FROM `Topics`;";
				var reader_temp = SQL.ExecuteQuery(query);
				MySqlDataReader reader;
				if (reader_temp.Item2)
				{
					reader = reader_temp.Item1;
					DataTable dt = new DataTable();
					dt.Load(reader);
					string topic;
					foreach (DataRow row in dt.Rows)
					{
						topic = row["Topic"].ToString();
						Topics.Add(topic);
					}
				}
			}
		}

		private bool TopicsInitialize()
		{
			string query = "CREATE TABLE IF NOT EXISTS `Topics`(Topic varchar(255) PRIMARY KEY);";
			return SQL.ExecuteNonQuery(query);
		}

		private void SaveTopics()
		{
			if (TopicsInitialize())
			{
				StringBuilder query = new StringBuilder();
				foreach (string topic in Topics)
				{
					query.Append($"INSERT INTO `Topics` (Topic) VALUES ('{Topics}');");
				}
				SQL.ExecuteQuery(query.ToString());
			}
		}

		private void LoadMembers()
		{
			string query = "SELECT * FROM `members` WHERE 1=1;";
			var conn_temp = SQL.ExecuteQuery(query);
			Member member;
			MySqlDataReader reader;
			Members = new List<Member>(); 
			if (conn_temp.Item2)
			{
				reader = conn_temp.Item1;
				DataTable dt = new DataTable();
				dt.Load(reader);
				foreach (DataRow row in dt.Rows)
				{
					member = new Member(int.Parse(row["membership_id"].ToString()),
						row["Name"].ToString(),
						row["email"].ToString(),
						int.Parse(row["Age"].ToString()));
					Members.Add(member);
				}
			}
		}
	}
}
