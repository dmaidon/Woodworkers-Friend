# Miter Angle Calculator - Quick Diagnostic Script

## Run this PowerShell script to check both issues:

```powershell
# Check Help Database
Write-Host "=== CHECKING HELP DATABASE ===" -ForegroundColor Cyan
$helpDb = "$env:APPDATA\WoodworkersFriend\Resources\Help.db"

if (Test-Path $helpDb) {
    Write-Host "✓ Help.db exists" -ForegroundColor Green
    
    # Check if sqlite3 is available
    try {
        $result = sqlite3 $helpDb "SELECT ModuleName, Title, Category, SortOrder FROM HelpContent WHERE ModuleName='MiterAngle';" 2>$null
        if ($result) {
            Write-Host "✓ Miter Angle help found:" -ForegroundColor Green
            Write-Host $result
        } else {
            Write-Host "✗ Miter Angle help NOT found in database" -ForegroundColor Red
        }
        
        # List all Calculators category items
        Write-Host "`n=== ALL CALCULATOR HELP TOPICS ===" -ForegroundColor Cyan
        $calcs = sqlite3 $helpDb "SELECT ModuleName, Title, SortOrder FROM HelpContent WHERE Category='Calculators' ORDER BY SortOrder;"
        Write-Host $calcs
        
    } catch {
        Write-Host "! sqlite3 not in PATH, checking with .NET..." -ForegroundColor Yellow
        
        # Alternative check using .NET
        Add-Type -AssemblyName System.Data
        $conn = New-Object System.Data.SQLite.SQLiteConnection("Data Source=$helpDb;Version=3;Read Only=True;")
        $conn.Open()
        
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = "SELECT ModuleName, Title, Category FROM HelpContent WHERE ModuleName='MiterAngle'"
        $reader = $cmd.ExecuteReader()
        
        if ($reader.Read()) {
            Write-Host "✓ Miter Angle help found:" -ForegroundColor Green
            Write-Host "  ModuleName: $($reader['ModuleName'])"
            Write-Host "  Title: $($reader['Title'])"
            Write-Host "  Category: $($reader['Category'])"
        } else {
            Write-Host "✗ Miter Angle help NOT found" -ForegroundColor Red
        }
        
        $reader.Close()
        $conn.Close()
    }
} else {
    Write-Host "✗ Help.db does NOT exist at: $helpDb" -ForegroundColor Red
}

Write-Host "`n=== CHECKING ERROR LOG ===" -ForegroundColor Cyan
$errorLog = "$env:APPDATA\WoodworkersFriend\ErrorLog.txt"

if (Test-Path $errorLog) {
    Write-Host "✓ Error log exists" -ForegroundColor Green
    Write-Host "`nLast 20 Miter Angle related messages:" -ForegroundColor Yellow
    Get-Content $errorLog | Select-String "MiterAngle|Miter Angle" | Select-Object -Last 20
} else {
    Write-Host "! No error log found" -ForegroundColor Yellow
}

Write-Host "`n=== INSTRUCTIONS ===" -ForegroundColor Cyan
Write-Host "1. If help exists but doesn't show in UI:"
Write-Host "   - Check Help tab → Look for category filter/dropdown"
Write-Host "   - Try searching for 'miter' in search box"
Write-Host "   - Check if 'Calculators' category exists in dropdown"
Write-Host ""
Write-Host "2. If calculator doesn't initialize:"
Write-Host "   - Run the app and navigate to Angles tab"
Write-Host "   - Check error log for 'ForceMiterAngleInitialization' messages"
Write-Host "   - Look for any exceptions or errors"
Write-Host ""
Write-Host "3. To force help refresh:"
Write-Host "   Remove-Item '$helpDb' -Force"
Write-Host "   # Then restart the application"
```

Save this as `Test-MiterAngle.ps1` and run it!
