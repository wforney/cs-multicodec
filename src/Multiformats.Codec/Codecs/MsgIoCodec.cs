namespace Multiformats.Codec.Codecs;

using System.Text;

/// <summary>
/// The message I/O codec class
/// </summary>
/// <seealso cref="ICodec" />
public partial class MsgIoCodec : ICodec
{
    /// <summary>The header bytes</summary>
    private static byte[] headerBytes = Multicodec.Header(Encoding.UTF8.GetBytes(HeaderPath));

    /// <summary>The header path</summary>
    private static string headerPath = "/msgio";

    /// <summary>
    /// The multicodec
    /// </summary>
    private readonly bool _multicodec;

    /// <summary>Initializes a new instance of the <see cref="MsgIoCodec"/> class.</summary>
    /// <param name="multicodec">if set to <c>true</c> [multicodec].</param>
    protected MsgIoCodec(bool multicodec)
    {
        _multicodec = multicodec;
    }

    /// <summary>
    /// Gets or sets the header bytes.
    /// </summary>
    /// <value>The header bytes.</value>
    public static byte[] HeaderBytes { get => headerBytes; set => headerBytes = value; }

    /// <summary>
    /// Gets or sets the header path.
    /// </summary>
    /// <value>The header path.</value>
    public static string HeaderPath { get => headerPath; set => headerPath = value; }

    /// <summary>Gets the header.</summary>
    /// <value>The header.</value>
    public byte[] Header => HeaderBytes;

    /// <summary>Creates the codec.</summary>
    /// <returns></returns>
    public static MsgIoCodec CreateCodec()
    {
        return new MsgIoCodec(false);
    }

    /// <summary>Creates the multicodec.</summary>
    /// <returns></returns>
    public static MsgIoCodec CreateMulticodec()
    {
        return new MsgIoCodec(true);
    }

    /// <summary>Decoders the specified stream.</summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public ICodecDecoder Decoder(Stream stream)
    {
        return new MsgIoDecoder(stream, this);
    }

    /// <summary>Encoders the specified stream.</summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public ICodecEncoder Encoder(Stream stream)
    {
        return new MsgIoEncoder(stream, this);
    }

    /// <summary>
    /// The message I/O encoder class
    /// </summary>
    /// <seealso cref="ICodecEncoder" />
    private class MsgIoEncoder : ICodecEncoder
    {
        private readonly MsgIoCodec _codec;
        private readonly Stream _stream;

        /// <summary>Initializes a new instance of the <see cref="MsgIoEncoder"/> class.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public MsgIoEncoder(Stream stream, MsgIoCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        /// <summary>Encodes the specified object.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <exception cref="InvalidDataException">input must be byte array</exception>
        public void Encode<T>(T obj)
        {
            if (obj as object is not byte[] bytes)
            {
                throw new InvalidDataException("input must be byte array");
            }

            if (_codec._multicodec)
            {
                _stream.Write(_codec.Header, 0, _codec.Header.Length);
            }

            MessageIo.WriteMessage(_stream, bytes, flush: true);
        }

        /// <summary>Encodes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidDataException">input must be byte array</exception>
        public async Task EncodeAsync<T>(T obj, CancellationToken cancellationToken = default)
        {
            if (obj as object is not byte[] bytes)
            {
                throw new InvalidDataException("input must be byte array");
            }

            if (_codec._multicodec)
            {
                await _stream.WriteAsync(_codec.Header.AsMemory(0, _codec.Header.Length), cancellationToken);
            }

            await MessageIo.WriteMessageAsync(_stream, bytes, flush: true, cancellationToken: cancellationToken);
        }
    }
}
