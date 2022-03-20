namespace Multiformats.Codec.Codecs;

using System.Text;

public partial class MuxCodec
{
    private class MuxDecoder : ICodecDecoder
    {
        private readonly Stream _stream;
        private readonly MuxCodec _codec;

        public MuxDecoder(Stream stream, MuxCodec codec)
        {
            _stream = stream;
            _codec = codec;
        }

        public T Decode<T>()
        {
            if (_codec.Wrap)
            {
                Multicodec.ConsumeHeader(_stream, _codec.Header);
            }

            byte[]? hdr = Multicodec.PeekHeader(_stream);
            if (hdr is null || hdr.Length == 0)
            {
                throw new EndOfStreamException();
            }

            ICodec? subcodec = _codec._codecs.SingleOrDefault(c => c.Header.SequenceEqual(hdr));
            if (subcodec is null)
            {
                throw new Exception($"no codec found for {Encoding.UTF8.GetString(hdr)}");
            }

            _codec.Last = subcodec;

            return subcodec.Decoder(_stream).Decode<T>();
        }

        public async Task<T> DecodeAsync<T>(CancellationToken cancellationToken = default)
        {
            if (_codec.Wrap)
            {
                await Multicodec.ConsumeHeaderAsync(_stream, _codec.Header, cancellationToken);
            }

            byte[]? hdr = await Multicodec.PeekHeaderAsync(_stream, cancellationToken);
            if (hdr is null || hdr.Length == 0)
            {
                throw new EndOfStreamException();
            }

            ICodec? subcodec = _codec._codecs.SingleOrDefault(c => c.Header.SequenceEqual(hdr));
            if (subcodec is null)
            {
                throw new Exception($"no codec found for {Encoding.UTF8.GetString(hdr)}");
            }

            _codec.Last = subcodec;

            return await subcodec.Decoder(_stream).DecodeAsync<T>(cancellationToken);
        }
    }
}
