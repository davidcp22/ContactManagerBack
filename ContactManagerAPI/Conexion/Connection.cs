using Npgsql;

namespace ContactManagerAPI.Context
{
    public class Connection
    {
        private string DataBase { get; set; }
        private string Server { get; set; }
        private string Port { get; set; }
        private string User { get; set; }
        private string Password { get; set; }

        private static Connection Con = null;

        public Connection()
        {
            this.DataBase = "bd_contact";
            this.Server = "localhost";
            this.Port = "5432";
            this.User = "user_contact";
            this.Password = "admin12345*";
        }

        public NpgsqlConnection CreateConnection()
        {
            NpgsqlConnection connectionString = new NpgsqlConnection();
            try
            {
                connectionString.ConnectionString = "Server=" + this.Server +
                    ";Port=" + this.Port +
                    ";User id=" + this.User +
                    ";Password=" + this.Password +
                    ";Database=" + this.DataBase;
            }
            catch (Exception ex)
            {
                connectionString = null;
                throw ex;
            }
            return connectionString;
        }

        public static Connection getInstance()
        {
            if ( Con == null )
            {
                Con = new Connection();
            }
            return Con;
        }
    }
}
