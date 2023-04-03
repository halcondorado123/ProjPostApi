using System;
using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjPostApi.Security
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userReporitory;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
                ILoggerFactory logger,
                UrlEncoder encoder,
                ISystemClock clock,
                IUserRepository userRepository
            ) : base( options, logger, encoder, clock )
        {
            _userReporitory = userRepository;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization no encontrado");
            }

            bool result = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var creBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(creBytes).Split(new[] { ':' }, 2);
                var userName = credentials[0];
                var password = credentials[1];

                result = _userReporitory.IsUser(userName, password);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("ERROR: " + ex.Message.ToString());
            }


            if (!result)
            {
                return AuthenticateResult.Fail("Usuario y/o contraseña invalidas.");
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "id"),
                new Claim(ClaimTypes.Name, "user")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
