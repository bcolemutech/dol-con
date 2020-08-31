using dol_sdk.Enums;

namespace dol_sdk.Controllers
{
    public interface IAdminController
    {
        void UpdateUser(string email, Authority authority);
    }

    public class AdminController : IAdminController
    {
        public void UpdateUser(string email, Authority authority)
        {
            throw new System.NotImplementedException();
        }
    }
}
