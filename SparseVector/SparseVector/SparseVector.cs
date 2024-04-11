namespace TestWork;

/// <summary>
/// Sparse vector
/// </summary>
public class SparseVector
{
    /// <summary>
    /// Gets size of sparce vector.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// Checks whether vector is sparse.
    /// </summary>
    /// <returns>true if vector is sparse; otherwise false.</returns>
    public bool IsNullVector() => _sparceVector.Count == 0;

    /// <summary>
    /// Initializes a new instance of <see cref="SparseVector"> class.
    /// </summary>
    public SparseVector(int[] vector)
    {
        if (vector.Length == 0)
        {
            throw new ArgumentException("Vector cannot be empty.");
        }
        
        for (int i = 0; i < vector.Length; ++i)
        {
            if (vector[i] != 0)
            {
                _sparceVector.Add(i, vector[i]);
            }
        }
        
        Size = vector.Length;
    }
    
    /// <summary>
    /// Makes addition of sparce vector and given vector.
    /// </summary>
    /// <param name="addingVector">Adding vector.</param>
    /// <exception cref="ArgumentException">Throws when vectors have different lengths.</exception>
    /// <returns>Result of addition.</returns>
    public int[] MakeAddition(int[] addingVector)
    {
        return MakeOperation(addingVector, (x, addingVectorX) => x + addingVectorX);
    }
    
    /// <summary>
    /// Makes substraction of sparce vector and given vector.
    /// </summary>
    /// <param name="substractionVector">Specified vector.</param>
    /// <exception cref="ArgumentException">Throws when vectors have different lengths.</exception>
    /// <returns>Result of substraction.</returns>
    public int[] MakeSubstraction(int[] substractionVector)
    {
        return MakeOperation(substractionVector, (x, substractionVectorX) => x - substractionVectorX);
    }
    
    /// <summary>
    /// Makes multiplication of sparce vector and given vector.
    /// </summary>
    /// <param name="scalarVector">Scalar vector.</param>
    /// <exception cref="ArgumentException">Throws when vectors have different lengths.</exception>
    /// <returns>Result of scalar multiplication.</returns>

    public int[] MakeMultiplication(int[] scalarVector)
    {
        return MakeOperation(scalarVector, (x, scalarVector) => x * scalarVector);
    }
    
    private int[] MakeOperation(int[] vector, Func<int, int, int> func)
    {
        if (vector.Length != Size)
        {
            throw new ArgumentException("You can't make operation with vectors of different lengths.");
        }
        
        for (int i = 0; i < vector.Length; ++i)
        {
            if (vector[i] == 0 && func(123, 0) != 0)
            {
                continue;
            }
            
            if (!_sparceVector.ContainsKey(i))
            {
                _sparceVector[i] = 0;
            }
            
            _sparceVector[i] = func(_sparceVector[i], vector[i]);
        }

        return CollectFullVector();
    }
    
    private int[] CollectFullVector()
    {
        var result = new int[Size];
        
        foreach (var (index, value) in _sparceVector)
        {
            result[index] = value;
        }
        
        return result;
    }

    private SortedDictionary<int, int> _sparceVector = new();
}