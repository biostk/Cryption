## 使用说明

### 公共方法

- `string ComputeMD5(string data)`
  - 计算给定字符串的 MD5 哈希值。
  - 参数：
    - `data`：要计算哈希值的字符串。
  - 返回值：
    - `MD5 哈希值的十六进制字符串形式。`
- `string ComputeMD5(byte[] data)`
  - 计算给定字节数组的 MD5 哈希值。
  - 参数：
    - `data`：要计算哈希值的字节数组。
  - 返回值：
    - `MD5 哈希值的十六进制字符串形式。`
- `string ComputeSHA1(string data)`
  - 计算给定字符串的 SHA-1 哈希值。
  - 参数：
    - `data`：要计算哈希值的字符串。
  - 返回值：
    - `SHA-1 哈希值的十六进制字符串形式。`
- `string ComputeSHA1(byte[] data)`
  - 计算给定字节数组的 SHA-1 哈希值。
  - 参数：
    - `data`：要计算哈希值的字节数组。
  - 返回值：
    - `SHA-1 哈希值的十六进制字符串形式。`
- `string ComputeSHA256(string data)`
  - 计算给定字符串的 SHA-256 哈希值。
  - 参数：
    - `data`：要计算哈希值的字符串。
  - 返回值：
    - `SHA-256 哈希值的十六进制字符串形式。`
- `string ComputeSHA256(byte[] data)`
  - 计算给定字节数组的 SHA-256 哈希值。
  - 参数：
    - `data`：要计算哈希值的字节数组。
  - 返回值：
    - `SHA-256 哈希值的十六进制字符串形式。`
- `string ComputeSHA384(string data)`
  - 计算给定字符串的 SHA-384 哈希值。
  - 参数：
    - `data`：要计算哈希值的字符串。
  - 返回值：
    - `SHA-384 哈希值的十六进制字符串形式。`
- `string ComputeSHA384(byte[] data)`
  - 计算给定字节数组的 SHA-384 哈希值。
  - 参数：
    - `data`：要计算哈希值的字节数组。
  - 返回值：
    - `SHA-384 哈希值的十六进制字符串形式。`
- `string ComputeSHA512(string data)`
  - 计算给定字符串的 SHA-512 哈希值。
  - 参数：
    - `data`：要计算哈希值的字符串。
  - 返回值：
    - `SHA-512 哈希值的十六进制字符串形式。`
- `string ComputeSHA512(byte[] data)`
  - 计算给定字节数组的 SHA-512 哈希值。
  - 参数：
    - `data`：要计算哈希值的字节数组。
  - 返回值：
    - `SHA-512 哈希值的十六进制字符串形式。`

### 私有方法

- ```
  string ConvertToHexString(byte[] bytes)
  ```

  - 将字节数组转换为十六进制字符串。
  - 参数：
    - `bytes`：字节数组。
  - 返回值：
    - `十六进制字符串。`

## 示例

以下是如何使用 `Hashing` 类计算各种哈希值的示例代码：

```
csharp复制using System;
using CryptoUtils;

class Program
{
    static void Main()
    {
        Hashing hashing = new Hashing();
        string data = "Hello, Hashing!";
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        string md5Hash = hashing.ComputeMD5(data);
        string md5HashBytes = hashing.ComputeMD5(dataBytes);

        string sha1Hash = hashing.ComputeSHA1(data);
        string sha1HashBytes = hashing.ComputeSHA1(dataBytes);

        string sha256Hash = hashing.ComputeSHA256(data);
        string sha256HashBytes = hashing.ComputeSHA256(dataBytes);

        string sha384Hash = hashing.ComputeSHA384(data);
        string sha384HashBytes = hashing.ComputeSHA384(dataBytes);

        string sha512Hash = hashing.ComputeSHA512(data);
        string sha512HashBytes = hashing.ComputeSHA512(dataBytes);

        Console.WriteLine("Original Data: " + data);
        Console.WriteLine("MD5 Hash (String): " + md5Hash);
        Console.WriteLine("MD5 Hash (Bytes): " + md5HashBytes);
        Console.WriteLine("SHA-1 Hash (String): " + sha1Hash);
        Console.WriteLine("SHA-1 Hash (Bytes): " + sha1HashBytes);
        Console.WriteLine("SHA-256 Hash (String): " + sha256Hash);
        Console.WriteLine("SHA-256 Hash (Bytes): " + sha256HashBytes);
        Console.WriteLine("SHA-384 Hash (String): " + sha384Hash);
        Console.WriteLine("SHA-384 Hash (Bytes): " + sha384HashBytes);
        Console.WriteLine("SHA-512 Hash (String): " + sha512Hash);
        Console.WriteLine("SHA-512 Hash (Bytes): " + sha512HashBytes);
    }
}
```