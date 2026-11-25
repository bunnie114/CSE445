namespace DefaultPage
{
    public class User
    {
        private string username;
        private string email;
        private string password;

        public User()
        {
            this.username = "";
            this.email = "";
            this.password = "";
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }
    }
}