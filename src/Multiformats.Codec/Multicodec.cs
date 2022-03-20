namespace Multiformats.Codec;

/// <summary>
/// The multicodec class.
/// </summary>
public static class Multicodec
{
    /// <summary>
    /// Creates new line.
    /// </summary>
    internal const byte NewLine = (byte)'\n';

    /// <summary>
    /// Consumes the header.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="header">The header.</param>
    /// <exception cref="Exception">Could not consume header</exception>
    /// <exception cref="Exception">Mismatch</exception>
    public static void ConsumeHeader(Stream stream, byte[] header)
    {
        byte[]? actual = new byte[header.Length];
        if (stream.Read(actual, 0, actual.Length) != actual.Length)
        {
            throw new Exception("Could not consume header");
        }

        if (!actual.SequenceEqual(header))
        {
            throw new Exception("Mismatch");
        }
    }

    /// <summary>
    /// Consume header as an asynchronous operation.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="header">The header.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Could not consume header</exception>
    /// <exception cref="Exception">Mismatch</exception>
    public static async Task ConsumeHeaderAsync(Stream stream, byte[] header, CancellationToken cancellationToken = default)
    {
        byte[]? actual = new byte[header.Length];
        if (await stream.ReadAsync(actual, cancellationToken) != actual.Length)
        {
            throw new Exception("Could not consume header");
        }

        if (!actual.SequenceEqual(header))
        {
            throw new Exception("Mismatch");
        }
    }

    /// <summary>
    /// Consumes the path.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="path">The path.</param>
    /// <exception cref="Exception">Mismatch</exception>
    public static void ConsumePath(Stream stream, byte[] path)
    {
        byte[]? actual = ReadPath(stream);
        if (!actual.SequenceEqual(path))
        {
            throw new Exception("Mismatch");
        }
    }

    /// <summary>
    /// Headers the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>System.Byte[].</returns>
    /// <exception cref="Exception">Multicodec varints not supported</exception>
    public static byte[] Header(byte[] path)
    {
        int length = path.Length + 1;
        if (length >= 127)
        {
            throw new Exception("Multicodec varints not supported");
        }

        return new[] { (byte)length }.Concat(path).Concat(new[] { NewLine }).ToArray();
    }

    /// <summary>
    /// Headers the path.
    /// </summary>
    /// <param name="header">The header.</param>
    /// <returns>System.Byte[].</returns>
    public static byte[] HeaderPath(byte[] header)
    {
        header = header.Slice(1);
        if (header[^1] == NewLine)
        {
            header = header.Slice(0, header.Length - 1);
        }

        return header;
    }

    /// <summary>
    /// Peeks the header.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>System.Byte[].</returns>
    /// <exception cref="Exception">Stream does not support peeking</exception>
    public static byte[] PeekHeader(Stream stream)
    {
        if (!stream.CanSeek)
        {
            throw new Exception("Stream does not support peeking");
        }

        byte[]? buf = ReadHeader(stream);
        if (buf.Length > 0)
        {
            stream.Seek(-buf.Length, SeekOrigin.Current);
        }

        return buf;
    }

    /// <summary>
    /// Peek header as an asynchronous operation.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task&lt;System.Byte[]&gt; representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Stream does not support peeking</exception>
    public static async Task<byte[]> PeekHeaderAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        if (!stream.CanSeek)
        {
            throw new Exception("Stream does not support peeking");
        }

        byte[]? buf = await ReadHeaderAsync(stream, cancellationToken);
        if (buf.Length > 0)
        {
            stream.Seek(-buf.Length, SeekOrigin.Current);
        }

        return buf;
    }

    /// <summary>
    /// Reads the header.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>System.Byte[].</returns>
    /// <exception cref="Exception">[ReadHeader] Multicodec varints not supported, got {length}.</exception>
    /// <exception cref="Exception">Zero or negative length: {length}</exception>
    /// <exception cref="Exception">Could not read header</exception>
    /// <exception cref="Exception">Invalid header</exception>
    public static byte[] ReadHeader(Stream stream)
    {
        int length = stream.ReadByte();
        if (length > 127)
        {
            throw new Exception($"[ReadHeader] Multicodec varints not supported, got {length}.");
        }

        if (length <= 0)
        {
            throw new Exception($"Zero or negative length: {length}");
        }

        byte[]? buf = new byte[length + 1];
        buf[0] = (byte)length;
        if (stream.Read(buf, 1, length) != length)
        {
            throw new Exception("Could not read header");
        }

        if (buf[length] != NewLine)
        {
            throw new Exception("Invalid header");
        }

        return buf;
    }

    /// <summary>
    /// Read header as an asynchronous operation.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task&lt;System.Byte[]&gt; representing the asynchronous operation.</returns>
    /// <exception cref="Exception">[ReadHeader] Multicodec varints not supported, got {length}.</exception>
    /// <exception cref="Exception">Zero or negative length: {length}</exception>
    /// <exception cref="Exception">Could not read header</exception>
    /// <exception cref="Exception">Invalid header</exception>
    public static async Task<byte[]> ReadHeaderAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        byte length = await stream.ReadByteAsync(cancellationToken);
        if (length > 127)
        {
            throw new Exception($"[ReadHeader] Multicodec varints not supported, got {length}.");
        }

        if (length <= 0)
        {
            throw new Exception($"Zero or negative length: {length}");
        }

        byte[]? buf = new byte[length + 1];
        buf[0] = length;
        if (await stream.ReadAsync(buf.AsMemory(1, length), cancellationToken) != length)
        {
            throw new Exception("Could not read header");
        }

        if (buf[length] != NewLine)
        {
            throw new Exception("Invalid header");
        }

        return buf;
    }

    /// <summary>
    /// Reads the path.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>System.Byte[].</returns>
    public static byte[] ReadPath(Stream stream)
    {
        byte[]? header = ReadHeader(stream);
        return HeaderPath(header);
    }

    /// <summary>
    /// Writes the header.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="path">The path.</param>
    public static void WriteHeader(Stream stream, byte[] path)
    {
        byte[]? header = Header(path);
        stream.Write(header, 0, header.Length);
    }
}
