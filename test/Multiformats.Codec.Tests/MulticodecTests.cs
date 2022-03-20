namespace Multiformats.Codec.Tests;

using Multiformats.Codec.Codecs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Class MulticodecTests.
/// </summary>
public partial class MulticodecTests
{
    /// <summary>
    /// Defines the test method CBORCodecRoundTrip.
    /// </summary>
    [Fact]
    public void CBORCodecRoundTrip()
    {
        MulticodecRoundTrip(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORCodecRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTripMany.
    /// </summary>
    [Fact]
    public void CBORCodecRoundTripMany()
    {
        MulticodecRoundTripMany(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORCodecRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTripManyNested_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORCodecRoundTripManyNested_Async()
    {
        return MulticodecRoundTripManyNestedAsync(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTripNested.
    /// </summary>
    [Fact]
    public void CBORCodecRoundTripNested()
    {
        MulticodecRoundTripNested(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORCodecRoundTripNested_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORCodecRoundTripNested_Async()
    {
        return MulticodecRoundTripNestedAsync(CborCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTrip.
    /// </summary>
    [Fact]
    public void CBORMulticodecRoundTrip()
    {
        MulticodecRoundTrip(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORMulticodecRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTripMany.
    /// </summary>
    [Fact]
    public void CBORMulticodecRoundTripMany()
    {
        MulticodecRoundTripMany(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORMulticodecRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTripManyNested_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORMulticodecRoundTripManyNested_Async()
    {
        return MulticodecRoundTripManyNestedAsync(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTripNested.
    /// </summary>
    [Fact]
    public void CBORMulticodecRoundTripNested()
    {
        MulticodecRoundTripNested(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method CBORMulticodecRoundTripNested_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task CBORMulticodecRoundTripNested_Async()
    {
        return MulticodecRoundTripNestedAsync(CborCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method JsonCodecWithMsgIoRoundTrip.
    /// </summary>
    [Fact]
    public void JsonCodecWithMsgIoRoundTrip()
    {
        MulticodecRoundTrip(JsonCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithMsgIoRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonCodecWithMsgIoRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(JsonCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void JsonCodecWithMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(JsonCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonCodecWithMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(JsonCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithoutMsgIoRoundTrip.
    /// </summary>
    [Fact]
    public void JsonCodecWithoutMsgIoRoundTrip()
    {
        MulticodecRoundTrip(JsonCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithoutMsgIoRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonCodecWithoutMsgIoRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(JsonCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithoutMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void JsonCodecWithoutMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(JsonCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method JsonCodecWithoutMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonCodecWithoutMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(JsonCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithMsgIoRoundTrip.
    /// </summary>
    [Fact]
    public void JsonMulticodecWithMsgIoRoundTrip()
    {
        MulticodecRoundTrip(JsonCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithMsgIoRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonMulticodecWithMsgIoRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(JsonCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void JsonMulticodecWithMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(JsonCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonMulticodecWithMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(JsonCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithoutMsgIoRoundTrip.
    /// </summary>
    [Fact]
    public void JsonMulticodecWithoutMsgIoRoundTrip()
    {
        MulticodecRoundTrip(JsonCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithoutMsgIoRoundTrip_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonMulticodecWithoutMsgIoRoundTrip_Async()
    {
        return MulticodecRoundTripAsync(JsonCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithoutMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void JsonMulticodecWithoutMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(JsonCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// Defines the test method JsonMulticodecWithoutMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task JsonMulticodecWithoutMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(JsonCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// Defines the test method MsgIoCodecRoundTripMany.
    /// </summary>
    [Fact]
    public void MsgIoCodecRoundTripMany()
    {
        MsgIoRoundTripMany(MsgIoCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method MsgIoCodecRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task MsgIoCodecRoundTripMany_Async()
    {
        return MsgIoRoundTripManyAsync(MsgIoCodec.CreateCodec());
    }

    /// <summary>
    /// Defines the test method MsgIoMulticodecRoundTripMany.
    /// </summary>
    [Fact]
    public void MsgIoMulticodecRoundTripMany()
    {
        MsgIoRoundTripMany(MsgIoCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method MsgIoMulticodecRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task MsgIoMulticodecRoundTripMany_Async()
    {
        return MsgIoRoundTripManyAsync(MsgIoCodec.CreateMulticodec());
    }

    /// <summary>
    /// Defines the test method MuxCodecRoundTripsNoWrap.
    /// </summary>
    [Fact]
    public void MuxCodecRoundTripsNoWrap()
    {
        MuxCodec codec = RandomMux();
        codec.Wrap = false;
        MulticodecRoundTripMany(codec);
    }

    /// <summary>
    /// Defines the test method MuxCodecRoundTripsWrap.
    /// </summary>
    [Fact]
    public void MuxCodecRoundTripsWrap()
    {
        MuxCodec codec = RandomMux();
        MulticodecRoundTripMany(codec);
    }

    /// <summary>
    /// Defines the test method ProtoBufCodecWithMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void ProtoBufCodecWithMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(ProtoBufCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method ProtoBufCodecWithMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task ProtoBufCodecWithMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(ProtoBufCodec.CreateCodec(true));
    }

    /// <summary>
    /// Defines the test method ProtoBufCodecWithoutMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void ProtoBufCodecWithoutMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(ProtoBufCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method ProtoBufCodecWithoutMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task ProtoBufCodecWithoutMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(ProtoBufCodec.CreateCodec(false));
    }

    /// <summary>
    /// Defines the test method ProtoBufMulticodecWithiutMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task ProtoBufMulticodecWithiutMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(ProtoBufCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// Defines the test method ProtoBufMulticodecWithMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void ProtoBufMulticodecWithMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(ProtoBufCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method ProtoBufMulticodecWithMsgIoRoundTripMany_Async.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public Task ProtoBufMulticodecWithMsgIoRoundTripMany_Async()
    {
        return MulticodecRoundTripManyAsync(ProtoBufCodec.CreateMulticodec(true));
    }

    /// <summary>
    /// Defines the test method ProtoBufMulticodecWithoutMsgIoRoundTripMany.
    /// </summary>
    [Fact]
    public void ProtoBufMulticodecWithoutMsgIoRoundTripMany()
    {
        MulticodecRoundTripMany(ProtoBufCodec.CreateMulticodec(false));
    }

    /// <summary>
    /// MSGs the io round trip many.
    /// </summary>
    /// <param name="codec">The codec.</param>
    private static void MsgIoRoundTripMany(MsgIoCodec codec)
    {
        int count = 1000;
        Random r = new(Environment.TickCount);
        byte[][] tests = Enumerable.Range(0, count).Select(i =>
        {
            byte[] bytes = new byte[1024];
            r.NextBytes(bytes);
            return bytes;
        }).ToArray();
        List<byte[]> results = new();

        using (MemoryStream stream = new())
        {
            ICodecEncoder enc = codec.Encoder(stream);
            foreach (byte[] test in tests)
            {
                enc.Encode(test);
            }

            _ = stream.Seek(0, SeekOrigin.Begin);
            ICodecDecoder dec = codec.Decoder(stream);
            for (int i = 0; i < tests.Length; i++)
            {
                results.Add(dec.Decode<byte[]>());
            }
        }

        Assert.Equal(results.ToArray(), tests);
    }

    /// <summary>
    /// MSG io round trip many as an asynchronous operation.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task MsgIoRoundTripManyAsync(MsgIoCodec codec)
    {
        int count = 1000;
        Random r = new(Environment.TickCount);
        byte[][] tests = Enumerable.Range(0, count).Select(i =>
        {
            byte[] bytes = new byte[1024];
            r.NextBytes(bytes);
            return bytes;
        }).ToArray();
        List<byte[]> results = new();

        using (MemoryStream stream = new())
        {
            ICodecEncoder enc = codec.Encoder(stream);
            foreach (byte[] test in tests)
            {
                await enc.EncodeAsync(test, CancellationToken.None);
            }

            _ = stream.Seek(0, SeekOrigin.Begin);
            ICodecDecoder dec = codec.Decoder(stream);
            for (int i = 0; i < tests.Length; i++)
            {
                results.Add(await dec.DecodeAsync<byte[]>(CancellationToken.None));
            }
        }

        Assert.Equal(results.ToArray(), tests);
    }

    /// <summary>
    /// Multicodecs the round trip.
    /// </summary>
    /// <param name="codec">The codec.</param>
    private static void MulticodecRoundTrip(ICodec codec)
    {
        TestClass test = new()
        {
            HelloString = "Hello World",
            HelloInt = int.MaxValue,
            HelloBool = true
        };
        TestClass result;

        using (MemoryStream stream = new())
        {
            codec.Encoder(stream).Encode(test);
            _ = stream.Seek(0, SeekOrigin.Begin);
            result = codec.Decoder(stream).Decode<TestClass>();
        }

        Assert.Equal(result, test);
    }

    /// <summary>
    /// Multicodec round trip as an asynchronous operation.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task MulticodecRoundTripAsync(ICodec codec)
    {
        TestClass test = new()
        {
            HelloString = "Hello World",
            HelloInt = int.MaxValue,
            HelloBool = true
        };
        TestClass result;

        using (MemoryStream stream = new())
        {
            await codec.Encoder(stream).EncodeAsync(test, CancellationToken.None);
            _ = stream.Seek(0, SeekOrigin.Begin);
            result = await codec.Decoder(stream).DecodeAsync<TestClass>(CancellationToken.None);
        }

        Assert.Equal(result, test);
    }

    /// <summary>
    /// Multicodecs the round trip many.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <param name="count">The count.</param>
    private static void MulticodecRoundTripMany(ICodec codec, int count = 1000)
    {
        TestClass[] tests = Enumerable.Range(0, count).Select(i => new TestClass
        {
            HelloString = "Hello World " + i,
            HelloInt = int.MaxValue,
            HelloBool = true
        }).ToArray();
        List<TestClass> results = new();

        using (MemoryStream stream = new())
        {
            ICodecEncoder enc = codec.Encoder(stream);
            foreach (TestClass test in tests)
            {
                enc.Encode(test);
            }

            _ = stream.Seek(0, SeekOrigin.Begin);
            ICodecDecoder dec = codec.Decoder(stream);
            for (int i = 0; i < tests.Length; i++)
            {
                results.Add(dec.Decode<TestClass>());
            }
        }

        Assert.Equal(results.ToArray(), tests);
    }

    /// <summary>
    /// Multicodec round trip many as an asynchronous operation.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <param name="count">The count.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task MulticodecRoundTripManyAsync(ICodec codec, int count = 1000)
    {
        TestClass[] tests = Enumerable.Range(0, count).Select(i => new TestClass
        {
            HelloString = "Hello World " + i,
            HelloInt = int.MaxValue,
            HelloBool = true
        }).ToArray();
        List<TestClass> results = new();

        using (MemoryStream stream = new())
        {
            ICodecEncoder enc = codec.Encoder(stream);
            foreach (TestClass test in tests)
            {
                await enc.EncodeAsync(test, CancellationToken.None);
            }

            _ = stream.Seek(0, SeekOrigin.Begin);
            ICodecDecoder dec = codec.Decoder(stream);
            for (int i = 0; i < tests.Length; i++)
            {
                results.Add(await dec.DecodeAsync<TestClass>(CancellationToken.None));
            }
        }

        Assert.Equal(results.ToArray(), tests);
    }

    /// <summary>
    /// Multicodec round trip many nested as an asynchronous operation.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <param name="count">The count.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task MulticodecRoundTripManyNestedAsync(ICodec codec, int count = 1000)
    {
        NestedTestClass[] tests = Enumerable.Range(0, count).Select(i => new NestedTestClass
        {
            HelloOther = new TestClass
            {
                HelloString = "Hello World",
                HelloInt = int.MaxValue,
                HelloBool = true
            }
        }).ToArray();
        List<NestedTestClass> results = new();

        using (MemoryStream stream = new())
        {
            ICodecEncoder enc = codec.Encoder(stream);
            foreach (NestedTestClass test in tests)
            {
                await enc.EncodeAsync(test, CancellationToken.None);
            }

            _ = stream.Seek(0, SeekOrigin.Begin);
            ICodecDecoder dec = codec.Decoder(stream);
            for (int i = 0; i < tests.Length; i++)
            {
                results.Add(await dec.DecodeAsync<NestedTestClass>(CancellationToken.None));
            }
        }

        Assert.Equal(results.ToArray(), tests);
    }

    /// <summary>
    /// Multicodecs the round trip nested.
    /// </summary>
    /// <param name="codec">The codec.</param>
    private static void MulticodecRoundTripNested(ICodec codec)
    {
        NestedTestClass test = new()
        {
            HelloOther = new TestClass
            {
                HelloString = "Hello World",
                HelloInt = int.MaxValue,
                HelloBool = true
            }
        };
        NestedTestClass result;

        using (MemoryStream stream = new())
        {
            codec.Encoder(stream).Encode(test);
            _ = stream.Seek(0, SeekOrigin.Begin);
            result = codec.Decoder(stream).Decode<NestedTestClass>();
        }

        Assert.Equal(result, test);
    }

    /// <summary>
    /// Multicodec round trip nested as an asynchronous operation.
    /// </summary>
    /// <param name="codec">The codec.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task MulticodecRoundTripNestedAsync(ICodec codec)
    {
        NestedTestClass test = new()
        {
            HelloOther = new TestClass
            {
                HelloString = "Hello World",
                HelloInt = int.MaxValue,
                HelloBool = true
            }
        };
        NestedTestClass result;

        using (MemoryStream stream = new())
        {
            await codec.Encoder(stream).EncodeAsync(test, CancellationToken.None);
            _ = stream.Seek(0, SeekOrigin.Begin);
            result = await codec.Decoder(stream).DecodeAsync<NestedTestClass>(CancellationToken.None);
        }

        Assert.Equal(result, test);
    }

    /// <summary>
    /// Randoms the mux.
    /// </summary>
    /// <returns>MuxCodec.</returns>
    private static MuxCodec RandomMux()
    {
        MuxCodec c = MuxCodec.Standard;
        c.Select = SelectRandomCodec();
        return c;
    }

    /// <summary>
    /// Selects the random codec.
    /// </summary>
    /// <returns>MuxCodec.SelectCodecDelegate.</returns>
    private static MuxCodec.SelectCodecDelegate SelectRandomCodec()
    {
        bool reuse = false;
        int last = 0;
        Random rand = new(Environment.TickCount);

        return (o, codecs) =>
        {
            if (reuse)
            {
                reuse = false;
            }
            else
            {
                reuse = true;
                last = rand.Next(0, codecs.Length);
            }

            return codecs[last % codecs.Length];
        };
    }
}
