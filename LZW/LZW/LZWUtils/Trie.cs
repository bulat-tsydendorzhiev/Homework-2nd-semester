namespace LZW;

/// <summary>
/// Trie, data structure for compact string storage.
/// </summary>
public class Trie
{
    private class TrieNode
    {
        /// <summary>
        /// Field that shows end of word.
        /// </summary>
        public bool IsEndOfWord;

        /// <summary>
        /// Value in trie node.
        /// </summary>
        public int Value;

        /// <summary>
        /// Vertexes of next words.
        /// </summary>
        public Dictionary<byte, TrieNode> Vertexes;

        public TrieNode()
        {
            Vertexes = new Dictionary<byte, TrieNode>();
        }

    }

    /// <summary>
    /// Number of elements in trie.
    /// </summary>
    public int Size { get; private set; }

    private readonly TrieNode Root;

    /// <summary>
    /// Creates and initializes a new instance of Trie.
    /// </summary>
    public Trie() => Root = new TrieNode();

    /// <summary>
    /// Adds element to trie.
    /// </summary>
    /// <param name="element">Adding element.</param>
    /// <returns>true if element was added to trie; otherwise false.</returns>
    public bool Add(List<byte> element, int value)
    {
        var node = Root;
        foreach (var item in element)
        {
            if (!node.Vertexes.ContainsKey(item))
            {
                node.Vertexes[item] = new TrieNode();
            }
            node = node.Vertexes[item];
        }

        if (node.IsEndOfWord)
        {
            return false;
        }

        ++Size;
        node.IsEndOfWord = true;
        node.Value = value;

        return true;
    }

    /// <summary>
    /// Removes element from trie.
    /// </summary>
    /// <param name="element">Element which should be removed from trie.</param>
    /// <returns>true if element was removed from trie; otherwise false.</returns>
    public bool Remove(List<byte> element)
    {
        var node = Root;
        var stack = new Stack<Tuple<byte, TrieNode>>();
        foreach (var item in element)
        {
            if (!node.Vertexes.ContainsKey(item))
            {
                return false;
            }

            stack.Push(new Tuple<byte, TrieNode>(item, node));
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
                (byte symbol, node) = stack.Peek();
                node.Vertexes.Remove(symbol);
            }
            (_, node) = stack.Pop();
        }

        stack.Clear();
        return true;
    }

    private TrieNode? GetVertex(List<byte> element)
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
    /// Checks existance of element in trie.
    /// </summary>
    /// <param name="element">Element which should be in trie.</param>
    /// <returns>true if element contains in trie; otherwise false.</returns>
    public bool Contains(List<byte> element)
    {
        TrieNode? node = GetVertex(element);
        return node == null ? false : node.IsEndOfWord;
    }

    /// <summary>
    /// Gets value stored in trie element.
    /// </summary>
    /// <param name="element">Element whose value is to be retrieved.</param>
    /// <returns>Value which contains in element of trie if it exists; otherwise -1.</returns>
    public int GetValue(List<byte> element)
    {
        TrieNode? node = GetVertex(element);
        return node == null ? -1 : node.Value;
    }
}
