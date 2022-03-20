namespace Multiformats.Codec.Codecs;

using System.Text;

/// <summary>
/// The Mux codec class
/// </summary>
/// <seealso cref="ICodec" />
public partial class MuxCodec : ICodec
{
    /// <summary>The header bytes</summary>
    private static readonly byte[] headerBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderPath));

    /// <summary>The header path</summary>
    private static readonly string headerPath = "/multicodec";

    /// <summary>The codecs</summary>
    private readonly ICodec[] _codecs;

    /// <summary>Initializes a new instance of the <see cref="MuxCodec"/> class.</summary>
    /// <param name="codecs">The codecs.</param>
    /// <param name="select">The select.</param>
    /// <param name="wrap">if set to <c>true</c> [wrap].</param>
    protected MuxCodec(IEnumerable<ICodec> codecs, SelectCodecDelegate? @select, bool wrap)
    {
        _codecs = codecs.ToArray();
        Select = @select;
        Wrap = wrap;

        Last = null;
    }

    /// <summary>
    /// The select codec delegate.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="codecs">The codecs.</param>
    /// <returns></returns>
    public delegate ICodec SelectCodecDelegate(object? obj, ICodec[] codecs);

    /// <summary>
    /// Gets the header bytes.
    /// </summary>
    /// <value>The header bytes.</value>
    public static byte[] HeaderBytes => headerBytes;

    /// <summary>
    /// Gets the header path.
    /// </summary>
    /// <value>The header path.</value>
    public static string HeaderPath => headerPath;

    /// <summary>Gets the standard.</summary>
    /// <value>The standard.</value>
    public static MuxCodec Standard =>
        new(
            new ICodec[]
            {
                CborCodec.CreateMulticodec(),
                JsonCodec.CreateMulticodec(false),
                JsonCodec.CreateMulticodec(true),
                ProtoBufCodec.CreateMulticodec(false),
                ProtoBufCodec.CreateMulticodec(true),
            },
            SelectFirst,
            true);

    /// <summary>Gets the codecs.</summary>
    /// <value>The codecs.</value>
    public ICodec[] Codecs => _codecs;

    /// <summary>Gets the header.</summary>
    /// <value>The header.</value>
    public byte[] Header => HeaderBytes;

    /// <summary>Gets or sets the last.</summary>
    /// <value>The last.</value>
    public ICodec? Last { get; protected set; }

    /// <summary>Gets or sets the select.</summary>
    /// <value>The select.</value>
    public SelectCodecDelegate? Select { get; set; }

    /// <summary>Gets or sets a value indicating whether this <see cref="MuxCodec"/> is wrap.</summary>
    /// <value><c>true</c> if wrap; otherwise, <c>false</c>.</value>
    public bool Wrap { get; set; }

    /// <summary>Codecs the with header.</summary>
    /// <param name="header">The header.</param>
    /// <param name="codecs">The codecs.</param>
    /// <returns></returns>
    public static ICodec? CodecWithHeader(byte[] header, IEnumerable<ICodec> codecs)
    {
        return codecs.SingleOrDefault(c => c.Header.SequenceEqual(header));
    }

    /// <summary>Creates the specified codecs.</summary>
    /// <param name="codecs">The codecs.</param>
    /// <param name="select">The select.</param>
    /// <returns></returns>
    public static MuxCodec Create(IEnumerable<ICodec> codecs, SelectCodecDelegate? @select)
    {
        return new(codecs, @select ?? SelectFirst, true);
    }

    /// <summary>Decoders the specified stream.</summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public ICodecDecoder Decoder(Stream stream)
    {
        return new MuxDecoder(stream, this);
    }

    /// <summary>Encoders the specified stream.</summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public ICodecEncoder Encoder(Stream stream)
    {
        return new MuxEncoder(stream, this);
    }

    /// <summary>Selects the first.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="codecs">The codecs.</param>
    /// <returns></returns>
    private static ICodec SelectFirst(object? obj, ICodec[] codecs)
    {
        return codecs.First();
    }

    /// <summary>Gets the codec.</summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    private ICodec? GetCodec(object? obj)
    {
        return Select?.Invoke(obj, _codecs);
    }
}
