using MessagePack;
using MessagePackStudy.Messages;

namespace MessagePackStudy;

internal class Program
{
    private static void Main(string[] args)
    {
        // Union 애트리뷰트가 붙은 타입으로 직렬화를 해야 한다.
        IContentsHeader message = new IntsMessage()
        {
            Value0 = 10000,
            Value1 = 20000,
            Value2 = 30000,
        };

        // 직렬화
        var serialized = MessagePackSerializer.Serialize(message);
        Console.WriteLine($"{nameof(serialized)}.Length = {serialized.Length}");
        Console.WriteLine($"{BitConverter.ToString(serialized)}");

        // 역직렬화
        Console.WriteLine("\n[Deserialize]");
        var deserialized = MessagePackSerializer.Deserialize<IContentsHeader>(serialized);
        switch (deserialized)
        {
            case TextMessage textMessage:
                Console.WriteLine($"{nameof(TextMessage)}: Text = {textMessage.Text}");
                break;

            case ImageMessage imageMessage:
                Console.WriteLine($"{nameof(ImageMessage)}: Image = {imageMessage.Image}");
                break;

            case IntsMessage intsMessage:
                Console.WriteLine($"{nameof(IntsMessage)}: {intsMessage.Value0} | {intsMessage.Value1} | {intsMessage.Value2}");
                break;
            default:
                throw new InvalidDataException($"Invalid Message = {deserialized}");
        }
    }
}