namespace Multiformats.Codec;

/// <summary>
/// Interface ICodecDecoder
/// </summary>
public interface ICodecDecoder
{
    /// <summary>
    /// Decodes this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>T.</returns>
    T Decode<T>();

    /// <summary>
    /// Decodes the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;T&gt;.</returns>
    Task<T> DecodeAsync<T>(CancellationToken cancellationToken = default);
}
