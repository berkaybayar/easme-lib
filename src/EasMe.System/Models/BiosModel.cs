namespace EasMe.System.Models;

public class BiosModel
{
  public string? BiosCharacteristics { get; set; }
  public string? BiosVersion { get; set; }
  public string? Caption { get; set; }
  public string? Description { get; set; }
  public string? EmbeddedControllerMajorVersion { get; set; }
  public string? EmbeddedControllerMinorVersion { get; set; }
  public string? Manufacturer { get; set; }
  public string? Name { get; set; }
  public string? PrimaryBios { get; set; }
  public string? ReleaseDate { get; set; }

  public string? SerialNumber { get; set; }

  // public string? SMBIOSBIOSVersion { get; set; }
  // public string? SMBIOSMajorVersion { get; set; }
  // public string? SMBIOSMinorVersion { get; set; }
  // public string? SMBIOSPresent { get; set; }
  public string? SoftwareElementId { get; set; }
  public string? SoftwareElementState { get; set; }
  public string? Status { get; set; }
  public string? SystemBiosMajorVersion { get; set; }
  public string? SystemBiosMinorVersion { get; set; }
  public string? TargetOperatingSystem { get; set; }
  public string? Version { get; set; }
}