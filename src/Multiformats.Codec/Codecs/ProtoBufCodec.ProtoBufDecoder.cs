namespace Multiformats.Codec.Codecs;

using ProtoBuf;

public partial class ProtoBufCodec
{
    /// <summary>
    /// Class ProtoBufDecoder.
    /// Implements the <see cref="ICodecDecoder" />
    /// </summary>
    /// <seealso cref="ICodecDecoder" />
    private class ProtoBufDecoder : ICodecDecoder
    {
        /// <summary>
        /// The codec
        /// </summary>
        private readonly ProtoBufCodec _codec;

        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtoBufDecoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public ProtoBufDecoder(Stream stream, ProtoBufCodec codec)
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

            if (_codec._msgio)
            {
                return Deserialize<T>(MessageIo.ReadMessage(_stream));
            }

            return ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(_stream, PrefixStyle.Fixed32BigEndian);
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

            if (_codec._msgio)
            {
                return Deserialize<T>(await MessageIo.ReadMessageAsync(_stream, cancellationToken));
            }

            return ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(_stream, PrefixStyle.Fixed32BigEndian);
        }

        /// <summary>
        /// Deserializes the specified buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">The buffer.</param>
        /// <returns>T.</returns>
        private static T Deserialize<T>(byte[] buffer)
        {
            using MemoryStream stream = new(buffer);
            return Serializer.Deserialize<T>(stream);
        }
    }
}
