# (c) Martin Korneffel, Oktober 2013
# Rechnen mit mko.Newton 

Clear-Host

Add-Type -Path "C:\trac\projekt\MkoIT\WocServer2012\mko.Newton\bin\Debug\mko.Euklid.dll"
Add-Type -Path "C:\trac\projekt\MkoIT\WocServer2012\mko.Newton\bin\Debug\mko.Newton.dll"

$Len = [mko.Newton.Length]


# Berechnung der Frequenz von grünem Licht nach der Formel f = c/Lambda
$d = $Len::AU(1)
$v1 = $d | Select-Object -Property VectorInBaseUnit

$e = $Len::AU($Len::Lightyear(1))






$lambda = [mko.Newton.Length]::Nanometer(400)
$v =  $lambda | Select-Object -Property Vector
$v[0] | Out-Host

[mko.Newton.Velocity] | Get-Member -Static | Out-Host

[mko.Newton.VelocityInMeterPerSec`1] $c = [mko.Newton.Velocity]::VelocitiyOfLight
$c | Out-Host

#$f = [mko.Newton.Time]::


