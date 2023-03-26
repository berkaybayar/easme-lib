namespace EasMe.SharpBuilder.Models;

public class SharpClass
{
    public string AccessModifierString { get; set; } = "public";
    public string Name { get; set; }
    public List<SharpProperty> Properties { get; set; } = new List<SharpProperty>();
    public bool IsStatic { get; set; } = false;
    public bool IsPartial { get; set; } = false;
    public bool IsAbstract { get; set; } = false;
    public bool IsSealed { get; set; } = false;
    public bool IsInterface { get; set; } = false;
    public bool IsEnum { get; set; } = false;
    public bool IsStruct { get; set; } = false;
    public bool IsDelegate { get; set; } = false;
    public bool IsRecord { get; set; } = false;
    public bool IsReadOnly { get; set; } = false;
    public bool IsUnsafe { get; set; } = false;
    public bool IsNew { get; set; } = false;
    public bool IsVirtual { get; set; } = false;
    public bool IsOverride { get; set; } = false;
    public bool IsExtern { get; set; } = false;
    public bool IsConst { get; set; } = false;
}