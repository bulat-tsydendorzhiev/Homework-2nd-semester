using System.Xml.Linq;

namespace Trie
{
    /// <summary>
    /// Trie, data structure for compact string storage.
    /// </summary>
    public class Trie
    {
        private class TrieNode
        {
            public bool IsEndOfWord;
            public int PrefixWordsCount;
            public Dictionary<char, TrieNode> Vertexes;

            public TrieNode()
            {
                Vertexes = new Dictionary<char, TrieNode>();
            }
        }

        /// <summary>
        /// Number of elements in trie.
        /// </summary>
        public int Size { get; private set; }
        private readonly TrieNode Root;

        public Trie() => Root = new TrieNode();

        /// <summary>
        /// Add element to trie.
        /// </summary>
        /// <param name="element">Adding element.</param>
        /// <returns>true if element wasn't in trie.</returns>
        public bool Add(string element)
        {
            var node = Root;
            var stack = new Stack<TrieNode>();
            foreach (var item in element)
            {
                if (!node.Vertexes.ContainsKey(item))
                {
                    node.Vertexes[item] = new TrieNode();
                }
                stack.Push(node);
                node = node.Vertexes[item];
            }

            if (node.IsEndOfWord)
            {
                return false;
            }
            ++Size;
            node.IsEndOfWord = true;
            while (stack.Count > 0)
            {
                ++node.PrefixWordsCount;
                node = stack.Pop();
            }
            ++node.PrefixWordsCount;

            stack.Clear();
            return true;
        }

        /// <summary>
        /// Remove element from trie.
        /// </summary>
        /// <param name="element">Element which should be removed from trie.</param>
        /// <returns>true if element was removed from trie.</returns>
        public bool Remove(string element)
        {
            var node = Root;
            var stack = new Stack<Tuple<char, TrieNode>>();
            foreach (var item in element)
            {
                if (!node.Vertexes.ContainsKey(item))
                {
                    return false;
                }

                stack.Push(new Tuple<char, TrieNode>(item, node));
                node = node.Vertexes[item];
            }

            if (!node.IsEndOfWord)
            {
                return false;
            }
            --Size;
            node.IsEndOfWord = false;

            while (stack.Count > 0)
            {
                if (node.Vertexes.Count == 0 && !node.IsEndOfWord)
                {
                    (char letter, node) = stack.Peek();
                    node.Vertexes.Remove(letter);
                }
                --node.PrefixWordsCount;
                (_, node) = stack.Pop();
            }

            stack.Clear();
            return true;
        }

        private TrieNode? GetVertex(string element)
        {
            var node = Root;
            foreach (var item in element)
            {
                if (!node.Vertexes.ContainsKey(item))
                {
                    return null;
                }
                node = node.Vertexes[item];
            }
            return node;
        }

        /// <summary>
        /// Check existance of element in trie.
        /// </summary>
        /// <param name="element">Element which should be in trie.</param>
        /// <returns>true if element contains in trie.</returns>
        public bool Contains(string element)
        {
            TrieNode? node = GetVertex(element);
            return node == null ? false : node.IsEndOfWord;
        }

        /// <summary>
        /// Count how many words starts with prefix.
        /// </summary>
        /// <param name="prefix">Prefix with which words begin.</param>
        /// <returns>Number of words which starts with prefix.</returns>
        public int HowManyStartsWithPrefix(string prefix)
        {
            TrieNode? node = GetVertex(prefix);
            return node == null ? 0 : node.PrefixWordsCount;
        }
    }
}