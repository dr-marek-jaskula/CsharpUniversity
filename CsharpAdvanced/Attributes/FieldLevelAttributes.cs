namespace CsharpAdvanced.Attributes;

public class FieldLevelAttributes
{
    //In order to place the attribute on the level of field (that is auto-generated) we need to do the following 
    [field: Obsolete] //this attributes is only on the field level, not the property level
    public int MyCustomProperty { get; set; }
}

