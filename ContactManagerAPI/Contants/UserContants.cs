using ContactManagerAPI.Models;

namespace ContactManagerAPI.Contants
{
    public class UserContants
    {
        public static List<User> Users = new List<User>() 
        {
            new User() {Username = "admin" ,Password = "password" , Rol="Administrator"}
        };
    }
}
