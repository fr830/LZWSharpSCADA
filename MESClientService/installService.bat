%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe MESClientService.exe
Net Start MESClientService
sc config MESClientService start= auto