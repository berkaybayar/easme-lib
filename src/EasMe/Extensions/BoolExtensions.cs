namespace EasMe.Extensions;

public static class BoolExtensions
{
  public static bool GetValueOrDefault(bool? value) {
    if (value == null) return default;
    return (bool)value;
  }
}