// ----------------------------------------------------
// FILE: Program.cs (Version 2 with Audio Recording)
// ----------------------------------------------------

using System.Diagnostics;
// We need to import the NAudio libraries we just installed.
using NAudio.Wave;

string processNameToFind = "notepad";

// --- CONFIGURATION ---
// 📁 Here's where we set the save location!
// This gets the path to your Desktop. You can change this to any folder you like,
// for example: @"C:\MyRecordings"
string savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

Console.WriteLine($"🚀 Starting process monitor. Looking for '{processNameToFind}.exe'");
Console.WriteLine($"🔊 Audio recordings will be saved to: {savePath}");

// --- STATE MANAGEMENT ---
// These variables will hold our audio recording objects.
WaveInEvent? waveIn = null;         // The microphone input.
WaveFileWriter? writer = null;       // The object that writes the .wav file.
bool isRecording = false;          // A flag to track if we are currently recording.

// The main loop for our program.
while (true)
{
    Process[] processes = Process.GetProcessesByName(processNameToFind);

    // --- LOGIC TO START RECORDING ---
    // If the process is running AND we are NOT currently recording...
    if (processes.Length > 0 && !isRecording)
    {
        isRecording = true; // Set our flag to true.
        
        // Create a unique filename with the current date and time.
        string outputFileName = $"recording_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.wav";
        string fullOutputPath = Path.Combine(savePath, outputFileName);
        
        Console.WriteLine($"🎤 STARTING recording to {outputFileName}...");

        // Initialize the microphone input object.
        waveIn = new WaveInEvent();
        
        // Initialize the .wav file writer.
        writer = new WaveFileWriter(fullOutputPath, waveIn.WaveFormat);

        // This is the most important part:
        // When the microphone gets data, this event fires. We tell it to
        // write that data directly into our .wav file.
        waveIn.DataAvailable += (sender, args) => 
        {
            writer.Write(args.Buffer, 0, args.BytesRecorded);
        };
        
        // Start listening to the microphone.
        waveIn.StartRecording();
    }
    // --- LOGIC TO STOP RECORDING ---
    // If the process is NOT running AND we ARE currently recording...
    else if (processes.Length == 0 && isRecording)
    {
        Console.WriteLine("🛑 STOPPING recording...");
        
        // Stop the microphone.
        waveIn?.StopRecording();
        
        // Important: Dispose of the objects to save the file correctly and release resources.
        // The '?' checks if the object is not null before trying to dispose it.
        writer?.Dispose();
        waveIn?.Dispose();
        
        // Set objects to null and reset our flag.
        writer = null;
        waveIn = null;
        isRecording = false; 
    }

    // Wait for 1 second before checking again.
    Thread.Sleep(1000);
}