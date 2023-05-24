namespace Sharapova.model
{
    public class User
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public User(int id, string nickname, string name, string password)
        {
            this.id = id;
            this.nickname = nickname;
            this.name = name;
            this.password = password;
        }

    }
}
