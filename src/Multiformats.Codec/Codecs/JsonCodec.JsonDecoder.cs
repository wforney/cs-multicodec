namespace Multiformats.Codec.Codecs;

using Newtonsoft.Json;
using System.Text;

public partial class JsonCodec
{
    /// <summary>
    /// Class JsonDecoder.
    /// Implements the <see cref="ICodecDecoder" />
    /// </summary>
    /// <seealso cref="ICodecDecoder" />
    private class JsonDecoder : ICodecDecoder
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
        /// Initializes a new instance of the <see cref="JsonDecoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public JsonDecoder(Stream stream, JsonCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        /// <typeparam name="T">The type being deserialized.</typeparam>
        /// <returns>T.</returns>
        public T Decode<T>()
        {
            if (_codec._multicodec)
            {
                Multicodec.ConsumeHeader(_stream, _codec.Header);
            }

            string? json = _codec._msgio
                ? Encoding.UTF8.GetString((byte[]?)MessageIo.ReadMessage(_stream) ?? Array.Empty<byte>())
                : ReadLine(_stream);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Decode as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The type being deserialized.</typeparam>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
        public async Task<T> DecodeAsync<T>(CancellationToken cancellationToken = default)
        {
            if (_codec._multicodec)
            {
                await Multicodec.ConsumeHeaderAsync(_stream, _codec.Header, cancellationToken);
            }

            string? json;
            if (_codec._msgio)
            {
                byte[]? bytes = await MessageIo.ReadMessageAsync(_stream, cancellationToken);
                json = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                json = await ReadLineAsync(_stream, cancellationToken);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>System.String.</returns>
        private static string ReadLine(Stream stream)
        {
            byte[]? buffer = new byte[4096];
            int offset = 0;
            while ((_ = stream.Read(buffer, offset, 1)) != -1 && buffer[offset] != Multicodec.NewLine)
            {
                offset++;
            }

            return Encoding.UTF8.GetString(buffer.Slice(0, offset)).Trim();
        }

        /// <summary>
        /// Read line as an asynchronous operation.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        private static async Task<string> ReadLineAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            byte[]? buffer = new byte[4096];
            int offset = 0;
            while ((_ = await stream.ReadAsync(buffer.AsMemory(offset, 1), cancellationToken)) != -1 && buffer[offset] != Multicodec.NewLine)
            {
                offset++;
            }

            return Encoding.UTF8.GetString(buffer.Slice(0, offset)).Trim();
        }
    }
}
