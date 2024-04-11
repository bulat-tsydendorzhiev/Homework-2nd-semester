using TestWork;

var vector1 = new int[] { 1, 2, 3};
var vector2 = new int[] { 0, 0, 0};

var vector = new SparseVector(vector1);

var result = vector.MakeMultiplication(vector2);

foreach (var item in result)
{
    Console.WriteLine(item);
}