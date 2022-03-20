namespace Multiformats.Codec;

using BinaryEncoding;

/// <summary>
/// The packed multicodec class
/// </summary>
public static class MulticodecPacked
{
    /// <summary>Gets the code.</summary>
    /// <param name="data">The data.</param>
    /// <param name="offset">The offset.</param>
    /// <returns></returns>
    public static MulticodecCode GetCode(byte[] data, int offset = 0)
    {
        if (data is null || data.Length == 0)
        {
            return MulticodecCode.Unknown;
        }

        int n = Binary.Varint.Read(data, offset, out ulong code);
        if (n == 0 || !Enum.IsDefined(typeof(MulticodecCode), code))
        {
            return MulticodecCode.Unknown;
        }

        return (MulticodecCode)code;
    }

    /// <summary>Adds the prefix.</summary>
    /// <param name="code">The code.</param>
    /// <param name="data">The data.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="count">The count.</param>
    /// <returns></returns>
    public static byte[] AddPrefix(MulticodecCode code, byte[] data, int offset = 0, int? count = null)
    {
        return Binary.Varint.GetBytes((ulong)code).Concat(data.Skip(offset).Take(count ?? data.Length - offset)).ToArray();
    }

    /// <summary>Splits the prefix.</summary>
    /// <param name="data">The data.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static byte[] SplitPrefix(byte[] data, out MulticodecCode code)
    {
        return SplitPrefix(data, 0, data.Length, out code);
    }

    /// <summary>Splits the prefix.</summary>
    /// <param name="data">The data.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static byte[] SplitPrefix(byte[] data, int offset, out MulticodecCode code)
    {
        return SplitPrefix(data, offset, data.Length - offset, out code);
    }

    /// <summary>Splits the prefix.</summary>
    /// <param name="data">The data.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="count">The count.</param>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    public static byte[] SplitPrefix(byte[] data, int offset, int count, out MulticodecCode code)
    {
        int n = Binary.Varint.Read(data, offset, out ulong ulcode);
        code = (MulticodecCode)ulcode;
        return data.Skip(offset + n).Take(count - (offset + n)).ToArray();
    }
}
