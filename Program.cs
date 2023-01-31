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
        FindPotentialSentece(sentences[x].Split(' '), cont);
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
    static void FindPotentialSentece(string[] sentence, string[] allSentences)
    {
        List<string> potentialSentences = new List<string>();
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
            int count = keys.Count;
            for (int j = 0; j < keys.Count; j++)
            {
                wordIsInSentence.Add(wasnMüll.Contains(keys[j]) ? true : false);
            }
            if (!wordIsInSentence.Contains(false) && wasnMüll.Length >= sentence.Length)
            {
                potentialSentences.Add(pureSentences);
            }
            wordIsInSentence.Clear();
        }
        FindSentence(potentialSentences, sentence);
    }

    static void FindSentence(List<string> potentialSentences, string[] sentence)
    {
        HashSet<bool> matches = new HashSet<bool>();
        if (potentialSentences.Count == 1)
        {
            Console.WriteLine(potentialSentences[0]);
            return;
        }
        for (int i = 0; i < potentialSentences.Count; i++)
        {
            string[] storage = new string[sentence.Length];
            string[] x = potentialSentences[i].Split(' ');
            int index = 0;
            for (int j = 0; j < x.Length; j++)
            {
                if (x[j] == sentence[0])
                {
                    index = j;
                    break;
                }
            }
            int IndexForStorage = 0;
            for (int j = index; j < index + storage.Length; j++)
            {
                storage[IndexForStorage] = x[j];
                IndexForStorage++;
            }
            for (int j = 0; j < storage.Length; j++)
            {
                matches.Add((storage[j] == sentence[j] || sentence[j] == "_") ? true : false);
            }
            if (!matches.Contains(false))
            {
                Console.WriteLine(potentialSentences[i]);
            }
            matches.Clear();
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
            sb.Append((Char.IsLetter(c) || Char.IsPunctuation(c)) ? c : '¥');
        }
        return sb.ToString()!.Replace('!', '.').Replace('?', '.').Replace(';', '.').Replace(',', '.').Replace('»', '.').Replace('«', '.').Replace(':', '.');
    }
}