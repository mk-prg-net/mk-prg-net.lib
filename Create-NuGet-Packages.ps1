## 19.6.2013, Martin Korneffel
# Create-NuGetPackages.ps1
# Erstellt alle Nuget- Pakete in einer Projektmappe, für die bereits ein nuspec- File angelegt wurde.
# Kopiert anschließend alle NuGet- Pakete in den zentralen Packetordner
Clear-Host

$params = @{}

# Liest alle Definitionen für die Programmkonstanten aus einer CSV- Konfigdatei ein
# Diese hat folgenden Namen: Create-NuGet-Packages.ps1.config
# Sie hat folgenden Aufbau: <Name Konstante>, <Wert Konstante>
$Scriptfilename = $MyInvocation.MyCommand.Path
$CsvLines = Import-Csv "$Scriptfilename.config" | Select -Property Name, Value 
$CsvLines | ForEach-Object{$params[$_.Name] = $_.Value}



# Prüft, ob die Args eine Definition für eine Programmkonstante enthalten
function HasParam {
	param($MainArgs, [int]$i, [string]$PName)
	if($MainArgs[$i].Trim().ToLower() -eq $PName.Trim().ToLower() -and $i -lt $MainArgs.Length) 
	{
		return $true
	}else 
	{
		return $false
	}
		
}

# Liest alle Definitionen für Programmkonstanten aus den Befehlzeilenargumenten ein 
# (höhere Priorität als Einträge in CSV- Datei)
for([int]$i= 0; $i -lt $args.Length; $i +=1)
{
	if(HasParam $args $i "NuGetDir"`
		-or HasParam $args $i "NuGetExe"`
		-or HasParam $args $i "SolutionFolder"`
		-or HasParam $args $i "PackageFolder" )
	{
		$params[$args[$i]] = $args[$i+1]; $i += 1;
	} 	
}

# Definition der Programmkonstanten
$NuGetDir = 'c:\trac\Nuget\'
if($params.ContainsKey("NuGetDir")){$NuGetDir = $params["NuGetDir"];}

$NuGetExe = $NuGetDir + 'Nuget.exe'
if($params.ContainsKey("NuGetExe")){$NuGetExe = $NuGetDir + $params["NuGetExe"];}

$SolutionFolder = 'c:\trac\projekt\MkoIT\WocServer2012'
if($params.ContainsKey("SolutionFolder")){$SolutionFolder = $params["SolutionFolder"];}

$PackageFolder = 'c:\trac\packages'
if($params.ContainsKey("PackageFolder")){$PackageFolder = $params["PackageFolder"];}


# Ausgabe der Grundeinstellungen
Write-Host 	"NuGetDir       : $NuGetDir`n"`
 			"NuGetExe       : $NuGetExe`n"`
			"SolutionFolder : $SolutionFolder`n"`
			"PackageFolder  : $PackageFolder`n";


# Alle Dateipfade von Nuspecs in der Projektmappe ermitteln
$Nuspecs = Get-ChildItem -Path $SolutionFolder  -Recurse |`
Where-Object{	($_.Name -match '^mko.*nuspec$' -or`
                 $_.Name -match '^MkPrgNet.*nuspec$' -or`
				#$_.Name -match '^FeatureCollector.*nuspec$' -or`
				$_.Name -eq 'DirTree.nuspec' # -or`
				#$_.Name -eq 'FileClassificator.nuspec' -or`
				#$_.Name -match '^KeplerBI.*nuspec$' -or`				
				#$_.Name -eq 'DB.Kepler.EF50.nuspec' -or`
				#$_.Name -eq 'TextGenerator.nuspec'
				) -and -not ($_.Name -match '\.test$')} 
Write-Host "Liste aller gefundenen Nuspecs:"
$Nuspecs | Select -Property FullName | Out-Host


# Nuspec- Erweiterung in den Pfaden durch .csproj austauschen -> Liste der betroffenen C#- Projekte erstellen
$Csprojs = $Nuspecs |`
Select-Object -Property @{Name="Name"; Expression = {$_.Name.Replace("nuspec", "csproj")}}, DirectoryName 
Write-Host "Liste aller betroffenen Projekte:"
$Csprojs | Out-Host

# NuGet pack für alle betroffenen C#- Projekte ausführen
foreach($Finfo in $Csprojs){
	$Filename = $Finfo.DirectoryName+'\'+$Finfo.Name
	$Dirname = $Finfo.DirectoryName
	&$NuGetExe pack "$Filename" -OutputDirectory  $Dirname -BasePath $Dirname -IncludeReferencedProjects -NonInteractive	
}

&$NuGetExe pack $SolutionFolder"\TextGenerator\TextGenerator.vbproj" -OutputDirectory  $Dirname -BasePath $Dirname -IncludeReferencedProjects -NonInteractive	


# 18.6.2013, Martin Korneffel
# Kopiert alle NuGet- Pakete aus der Projektmappe zum zentralen Paketordner
Get-ChildItem -Path $SolutionFolder  -Recurse |`
Where-Object{$_.Name -like "*.nupkg"} |`
Copy-Item -Destination $PackageFolder

Write-Host "Fertig."
