namespace LZW;

/// <summary>
/// Trie, data structure for compact byte sequence storage.
/// </summary>
public class Trie
{
    private class TrieNode
    {
        /// <summary>
        /// Shows whether current node is end of word.
        /// </summary>
        public bool IsEndOfWord {get; set; }

        /// <summary>
        /// Value from trie node.
        /// </summary>
        public int Value {get; set; }

        /// <summary>
        /// Vertexes of next words.
        /// </summary>
        public Dictionary<byte, TrieNode> Vertexes {get; set; }

        public TrieNode()
        {
            Vertexes = [];
        }
    }

    /// <summary>
    /// Number of elements in trie.
    /// </summary>
    public int Size { get; private set; }

    private readonly TrieNode _root;

    /// <summary>
    /// Initializes a new instance of <see cref="Trie">.
    /// </summary>
    public Trie()
    {
        _root = new TrieNode();
    }

    /// <summary>
    /// Adds element to trie.
    /// </summary>
    /// <param name="element">Adding element.</param>
    /// <returns>true if element was added to trie; otherwise false.</returns>
    public bool Add(List<byte> element, int value)
    {
        var node = _root;
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
        var node = _root;
        var stack = new Stack<(byte, TrieNode)>();
        foreach (var item in element)
        {
            if (!node.Vertexes.ContainsKey(item))
            {
                return false;
            }

            stack.Push((item, node));
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
        var node = _root;
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
    /// Determines whether an element is in trie.
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
