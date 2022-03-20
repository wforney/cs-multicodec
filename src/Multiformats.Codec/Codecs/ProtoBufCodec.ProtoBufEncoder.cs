using System;

namespace Multiformats.Codec.Codecs;

using ProtoBuf;

public partial class ProtoBufCodec
{
    /// <summary>
    /// Class ProtoBufEncoder.
    /// Implements the <see cref="ICodecEncoder" />
    /// </summary>
    /// <seealso cref="ICodecEncoder" />
    private class ProtoBufEncoder : ICodecEncoder
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
        /// Initializes a new instance of the <see cref="ProtoBufEncoder"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="codec">The codec.</param>
        public ProtoBufEncoder(Stream stream, ProtoBufCodec codec)
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
                MessageIo.WriteMessage(_stream, Serialize(obj));
            }
            else
            {
                ProtoBuf.Serializer.SerializeWithLengthPrefix(_stream, obj, PrefixStyle.Fixed32BigEndian);
            }
            _stream.Flush();
        }

        /// <summary>
        /// Encode as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.Threading.Tasks.Task&gt; representing the asynchronous operation.</returns>
        public async Task EncodeAsync<T>(T obj, CancellationToken cancellationToken = default)
        {
            if (_codec._multicodec)
            {
                await _stream.WriteAsync(_codec.Header.AsMemory(0, _codec.Header.Length), cancellationToken);
            }

            if (_codec._msgio)
            {
                await MessageIo.WriteMessageAsync(_stream, Serialize(obj), cancellationToken: cancellationToken);
            }
            else
            {
                ProtoBuf.Serializer.SerializeWithLengthPrefix(_stream, obj, PrefixStyle.Fixed32BigEndian);
            }
            await _stream.FlushAsync(cancellationToken);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>byte[].</returns>
        private static byte[] Serialize<T>(T obj)
        {
            using MemoryStream? stream = new();
            Serializer.Serialize(stream, obj);
            return stream.ToArray();
        }
    }
}
