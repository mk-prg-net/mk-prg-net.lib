## 19.6.2013, Martin Korneffel
# Kopiert alle NuGet- Pakete in der Projektmappe zum zentralen Paketordner
Clear-Host
$NuGetDir = 'c:\trac\Nuget\'
$NuGetExe = $NuGetDir + 'Nuget.exe'
$DirProjektmappe = 'c:\trac\projekt\MkoIT\WocServer2012'

$Nuspecs = Get-ChildItem -Path $DirProjektmappe  -Recurse | Where-Object{$_.Name -match '^mko.\w+.*nuspec$' -and -not ($_.Name -match 'test')} 

# Nuspec- Erweiterung durch .csproj austauschen
$Csprojs = $Nuspecs | Select-Object -Property @{Name="Name"; Expression = {$_.Name.Replace("nuspec", "csproj")}}, DirectoryName 

foreach($Finfo in $Csprojs){
	$Filename = $Finfo.DirectoryName+'\'+$Finfo.Name
	$Dirname = $Finfo.DirectoryName
	&$NuGetExe pack "$Filename" -OutputDirectory  $Dirname -BasePath $Dirname -IncludeReferencedProjects -NonInteractive	
}

