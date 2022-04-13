using CompDirectorAPI.Library.Models;

namespace CompDirectorAPI.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<UserModel> GetUserById(string Id)
        {
            try
            {
                _sql.StartTransaction("WlAuthDB");

                var output = _sql.LoadDataInTransaction<UserModel, dynamic>("dbo.spUserLookup", new { Id });

                _sql.CommitTransaction();

                return output;
            }
            catch
            {
                _sql.RollBackTransaction();
                throw;
            }

        }

        public void CreateUser(UserModel user)
        {
            try
            {
                _sql.StartTransaction("WlAuthDB");

                _sql.SaveDataInTransaction("dbo.spUser_Insert", new { user.Id, user.FirstName, user.LastName, user.EmailAddress });

                _sql.CommitTransaction();
            }
            catch
            {
                _sql.RollBackTransaction();
                throw;
            }
        }
    }
}
