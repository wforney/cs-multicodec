namespace Multiformats.Codec;

/// <summary>
/// Class StringValueAttribute.
/// Implements the <see cref="Attribute" />.
/// </summary>
/// <seealso cref="Attribute" />
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class StringValueAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringValueAttribute"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public StringValueAttribute(string value)
        : base()
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public string Value { get; }
}
