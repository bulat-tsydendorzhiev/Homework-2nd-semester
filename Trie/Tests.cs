namespace Tests
{
    class Test
    {
        static private void RunTestForAdd(Trie.Trie trie, string[] words)
        {
            foreach (var word in words)
            {
                bool successfulAdding = trie.Add(word);
                if (!successfulAdding)
                {
                    throw new Exception($"Test for Add() failed with word {word}");
                }
            }

            foreach (var word in words)
            {
                bool successfulAdding = trie.Add(word);
                if (successfulAdding)
                {
                    throw new Exception($"Test for Add() failed with word {word}, which was added");
                }
            }

            if (trie.Size != words.Length)
            {
                throw new Exception($"Test for Add() failed with size of trie");
            }
        }

        static private void RunTestForContains(Trie.Trie trie, string[] existingWords, string[] nonExistingWords)
        {
            foreach (var word in existingWords)
            {
                if (!trie.Contains(word))
                {
                    throw new Exception($"Test for Contains() failed with existing word {word}");
                }
            }

            foreach (var word in nonExistingWords)
            {
                if (trie.Contains(word))
                {
                    throw new Exception($"Test for Contains() test failed with non existing word {word} ");
                }
            }
        }

        static private void RunTestForRemove(Trie.Trie trie, string[] existingWords, string[] nonExistingWords)
        {
            int expectedSize = trie.Size - existingWords.Length;

            foreach (var word in existingWords)
            {
                bool isDeleted = trie.Remove(word);
                if (trie.Contains(word) || !isDeleted)
                {
                    throw new Exception($"Test for Remove() test failed with existing word {word}");
                }
            }

            foreach (var word in nonExistingWords)
            {
                bool isDeleted = trie.Remove(word);
                if (trie.Contains(word) || isDeleted)
                {
                    throw new Exception($"Test for Remove() test failed with non existing word {word}");
                }
            }

            if (trie.Size != expectedSize)
            {
                throw new Exception($"Test for Remove() failed with size of trie");
            }
        }

        static private void RunTestForHowManyStartsWithPrefix(Trie.Trie trie, string[] testCases, int[] expectedNumbers)
        {
            for (int i = 0; i < testCases.Length; ++i)
            {
                int amountOfWordStartsWithPrefix = trie.HowManyStartsWithPrefix(testCases[i]);
                if (amountOfWordStartsWithPrefix != expectedNumbers[i])
                {
                    throw new Exception($"Prefix test failed with test case {i + 1}");
                }
            }
        }

        static public void RunTests()
        {
            Trie.Trie trie = new Trie.Trie();
            string[] addingWords = new string[] { "he", "she", "hers", "here", "helmet",
                                                  "his", "sheep", "soul", "horn", "God", "shoes" };
            string[] removingWords = new string[] { "helmet", "God", "she", "soul" };
            string[] existingWords = new string[] { "he", "hers", "here", "his", "sheep", "horn" };
            string[] nonExistingWords = new string[] { "godsend", "healthy", "A-mark", "hi", "helmets" };
            string[] testCasesForCountingFunction = new string[] { "h", "sheep", "hornet", "doom", "sh" };
            int[] expectedNumbers = new int[] { 3, 1, 0, 0, 2 };

            RunTestForAdd(trie, addingWords);
            RunTestForContains(trie, existingWords, nonExistingWords);
            RunTestForRemove(trie, removingWords, nonExistingWords);
            RunTestForHowManyStartsWithPrefix(trie, testCasesForCountingFunction, expectedNumbers);
        }
    }
}