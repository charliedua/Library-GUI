using MySql.Data.MySqlClient;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;

namespace library_t
{
	public class Library
	{
		public Library(List<Shelf> shelves, List<string> topics)
		{
			Shelves = shelves;
			Topics = topics;
			Members = GetMembers();
		}

		public List<Shelf> Shelves { get; set; }

		private List<string> Topics { get; set; }

		private List<Member> Members { get; set; }

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
