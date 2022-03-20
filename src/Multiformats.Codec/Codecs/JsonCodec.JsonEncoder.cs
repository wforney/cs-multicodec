using System;

namespace Multiformats.Codec.Codecs;

using Newtonsoft.Json;
using System.Text;

public partial class JsonCodec
{
    /// <summary>
    /// Class JsonEncoder.
    /// Implements the <see cref="ICodecEncoder" />
    /// </summary>
    /// <seealso cref="ICodecEncoder" />
    private class JsonEncoder : ICodecEncoder
    {
        /// <summary>
        /// The codec
        /// </summary>
        private readonly JsonCodec _codec;

        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonEncoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public JsonEncoder(Stream stream, JsonCodec codec)
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

            if (_codec._msgio)
            {
                MessageIo.WriteMessage(_stream, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.None)));
            }
            else
            {
                byte[]? bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.None) + '\n');
                _stream.Write(bytes, 0, bytes.Length);
            }
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
            if (_codec._multicodec)
            {
                await _stream.WriteAsync(_codec.Header.AsMemory(0, _codec.Header.Length), cancellationToken);
            }

            if (_codec._msgio)
            {
                await MessageIo.WriteMessageAsync(_stream, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.None)), cancellationToken: cancellationToken);
            }
            else
            {
                byte[]? bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.None) + '\n');
                await _stream.WriteAsync(bytes, cancellationToken);
            }
        }
    }
}
