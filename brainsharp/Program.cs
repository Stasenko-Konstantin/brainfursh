namespace brainsharp;

internal static class Program
{
    private const int Size = 30000;
    private const char Nil = '\0';
    private static string? _oldCode;
    private static int _i;
    private static readonly char[] Arr = new char[Size];
    private static readonly char[] Syms = { '>', '<', '+', '-', '.', ',', '[', ']' };

    static void Eval(char c)
    {
        switch (c)
        {
            case '>':
                _i++;
                break;
            case '<':
                _i--;
                break;
            case '+':
                Arr[_i]++;
                break;
            case '-':
                Arr[_i]--;
                break;
            case '.':
                Console.Write(Arr[_i]);
                break;
            case ',':
                Arr[_i] = Console.ReadKey().KeyChar;
                break;
        }
    }

    static void Scan(string? input)
    {
        var nInput = input?.ToCharArray().ToList();
        if (nInput!.Count > 0)
        {
            char c = nInput.First();
            input = nInput.Skip(1).Aggregate("", (prod, next) => prod + next);

            if (c == '[')
            {
                if (Arr[_i] != Nil)
                {
                    _oldCode = input;
                    char newC = nInput.First();
                    input = nInput.Skip(1).Aggregate("", (prod, next) => prod + next);
                    Eval(newC);
                    Scan(input);
                }
                else
                {
                    Scan(input.ToCharArray().ToList().SkipWhile(c => c != ']')
                        .Aggregate("", (prod, next) => prod + next));
                }
            }
            else if (c == ']')
            {
                if (Arr[_i] != Nil)
                {
                    input = _oldCode;
                    Scan(input);
                }
                else
                {
                    char newC = nInput.First();
                    input = nInput.Skip(1).Aggregate("", (prod, next) => prod + next);
                    Eval(newC);
                    Scan(input);
                }
            }
            else
            {
                Eval(c);
                Scan(input);
            }
        }
    }

    static void ScanFile(string file)
    {
        var code = File.ReadAllText(file);
        Scan(code.Where(c => Syms.Contains(c)).Aggregate("", (prod, next) => prod + next));
    }

    static void Repl()
    {
        for (;;)
        {
            Console.Write("   ");
            var code = Console.ReadLine();
            if (code != null) Scan(code.Where(c => Syms.Contains(c)).Aggregate("", (prod, next) => prod + next));
        }
    }

    static void Init()
    {
        for (int i = 0; i < Size; i++)
        {
            Arr[i] = Nil;
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