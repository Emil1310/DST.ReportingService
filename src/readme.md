# The DST Reporting Service helper tool

To generate C# files from the downloaded XSD files, locate your Windows SDK and find the xsd.exe file in the sub folder, example:
C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\xsd.exe

Remove the 'encoding="utf-16"' from the XML description in the beginning of all XSD files, otherwise an error will be thrown when attempting to create C# classes. 

Run the following commands from the folder with the downloaded XSD files, with the correct file names for each XSD file:
"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\xsd.exe" /c /namespace:DST.ReportingService.Models.Camping /language:CS 1156000-01-08-2019-31-08-2019.xsd
"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\xsd.exe" /c /namespace:DST.ReportingService.Models.Hotels /language:CS 1153000-01-08-2019-31-08-2019.xsd

Copy the new create C# classes into the "Models" folder in the project. 