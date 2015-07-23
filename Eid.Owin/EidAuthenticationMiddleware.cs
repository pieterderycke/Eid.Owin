using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeRycke.Eid.Owin
{
    public class EidAuthenticationMiddleware : AuthenticationMiddleware<EidAuthenticationOptions>
    {
        public EidAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, EidAuthenticationOptions options)
            : base(next, options)
        {
            if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
            {
                options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            }
        }

        protected override AuthenticationHandler<EidAuthenticationOptions> CreateHandler()
        {
            return new EidAuthenticationHandler();
        }
    }
}
