using PriorityQueueTask;

var queue = new PriorityQueue();

List<(int Priority, int Value)> list = [(1, 2), (1, 3), (2, 3), (2, 4)];

foreach (var pair in list)
{
    queue.Enqueue(pair.Priority, pair.Value);
}

var newList = list.OrderByDescending(x => x.Priority).Select(x => x.Value).ToList();

foreach (var item in newList)
{
    System.Console.WriteLine(item == queue.Dequeue());
}