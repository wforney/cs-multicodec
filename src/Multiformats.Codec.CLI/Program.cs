namespace Multiformats.Codec.CLI;

using Multiformats.Codec.Codecs;
using System.Diagnostics;
using System.Text;

/// <summary>
/// The program class.
/// </summary>
internal class Program
{
    /// <summary>
    /// The asynchronous
    /// </summary>
    private static bool @async = false;

    /// <summary>
    /// The mcwrap
    /// </summary>
    private static bool mcwrap = false;

    /// <summary>
    /// The msgio
    /// </summary>
    private static bool msgio = false;

    /// <summary>
    /// The verbose
    /// </summary>
    private static bool verbose = true;

    /// <summary>
    /// Codecs this instance.
    /// </summary>
    /// <returns>Multiformats.Codec.Codecs.MuxCodec.</returns>
    private static MuxCodec Codec()
    {
        MuxCodec? m = MuxCodec.Standard;
        m.Wrap = mcwrap;
        return m;
    }

    /// <summary>
    /// Codecs the with path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>Multiformats.Codec.ICodec.</returns>
    private static ICodec? CodecWithPath(string path)
    {
        byte[]? hdr = Multicodec.Header(Encoding.UTF8.GetBytes(path));

        return MuxCodec.CodecWithHeader(hdr, MuxCodec.Standard.Codecs);
    }

    /// <summary>
    /// Decodes the specified reader.
    /// </summary>
    /// <param name="r">The reader.</param>
    /// <param name="next">The next.</param>
    private static void Decode(StreamReader r, Func<MuxCodec, dynamic, bool> next)
    {
        MuxCodec? c = Codec();
        ICodecDecoder? dec = c.Decoder(r.BaseStream);

        try
        {
            while (true)
            {
                dynamic v = dec.Decode<dynamic>();

                if (!next(c, v))
                {
                    break;
                }
            }
        }
        catch (EndOfStreamException)
        {
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
        }
    }

    /// <summary>
    /// Decode as an asynchronous operation.
    /// </summary>
    /// <param name="r">The reader.</param>
    /// <param name="next">The next.</param>
    /// <returns>A Task&lt;System.Threading.Tasks.Task&gt; representing the asynchronous operation.</returns>
    private static async Task DecodeAsync(StreamReader r, Func<MuxCodec, dynamic, Task<bool>> next)
    {
        MuxCodec? c = Codec();
        ICodecDecoder? dec = c.Decoder(r.BaseStream);

        try
        {
            while (true)
            {
                dynamic v = await dec.DecodeAsync<dynamic>(CancellationToken.None);

                if (!await next(c, v))
                {
                    break;
                }
            }
        }
        catch (EndOfStreamException)
        {
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error: {e.Message}");
        }
    }

    /// <summary>
    /// Filters the specified writer.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    /// <param name="path">The path.</param>
    /// <exception cref="NotImplementedException"></exception>
    private static void Filter(StreamWriter w, StreamReader r, string path)
    {
        throw new NotImplementedException();
        Decode(r, (codec, value) =>
        {
            byte[]? hdr = Multicodec.Header(Encoding.UTF8.GetBytes(path));
            return !(codec.Last?.Header.SequenceEqual(hdr) ?? false) || true;
        });
    }

    /// <summary>
    /// H2P
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    private static void H2P(StreamWriter w, StreamReader r)
    {
        while (true)
        {
            byte[]? hdr = Multicodec.ReadHeader(r.BaseStream);
            if (hdr is null || hdr.Length == 0)
            {
                return;
            }

            string? p = Encoding.UTF8.GetString(Multicodec.HeaderPath(hdr));
            w.WriteLine(p);
        }
    }

    /// <summary>
    /// Headers the specified writer.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="path">The path.</param>
    private static void Header(StreamWriter w, string path)
    {
        w.Write(Multicodec.Header(Encoding.UTF8.GetBytes(path)));
    }

    /// <summary>
    /// Headerses the specified writer.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    private static void Headers(StreamWriter w, StreamReader r)
    {
        Decode(r, (codec, obj) =>
        {
            w.Write(Encoding.UTF8.GetString(codec.Last?.Header ?? Array.Empty<byte>()));
            return true;
        });
    }

