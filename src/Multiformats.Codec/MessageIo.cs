using System;

namespace Multiformats.Codec;

using BinaryEncoding;

/// <summary>
/// The message I/O class
/// </summary>
public static class MessageIo
{
    /// <summary>
    /// Reads the message.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>System.Byte[].</returns>
    /// <exception cref="EndOfStreamException"></exception>
    /// <exception cref="Exception">Could not read full message</exception>
    public static byte[] ReadMessage(Stream stream)
    {
        uint len = Binary.BigEndian.ReadUInt32(stream);
        if (len == 0)
        {
            throw new EndOfStreamException();
        }

        byte[]? bytes = new byte[len];
        if (stream.Read(bytes, 0, bytes.Length) != len)
        {
            throw new Exception("Could not read full message");
        }

        return bytes;
    }

    /// <summary>
    /// Read message as an asynchronous operation.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task&lt;System.Byte[]&gt; representing the asynchronous operation.</returns>
    /// <exception cref="EndOfStreamException"></exception>
    /// <exception cref="Exception">Could not read full message</exception>
    public static async Task<byte[]> ReadMessageAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        uint len = await Binary.BigEndian.ReadUInt32Async(stream, cancellationToken);
        if (len == 0)
        {
            throw new EndOfStreamException();
        }

        byte[]? bytes = new byte[len];
        if (await stream.ReadAsync(bytes, cancellationToken) != len)
        {
            throw new Exception("Could not read full message");
        }

        return bytes;
    }

    /// <summary>
    /// Writes the message.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="bytes">The bytes.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="count">The count.</param>
    /// <param name="flush">if set to <c>true</c> [flush].</param>
    public static void WriteMessage(Stream stream, byte[] bytes, int offset = 0, int count = 0, bool flush = false)
    {
        if (count == 0)
        {
            count = bytes.Length - offset;
        }

        Binary.BigEndian.Write(stream, (uint)count);

        stream.Write(bytes, offset, count);
        if (flush)
        {
            stream.Flush();
        }
    }

    /// <summary>
    /// Write message as an asynchronous operation.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="bytes">The bytes.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="count">The count.</param>
    /// <param name="flush">if set to <c>true</c> [flush].</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task WriteMessageAsync(Stream stream, byte[] bytes, int offset = 0, int count = 0, bool flush = false, CancellationToken cancellationToken = default)
    {
        if (count == 0)
        {
            count = bytes.Length - offset;
        }

        await Binary.BigEndian.WriteAsync(stream, (uint)count, cancellationToken);

        await stream.WriteAsync(bytes.AsMemory(offset, count), cancellationToken);
        if (flush)
        {
            await stream.FlushAsync(cancellationToken);
        }
    }
}
