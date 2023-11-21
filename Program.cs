namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss>();
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
            string defaultFile = "sweeng.lis";

            Console.WriteLine("Welcome to the dictionary app!");
            Console.WriteLine("Write 'help' to list available commands");
            bool shouldRun = true;

            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    shouldRun = false;
                }
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        LoadFileGlossary(argument[1]);
                    }
                    else if(argument.Length == 1)
                    {
                        LoadFileGlossary(defaultFile);
                    }
                }
                else if (command == "list")
                {
                    ListGlossary();
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        AddNewGlossToDictionary(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 2)
                    {
                        string swedish_word = argument[1];

                        Console.Write("Write word in English: ");
                        string english_word = Console.ReadLine();

                        AddNewGlossToDictionary(new SweEngGloss(swedish_word, english_word));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish_word = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english_word = Console.ReadLine();

                        AddNewGlossToDictionary(new SweEngGloss(swedish_word, english_word)); 
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        DeleteGlossFromDictionary(argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish_word = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english_word = Console.ReadLine();

                        DeleteGlossFromDictionary(swedish_word, english_word);
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
                    HelpUser();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (shouldRun == true);
        }

        private static void LoadFileGlossary(string fileName)
        {
            string defaultPath = "..\\..\\..\\dict\\";
            try
            {
                using (StreamReader sr = new StreamReader(defaultPath + fileName))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            AddNewGlossToDictionary(gloss);
                        }
                        line = sr.ReadLine();
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine($"> [FileNotFoundException] : {e.Message}");
            }
        }

        private static void HelpUser()
        {
            Console.WriteLine("quit - closes the application");
            Console.WriteLine("load - load file glossary to the programs dictionary");
            Console.WriteLine("list - list the current dictionary");
            Console.WriteLine("new - add new glossary to the dictionary");
            Console.WriteLine("delete - delete one glossary from the dictionary");
            Console.WriteLine("translate - translate a glossary from the dictionary");
        }

        private static void ListGlossary()
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }
        
        private static void AddNewGlossToDictionary(SweEngGloss sweEngGloss)
        {
            if(!string.IsNullOrWhiteSpace(sweEngGloss.word_swe) && !string.IsNullOrWhiteSpace(sweEngGloss.word_eng))
            {
                dictionary.Add(sweEngGloss);
            }
        }

        private static void DeleteGlossFromDictionary(string swedish_word, string english_word)
        {
            try
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == swedish_word && gloss.word_eng == english_word)
                        index = i;
                }
                dictionary.RemoveAt(index);
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("> [ArgumentOutOfRangeException] : Didn't find the word that is wanted to be removed, maybe the word doesn't exist in the dictionary, or you misspelled the word in swedish or english");
            }
        }
    }
}