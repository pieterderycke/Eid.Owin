using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeRycke.Eid.Owin
{
    public class EidAuthenticationOptions : AuthenticationOptions
    {
        public EidAuthenticationOptions()
            : base("Eid")
        {

        }

        public string SignInAsAuthenticationType { get; set; }
    }
}
