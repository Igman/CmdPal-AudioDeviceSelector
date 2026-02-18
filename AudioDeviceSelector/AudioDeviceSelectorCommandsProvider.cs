using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CommandPalette.AudioDeviceSelector;

public partial class AudioDeviceSelectorCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public AudioDeviceSelectorCommandsProvider()
    {
        DisplayName = "Audio Device Selector";
        Icon = new IconInfo("🔊");
        _commands = [
            new CommandItem(new AudioDeviceSelectorPage())
            {
                Title = DisplayName,
                Subtitle = "Select and switch audio output devices",
                Icon = new IconInfo("🔊")
            },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
