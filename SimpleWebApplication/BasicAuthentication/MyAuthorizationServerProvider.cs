using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SimpleWebApplication.BasicAuthentication
{
    public class MyAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (ValidateUser validateUser = new ValidateUser())
            {
                var user = validateUser.ValidateUserByUserNameAndPassword(context.UserName, context.Password);
                if(user == null)
                {
                    context.SetError("invalid-grant", "Provided username and password incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.UserEmailID));

                context.Validated(identity);

            }
        }
    }
}