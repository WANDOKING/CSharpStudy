using MessagePack;

namespace MessagePackStudy.Messages;

[Union(0, typeof(TextMessage))]
[Union(1, typeof(ImageMessage))]
[Union(2, typeof(IntsMessage))]
public interface IContentsHeader
{
}

[MessagePackObject]
public class TextMessage : IContentsHeader
{
    [Key(0)]
    public string Text { get; set; }

    public override string ToString() => $"{nameof(TextMessage)} | {nameof(Text)} = {Text}";
}

[MessagePackObject]

public class ImageMessage : IContentsHeader
{
    [Key(0)]
    public byte[] Image { get; set; }

    public override string ToString() => $"{nameof(ImageMessage)} | {nameof(Image)} = {Image}";
}

[MessagePackObject]

public class IntsMessage : IContentsHeader
{
    [Key(0)]
    public int Value0 { get; set; }

    [Key(1)]
    public int Value1 { get; set; }

    [Key(2)]
    public int Value2 { get; set; }
}