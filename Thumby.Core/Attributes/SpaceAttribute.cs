namespace Thumby.Core.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SpaceAttribute(int space) : Attribute
{
    public int Space { get; } = space;
}
