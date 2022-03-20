namespace Multiformats.Codec.Codecs;

using PeterO.Cbor;

public partial class CborCodec
{
    /// <summary>
    /// Class CBOREncoder.
    /// Implements the <see cref="ICodecEncoder" />
    /// </summary>
    /// <seealso cref="ICodecEncoder" />
    private class CBOREncoder : ICodecEncoder
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
        /// Initializes a new instance of the <see cref="CBOREncoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public CBOREncoder(Stream stream, CborCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        /// <summary>
        /// Encodes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        public void Encode<T>(T obj)
        {
            if (_codec._multicodec)
            {
                _stream.Write(_codec.Header, 0, _codec.Header.Length);
            }

            CBORObject? cbor = CBORObject.FromObject(obj);
            cbor.WriteTo(_stream);
            _stream.Flush();
        }

        /// <summary>
        /// Encode as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task EncodeAsync<T>(T obj, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (_codec._multicodec)
            {
                await _stream.WriteAsync(_codec.Header.AsMemory(0, _codec.Header.Length), cancellationToken);
            }

            CBORObject? cbor = CBORObject.FromObject(obj);

            cbor.WriteTo(_stream);
            await _stream.FlushAsync(cancellationToken);
        }
    }
}
