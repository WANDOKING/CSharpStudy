namespace TestConcurrency;

using System.Collections.Immutable;
using System.Diagnostics;

[TestClass]
public class TestImmutableCollection
{
    [TestMethod]
    public void ImmutableStack()
    {
        var stack = ImmutableStack<int>.Empty;

        stack = stack.Push(1);
        stack = stack.Push(2);
        stack = stack.Push(3);

        var numsInStack = stack.Select((item) => item).ToList();

        CollectionAssert.Contains(numsInStack, 1);
        CollectionAssert.Contains(numsInStack, 2);
        CollectionAssert.Contains(numsInStack, 3);

        stack = stack.Pop();
        numsInStack = stack.Select((item) => item).ToList();

        CollectionAssert.Contains(numsInStack, 1);
        CollectionAssert.Contains(numsInStack, 2);
        CollectionAssert.DoesNotContain(numsInStack, 3);
    }

    [TestMethod]
    public void ImmutableQueue()
    {
        var queue = ImmutableQueue<int>.Empty;

        queue = queue.Enqueue(1);
        queue = queue.Enqueue(2);
        queue = queue.Enqueue(3);

        var numsInQueue = queue.Select((item) => item).ToList();

        CollectionAssert.Contains(numsInQueue, 1);
        CollectionAssert.Contains(numsInQueue, 2);
        CollectionAssert.Contains(numsInQueue, 3);

        queue = queue.Dequeue();
        numsInQueue = queue.Select((item) => item).ToList();

        CollectionAssert.DoesNotContain(numsInQueue, 1);
        CollectionAssert.Contains(numsInQueue, 2);
        CollectionAssert.Contains(numsInQueue, 3);
    }

    [TestMethod]
    public void ImmutableList()
    {
        var list = ImmutableList<int>.Empty;

        list = list.Add(1);
        list = list.Add(2);
        list = list.Add(3);

        var numsInList = list.Select((item) => item).ToList();

        CollectionAssert.Contains(numsInList, 1);
        CollectionAssert.Contains(numsInList, 2);
        CollectionAssert.Contains(numsInList, 3);

        list = list.Remove(2);
        numsInList = list.Select((item) => item).ToList();

        CollectionAssert.Contains(numsInList, 1);
        CollectionAssert.DoesNotContain(numsInList, 2);
        CollectionAssert.Contains(numsInList, 3);
    }
}
