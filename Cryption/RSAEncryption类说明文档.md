# Cryption Namespace

该命名空间包含用于 RSA 加密和解密的类，包括密钥对的生成、导出和导入。

## RSAEncryption 类

`RSAEncryption` 类提供了 RSA 加密和解密功能，密钥对的生成、导出和导入。

### 构造函数

- `RSAEncryption()`
  - 初始化 `RSACryptoServiceProvider` 实例。

### 公共方法

- `void GenerateKeys(int keySize)`
  - 生成指定大小的 RSA 密钥对。
  - 参数：
    - `keySize`：密钥的大小，以位为单位。常见的密钥大小包括 1024、2048、4096 等。

- `string ExportPublicKeyToPEM()`
  - 导出公钥为 PEM 格式字符串。
  - 返回值：
    - `PEM 格式的公钥字符串。`

- `string ExportPrivateKeyToPEM()`
  - 导出私钥为 PEM 格式字符串。
  - 返回值：
    - `PEM 格式的私钥字符串。`

- `void ImportPublicKeyFromPEM(string pem)`
  - 从 PEM 格式字符串导入公钥。
  - 参数：
    - `pem`：PEM 格式的公钥字符串。

- `void ImportPrivateKeyFromPEM(string pem)`
  - 从 PEM 格式字符串导入私钥。
  - 参数：
    - `pem`：PEM 格式的私钥字符串。

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

- `void SetPublicKeyFromModulusAndExponent(byte[] modulus, byte[] exponent)`
  - 设置公钥的模数和指数。
  - 参数：
    - `modulus`：公钥的模数。
    - `exponent`：公钥的指数。

- `byte[] HexModulusToByteArray(string hexModulus)`
  - 将十六进制表示的模数转换为字节数组。
  - 参数：
    - `hexModulus`：十六进制表示的模数字符串。
  - 返回值：
    - `模数的字节数组。`

- `string HexModulusToString(byte[] modulus)`
  - 将字节数组的模数转换为十六进制表示的字符串。
  - 参数：
    - `modulus`：模数的字节数组。
  - 返回值：
    - `十六进制表示的模数字符串。`

- `string ExportPublicKeyModulusToHex()`
  - 将公钥的模数导出为十六进制字符串。
  - 返回值：
    - `十六进制表示的公钥模数字符串。`

### 私有方法

- `string ExportParametersToPEM(RSAParameters parameters, bool includePrivateKey)`
  - 将 RSA 参数导出为 PEM 格式字符串。
  - 参数：
    - `parameters`：要导出的 RSA 参数。
    - `includePrivateKey`：是否包括私钥。如果为 `true`，则导出私钥；否则，仅导出公钥。
  - 返回值：
    - `PEM 格式的 RSA 参数字符串。`

- `RSAParameters ImportParametersFromPEM(string pem, bool includePrivateKey = false)`
  - 从 PEM 格式字符串导入 RSA 参数。
  - 参数：
    - `pem`：PEM 格式的 RSA 参数字符串。
    - `includePrivateKey`（可选）：是否包括私钥。如果为 `true`，则导入私钥；否则，仅导入公钥。
  - 返回值：
    - `导入的 RSA 参数。`

## PemWriter 类

`PemWriter` 类用于将 RSA 参数写入 PEM 格式。

### 构造函数

- `PemWriter(System.IO.TextWriter writer)`
  - 初始化文本写入器。
  - 参数：
    - `writer`：用于写入 PEM 格式的文本写入器。

### 公共方法

- `void WriteObject(RSAParameters parameters, bool includePrivateKey)`
  - 将 RSA 参数写入 PEM 格式。
  - 参数：
    - `parameters`：要写入的 RSA 参数。
    - `includePrivateKey`：是否包括私钥。如果为 `true`，则写入私钥；否则，仅写入公钥。

### 私有方法

- `byte[] ToAsn1PublicKey(RSAParameters parameters)`
  - 将公钥参数转换为 ASN.1 格式字节数组。
  - 参数：
    - `parameters`：公钥的 RSA 参数。
  - 返回值：
    - `ASN.1 格式的公钥字节数组。`

- `byte[] ToAsn1PrivateKey(RSAParameters parameters)`
  - 将私钥参数转换为 ASN.1 格式字节数组。
  - 参数：
    - `parameters`：私钥的 RSA 参数。
  - 返回值：
    - `ASN.1 格式的私钥字节数组。`

- `void EncodeLength(System.IO.BinaryWriter writer, int length)`
  - 编码长度。
  - 参数：
    - `writer`：用于写入编码长度的二进制写入器。
    - `length`：要编码的长度。

## PemReader 类

`PemReader` 类用于从 PEM 格式读取 RSA 参数。

### 构造函数

- `PemReader(System.IO.TextReader reader)`
  - 初始化文本读取器。
  - 参数：
    - `reader`：用于读取 PEM 格式的文本读取器。

### 公共方法

- `RSAParameters ReadObject(bool includePrivateKey)`
  - 读取 PEM 格式并返回 RSA 参数。
  - 参数：
    - `includePrivateKey`：是否包括私钥。如果为 `true`，则读取私钥；否则，仅读取公钥。
  - 返回值：
    - `读取的 RSA 参数。`

### 私有方法

- `RSAParameters FromAsn1PublicKey(byte[] bytes)`
  - 从 ASN.1 格式字节数组中提取公钥参数。
  - 参数：
    - `bytes`：ASN.1 格式的公钥字节数组。
  - 返回值：
    - `提取的公钥参数。`

- `RSAParameters FromAsn1PrivateKey(byte[] bytes)`
  - 从 ASN.1 格式字节数组中提取私钥参数。
  - 参数：
    - `bytes`：ASN.1 格式的私钥字节数组。
  - 返回值：
    - `提取的私钥参数。`

- `int ReadLength(System.IO.BinaryReader reader)`
  - 读取长度。
  - 参数：
    - `reader`：用于读取长度的二进制读取器。
  - 返回值：
    - `读取的长度。`

## 示例

以下是如何使用 `RSAEncryption` 类进行 RSA 加密和解密的示例代码：

```csharp
using Cryption;

class Program
{
    static void Main()
    {
        RSAEncryption rsaEncryption = new RSAEncryption();
        rsaEncryption.GenerateKeys(2048);

        // 导出和打印公钥和私钥
        string publicKey = rsaEncryption.ExportPublicKeyToPEM();
        string privateKey = rsaEncryption.ExportPrivateKeyToPEM();
        Console.WriteLine("Public Key:\n" + publicKey);
        Console.WriteLine("Private Key:\n" + privateKey);

        // 加密和解密示例
        string originalText = "Hello, RSA!";
        string encryptedText = rsaEncryption.Encrypt(originalText);
        string decryptedText = rsaEncryption.Decrypt(encryptedText);
        Console.WriteLine("Original Text: " + originalText);
        Console.WriteLine("Encrypted Text: " + encryptedText);
        Console.WriteLine("Decrypted Text: " + decryptedText);

        // 示例：将公钥模数转换为十六进制字符串
        string publicKeyModulusHex = rsaEncryption.ExportPublicKeyModulusToHex();
        Console.WriteLine("Public Key Modulus in Hex: " + publicKeyModulusHex);
    }
}