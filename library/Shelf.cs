﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
	class Shelf: List<Book>
	{
		// Syntax: SyntaxExample
		public string Topic { get; private set; }

		public Shelf(string topic) => Topic = topic;
	}
}
