using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
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
		public int YearOfPublishing{ get; set; }
	}
}
