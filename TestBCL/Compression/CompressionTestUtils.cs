namespace TestBCL.Compression;

/// <summary>
/// 압축 테스트를 위한 유틸 클래스입니다.
/// </summary>
public static class CompressionTestUtils
{
    /// <summary>
    /// 랜덤한 값으로 채워진 바이트 배열을 반환합니다.
    /// </summary>
    /// <param name="size">바이트 배열의 길이입니다.</param>
    /// <returns>랜덤한 값으로 채워진 바이트 배열 입니다.</returns>
    public static byte[] GetBytesFilledWithRandomValue(int size)
    {
        byte[] bytes = new byte[size];
        new Random().NextBytes(bytes);
        return bytes;
    }

    /// <summary>
    /// 0으로 채워진 바이트 배열을 반환합니다.
    /// </summary>
    /// <param name="size">바이트 배열의 길이입니다.</param>
    /// <returns>0으로 채워진 바이트 배열 입니다.</returns>
    public static byte[] GetBytesFilledWithZero(int size)
    {
        byte[] bytes = new byte[size];
        return bytes;
    }

    /// <summary>
    /// 0xFF로 채워진 바이트 배열을 반환합니다.
    /// </summary>
    /// <param name="size">바이트 배열의 길이입니다.</param>
    /// <returns>0xFF로 채워진 바이트 배열 입니다.</returns>
    public static byte[] GetBytesFilledWithFF(int size)
    {
        byte[] bytes = new byte[size];

        for (int i = 0; i < bytes.Length; ++i)
        {
            bytes[i] = 0xFF;
        }

        return bytes;
    }

}
