namespace SMSSplitter;

class SMSSplitter
{
    public static void Main()
    {
        string sms = Console.ReadLine();

        if (sms is null)
        {
            return;
        }

        var parts = Splitter.Split(sms);

        Console.WriteLine(parts.Count());
        int x = 1;
        foreach (string s in parts)
        {
            Console.WriteLine($"Part {x} : ");
            Console.WriteLine($"Lenght of Part {x++} : {s.Length}");
            Console.WriteLine(s);
            Console.WriteLine("___________");
        }

        Console.ReadLine();
    }

}






