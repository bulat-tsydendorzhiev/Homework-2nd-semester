namespace Trie
{
    public class Trie
    {
        private class TrieNode
        {
            public bool IsEndOfWord;
            public Dictionary<char, TrieNode> Vertexes;

            public TrieNode()
            {
                this.IsEndOfWord = false;
                this.Vertexes = new Dictionary<char, TrieNode>();
            }
        }

        private readonly TrieNode Root;
        public int Size { get; private set; }
        public Trie() => this.Root = new TrieNode();

        public bool Add(string element)
        {
            var node = this.Root;
            foreach (var item in element)
            {
                if (!node.Vertexes.ContainsKey(item))
                {
                    node.Vertexes[item] = new TrieNode();
                }
                node = node.Vertexes[item];
            }

            if (!node.IsEndOfWord)
            {
                node.IsEndOfWord = true;
                ++this.Size;
                return true;
            }
            return false;
        }

        public bool Remove(string element)
        {
            TrieNode node = this.Root;
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
            --this.Size;
            node.IsEndOfWord = false;

            while (stack.Count > 0)
            {
                if (node.Vertexes.Count == 0 && !node.IsEndOfWord)
                {
                    (char deletingLetter, node) = stack.Peek();
                    node.Vertexes.Remove(deletingLetter);
                }
                (char letter, node) = stack.Pop();
            }

            stack.Clear();
            return true;
        }

        private TrieNode? GetVertex(string element)
        {
            var node = this.Root;
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

        public bool Contains(string element)
        {
            TrieNode? node = GetVertex(element);
            return node == null ? false : node.IsEndOfWord;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            TrieNode? node = GetVertex(prefix);
            return node == null ? 0 : (node.IsEndOfWord && node.Vertexes.Count == 0) ? 1: node.Vertexes.Count;
        }
    }
}