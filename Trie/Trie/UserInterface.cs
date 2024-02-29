using Trie;



namespace UserInterface
{
    /// <summary>
    /// User interface to work with class trie.
    /// </summary>
    public class UserInterface
    {
        /// <summary>
        /// Run a program with a choice of commands for the user.
        /// </summary>
        public static void Run()
        {
            var trie = new Trie.Trie();
            while (true)
            {
                GiveChoiceToUser();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        PerformBoolMethod(trie, choice, "Word was added in trie", "There is already such element in trie");
                        break;
                    case "2":
                        PerformBoolMethod(trie, choice, "There is such word in trie", "There is no such element in trie");
                        break;
                    case "3":
                        PerformBoolMethod(trie, choice, "Word was removed from trie", "There is no such element in trie");
                        break;
                    case "4":
                        CountWords(trie);
                        break;
                    case "5":
                        Console.WriteLine($"Size of trie = {trie.Size}");
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void GiveChoiceToUser()
        {
            Console.WriteLine("Choose one of the commands:");
            Console.WriteLine("0 - exit from program");
            Console.WriteLine("1 - add word in trie");
            Console.WriteLine("2 - check existence of word in trie");
            Console.WriteLine("3 - remove word from trie");
            Console.WriteLine("4 - count how many words starts with such prefix in trie");
            Console.WriteLine("5 - get number of words in trie");
            Console.Write("Your choice: ");
        }

        private static void PerformBoolMethod(Trie.Trie trie, string choice, string messageForTrueCase, string messageForFalseCase)
        {
            Console.Write("Enter your word: ");
            string? word = Console.ReadLine();
            if (word == null)
            {
                Console.WriteLine("Incorrect input, try again");
                return;
            }

            bool methodResult = choice == "1" ? trie.Add(word) : choice == "2" ? trie.Contains(word): trie.Remove(word);
            Console.WriteLine(methodResult ? messageForTrueCase : messageForFalseCase);
        }

        private static void CountWords(Trie.Trie trie)
        {
            Console.Write("Enter your word: ");
            string? prefix = Console.ReadLine();
            if (prefix == null)
            {
                Console.WriteLine("Incorrect input, try again");
                return;
            }
            Console.WriteLine($"Number of words which starts with prefix '{prefix}': {trie.HowManyStartsWithPrefix(prefix)}");
        }
    }
}
