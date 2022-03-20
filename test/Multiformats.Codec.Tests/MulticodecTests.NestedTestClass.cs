namespace Multiformats.Codec.Tests;

/// <summary>
/// Class MulticodecTests.
/// </summary>
public partial class MulticodecTests
{
    /// <summary>
    /// Class NestedTestClass.
    /// </summary>
    [Serializable, ProtoBuf.ProtoContract]
    public class NestedTestClass
    {
        /// <summary>
        /// Gets or sets the hello other.
        /// </summary>
        /// <value>The hello other.</value>
        [ProtoBuf.ProtoMember(1)]
        public TestClass? HelloOther { get; set; }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is NestedTestClass other && other.HelloOther is not null && (HelloOther?.Equals(other.HelloOther) ?? false);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
