UniqueList.UniqueList list = new();
        
int[] values = [1, 2, 3, 4, 5, 6, 7, 8];

foreach (var item in values)
{
    list.Add(item);
}

try
{
    list.Add(2);
}
catch (UniqueList.ExistingValueException)
{
    Console.WriteLine("Such value is already in the list.");
    return;
}