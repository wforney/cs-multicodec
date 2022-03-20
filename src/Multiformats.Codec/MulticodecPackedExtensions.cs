namespace Multiformats.Codec;

using System.Reflection;

/// <summary>
/// The multicodec packed extensions class
/// </summary>
public static class MulticodecPackedExtensions
{
    /// <summary>Gets the string.</summary>
    /// <param name="code">The code.</param>
    /// <returns>The string.</returns>
    public static string GetString(this MulticodecCode code)
    {
        MemberInfo[]? memberInfo = code.GetType().GetMember(code.ToString());
        if (memberInfo is not null && memberInfo.Length > 0)
        {
            StringValueAttribute? attr = memberInfo[0].GetCustomAttribute<StringValueAttribute>();
            if (attr is not null)
            {
                return attr.Value;
            }
        }

        return code.ToString().ToLower();
    }
}
