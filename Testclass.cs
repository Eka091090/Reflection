
namespace hw6;

class TestClass
{
    [CustomName("CustomName")]
    public int I { get; set; }

    [CustomName("CustomName")]
    public string? S { get; set; }

    [CustomName("CustomName")]
    public decimal D { get; set; }

    [CustomName("CustomName")]
    public char[]? C { get; set; }

    public TestClass()
    { }

    private TestClass(int i)
    {
        I = I;
    }

    public TestClass(int i, string s, decimal d, char[] c) : this(i)
    {
        S = s;
        D = d;
        C = c;
    }
}



[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class CustomNameAttribute : Attribute
{
    private string v;

    public CustomNameAttribute(string v)
    {
        this.v = v;
    }

    public string Name { get; private set; }

    public string CustomNameAttrbute(string name)
    {
        Name = name;
        return name;
    }
}