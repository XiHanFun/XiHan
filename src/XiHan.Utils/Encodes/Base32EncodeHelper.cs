#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:Base32EncodeHelper
// Guid:87047528-03ee-4970-bfbd-94c861e3f0b3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:15:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Encodes;

/// <summary>
/// Base32编码帮助类
/// </summary>
public static class Base32EncodeHelper
{
    /// <summary>
    /// 对字符串进行 Base32 编码
    /// </summary>
    /// <param name="data">待编码的字符串</param>
    /// <returns>编码后的字符串</returns>
    public static string Base32Encode(this string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        return Base32.ToBase32String(bytes);
    }

    /// <summary>
    /// 对 Base32 编码的字符串进行解码
    /// </summary>
    /// <param name="data">待解码的字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string Base32Decode(this string data)
    {
        byte[] bytes = Base32.FromBase32String(data);
        return Encoding.UTF8.GetString(bytes);
    }
}

/// <summary>
/// 手写Base32转换
/// </summary>
public static class Base32
{
    private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    /// <summary>
    /// byte转换为Base32
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ToBase32String(byte[] bytes)
    {
        if (bytes == null)
        {
            throw new ArgumentNullException(nameof(bytes));
        }

        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        StringBuilder sb = new((bytes.Length * 8 + 4) / 5);

        int bitCount = 0;
        int accumulatedBits = 0;

        foreach (byte currentByte in bytes)
        {
            accumulatedBits |= currentByte << bitCount;
            bitCount += 8;
            while (bitCount >= 5)
            {
                int mask = 0x1f;
                int currentBase32Value = accumulatedBits & mask;
                sb.Append(Base32Alphabet[currentBase32Value]);
                accumulatedBits >>= 5;
                bitCount -= 5;
            }
        }

        if (bitCount > 0)
        {
            int mask = 0x1f;
            int currentBase32Value = accumulatedBits & mask;
            sb.Append(Base32Alphabet[currentBase32Value]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Base32转换为byte
    /// </summary>
    /// <param name="base32String"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static byte[] FromBase32String(string base32String)
    {
        if (base32String == null)
        {
            throw new ArgumentNullException("base32String");
        }

        if (base32String.Length == 0)
        {
            return Array.Empty<byte>();
        }

        base32String = base32String.TrimEnd('=');

        int byteCount = base32String.Length * 5 / 8;
        byte[] buffer = new byte[byteCount];

        int bitCount = 0;
        int accumulatedBits = 0;
        int bufferIndex = 0;
        foreach (char currentChar in base32String)
        {
            int currentCharValue = Base32Alphabet.IndexOf(currentChar);
            if (currentCharValue < 0 || currentCharValue > 31)
            {
                throw new ArgumentException("Invalid character in Base32 string.");
            }

            accumulatedBits |= currentCharValue << bitCount;
            bitCount += 5;

            if (bitCount >= 8)
            {
                int mask = 0xff;
                int currentByteValue = accumulatedBits & mask;
                buffer[bufferIndex++] = (byte)currentByteValue;
                accumulatedBits >>= 8;
                bitCount -= 8;
            }
        }

        return buffer;
    }
}