namespace EasMe.SharpBuilder.Models;

public class SharpFile
{
    public List<string> UsingList { get; set; } = new List<string>();
    public string NameSpace { get; set; }
    public List<SharpClass> Classes { get; set; } = new List<SharpClass>();
}