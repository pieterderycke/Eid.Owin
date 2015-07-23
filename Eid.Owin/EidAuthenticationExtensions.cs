using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeRycke.Eid.Owin
{
    public static class EidAuthenticationExtensions
    {
        public static IAppBuilder UseEidAuthentication(this IAppBuilder app)
        {
            return UseEidAuthentication(app, new EidAuthenticationOptions());
        }

        public static IAppBuilder UseEidAuthentication(this IAppBuilder app, EidAuthenticationOptions options)
        {
            return app.Use(typeof(EidAuthenticationMiddleware), app, options);
        }
    }
}
