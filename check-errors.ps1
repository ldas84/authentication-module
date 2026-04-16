Write-Host "Analizando errores de compilacion..." -ForegroundColor Cyan

$logPath = "$PSScriptRoot/build.log"

dotnet build Auth.slnx -v m > $logPath 2>&1

Write-Host "`nErrores encontrados:" -ForegroundColor Yellow
Select-String -Path $logPath -Pattern "error CS" | ForEach-Object {
    Write-Host $_.Line -ForegroundColor Red
}

Write-Host "`nWarnings encontrados:" -ForegroundColor Yellow
Select-String -Path $logPath -Pattern "warning CS" | ForEach-Object {
    Write-Host $_.Line -ForegroundColor DarkYellow
}

Write-Host "`nAnalisis completado." -ForegroundColor Green