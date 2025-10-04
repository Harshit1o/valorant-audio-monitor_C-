# VALORANT Audio Monitor

🎮 **Automatic audio recording for VALORANT gameplay sessions**

## What it does
- Monitors for VALORANT process automatically
- Starts audio recording when game launches
- Stops recording when game closes
- Runs silently in the background

## Download & Install

### Quick Install (Windows)
1. **Download** the latest release: [Download C#_sdk.exe](https://github.com/Harshit1o/valorant-audio-monitor_C-/releases)
2. **Download** the installer: [Download install.bat](https://github.com/Harshit1o/valorant-audio-monitor_C-/raw/master/install.bat)
3. **Run** `install.bat` as Administrator
4. **Done!** The monitor will start automatically with Windows

### Manual Install
1. Download `C#_sdk.exe`
2. Place it anywhere on your computer
3. Create a shortcut in your Windows Startup folder:
   - Press `Win + R`, type `shell:startup`
   - Copy the `.exe` or create a shortcut there

## Usage
- The program runs in the background
- Look for console messages:
  - ✅ "Found it! 'VALORANT.exe' is running." - Recording started
  - ❌ "'VALORANT.exe' is not running." - Recording stopped
- Audio files are saved to: `[Location where recordings are saved]`

## Requirements
- Windows 10/11
- No additional software needed (self-contained)

## Troubleshooting
- **Program won't start**: Right-click → "Run as Administrator"
- **No audio recording**: Check Windows audio permissions
- **High CPU usage**: This shouldn't happen (program checks every 5 seconds)

## Uninstall
Delete these files:
- The installed program folder
- Startup shortcut in `%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup`

---
*Made for VALORANT players who want automatic gameplay recording* 🎯