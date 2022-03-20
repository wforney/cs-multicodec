namespace Multiformats.Codec.Tests;

using System;
using System.Collections.Generic;

/// <summary>
/// The multicodec tests class
/// </summary>
public partial class MulticodecTests
{
    /// <summary>
    /// The test class
    /// </summary>
    [Serializable, ProtoBuf.ProtoContract]
    public class TestClass : IEquatable<TestClass?>
    {
        /// <summary>
        /// Gets or sets a value indicating whether [hello bool].
        /// </summary>
        /// <value><c>true</c> if [hello bool]; otherwise, <c>false</c>.</value>
        [ProtoBuf.ProtoMember(3)]
        public bool HelloBool { get; set; }

        /// <summary>Gets or sets the hello int.</summary>
        /// <value>The hello int.</value>
        [ProtoBuf.ProtoMember(2)]
        public int HelloInt { get; set; }

        /// <summary>Gets or sets the hello string.</summary>
        /// <value>The hello string.</value>
        [ProtoBuf.ProtoMember(1)]
        public string? HelloString { get; set; }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return Equals(obj as TestClass);
        }

        /// <inheritdoc />
        public bool Equals(TestClass? other)
        {
            return other is not null &&
                   HelloBool == other.HelloBool &&
                   HelloInt == other.HelloInt &&
                   HelloString == other.HelloString;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(HelloBool, HelloInt, HelloString);
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TestClass? left, TestClass? right)
        {
            return EqualityComparer<TestClass>.Default.Equals(left, right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TestClass? left, TestClass? right)
        {
            return !(left == right);
        }
    }
}
