namespace Multiformats.Codec.Codecs;

using System.Text;

/// <summary>
/// Class CborCodec.
/// Implements the <see cref="ICodec" />.
/// </summary>
/// <seealso cref="ICodec" />
public partial class CborCodec : ICodec
{
    /// <summary>
    /// The header bytes
    /// </summary>
    public static readonly byte[] HeaderBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderPath));

    /// <summary>
    /// The header path
    /// </summary>
    private static readonly string headerPath = "/cbor";

    /// <summary>
    /// The multicodec
    /// </summary>
    private readonly bool _multicodec;

    /// <summary>
    /// Initializes a new instance of the <see cref="CborCodec"/> class.
    /// </summary>
    /// <param name="multicodec">if set to <c>true</c> [multicodec].</param>
    protected CborCodec(bool multicodec)
    {
        _multicodec = multicodec;
    }

    /// <summary>
    /// Gets the header path.
    /// </summary>
    /// <value>The header path.</value>
    public static string HeaderPath => headerPath;

    /// <summary>
    /// Gets the header.
    /// </summary>
    /// <value>The header.</value>
    public byte[] Header => HeaderBytes;

    /// <summary>
    /// Creates the codec.
    /// </summary>
    /// <returns>CborCodec.</returns>
    public static CborCodec CreateCodec()
    {
        return new(false);
    }

    /// <summary>
    /// Creates the multicodec.
    /// </summary>
    /// <returns>CborCodec.</returns>
    public static CborCodec CreateMulticodec()
    {
        return new(true);
    }

    /// <summary>
    /// Decoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecDecoder.</returns>
    public ICodecDecoder Decoder(Stream stream)
    {
        return new CBORDecoder(stream, this);
    }

    /// <summary>
    /// Encoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecEncoder.</returns>
    public ICodecEncoder Encoder(Stream stream)
    {
        return new CBOREncoder(stream, this);
    }
}
