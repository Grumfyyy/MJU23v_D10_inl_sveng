namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            Console.WriteLine("Write 'help' to list available commands");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    //FIXME: Detta stänger aldrig ner programmet, utan bara skriver ut till kommando prompten. Gör om programmet så den stänger ner vid kommandot "quit"
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    //FIXME: Här nedan har vi repetetiv kod som kan istället ha en metod, typ LoadFileData(string fileName)

                    //FIXME: Nedan så kollar vi inte om line är tomt, dvs om det är tomt skapa inte en ny glossa och lägg inte till glossan i dictionary

                    if (argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    //FIXME : Omvandla koden nedan i 'if'-satsen till en metod, ListGloss()
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    //FIXME: Om man nu har all data, så kan man ha en metod där, svenska och engelska ordet skickas till en metod, AddNewGloss(string swedish, string english)

                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish_word = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english_word = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(swedish_word, english_word));  
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1; 
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index); //FIXME : Fixa en check så att dictionary inte försöker radera vid indexet -1, utan att det måste vara värde >= 0
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish_word = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english_word = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedish_word && gloss.word_eng == english_word)
                                index = i;
                        }

                        dictionary.RemoveAt(index); //FIXME : Fixa en check så att dictionary inte försöker radera vid indexet -1, utan att det måste vara värde >= 0
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string input_word = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == input_word)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == input_word)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else if(command == "help")
                {
                    //FIXME : Gör detta till en funktion, Help()

                    Console.WriteLine("quit - closes the application");
                    Console.WriteLine("load - load file glossary to the programs dictionary");
                    Console.WriteLine("list - list the current dictionary");
                    Console.WriteLine("new - add new glossary to the dictionary");
                    Console.WriteLine("delete - delete one glossary from the dictionary");
                    Console.WriteLine("translate - translate a glossary from the dictionary");
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
    }
}