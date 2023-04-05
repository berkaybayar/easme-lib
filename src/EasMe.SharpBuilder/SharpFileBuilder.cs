using EasMe.SharpBuilder.Models;

namespace EasMe.SharpBuilder;

public class SharpFileBuilder
{
    private readonly SharpFile _sharpFile;

    public SharpFileBuilder()
    {
        _sharpFile = new SharpFile();
    }

    public SharpFileBuilder WithUsing(string usingString)
    {
        _sharpFile.UsingList.Add(usingString);
        return this;
    }

    public SharpFileBuilder WithUsingList(List<string> list)
    {
        _sharpFile.UsingList = list;
        return this;
    }

    public SharpFileBuilder WithNameSpace(string nameSpace)
    {
        _sharpFile.NameSpace = nameSpace;
        return this;
    }

    public SharpFileBuilder WithClass(SharpClass sharpClass)
    {
        _sharpFile.Classes.Add(sharpClass);
        return this;
    }

    public SharpFileBuilder WithClassList(List<SharpClass> list)
    {
        _sharpFile.Classes = list;
        return this;
    }

    public SharpFile Build()
    {
        return _sharpFile;
    }
}