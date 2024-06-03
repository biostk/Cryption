# Cryption Namespace

该命名空间包含用于 RSA 加密和解密的类，包括密钥对的生成、导出和导入。

## AESEncryption 类

`AESEncryption` 类提供了 AES加密和解密功能，密钥的生成。

### 构造函数

- ```
  AESEncryption()
  ```

  - 初始化一个 `Aes` 实例。

### 公共方法

- `void SetKey(byte[] key)`
  - 设置 AES 的密钥。
  - 参数：
    - `key`：AES 密钥，字节数组。
- `void SetIV(byte[] iv)`
  - 设置 AES 的初始化向量 (IV)。
  - 参数：
    - `iv`：初始化向量，字节数组。
- `void SetMode(CipherMode mode)`
  - 设置 AES 的加密模式。
  - 参数：
    - `mode`：加密模式，例如 `CipherMode.CBC`、`CipherMode.ECB` 等。
- `void SetPadding(PaddingMode padding)`
  - 设置 AES 的填充模式。
  - 参数：
    - `padding`：填充模式，例如 `PaddingMode.PKCS7`、`PaddingMode.None` 等。
- `byte[] Encrypt(byte[] data)`
  - 加密字节数组。
  - 参数：
    - `data`：待加密的字节数组。
  - 返回值：
    - `加密后的字节数组。`
- `byte[] Decrypt(byte[] data)`
  - 解密字节数组。
  - 参数：
    - `data`：待解密的字节数组。
  - 返回值：
    - `解密后的字节数组。`
- `string Encrypt(string data)`
  - 加密字符串。
  - 参数：
    - `data`：待加密的字符串。
  - 返回值：
    - `加密后的字符串，以 Base64 编码表示。`
- `string Decrypt(string data)`
  - 解密字符串。
  - 参数：
    - `data`：待解密的字符串，以 Base64 编码表示。
  - 返回值：
    - `解密后的字符串。`
- `static byte[] GenerateRandomKey(int size)`
  - 生成随机密钥。
  - 参数：
    - `size`：密钥的大小，以字节为单位。
  - 返回值：
    - `随机生成的密钥字节数组。`
- `static byte[] GenerateRandomIV(int size)`
  - 生成随机初始化向量 (IV)。
  - 参数：
    - `size`：IV 的大小，以字节为单位。
  - 返回值：
    - `随机生成的 IV 字节数组。`

### 私有方法

- ```
  byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
  ```

  - 执行加密或解密操作。
  - 参数：
    - `data`：待处理的字节数组。
    - `cryptoTransform`：加密或解密转换器。
  - 返回值：
    - `处理后的字节数组。`

## 示例

以下是如何使用 `AESEncryption` 类进行 AES 加密和解密的示例代码：

```
csharp复制using System;
using System.Security.Cryptography;
using Cryption;

class Program
{
    static void Main()
    {
        AESEncryption aesEncryption = new AESEncryption();
        
        // 设置密钥和初始化向量 (IV)
        byte[] key = AESEncryption.GenerateRandomKey(32); // 例如，256 位密钥
        byte[] iv = AESEncryption.GenerateRandomIV(16);   // 例如，128 位 IV

        aesEncryption.SetKey(key);
        aesEncryption.SetIV(iv);

        // 设置加密模式和填充模式
        aesEncryption.SetMode(CipherMode.CBC);
        aesEncryption.SetPadding(PaddingMode.PKCS7);

        // 加密和解密示例
        string originalText = "Hello, AES!";
        string encryptedText = aesEncryption.Encrypt(originalText);
        string decryptedText = aesEncryption.Decrypt(encryptedText);

        Console.WriteLine("Original Text: " + originalText);
        Console.WriteLine("Encrypted Text: " + encryptedText);
        Console.WriteLine("Decrypted Text: " + decryptedText);
    }
}
```