using CompDirectorAPI.Library.Models;

namespace CompDirectorAPI.Library.DataAccess
{
    public interface IUserData
    {
        void CreateUser(UserModel user);
        List<UserModel> GetUserById(string Id);
    }
}