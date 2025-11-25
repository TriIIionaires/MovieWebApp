namespace MovieDLL.Models
{
	public class UserModel
	{
        public int ID { get; set; }
        public string Username { get; set; }
        public string PwdHash { get; set; }
        public string Salt { get; set; }

		public override string ToString()
		{
			return $"ID: {ID}, Username: {Username}";
		}

	}

}
