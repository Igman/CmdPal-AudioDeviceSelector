using System;

namespace CommandPalette.AudioDeviceSelector.Interop;

internal static class PropertyKeys
{
    public static readonly PROPERTYKEY PKEY_ItemNameDisplay = new PROPERTYKEY
    {
        fmtid = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac"),
        pid = new UIntPtr(10)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_Background = new PROPERTYKEY
    {
        fmtid = new Guid("86d40b4d-9069-443c-819a-2a54090dccec"),
        pid = new UIntPtr(4)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_PackageInstallPath = new PROPERTYKEY
    {
        fmtid = new Guid("9f4c2855-9f79-4b39-a8d0-e1d42de1d5f3"),
        pid = new UIntPtr(15)
    };

    public static readonly PROPERTYKEY PKEY_Tile_SmallLogoPath = new PROPERTYKEY
    {
        fmtid = new Guid("86d40b4d-9069-443c-819a-2a54090dccec"),
        pid = new UIntPtr(2)
    };

    public static readonly PROPERTYKEY PKEY_AppUserModel_PackageFullName = new PROPERTYKEY
    {
        fmtid = new Guid("9f4c2855-9f79-4b39-a8d0-e1d42de1d5f3"),
        pid = new UIntPtr(21)
    };

    public static readonly PROPERTYKEY PKEY_Device_FriendlyName = new PROPERTYKEY
    {
        fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"),
        pid = new UIntPtr(14)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndPoint_Interface = new PROPERTYKEY
    {
        fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"),
        pid = new UIntPtr(2)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndpoint_PhysicalSpeakers = new PROPERTYKEY
    {
        fmtid = new Guid("1da5d803-d492-4edd-8c23-e0c0ffee7f0e"),
        pid = new UIntPtr(3)
    };

    public static readonly PROPERTYKEY PKEY_AudioEndpoint_FormFactor = new PROPERTYKEY
    {
        fmtid = new Guid("1da5d803-d492-4edd-8c23-e0c0ffee7f0e"),
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
