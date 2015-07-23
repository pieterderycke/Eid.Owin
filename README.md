# eID OWIN Middleware
This library provides an OWIN Middleware for authenticating users based on the Belgium eID identity cards.

## Prerequisites
Users need to have the eID middleware installed. The latest version can be found at: http://eid.belgium.be/en/using_your_eid/installing_the_eid_software/

You need to minimally use ASP.NET 5.0.

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
	
	app.UseEidAuthentication();
}	
```

## More Information
For more information, you can read the following articles and documents:
* http://repository.eid.belgium.be/downloads/citizen/nl/CPS_CitizenCA.pdf