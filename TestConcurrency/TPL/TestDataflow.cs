namespace TestConcurrency.TPL;

using System.Threading.Tasks.Dataflow;

[TestClass]
public class TestDataflow
{
    [TestMethod]
    public async Task BasicUsageOfDataflow()
    {
        int randomInput = new Random().Next();

        int result = 0;
        int actionBlockRunCount = 0;

        BufferBlock<int> bufferBlock = new BufferBlock<int>();
        TransformBlock<int, int> multiplyBlock = new TransformBlock<int, int>(item => item * 2);
        TransformBlock<int, int> subtractBlock = new TransformBlock<int, int>(item => item - 2);
        ActionBlock<int> actionBlock = new ActionBlock<int>(item =>
        {
            actionBlockRunCount++;
            result = item;
        });

        var option = new DataflowLinkOptions() { PropagateCompletion = true };

        bufferBlock.LinkTo(multiplyBlock, option);
        multiplyBlock.LinkTo(subtractBlock, option);
        subtractBlock.LinkTo(actionBlock, option);

        for (int i = 0; i < 10_000; ++i)
        {
            _ = bufferBlock.SendAsync(randomInput);
        }

        // 더 이상 추가적인 메시지를 보내지 않겠다
        bufferBlock.Complete();
        await actionBlock.Completion;

        Assert.AreEqual(randomInput * 2 - 2, result, $"unexpected result. {nameof(randomInput)} = {randomInput}");
        Assert.AreEqual(10_000, actionBlockRunCount);
    }
}