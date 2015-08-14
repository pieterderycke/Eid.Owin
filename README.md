# eID OWIN Middleware
This library provides an OWIN Middleware for authenticating users based on the Belgium eID identity cards. Based on the data that can be read from the client certificate, it will create a ClaimsIdentity for use in your web applications.

## Prerequisites
Users need to have the eID middleware installed. The latest version can be found at: http://eid.belgium.be/en/using_your_eid/installing_the_eid_software/

You need to minimally use ASP.NET 5.0 and the technical account running your web application needs access to the following root authorities:
* http://certs.eid.belgium.be/belgiumrs.crt (Belgium Root CA)
* http://certs.eid.belgium.be/belgiumrs2.crt (Belgium Root CA 2)
* http://certs.eid.belgium.be/belgiumrs3.crt (Belgium Root CA 3)
* http://certs.eid.belgium.be/belgiumrs4.crt (Belgium Root CA 4)

Alternatively, you can use the following PowerShell script to automatically install the necessary root authority certificates: https://github.com/pieterderycke/Eid.Owin/blob/master/Install-EidRootCertificates.ps1.

You need to configure IIS to Accept or Require a Client Certificate in the SSL settings of your web application.

## Example
Import the following using declaration in the "App_Start\Startup.Auth.cs" configuration file of your web application.

```csharp
using DeRycke.Eid.Owin;
```

In the method "ConfigureAuth" add the following code at the bottom:

```csharp
public void ConfigureAuth(IAppBuilder app)
{
	// ...
	
	app.UseEidAuthentication(new EidAuthenticationOptions() 
	{
		SignInAsAuthenticationType = "Cookies"
	});
}	
```

## More Information
For more information, you can read the following articles and documents:
* http://repository.eid.belgium.be/downloads/citizen/nl/CPS_CitizenCA_34.pdf
* http://repository.eid.belgium.be/index.php?lang=en