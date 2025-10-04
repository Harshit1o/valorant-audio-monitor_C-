@echo off
echo ========================================
echo   VALORANT Audio Monitor Installer
echo ========================================
echo.

REM Get current directory
set "CURRENT_DIR=%~dp0"
set "EXE_PATH=%CURRENT_DIR%VALORANT Audio Monitor.exe"

REM Check if exe exists
if not exist "%EXE_PATH%" (
    echo ERROR: VALORANT Audio Monitor.exe not found!
    echo Please ensure VALORANT Audio Monitor.exe is in the same folder as this installer.
    pause
    exit /b 1
)

echo Found VALORANT Audio Monitor.exe
echo.

REM Create program folder in Program Files
set "INSTALL_DIR=%PROGRAMFILES%\VALORANT Audio Monitor"
echo Creating installation directory...
mkdir "%INSTALL_DIR%" 2>nul

REM Copy executable
echo Installing executable...
copy "%EXE_PATH%" "%INSTALL_DIR%\" >nul
if errorlevel 1 (
    echo ERROR: Failed to copy executable. Run as Administrator.
    pause
    exit /b 1
)

REM Add to startup
echo Adding to Windows startup...
set "STARTUP_DIR=%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup"
echo @echo off > "%STARTUP_DIR%\VALORANT_Audio_Monitor.bat"
echo cd /d "%INSTALL_DIR%" >> "%STARTUP_DIR%\VALORANT_Audio_Monitor.bat"
echo start "" "VALORANT Audio Monitor.exe" >> "%STARTUP_DIR%\VALORANT_Audio_Monitor.bat"

echo.
echo ========================================
echo   Installation Complete!
echo ========================================
echo.
echo The VALORANT Audio Monitor has been installed to:
echo   %INSTALL_DIR%
echo.
echo It will automatically start when Windows boots.
echo.
echo To start it now, press any key...
pause >nul

cd /d "%INSTALL_DIR%"
start "" "VALORANT Audio Monitor.exe"

echo Program started! Check the console window for status.
echo.
echo To uninstall, delete:
echo   - %INSTALL_DIR%
echo   - %STARTUP_DIR%\VALORANT_Audio_Monitor.bat
echo.
pause