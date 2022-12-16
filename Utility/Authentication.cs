using Api.DataServices;
using Api.Models;

namespace Api.Utility
{
    public static class Authentication
    {
        public static async Task<UserModel> Authenticate(UserLogin userLogin, UserDataService userDataService, AuthDataService authDataService)
        {
            bool validated = await authDataService.ValidateUserCredentialsAsync(userLogin);
            if (validated == false)
            {
                return null;
            }
            User userData = (await userDataService.GetUsersByUsernameAsync(userLogin.UserName)).FirstOrDefault();
            if (userData == null)
            {
                return null;
            }
            return new UserModel() { Username = userLogin.UserName, Password = userLogin.Password, Roles = "validated", UserID = userData.Id };
        }
    }
}
