namespace brainsharp;

internal class Program
{
    const int size = 30000;
    static int i = 0;
    private static char[] arr = new char[size];

    static void Eval(char c)
    {
        switch (c)
        {
            case '>': i++; break;
            case '<': i--; break;
            case '+': arr[i]++; break;
            case '-': arr[i]--; break;
            case '.': Console.Write(arr[i]); break;
            case ',': arr[i] = Console.ReadKey().KeyChar; break;
            case '[': break;
            case ']': break;
        };
    }
    
    static void Scan(string input)
    {
        var nInput = input.ToCharArray().ToList();
        if (nInput.Count > 0)
        {
            Eval(nInput.First());
            input = nInput.Skip(1).Aggregate("", (prod, next) => prod + next);
            Scan(input);
        }
    }

    static void ScanFile(string file)
    {
        var code = File.ReadAllText(file);
        Scan(code);
    }
    
    static void Repl()
    {
        for (;;)
        {
            Console.Write("   ");
            var code = Console.ReadLine();
            Scan(code);
        }
    }

    static void Init()
    {
        for (int i = 0; i < size; i++)
        {
            arr[i] = 'a';
        }
    }
    
    public static void Main(string[] args)
    {
        Init();
        if (args.Length == 1)
        {
            ScanFile(args[0]);
        }
        else
        {
            Repl();
        }
    }
}