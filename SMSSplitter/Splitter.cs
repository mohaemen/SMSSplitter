namespace SMSSplitter;

public static class Splitter
{
    private const string AllowedCharacters = " @£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞÆæßÉ!“#¤%&‘()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\r\n";
    private const string TwoByteChars = "€[]|^\\~{}";
    private const int Part_Length = 153;
    private const int Full_Length = 160;

    public static IEnumerable<string> Split(string sms)
    {
        List<string> parts = new List<string>();

        int partLength = 0;
        string part = string.Empty;
        bool isLongMessage = false;
        string tempPart = string.Empty;
        int tempPartLength = 0;

        foreach (char c in sms)
        {
            if(!AllowedCharacters.Contains(c) && !TwoByteChars.Contains(c))
            {
                throw new ArgumentException("Not allowed GSM characters.");
            }

            if (partLength >= Full_Length)
            {
                isLongMessage = true;
                parts.Add(tempPart);
                part = part.Replace(tempPart, "");
                partLength = partLength - tempPartLength;
            }

            if (TwoByteChars.Contains(c))
            {
                if (partLength == Part_Length - 1)
                {
                    if (isLongMessage)
                    {
                        parts.Add(part);
                        part = string.Empty;
                        partLength = 0;
                    }
                    else
                    {
                        tempPart = part;
                        tempPartLength = partLength;
                    }
                }

                partLength += 2;
            }
            else
            {
                partLength++;
            }

            part += c;

            if (partLength == Part_Length)
            {
                if (isLongMessage)
                {
                    parts.Add(part);
                    part = string.Empty;
                    partLength = 0;
                }
                else
                {
                    tempPart = part;
                    tempPartLength = partLength;
                }
            }
        }

        if (partLength > 0)
        {
            parts.Add(part);
        }

        return parts;
    }
}