    /// <summary>
    /// Logs the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    private static void Log(string message)
    {
        if (verbose)
        {
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintUsage();
            return;
        }

        int i = 0;
        string? command = args[i++];
        while (i < args.Length && args[i].StartsWith("--"))
        {
            switch (args[i].TrimStart('-').ToLower())
            {
                case "mcwrap":
                    mcwrap = true;
                    break;

                case "msgio":
                    msgio = true;
                    break;

                case "verbose":
                    verbose = true;
                    break;

                case "async":
                    @async = true;
                    break;
            }

            i++;
        }

        Console.WriteLine($"Options: wrap={mcwrap}, msgio={msgio}, verbose={verbose}, async={async}");

        using (StreamWriter? w = new(Console.OpenStandardOutput(), Encoding.ASCII))
        {
            using MemoryStream? mem = new();
            Console.OpenStandardInput().CopyTo(mem);
            mem.Seek(0, SeekOrigin.Begin);

            using StreamReader? r = new(mem);

            switch (command.ToLower())
            {
                case "header":
                    Header(w, args[i]);
                    break;

                case "headers":
                    Headers(w, r);
                    break;

                case "paths":
                    Paths(w, r);
                    break;

                case "wrap":
                    Wrap(w, r, args[i]);
                    break;

                case "filter":
                    Filter(w, r, args[i]);
                    break;

                case "recode":
                    if (@async)
                    {
                        RecodeAsync(w, r, args[i]).Wait();
                    }
                    else
                    {
                        Recode(w, r, args[i]);
                    }

                    break;

                case "h2p":
                    H2P(w, r);
                    break;

                case "p2h":
                    P2H(w, r);
                    break;

                default:
                    PrintUsage();
                    break;
            }
        }

        if (Debugger.IsAttached)
        {
            Console.ReadLine();
        }
    }

    /// <summary>
    /// P2H
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    private static void P2H(StreamWriter w, StreamReader r)
    {
        while (true)
        {
            string? p = r.ReadLine();
            if (string.IsNullOrEmpty(p))
            {
                break;
            }

            byte[]? hdr = Multicodec.Header(Encoding.UTF8.GetBytes(p));
            w.Write(hdr);
        }
    }

    /// <summary>
    /// Pathses the specified writer.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    private static void Paths(StreamWriter w, StreamReader r)
    {
        Decode(r, (codec, obj) =>
        {
            byte[]? p = Multicodec.HeaderPath(codec.Last?.Header ?? Array.Empty<byte>());
            w.WriteLine(Encoding.UTF8.GetString(p));
            return true;
        });
    }

    /// <summary>
    /// Prints the usage.
    /// </summary>
    private static void PrintUsage()
    {
        Console.WriteLine(
            string.Join(
                Environment.NewLine,
                "multicodec - tool to inspect and manipulate mixed codec streams",
                "",
                "Usage",
                "\tcat rawjson | multicodec wrap /json/msgio >mcjson"));
    }

    /// <summary>
    /// Recodes the specified w.
    /// </summary>
    /// <param name="w">The w.</param>
    /// <param name="r">The r.</param>
    /// <param name="path">The path.</param>
    /// <exception cref="Exception">$"unknown codec {path}</exception>
    private static void Recode(StreamWriter w, StreamReader r, string path)
    {
        ICodec? codec = CodecWithPath(path);
        if (codec is null)
        {
            throw new Exception($"unknown codec {path}");
        }

        ICodecEncoder? enc = codec.Encoder(w.BaseStream);

        Decode(
            r,
            (c, v) =>
            {
                enc.Encode(v);
                return true;
            });
    }

    /// <summary>
    /// Recode as an asynchronous operation.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    /// <param name="path">The path.</param>
    /// <returns>A Task&lt;System.Threading.Tasks.Task&gt; representing the asynchronous operation.</returns>
    /// <exception cref="Exception">$"unknown codec {path}</exception>
    private static async Task RecodeAsync(StreamWriter w, StreamReader r, string path)
    {
        ICodec? codec = CodecWithPath(path);
        if (codec is null)
        {
            throw new Exception($"unknown codec {path}");
        }

        ICodecEncoder? enc = codec.Encoder(w.BaseStream);

        await DecodeAsync(
            r,
            async (c, v) =>
            {
                await enc.EncodeAsync(v, CancellationToken.None);
                return true;
            });
    }

    /// <summary>
    /// Wraps the specified writer.
    /// </summary>
    /// <param name="w">The writer.</param>
    /// <param name="r">The reader.</param>
    /// <param name="path">The path.</param>
    /// <exception cref="Exception">$"unknown codec {path}</exception>
    /// <exception cref="Exception">$"wrap unsupported for codec {hdrs}</exception>
    private static void Wrap(StreamWriter w, StreamReader r, string path)
    {
        ICodec? mcc = CodecWithPath(path);
        if (mcc is null)
        {
            throw new Exception($"unknown codec {path}");
        }

        string? hdrs = Encoding.UTF8.GetString(mcc.Header);

        void wrapRT(ICodec c, ICodec mc)
        {
            dynamic v = c.Decoder(r.BaseStream).Decode<dynamic>();
            mc.Encoder(w.BaseStream).Encode(v);
        }

        if (hdrs == JsonCodec.HeaderMsgioPath)
        {
            wrapRT(JsonCodec.CreateCodec(true), mcc);
        }
        else if (hdrs == JsonCodec.HeaderPath)
        {
            wrapRT(JsonCodec.CreateCodec(false), mcc);
        }
        else if (hdrs == CborCodec.HeaderPath)
        {
            wrapRT(CborCodec.CreateCodec(), mcc);
        }
        else
        {
            throw new Exception($"wrap unsupported for codec {hdrs}");
        }
    }
}
