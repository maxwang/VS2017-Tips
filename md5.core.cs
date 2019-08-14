public static class HashAlgorithmHelper
{
    static readonly char[] Digitals = {'0','1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

    // 在这个函数里要用到的 bytes 成员 ReadOnlySpan<byte> 与 byte[] 的一致，所以我们只需要修改参数类型即可。
    static string ToString(ReadOnlySpan<byte> bytes)
    {
        unsafe
        {
            const int byte_len = 2; // 表示一个 byte 的字符长度。

            var str = new string('\0', byte_len * bytes.Length);

            fixed(char* pStr = str)
            {
                var pStr2 = pStr; // fixed pStr 是只读的，所以我们定义一个变量。

                foreach (var item in bytes)
                {
                    *pStr2 = Digitals[item >> 4/* byte high */]; ++pStr2;
                    *pStr2 = Digitals[item & 15/* byte low  */]; ++pStr2;
                }
            }

            return str;
        }
    }

    public static string ComputeHash<THashAlgorithm>(string input) where THashAlgorithm : HashAlgorithm
    {
        var instance = THashAlgorithmInstances<THashAlgorithm>.Instance; // 避免二次取值，微微提高效率（自我感觉）。
        var encoding = Encoding.UTF8;

        // 我们在这里声明一个足量的 byte 数组，足以容下字符串的 utf-8 字节码和 hash 值的字节码。
        var bytes = new byte[Encoding.UTF8.GetMaxByteCount(Math.Max(input.Length, instance.HashSize / 2))];

        var bytesCount = encoding.GetBytes(input, bytes);

        var source = new ReadOnlySpan<byte>(bytes, 0, bytesCount); // source: utf-8 bytes region.
        var destination = new Span<byte>(bytes, bytesCount, bytes.Length - bytesCount); // destination: buffer region.

        if (bytes.Length - bytesCount > instance.HashSize && instance.TryComputeHash(source, destination, out var bytesWritten))
        {
            return ToString(destination.Slice(0, bytesWritten));
        }
        else
        {
            // 通常情况下这里就很有可能抛出异常了，但是我们封装工具方法必须有一个原则，我们尽量不要自行抛出异常。
            // 用户的参数执行到这里我们依然调用 HashAlgorithm.ComputeHash，由它内部抛出异常。这样可以避免很多问题和歧义。
            return ToString(instance.ComputeHash(bytes, 0, bytesCount));
        }
    }
}
