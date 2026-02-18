using System;

namespace CommandPallet.AudioDeviceSelector.Interop;

internal static class PropertyKeys
{
    public static readonly PROPERTYKEY PKEY_ItemNameDisplay = new PROPERTYKEY
    {
        fmtid = new Guid("{B725F130-47EF-101A-A5F1-02608C9EEBAC}"),
        pid = new UIntPtr(10)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_Background = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{86D40B4D-9069-443C-819A-2A54090DCCEC}"),
        pid = new UIntPtr(4)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_PackageInstallPath = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"),
        pid = new UIntPtr(15)
    };

    public static readonly PROPERTYKEY PKEY_Tile_SmallLogoPath = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{86D40B4D-9069-443C-819A-2A54090DCCEC}"),
        pid = new UIntPtr(2)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_PackageFullName = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"),
        pid = new UIntPtr(21)
    };

    public static readonly PROPERTYKEY PKEY_Device_FriendlyName = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{a45c254e-df1c-4efd-8020-67d146a850e0}"),
        pid = new UIntPtr(14)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndPoint_Interface = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{a45c254e-df1c-4efd-8020-67d146a850e0}"),
        pid = new UIntPtr(2)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndpoint_PhysicalSpeakers = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{1da5d803-d492-4edd-8c23-e0c0ffee7f0e}"),
        pid = new UIntPtr(3)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndpoint_FormFactor = new PROPERTYKEY
    {
        fmtid = Guid.Parse("{1da5d803-d492-4edd-8c23-e0c0ffee7f0e}"),
        pid = new UIntPtr(0)
    };

    /// <summary>
    /// Formats a PROPERTYKEY as a string for use with DeviceInformation.FindAllAsync
    /// Format: {fmtid} pid
    /// </summary>
    public static string FormatPropertyKey(PROPERTYKEY key)
    {
        return $"{{{key.fmtid.ToString().ToLower()}}} {key.pid.ToUInt32()}";
    }
}
