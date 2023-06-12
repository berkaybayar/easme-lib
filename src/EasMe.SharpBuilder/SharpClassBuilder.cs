using EasMe.SharpBuilder.Models;

namespace EasMe.SharpBuilder;

public class SharpClassBuilder {
    private readonly SharpClass _sharpClass;

    public SharpClassBuilder() {
        _sharpClass = new SharpClass();
    }

    public SharpClassBuilder WithAccessModifier(string accessModifier) {
        _sharpClass.AccessModifierString = accessModifier;
        return this;
    }


    public SharpClassBuilder WithName(string name) {
        _sharpClass.Name = name;
        return this;
    }

    public SharpClassBuilder WithProperties(List<SharpProperty> properties) {
        _sharpClass.Properties = properties;
        return this;
    }

    public SharpClassBuilder WithIsStatic(bool isStatic) {
        _sharpClass.IsStatic = isStatic;
        return this;
    }

    public SharpClassBuilder WithIsPartial(bool isPartial) {
        _sharpClass.IsPartial = isPartial;
        return this;
    }

    public SharpClassBuilder WithIsAbstract(bool isAbstract) {
        _sharpClass.IsAbstract = isAbstract;
        return this;
    }

    public SharpClassBuilder WithIsSealed(bool isSealed) {
        _sharpClass.IsSealed = isSealed;
        return this;
    }

    public SharpClassBuilder WithIsInterface(bool isInterface) {
        _sharpClass.IsInterface = isInterface;
        return this;
    }


    public SharpClassBuilder WithIsEnum(bool isEnum) {
        _sharpClass.IsEnum = isEnum;
        return this;
    }

    public SharpClassBuilder WithIsStruct(bool isStruct) {
        _sharpClass.IsStruct = isStruct;
        return this;
    }

    public SharpClassBuilder WithIsDelegate(bool isDelegate) {
        _sharpClass.IsDelegate = isDelegate;
        return this;
    }

    public SharpClassBuilder WithIsRecord(bool isRecord) {
        _sharpClass.IsRecord = isRecord;
        return this;
    }

    public SharpClassBuilder WithIsReadOnly(bool isReadOnly) {
        _sharpClass.IsReadOnly = isReadOnly;
        return this;
    }

    public SharpClassBuilder WithIsUnsafe(bool isUnsafe) {
        _sharpClass.IsUnsafe = isUnsafe;
        return this;
    }

    public SharpClassBuilder WithIsNew(bool isNew) {
        _sharpClass.IsNew = isNew;
        return this;
    }


    public SharpClassBuilder WithIsVirtual(bool isVirtual) {
        _sharpClass.IsVirtual = isVirtual;
        return this;
    }

    public SharpClassBuilder WithIsOverride(bool isOverride) {
        _sharpClass.IsOverride = isOverride;
        return this;
    }

    public SharpClassBuilder WithIsExtern(bool isExtern) {
        _sharpClass.IsExtern = isExtern;
        return this;
    }

    public SharpClassBuilder WithIsConst(bool isConst) {
        _sharpClass.IsConst = isConst;
        return this;
    }

    public SharpClass Build() {
        return _sharpClass;
    }
}
