%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe BatchCoreService.exe
Net Start BatchCoreService.exe
sc config BatchCoreService.exe start= auto