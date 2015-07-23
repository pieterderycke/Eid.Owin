using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeRycke.Eid.Owin
{
    public class EidAuthenticationHandler : AuthenticationHandler<EidAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var x509 = Context.Get<X509Certificate2>("ssl.ClientCertificate");

            if (x509 != null)
            {
                var chain = new X509Chain(true);
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;
                chain.Build(x509);

                X509Certificate2 citizenCertificate = chain.ChainElements[0].Certificate;

                if (citizenCertificate.NotAfter < DateTime.Now ||
                    citizenCertificate.NotBefore > DateTime.Now)
                    throw new Exception("The citizen certificate is not (longer) valid.");

                //TODO verify if citizen certificate has not been revoked

                if (chain.ChainElements[1].Certificate.Thumbprint != "74CC6E5559FFD7C2DD0526C0C21593C56C9384F3")
                    throw new Exception("Invalid Citizen CA certificate.");

                if (chain.ChainElements[2].Certificate.Thumbprint != "51CCA0710AF7733D34ACDC1945099F435C7FC59F")
                    throw new Exception("Invalid Belgium Root CA certificate.");

                string firstName = Regex.Match(citizenCertificate.Subject, "G=([^,]*),").Groups[1].Value;
                string lastName = Regex.Match(citizenCertificate.Subject, "SN=([^,]*),").Groups[1].Value;
                string nationalRegisterIdentificationNumber = Regex.Match(citizenCertificate.Subject, "SERIALNUMBER=([^,]*),").Groups[1].Value;
                string nationality = Regex.Match(citizenCertificate.Subject, "C=([^,]*)").Groups[1].Value;

                // Based on information of: https://www.ksz-bcss.fgov.be/nl/bcss/page/content/websites/belgium/services/docutheque/technical_faq/faq_5.html
                bool isMale = int.Parse(nationalRegisterIdentificationNumber.Substring(6, 3)) % 2 == 1;

                ClaimsIdentity identity = new ClaimsIdentity(Options.SignInAsAuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, firstName + " " + lastName, null, Options.AuthenticationType));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName));
                identity.AddClaim(new Claim(ClaimTypes.Name, lastName));
                identity.AddClaim(new Claim(ClaimTypes.Gender, isMale ? "M" : "F"));
                identity.AddClaim(new Claim(ClaimTypes.Country, nationality));

                return new AuthenticationTicket(identity, new AuthenticationProperties());
            }
            else
            {
                return new AuthenticationTicket(null, new AuthenticationProperties());
            }
        }

        private ClaimsIdentity ValidateX509Certificate(X509Certificate2 x509)
        {
            var chain = new X509Chain(true);
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;
            chain.Build(x509);

            X509Certificate2 citizenCertificate = chain.ChainElements[0].Certificate;

            if (citizenCertificate.NotAfter < DateTime.Now ||
                citizenCertificate.NotBefore > DateTime.Now)
                throw new Exception("The citizen certificate is not (longer) valid.");

            //TODO verify if citizen certificate has not been revoked

            if (chain.ChainElements[1].Certificate.Thumbprint != "74CC6E5559FFD7C2DD0526C0C21593C56C9384F3")
                throw new Exception("Invalid Citizen CA certificate.");

            if (chain.ChainElements[2].Certificate.Thumbprint != "51CCA0710AF7733D34ACDC1945099F435C7FC59F")
                throw new Exception("Invalid Belgium Root CA certificate.");

            string firstName = Regex.Match(citizenCertificate.Subject, "G=([^,]*),").Groups[1].Value;
            string lastName = Regex.Match(citizenCertificate.Subject, "SN=([^,]*),").Groups[1].Value;
            string nationalRegisterIdentificationNumber = Regex.Match(citizenCertificate.Subject, "SERIALNUMBER=([^,]*),").Groups[1].Value;
            string nationality = Regex.Match(citizenCertificate.Subject, "C=([^,]*)").Groups[1].Value;

            // Based on information of: https://www.ksz-bcss.fgov.be/nl/bcss/page/content/websites/belgium/services/docutheque/technical_faq/faq_5.html
            bool isMale = int.Parse(nationalRegisterIdentificationNumber.Substring(6, 3)) % 2 == 1;

            ClaimsIdentity identity = new ClaimsIdentity(Options.SignInAsAuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, firstName + " " + lastName, null, Options.AuthenticationType));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName));
            identity.AddClaim(new Claim(ClaimTypes.Name, lastName));
            identity.AddClaim(new Claim(ClaimTypes.Gender, isMale ? "M" : "F"));
            identity.AddClaim(new Claim(ClaimTypes.Country, nationality));

            return identity;
        }
    }
}
