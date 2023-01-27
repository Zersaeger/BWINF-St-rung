class Program
{
    public static void Main()
    {
        string Buch = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Buch.txt"));
        Buch = TrimChars(Buch);
        string[] cont = Buch.Split('¥');
        List<string> allWords = new List<string>();
        for (int i = 0; i < cont.Length; i++)
        {
            if (cont[i] == "" || cont[i] == " ")
                continue;
            allWords.Add(cont[i].ToLower());
        }
        cont = allWords.ToArray();
        cont = MakeSentence(cont);
        File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "BuchNeu.txt"), cont);
        // array of the sentences we are searching for
        string[] sentences = {
            "das _ mir _ _ _ vor",
            "ich muß _ clara _",
            "fressen _ gern _",
            "das _ fing _",
            "ein _ _ tag",
            "wollen _ so _ sein"
        };
        // select a sentence
        Console.Write("Auf welchen Satz möchtest Du überprüfen?: ");
        int x = Int32.Parse(Console.ReadLine()!) - 1;
        FindSentece(sentences[x].Split(' '), cont);
    }

    static string[] MakeSentence(string[] allWords)
    {
        List<string> allSentences = new List<string>();
        string sentence = "";
        for (int i = 0; i < allWords.Length; i++)
        {
            sentence += allWords[i] + " ";
            if (allWords[i].EndsWith("."))
            {
                allSentences.Add(sentence);
                sentence = "";
            }
        }
        return allSentences.ToArray();
    }
    static void FindSentece(string[] sentence, string[] allSentences)
    {
        HashSet<bool> wordIsInSentence = new HashSet<bool>();
        List<string> keys = new List<string>();
        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] != "_")
            {
                keys.Add(sentence[i]);
            }
        }

        for (int i = 0; i < allSentences.Length; i++)
        {
            string pureSentences = RemoveDots(allSentences[i]);
            string[] wasnMüll = pureSentences.Split(' ');
            /*if (wasnMüll.Contains("das") && wasnMüll.Contains("mir") && wasnMüll.Contains("vor"))
            {
                Console.WriteLine(pureSentences);
            }*/
            int count = keys.Count;
            for (int j = 0; j < keys.Count; j++)
            {
                if (wasnMüll.Contains(keys[j]))
                {
                    wordIsInSentence.Add(true);
                }
                else
                {
                    j = keys.Count;
                    wordIsInSentence.Add(false);
                }
            }
            if (!wordIsInSentence.Contains(false))
            {
                Console.WriteLine(pureSentences);
            }
            wordIsInSentence.Clear();
        }
    }

    static string RemoveDots(string sentence)
    {
        System.Text.StringBuilder x = new System.Text.StringBuilder();
        foreach (char c in sentence)
        {
            if (Char.IsLetter(c) || Char.IsWhiteSpace(c))
            {
                x.Append(c);
            }
        }
        return x.ToString();
    }
    public static string TrimChars(string s)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (char c in s)
        {
            if (Char.IsLetter(c) || Char.IsPunctuation(c))
                sb.Append(c);
            else
                sb.Append('¥');
        }
        return sb.ToString()!.Replace('!', '.').Replace('?', '.').Replace(';', '.').Replace(',', '.').Replace('»', '.').Replace('«', '.').Replace(':', '.');
    }
}