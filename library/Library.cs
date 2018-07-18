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

		public Library(List<Shelf> shelves)
		{
			Shelves = shelves;
			Members = GetMembers();
			Topics = LoadTopics();
		}

		private List<string> LoadTopics()
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
					List<string> topics = new List<string>();
					foreach (DataRow row in dt.Rows)
					{
						topic = row["Topic"].ToString();
						topics.Add(topic);
					}
					return topics;
				}
				else
					return null;
			}
			else
				return null;

		}

		// Helper
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

		private List<Member> GetMembers()
		{
			var temp = LoadMembers();
			if (temp.Item2)
			{
				return temp.Item1;
			}
			else
			{
				return new List<Member>();
			}
		}

		private static Tuple<List<Member>, bool> LoadMembers()
		{
			string query = "SELECT * FROM `members` WHERE 1=1;";
			var conn_temp = SQL.ExecuteQuery(query);
			Member member = new Member();
			List<Member> members = new List<Member>();
			MySqlDataReader reader;
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
					members.Add(member);
				}
				return Tuple.Create(members, true);
			}
			else
			{
				return Tuple.Create(members, false);
			}
		}
	}
}
