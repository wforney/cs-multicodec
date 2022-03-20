namespace Multiformats.Codec.Codecs;

using System.Text;

/// <summary>
/// Class JsonCodec.
/// Implements the <see cref="ICodec" />
/// </summary>
/// <seealso cref="ICodec" />
public partial class JsonCodec : ICodec
{
    /// <summary>
    /// The header bytes
    /// </summary>
    public static readonly byte[] HeaderBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderPath));

    /// <summary>
    /// The header msgio bytes
    /// </summary>
    public static readonly byte[] HeaderMsgioBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderMsgioPath));

    /// <summary>
    /// The header msgio path
    /// </summary>
    private static readonly string headerMsgioPath = "/json/msgio";

    /// <summary>
    /// The header path
    /// </summary>
    private static readonly string headerPath = "/json";

    /// <summary>
    /// The msgio
    /// </summary>
    private readonly bool _msgio;

    /// <summary>
    /// The multicodec
    /// </summary>
    private readonly bool _multicodec;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonCodec"/> class.
    /// </summary>
    /// <param name="multicodec">if set to <c>true</c> [multicodec].</param>
    /// <param name="msgio">if set to <c>true</c> [msgio].</param>
    protected JsonCodec(bool multicodec, bool msgio)
    {
        _multicodec = multicodec;
        _msgio = msgio;
    }

    /// <summary>
    /// Gets the header msgio path.
    /// </summary>
    /// <value>The header msgio path.</value>
    public static string HeaderMsgioPath => headerMsgioPath;

    /// <summary>
    /// Gets the header path.
    /// </summary>
    /// <value>The header path.</value>
    public static string HeaderPath => headerPath;

    /// <summary>
    /// Gets the header.
    /// </summary>
    /// <value>The header.</value>
    public byte[] Header => _msgio ? HeaderMsgioBytes : HeaderBytes;

    /// <summary>
    /// Creates the codec.
    /// </summary>
    /// <param name="msgio">if set to <c>true</c> [msgio].</param>
    /// <returns>JsonCodec.</returns>
    public static JsonCodec CreateCodec(bool msgio)
    {
        return new(false, msgio);
    }

    /// <summary>
    /// Creates the multicodec.
    /// </summary>
    /// <param name="msgio">if set to <c>true</c> [msgio].</param>
    /// <returns>JsonCodec.</returns>
    public static JsonCodec CreateMulticodec(bool msgio)
    {
        return new(true, msgio);
    }

    /// <summary>
    /// Decoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecDecoder.</returns>
    public ICodecDecoder Decoder(Stream stream)
    {
        return new JsonDecoder(stream, this);
    }

    /// <summary>
    /// Encoders the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>ICodecEncoder.</returns>
    public ICodecEncoder Encoder(Stream stream)
    {
        return new JsonEncoder(stream, this);
    }
}
