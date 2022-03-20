namespace Multiformats.Codec;

/// <summary>
/// The codec interface
/// </summary>
public interface ICodec
{
    /// <summary>
    /// Gets the header.
    /// </summary>
    /// <value>The header.</value>
    byte[] Header { get; }

    //string HeaderPath { get; }

    /// <summary>
    /// Decoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecDecoder.</returns>
    ICodecDecoder Decoder(Stream stream);

    /// <summary>
    /// Encoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecEncoder.</returns>
    ICodecEncoder Encoder(Stream stream);
}
