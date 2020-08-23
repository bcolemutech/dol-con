namespace dol_con.Services
{
    public interface IUserService
    {
        string GetUserData(string idToken);
    }

    public class UserService : IUserService
    {
        public string GetUserData(string idToken)
        {
            throw new System.NotImplementedException();
        }
    }
}