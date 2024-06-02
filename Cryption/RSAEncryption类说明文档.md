# RSAEncryption 类说明

## 类概述
`RSAEncryption`类封装了RSA加密的常用功能，包括生成密钥对、导出和导入公钥和私钥、数据加密和解密。此类还支持将密钥导出为PEM格式，并从PEM格式导入密钥。

## 构造函数

### RSAEncryption()
初始化`RSAEncryption`类的新实例。

```csharp
public RSAEncryption()
```

## 方法

### GenerateKeys

生成指定大小的RSA密钥对。

```
csharp复制public void GenerateKeys(int keySize)
```

#### 参数

- `keySize` (`int`): 密钥的大小，以位为单位（例如：2048）。

### ExportPublicKeyToPEM

导出公钥为PEM格式字符串。

```
csharp复制public string ExportPublicKeyToPEM()
```

#### 返回值

- `string`: PEM格式的公钥字符串。

### ExportPrivateKeyToPEM

导出私钥为PEM格式字符串。

```
csharp复制public string ExportPrivateKeyToPEM()
```

#### 返回值

- `string`: PEM格式的私钥字符串。

### ImportPublicKeyFromPEM

从PEM格式字符串导入公钥。

```
csharp复制public void ImportPublicKeyFromPEM(string pem)
```

#### 参数

- `pem` (`string`): PEM格式的公钥字符串。

### ImportPrivateKeyFromPEM

从PEM格式字符串导入私钥。

```
csharp复制public void ImportPrivateKeyFromPEM(string pem)
```

#### 参数

- `pem` (`string`): PEM格式的私钥字符串。

### Encrypt

使用RSA加密数据。

```
csharp复制public byte[] Encrypt(byte[] data)
```

#### 参数

- `data` (`byte[]`): 要加密的字节数组。

#### 返回值

- `byte[]`: 加密后的字节数组。

### Decrypt

使用RSA解密数据。

```
csharp复制public byte[] Decrypt(byte[] data)
```

#### 参数

- `data` (`byte[]`): 要解密的字节数组。

#### 返回值

- `byte[]`: 解密后的字节数组。

### Encrypt (重载)

使用RSA加密字符串数据。

```
csharp复制public string Encrypt(string data)
```

#### 参数

- `data` (`string`): 要加密的字符串。

#### 返回值

- `string`: 加密后的字符串（Base64编码）。

### Decrypt (重载)

使用RSA解密字符串数据。

```
csharp复制public string Decrypt(string data)
```

#### 参数

- `data` (`string`): 要解密的字符串（Base64编码）。

#### 返回值

- `string`: 解密后的字符串。

## 内部方法

这些方法用于内部处理RSA参数的PEM格式转换。

### ExportParametersToPEM

将RSA参数导出为PEM格式字符串。

```
csharp复制private string ExportParametersToPEM(RSAParameters parameters, bool includePrivateKey)
```

#### 参数

- `parameters` (`RSAParameters`): 要导出的RSA参数。
- `includePrivateKey` (`bool`): 是否包含私钥。

#### 返回值

- `string`: PEM格式的RSA参数字符串。

### ImportParametersFromPEM

从PEM格式字符串导入RSA参数。

```
csharp复制private RSAParameters ImportParametersFromPEM(string pem, bool includePrivateKey = false)
```

#### 参数

- `pem` (`string`): PEM格式的RSA参数字符串。
- `includePrivateKey` (`bool`, 可选): 是否包含私钥，默认为`false`。

#### 返回值

- `RSAParameters`: 导入的RSA参数。

------

# PemWriter 类说明

## 类概述

`PemWriter`类用于将RSA参数对象导出为PEM格式。

## 构造函数

### PemWriter

初始化`PemWriter`类的新实例。

```
csharp复制public PemWriter(System.IO.TextWriter writer)
```

#### 参数

- `writer` (`System.IO.TextWriter`): 用于写入PEM格式字符串的文本写入器。

## 方法

### WriteObject

将RSA参数对象导出为PEM格式。

```
csharp复制public void WriteObject(RSAParameters parameters, bool includePrivateKey)
```

#### 参数

- `parameters` (`RSAParameters`): 要导出的RSA参数。
- `includePrivateKey` (`bool`): 是否包含私钥。

------

# PemReader 类说明

## 类概述

`PemReader`类用于从PEM格式字符串导入RSA参数对象。

## 构造函数

### PemReader

初始化`PemReader`类的新实例。

```
csharp复制public PemReader(System.IO.TextReader reader)
```

#### 参数

- `reader` (`System.IO.TextReader`): 用于读取PEM格式字符串的文本读取器。

## 方法

### ReadObject

从PEM格式字符串导入RSA参数对象。

```
csharp复制public RSAParameters ReadObject(bool includePrivateKey)
```

#### 参数

- `includePrivateKey` (`bool`): 是否包含私钥。

#### 返回值

- `RSAParameters`: 导入的RSA参数对象。

------

# RSAParametersExtensions 类说明

## 类概述

`RSAParametersExtensions`类包含扩展方法，用于将`RSAParameters`对象转换为ASN.1编码的二进制格式，以及从ASN.1编码的二进制格式解码为`RSAParameters`对象。

## 方法

### ToAsn1PublicKey

将`RSAParameters`对象转换为ASN.1编码的公钥格式。

```
csharp复制public static byte[] ToAsn1PublicKey(this RSAParameters parameters)
```

#### 参数

- `parameters` (`RSAParameters`): 要转换的RSA参数。

#### 返回值

- `byte[]`: ASN.1编码的公钥格式字节数组。

### FromAsn1PublicKey

从ASN.1编码的公钥格式解码为`RSAParameters`对象。

```
csharp复制public static RSAParameters FromAsn1PublicKey(byte[] bytes)
```

#### 参数

- `bytes` (`byte[]`): ASN.1编码的公钥格式字节数组。

#### 返回值

- `RSAParameters`: 解码后的RSA参数。

### ToAsn1PrivateKey

将`RSAParameters`对象转换为ASN.1编码的私钥格式。

```
csharp复制public static byte[] ToAsn1PrivateKey(this RSAParameters parameters)
```

#### 参数

- `parameters` (`RSAParameters`): 要转换的RSA参数。

#### 返回值

- `byte[]`: ASN.1编码的私钥格式字节数组。

### FromAsn1PrivateKey

从ASN.1编码的私钥格式解码为`RSAParameters`对象。

```
csharp复制public static RSAParameters FromAsn1PrivateKey(byte[] bytes)
```

#### 参数

- `bytes` (`byte[]`): ASN.1编码的私钥格式字节数组。

#### 返回值

- `RSAParameters`: 解码后的RSA参数。

### EncodeLength

编码长度信息。

```
csharp复制private static void EncodeLength(System.IO.BinaryWriter writer, int length)
```

#### 参数

- `writer` (`System.IO.BinaryWriter`): 用于写入数据的二进制写入器。
- `length` (`int`): 要编码的长度。

### ReadLength

读取长度信息。

```
csharp复制private static int ReadLength(System.IO.BinaryReader reader)
```

#### 参数

- `reader` (`System.IO.BinaryReader`): 用于读取数据的二进制读取器。

#### 返回值

- `int`: 读取的长度信息。

### SetPublicKeyFromModulusAndExponent 方法说明

```
markdown复制### SetPublicKeyFromModulusAndExponent
设置公钥的模数和指数。

```csharp
public void SetPublicKeyFromModulusAndExponent(byte[] modulus, byte[] exponent)
```

#### 参数

- `modulus` (`byte[]`): 公钥的模数。
- `exponent` (`byte[]`): 公钥的指数。

```
复制
```