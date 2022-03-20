namespace Multiformats.Codec;

/// <summary>
/// Interface ICodecEncoder
/// </summary>
public interface ICodecEncoder
{
    /// <summary>
    /// Encodes the specified object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    void Encode<T>(T obj);

    /// <summary>
    /// Encodes the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task.</returns>
    Task EncodeAsync<T>(T obj, CancellationToken cancellationToken = default);
}
