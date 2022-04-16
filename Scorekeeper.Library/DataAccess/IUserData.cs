using Scorekeeper.Library.Models;

namespace Scorekeeper.Library.DataAccess
{
    public interface IUserData
    {
        void CreateUser(UserModel user);
        List<UserModel> GetUserById(string Id);
    }
}