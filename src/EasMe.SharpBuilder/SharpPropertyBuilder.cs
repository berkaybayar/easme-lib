using EasMe.SharpBuilder.Models;

namespace EasMe.SharpBuilder;

public class SharpPropertyBuilder {
    private readonly SharpProperty _property;

    public SharpPropertyBuilder() {
        _property = new SharpProperty();
    }

    public SharpPropertyBuilder WithAccessModifier(string accessModifier) {
        _property.AccessModifierString = accessModifier;
        return this;
    }

    public SharpPropertyBuilder WithGetterAccessModifier(string getterAccessModifier) {
        _property.GetterAccessModifierString = getterAccessModifier;
        return this;
    }

    public SharpPropertyBuilder WithSetterAccessModifier(string setterAccessModifier) {
        _property.SetterAccessModifierString = setterAccessModifier;
        return this;
    }

    public SharpPropertyBuilder WithName(string name) {
        _property.Name = name;
        return this;
    }

    public SharpPropertyBuilder WithValueType(Type valueType) {
        _property.ValueType = valueType;
        return this;
    }

    public SharpPropertyBuilder WithIsField(bool isField) {
        _property.IsField = isField;
        return this;
    }

    public SharpPropertyBuilder WithIsStatic(bool isStatic) {
        _property.IsStatic = isStatic;
        return this;
    }

    public SharpPropertyBuilder ReadOnly() {
        _property.IsReadOnly = true;
        return this;
    }
    public SharpProperty Build() {
        return _property;
    }
}