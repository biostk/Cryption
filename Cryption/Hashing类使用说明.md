# Hashing 类

`Hashing` 类提供了一组方法，用于计算不同哈希算法（如 MD5、SHA-1、SHA-256、SHA-384 和 SHA-512）的哈希值。这个类支持对字符串和字节数组进行哈希计算。

## 构造函数

```
csharp复制public Hashing()
```

- 初始化一个 `Hashing` 类的实例。

## 公共方法

### ComputeMD5

```
csharp复制public string ComputeMD5(string data)
```

- 计算给定字符串的 MD5 哈希值。
- 参数：
  - `data`：类型为 `string`，要计算哈希值的字符串。
- 返回值：
  - `string`，MD5 哈希值的十六进制字符串形式。

```
csharp复制public string ComputeMD5(byte[] data)
```

- 计算给定字节数组的 MD5 哈希值。
- 参数：
  - `data`：类型为 `byte[]`，要计算哈希值的字节数组。
- 返回值：
  - `string`，MD5 哈希值的十六进制字符串形式。

### ComputeSHA1

```
csharp复制public string ComputeSHA1(string data)
```

- 计算给定字符串的 SHA-1 哈希值。
- 参数：
  - `data`：类型为 `string`，要计算哈希值的字符串。
- 返回值：
  - `string`，SHA-1 哈希值的十六进制字符串形式。

```
csharp复制public string ComputeSHA1(byte[] data)
```

- 计算给定字节数组的 SHA-1 哈希值。
- 参数：
  - `data`：类型为 `byte[]`，要计算哈希值的字节数组。
- 返回值：
  - `string`，SHA-1 哈希值的十六进制字符串形式。

### ComputeSHA256

```
csharp复制public string ComputeSHA256(string data)
```

- 计算给定字符串的 SHA-256 哈希值。
- 参数：
  - `data`：类型为 `string`，要计算哈希值的字符串。
- 返回值：
  - `string`，SHA-256 哈希值的十六进制字符串形式。

```
csharp复制public string ComputeSHA256(byte[] data)
```

- 计算给定字节数组的 SHA-256 哈希值。
- 参数：
  - `data`：类型为 `byte[]`，要计算哈希值的字节数组。
- 返回值：
  - `string`，SHA-256 哈希值的十六进制字符串形式。

### ComputeSHA384

```
csharp复制public string ComputeSHA384(string data)
```

- 计算给定字符串的 SHA-384 哈希值。
- 参数：
  - `data`：类型为 `string`，要计算哈希值的字符串。
- 返回值：
  - `string`，SHA-384 哈希值的十六进制字符串形式。

```
csharp复制public string ComputeSHA384(byte[] data)
```

- 计算给定字节数组的 SHA-384 哈希值。
- 参数：
  - `data`：类型为 `byte[]`，要计算哈希值的字节数组。
- 返回值：
  - `string`，SHA-384 哈希值的十六进制字符串形式。

### ComputeSHA512

```
csharp复制public string ComputeSHA512(string data)
```

- 计算给定字符串的 SHA-512 哈希值。
- 参数：
  - `data`：类型为 `string`，要计算哈希值的字符串。
- 返回值：
  - `string`，SHA-512 哈希值的十六进制字符串形式。

```
csharp复制public string ComputeSHA512(byte[] data)
```

- 计算给定字节数组的 SHA-512 哈希值。
- 参数：
  - `data`：类型为 `byte[]`，要计算哈希值的字节数组。
- 返回值：
  - `string`，SHA-512 哈希值的十六进制字符串形式。

## 私有方法

### ConvertToHexString

```
csharp复制private string ConvertToHexString(byte[] bytes)
```

- 将字节数组转换为十六进制字符串形式。
- 参数：
  - `bytes`：类型为 `byte[]`，字节数组。
- 返回值：
  - `string`，十六进制字符串。

## 使用示例

以下示例展示了如何使用 `Hashing` 类计算各种哈希值：

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