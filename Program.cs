using System.Diagnostics;
using NAudio.Wave;
using System.Windows.Forms;

// Create a mutex to ensure only one instance runs
using var mutex = new Mutex(true, "VALORANTAudioMonitor", out bool isNewInstance);
if (!isNewInstance)
{
    return; // Another instance is already running
}

string processNameToFind = "notepad";
string savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

WaveInEvent? waveIn = null;
WaveFileWriter? writer = null;
bool isRecording = false;

// Create system tray icon
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

NotifyIcon trayIcon = new NotifyIcon()
{
    Icon = SystemIcons.Application,
    Text = "VALORANT Audio Monitor",
    Visible = true
};

// Add context menu to tray icon
ContextMenuStrip contextMenu = new ContextMenuStrip();
contextMenu.Items.Add("Exit", null, (s, e) => Application.Exit());
trayIcon.ContextMenuStrip = contextMenu;

// Background monitoring thread
Thread monitorThread = new Thread(() =>
{

while (true)
{
    Process[] processes = Process.GetProcessesByName(processNameToFind);

    if (processes.Length > 0 && !isRecording)
    {
        isRecording = true;
        string outputFileName = $"recording_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.wav";
        string fullOutputPath = Path.Combine(savePath, outputFileName);

        waveIn = new WaveInEvent();
        writer = new WaveFileWriter(fullOutputPath, waveIn.WaveFormat);

        waveIn.DataAvailable += (sender, args) => 
        {
            writer.Write(args.Buffer, 0, args.BytesRecorded);
        };
        
        waveIn.StartRecording();
    }
    else if (processes.Length == 0 && isRecording)
    {
        waveIn?.StopRecording();
        writer?.Dispose();
        waveIn?.Dispose();
        
        writer = null;
        waveIn = null;
        isRecording = false; 
    }

    Thread.Sleep(5000);
}
});

monitorThread.IsBackground = true;
monitorThread.Start();

// Keep application running
Application.Run();