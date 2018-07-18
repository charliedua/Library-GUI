using SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace library_t
{
	public class Book
	{
		public Book(string topic, string title, int yearOfPublishing, string author = "Anonymous")
		{
			Topic = topic;
			Title = title;
			Author = author;
			YearOfPublishing = yearOfPublishing;
		}

		public string Topic { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int YearOfPublishing { get; set; }

		public void Save()
		{
			string query = "CREATE TABLE IF NOT EXISTS books(" +
				"ISBN int NOT NULL AUTO_INCREMENT PRIMARY KEY," +
				"Topic varchar(255) NOT NULL," +
				"Title varchar(255) NOT NULL," +
				"Author varchar(255) NOT NULL," +
				"YearOfPublishing int NOT NULL" +
				");";
			if (SQL.ExecuteNonQuery(query))
			{
				query = $"INSERT INTO `books` (Topic, Title, Author, YearOfPublishing) VALUES ('{Topic}', '{Title}', '{Author}', {YearOfPublishing})";
				if (SQL.ExecuteNonQuery(query))
				{
					MessageBox.Show("Book added Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
		}
	}
}
