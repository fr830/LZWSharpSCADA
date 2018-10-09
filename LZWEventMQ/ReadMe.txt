To register the LZWMessageQueue.dll its necessary to run the following:


regasm LZWMessageQueue.dll /tlb:LZWMessageQueue.tlb /codebase

Notes
You may need to use full path names.
Regasm is located in c:\windows\microsoft.net\framework\v2.0.50727
or similar depending on version of .NET

To unregister the dll use the following commad

regasm /unregister LZWMessageQueue.dll
