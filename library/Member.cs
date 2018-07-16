namespace library
{
	public class Member
	{
		public int MembershipID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }

		public Member(int membershipID, string name, string email, int age)
		{
			MembershipID = membershipID;
			Name = name;
			Email = email;
			Age = age;
		}

		public Member()
		{
			;
		}
	}
}