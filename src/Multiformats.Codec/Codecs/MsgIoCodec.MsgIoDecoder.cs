namespace Multiformats.Codec.Codecs;
public partial class MsgIoCodec
{
    /// <summary>
    /// The message I/O decoder class
    /// </summary>
    /// <seealso cref="ICodecDecoder" />
    private class MsgIoDecoder : ICodecDecoder
    {
        private readonly MsgIoCodec _codec;
        private readonly Stream _stream;

        /// <summary>Initializes a new instance of the <see cref="MsgIoDecoder"/> class.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public MsgIoDecoder(Stream stream, MsgIoCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        /// <summary>Decodes this instance.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Decode<T>()
        {
            if (_codec._multicodec)
            {
                Multicodec.ConsumeHeader(_stream, _codec.Header);
            }

            byte[]? bytes = MessageIo.ReadMessage(_stream);

            return (T)(object)bytes;
        }

        /// <summary>Decodes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<T> DecodeAsync<T>(CancellationToken cancellationToken = default)
        {
            if (_codec._multicodec)
            {
                await Multicodec.ConsumeHeaderAsync(_stream, _codec.Header, cancellationToken);
            }

            byte[]? bytes = await MessageIo.ReadMessageAsync(_stream, cancellationToken);

            return (T)(object)bytes;
        }
    }
}
