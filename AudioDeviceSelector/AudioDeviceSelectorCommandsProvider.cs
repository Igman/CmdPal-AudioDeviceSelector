// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CommandPallet.AudioDeviceSelector;

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
