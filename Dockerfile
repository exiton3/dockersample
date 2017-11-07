# escape=`
FROM microsoft/aspnet:4.6.2
SHELL ["powershell","-Command","$ErrorActionPreference = 'Stop'; $ProgressReference = 'SilentlyContinue';"];

RUN Remove-Website 'Default Web Site';

RUN New-Item -Path 'C:\Websites\Signalr' -Type Directory -Force;

RUN New-Website -Name 'Signalr' -PhysicalPath 'C:\Websites\Signalr';

EXPOSE 8090

COPY ["SignalR","/websites/signalr"]

RUN $path ='C:\Websites\Signalr';`
	$acl= Get-Acl $path;`
	$newOwner =[System.Security.Principal.NTAccount]('BUILTIN\IIS_IUSRS');`
	$acl.SetOwner($newOwner);`
	dir -r $path | Set-Acl -aclobject $acl