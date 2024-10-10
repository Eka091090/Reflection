using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace hw6;


internal class Program
{

    public static TestClass MakeTestclass()
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass) as TestClass;    
    }

    public static TestClass MakeTestclass(int i)
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass, new object[] { i }) as TestClass;  

    }

    public static TestClass MakeTestclass(int i, string s, decimal d, char[] c)
    {
        Type testclass = typeof(TestClass);
        return Activator.CreateInstance(testclass, new object[] { i, s, d, c }) as TestClass;  
    }

    public static string ObjectToString(object o)
    {
        Type type = o.GetType();
        StringBuilder res = new StringBuilder();

        res.Append(type.AssemblyQualifiedName + ":");
        res.Append(type.Name + "|");
        var prop = type.GetProperties();
        foreach (var p in prop)
        {
            var temp = p.GetValue(o);
            res.Append(p.Name + ":");
            if (p.PropertyType == typeof(char[]))
                res.Append(new string(temp as char[]) + "|");
            else
            {
                res.Append(temp);
                res.Append("|");
            } 
        }    
        return res.ToString();
    }

    public static object StringToObject(string s)
    {
        string[] arr = s.Split("|");
        string[] arr1 = arr[0].Split(":");
        object some = Activator.CreateInstance(null, arr1[0].Split(",")[0]);

        if(arr.Length > 1 && some != null)
        {
            var type = some.GetType();
            for (int i = 1; i < arr.Length; i++)
            {
                string[] nameAndValue = arr[i].Split(":");
                var p = type.GetProperty(nameAndValue[0]);
                if (p == null)
                    continue;
                if (p.PropertyType == typeof(int)) 
                    p.SetValue(some, int.Parse(nameAndValue[1]));
                else if (p.PropertyType == typeof(string))
                    p.SetValue(some, nameAndValue[1]);
                else if (p.PropertyType == typeof(decimal))
                    p.SetValue(some, decimal.Parse(nameAndValue[1]));
                else if (p.PropertyType == typeof(char[]))
                    p.SetValue(some, nameAndValue[1].ToCharArray());

            }
        }
        return some;
    }

    static void Main(string[] args)
    {
        var n1 = MakeTestclass();
        var n2 = MakeTestclass(5);
        char[] somearr = { 'a', 'b', 'c'};
        var n3 = MakeTestclass(8, "some", 1, somearr);

        System.Console.WriteLine(ObjectToString(n3));

        string some = ObjectToString(n3);
        System.Console.WriteLine(some);

        var some1 = StringToObject(some);
        System.Console.WriteLine(ObjectToString(some1));
    }
}