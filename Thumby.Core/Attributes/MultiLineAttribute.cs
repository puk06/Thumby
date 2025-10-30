namespace Thumby.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class MultiLineAttribute : Attribute
{
    // テキストボックスを複数行対応させるかどうかのAttribute
}
