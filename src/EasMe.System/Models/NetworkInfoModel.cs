using EasMe.Extensions;

namespace EasMe.System.Models;

public class NetworkInfoModel
{
    private string? _IpAddress;

    public string? IpAddress
    {
        get => _IpAddress;
        set => _IpAddress = value.IsNullOrEmpty() ? null : value;
    }

    public string? Location { get; set; }
    public bool IsWarpOn { get; set; } = false;
    public bool IsGatewayOn { get; set; } = false;
}