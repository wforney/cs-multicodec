namespace Multiformats.Codec.Codecs;

using System.Text;

/// <summary>
/// The protocol buffers codec class.
/// Implements the <see cref="ICodec" />.
/// </summary>
/// <seealso cref="ICodec" />
public partial class ProtoBufCodec : ICodec
{
    /// <summary>
    /// The header bytes
    /// </summary>
    public static readonly byte[] HeaderBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderPath ?? string.Empty));

    /// <summary>
    /// The header MSG io bytes
    /// </summary>
    public static readonly byte[] HeaderMsgIoBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderMsgIoPath ?? string.Empty));

    /// <summary>
    /// The header MSG io path
    /// </summary>
    public static readonly string HeaderMsgIoPath = "/protobuf/msgio";

    /// <summary>
    /// The header path
    /// </summary>
    public static readonly string HeaderPath = "/protobuf";

    /// <summary>
    /// The msgio
    /// </summary>
    private readonly bool _msgio;

    /// <summary>
    /// The multicodec
    /// </summary>
    private readonly bool _multicodec;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProtoBufCodec"/> class.
    /// </summary>
    /// <param name="multicodec">The multicodec.</param>
    /// <param name="msgio">The msgio.</param>
    protected ProtoBufCodec(bool multicodec, bool msgio)
    {
        _multicodec = multicodec;
        _msgio = msgio;
    }

    /// <summary>
    /// Gets the header.
    /// </summary>
    /// <value>The header.</value>
    public byte[] Header => _msgio ? HeaderMsgIoBytes : HeaderBytes;

    /// <summary>
    /// Creates the codec.
    /// </summary>
    /// <param name="msgio">The msgio.</param>
    /// <returns>Multiformats.Codec.Codecs.ProtoBufCodec.</returns>
    public static ProtoBufCodec CreateCodec(bool msgio)
    {
        return new(false, msgio);
    }

    /// <summary>
    /// Creates the multicodec.
    /// </summary>
    /// <param name="msgio">The msgio.</param>
    /// <returns>Multiformats.Codec.Codecs.ProtoBufCodec.</returns>
    public static ProtoBufCodec CreateMulticodec(bool msgio)
    {
        return new(true, msgio);
    }

    /// <summary>
    /// Decoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>Multiformats.Codec.ICodecDecoder.</returns>
    public ICodecDecoder Decoder(Stream stream)
    {
        return new ProtoBufDecoder(stream, this);
    }

    /// <summary>
    /// Encoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>Multiformats.Codec.ICodecEncoder.</returns>
    public ICodecEncoder Encoder(Stream stream)
    {
        return new ProtoBufEncoder(stream, this);
    }
}
