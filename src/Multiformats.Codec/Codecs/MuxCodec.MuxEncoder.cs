using System;

namespace Multiformats.Codec.Codecs;

public partial class MuxCodec
{
    private class MuxEncoder : ICodecEncoder
    {
        private readonly Stream _stream;
        private readonly MuxCodec _codec;

        public MuxEncoder(Stream stream, MuxCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        public void Encode<T>(T obj)
        {
            ICodec? subcodec = _codec.GetCodec(obj);
            if (subcodec is null)
            {
                throw new Exception("no suitable codec found");
            }

            if (_codec.Wrap)
            {
                _stream.Write(_codec.Header, 0, _codec.Header.Length);
            }

            _codec.Last = subcodec;
            subcodec.Encoder(_stream).Encode(obj);
        }

        public async Task EncodeAsync<T>(T obj, CancellationToken cancellationToken = default)
        {
            ICodec? subcodec = _codec.GetCodec(obj);
            if (subcodec is null)
            {
                throw new Exception("no suitable codec found");
            }

            if (_codec.Wrap)
            {
                await _stream.WriteAsync(_codec.Header.AsMemory(0, _codec.Header.Length), cancellationToken);
            }

            _codec.Last = subcodec;
            await subcodec.Encoder(_stream).EncodeAsync(obj, cancellationToken);
        }
    }
}
