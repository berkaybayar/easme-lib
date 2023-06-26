using System.Text;
using EasMe.SharpBuilder.Models;

namespace EasMe.SharpBuilder;

public static class SharpExtensions
{
    public static void Export(this SharpFile file) {
        var sb = new StringBuilder();
        foreach (var item in file.UsingList) sb.AppendLine("using " + item + ";");
        sb.AppendLine();
        sb.AppendLine("namespace " + file.NameSpace + " {");
        foreach (var item in file.Classes) {
            sb.Append(item.AccessModifierString);
            if (item.IsStatic) sb.Append(" static");
            if (item.IsPartial) sb.Append(" partial");
            if (item.IsAbstract) sb.Append(" abstract");
            if (item.IsSealed) sb.Append(" sealed");
            if (item.IsInterface) sb.Append(" interface");
            if (item.IsEnum) sb.Append(" enum");
            if (item.IsStruct) sb.Append(" struct");
            if (item.IsDelegate) sb.Append(" delegate");
            if (item.IsRecord) sb.Append(" record");

            sb.Append(" class " + item.Name + " {");
            sb.AppendLine();
            foreach (var prop in item.Properties) {
                sb.Append(prop.AccessModifierString);
                if (prop.IsStatic) sb.Append(" static");
                sb.Append(prop.ValueType.Name);
                if (prop.IsReadOnly) sb.Append(" readonly");

                if (prop.IsField) {
                    sb.Append(prop.Name + ";");
                    sb.AppendLine();
                    continue;
                }

                sb.Append(" " + prop.Name + " { ");
                if (prop.GetterAccessModifierString != "") sb.Append(prop.GetterAccessModifierString + " ");
                sb.Append("get;");
                if (prop.SetterAccessModifierString != "") sb.Append(prop.SetterAccessModifierString + " ");
                sb.Append("set;");
                sb.Append("}");
                sb.AppendLine();
            }

            sb.AppendLine("}");
            sb.AppendLine();
        }

        sb.AppendLine("}");
        var text = sb.ToString();
        File.WriteAllText("exported.cs", text);
    }
}