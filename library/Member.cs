namespace library_t
{
	public class Member
	{
		public int MembershipID { get; private set; } // is auto set by sql
		public string Name { get; private set; }
		public string Email { get; private set; }
		public int Age { get; private set; }

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