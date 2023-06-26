namespace EasMe.SharpBuilder.Models;

public class SharpProperty
{
    public string AccessModifierString { get; set; } = "public";
    public string GetterAccessModifierString { get; set; } = "";
    public string SetterAccessModifierString { get; set; } = "";
    public string Name { get; set; }
    public Type ValueType { get; set; }
    public bool IsField { get; set; } = false;
    public bool IsStatic { get; set; } = false;
    public bool IsReadOnly { get; set; } = false;
}