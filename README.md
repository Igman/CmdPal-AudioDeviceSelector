# Audio Device Selector

A Command Palette extension for Windows that allows you to quickly switch between audio output devices directly from the command palette. [It is based on the equivalent extension for FlowLauncher](https://github.com/attilakapostyak/Flow.Launcher.Plugin.AudioDeviceSelector), in particular the interop code was very helpful for this project.

![Alt text](/AudioDeviceSelector/Assets/Screenshot.png?raw=true "Screenshot")

## Features

- **List audio devices** - View all available audio output devices with visual indicators
- **Switch devices instantly** - Change your default audio device in seconds
- **Device type icons** - Visual indicators for different device types (Headphones, Speakers, Microphones, etc.)
- **All audio roles** - Automatically switches device for Console, Multimedia, and Communications roles

## Requirements

- **Windows 10/11**
- **.NET 9** or later
- [Command Palette](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/overview)

## Installation

1. Clone the repository:

2. Open `AudioDeviceSelector.sln` in Visual Studio 2022 or later

3. Build the project:
   - Right-click the solution and select **Build Solution** (or press `Ctrl+Shift+B`)
   - The build output should show a successful compilation

4. Deploy the solution to Command Palette:
   - Right-click the project and select **Deploy**

5. In Command Palette reload the extensions via the reload command

## Usage

Once installed, you can use the extension through your Command Palette host:

1. Open the Command Palette
2. Search for "audio" or "device"
3. Select an audio device from the list to make it your default
4. The device will be set as default for all audio roles (Console, Multimedia, Communications)

## Project Structure

```
AudioDeviceSelector/
 Commands/                               # Command implementations
 Pages/                                  # UI pages
   AudioDeviceSelectorPage.cs            # The page shown listing all audio devices  
 Services/                               # Core service logic
   AudioDeviceService.cs                 # Service wrapper around the COM api in Interop
 Interop/                                # Windows Audio API interop definitions
 Program.cs                              # Application entry point
 AudioDeviceSelector.cs                  # Main plugin class
 AudioDeviceSelectorCommandsProvider.cs  # Command provider
```

# Development

## Prerequisites

- Visual Studio 2022 or later
- Windows SDK (included with Visual Studio)
- .NET 9 SDK

## Building from Source

```bash
dotnet build AudioDeviceSelector.csproj
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For issues and questions, please open an issue on the [GitHub repository](https://github.com/microsoft/CmdPal-AudioDeviceSelector/issues).
