using MessagePack;
using MessagePackStudy.Messages;

namespace MessagePackStudy;

internal class Program
{
    private static void Main(string[] args)
    {
        IContentsHeader message = new IntsMessage()
        {
            Value0 = 10000,
            Value1 = 20000,
            Value2 = 30000,
        };

        var option = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.None);

        var serialized = MessagePackSerializer.Serialize(message, option);

        Console.WriteLine($"{nameof(serialized)}.Length = {serialized.Length}");
        Console.WriteLine($"{BitConverter.ToString(serialized)}");

        var deserialized = MessagePackSerializer.Deserialize<IContentsHeader>(serialized);

        var toMessage = deserialized as IntsMessage;

        Console.WriteLine($"{toMessage.Value0} | {toMessage.Value1} | {toMessage.Value2}");
    }
}