Invoke-WebRequest http://certs.eid.belgium.be/belgiumrs.crt -OutFile belgiumrs.crt
Invoke-WebRequest http://certs.eid.belgium.be/belgiumrs2.crt -OutFile belgiumrs2.crt
Invoke-WebRequest http://certs.eid.belgium.be/belgiumrs3.crt -OutFile belgiumrs3.crt
Invoke-WebRequest http://certs.eid.belgium.be/belgiumrs4.crt -OutFile belgiumrs4.crt

Import-Certificate -CertStoreLocation cert:\LocalMachine\Root belgiumrs.crt
Import-Certificate -CertStoreLocation cert:\LocalMachine\Root belgiumrs2.crt
Import-Certificate -CertStoreLocation cert:\LocalMachine\Root belgiumrs3.crt
Import-Certificate -CertStoreLocation cert:\LocalMachine\Root belgiumrs4.crt

Remove-Item belgiumrs.crt
Remove-Item belgiumrs2.crt
Remove-Item belgiumrs3.crt
Remove-Item belgiumrs4.crt