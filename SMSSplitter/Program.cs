namespace SMSSplitter;

class SMSSplitter
{
    public static void Main()
    {
        //string sms = Console.ReadLine();

        string sms = "Let our SMS counter help you calculate how many parts your message will get broken up into, the total characters, the characters remaining per part and €ore!. Paste or type your text above to check your text message length.";


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






