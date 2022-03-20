namespace Multiformats.Codec.Codecs;

using PeterO.Cbor;

public partial class CborCodec
{
    /// <summary>
    /// Class CBORDecoder.
    /// Implements the <see cref="ICodecDecoder" />
    /// </summary>
    /// <seealso cref="ICodecDecoder" />
    private class CBORDecoder : ICodecDecoder
    {
        /// <summary>
        /// The codec
        /// </summary>
        private readonly CborCodec _codec;

        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="CBORDecoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public CBORDecoder(Stream stream, CborCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public T Decode<T>()
        {
            if (_codec._multicodec)
            {
                Multicodec.ConsumeHeader(_stream, _codec.Header);
            }

            return CBORObject.Read(_stream).ToObject<T>();
        }

        /// <summary>
        /// Decode as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
        public async Task<T> DecodeAsync<T>(CancellationToken cancellationToken = default)
        {
            if (_codec._multicodec)
            {
                await Multicodec.ConsumeHeaderAsync(_stream, _codec.Header, cancellationToken);
            }

            return CBORObject.Read(_stream).ToObject<T>();
        }
    }
}
