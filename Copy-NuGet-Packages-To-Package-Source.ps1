# 18.6.2013, Martin Korneffel
# Kopiert alle NuGet- Pakete in der Projektmappe zum zentralen Paketordner
Get-ChildItem -Path c:\trac\projekt\MkoIT\WocServer2012  -Recurse | Where-Object{$_.Name -like "*.nupkg"} | Copy-Item -Destination c:\trac\NuGet\Packages 







