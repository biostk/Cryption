using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtils
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa;

        // 构造函数，初始化RSACryptoServiceProvider实例
        public RSAEncryption()
        {
            rsa = new RSACryptoServiceProvider();
        }

        // 生成指定大小的RSA密钥对
        public void GenerateKeys(int keySize)
        {
            rsa.KeySize = keySize;
        }

        // 导出公钥为PEM格式字符串
        public string ExportPublicKeyToPEM()
        {
            var parameters = rsa.ExportParameters(false);
            return ExportParametersToPEM(parameters, false);
        }

        // 导出私钥为PEM格式字符串
        public string ExportPrivateKeyToPEM()
        {
            var parameters = rsa.ExportParameters(true);
            return ExportParametersToPEM(parameters, true);
        }

        // 从PEM格式字符串导入公钥
        public void ImportPublicKeyFromPEM(string pem)
        {
            var parameters = ImportParametersFromPEM(pem);
            rsa.ImportParameters(parameters);
        }

        // 从PEM格式字符串导入私钥
        public void ImportPrivateKeyFromPEM(string pem)
        {
            var parameters = ImportParametersFromPEM(pem, true);
            rsa.ImportParameters(parameters);
        }

        // 加密字节数组
        public byte[] Encrypt(byte[] data)
        {
            return rsa.Encrypt(data, false);
        }

        // 解密字节数组
        public byte[] Decrypt(byte[] data)
        {
            return rsa.Decrypt(data, false);
        }

        // 加密字符串
        public string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = Encrypt(dataBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        // 解密字符串
        public string Decrypt(string data)
        {
            byte[] dataBytes = Convert.FromBase64String(data);
            byte[] decryptedBytes = Decrypt(dataBytes);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        // 设置公钥的模数和指数
        public void SetPublicKeyFromModulusAndExponent(byte[] modulus, byte[] exponent)
        {
            var parameters = new RSAParameters
            {
                Modulus = modulus,
                Exponent = exponent
            };
            rsa.ImportParameters(parameters);
        }

        // 将十六进制表示的模数转换为字节数组
        public byte[] HexModulusToByteArray(string hexModulus)
        {
            if (hexModulus.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have an even length.");
            }

            byte[] bytes = new byte[hexModulus.Length / 2];
            for (int i = 0; i < hexModulus.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexModulus.Substring(i, 2), 16);
            }
            return bytes;
        }

        // 将字节数组的模数转换为十六进制表示的字符串
        public string HexModulusToString(byte[] modulus)
        {
            StringBuilder hex = new StringBuilder(modulus.Length * 2);
            foreach (byte b in modulus)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        // 将公钥的模数导出为十六进制字符串
        public string ExportPublicKeyModulusToHex()
        {
            var parameters = rsa.ExportParameters(false);
            return HexModulusToString(parameters.Modulus);
        }

        // 内部方法：将RSA参数导出为PEM格式字符串
        private string ExportParametersToPEM(RSAParameters parameters, bool includePrivateKey)
        {
            StringBuilder sb = new StringBuilder();
            using (var sw = new System.IO.StringWriter(sb))
            {
                var pemWriter = new PemWriter(sw);
                pemWriter.WriteObject(parameters, includePrivateKey);
            }
            return sb.ToString();
        }

        // 内部方法：从PEM格式字符串导入RSA参数
        private RSAParameters ImportParametersFromPEM(string pem, bool includePrivateKey = false)
        {
            using (var sr = new System.IO.StringReader(pem))
            {
                var pemReader = new PemReader(sr);
                return pemReader.ReadObject(includePrivateKey);
            }
        }
    }

    public class PemWriter
    {
        private readonly System.IO.TextWriter _writer;

        // 构造函数，初始化文本写入器
        public PemWriter(System.IO.TextWriter writer)
        {
            _writer = writer;
        }

        // 将RSA参数写入PEM格式
        public void WriteObject(RSAParameters parameters, bool includePrivateKey)
        {
            var base64 = Convert.ToBase64String(includePrivateKey ? ToAsn1PrivateKey(parameters) : ToAsn1PublicKey(parameters));
            _writer.WriteLine(includePrivateKey ? "-----BEGIN PRIVATE KEY-----" : "-----BEGIN PUBLIC KEY-----");
            for (int i = 0; i < base64.Length; i += 64)
            {
                _writer.WriteLine(base64.Substring(i, Math.Min(64, base64.Length - i)));
            }
            _writer.WriteLine(includePrivateKey ? "-----END PRIVATE KEY-----" : "-----END PUBLIC KEY-----");
        }

        // 将公钥参数转换为ASN.1格式字节数组
        private byte[] ToAsn1PublicKey(RSAParameters parameters)
        {
            using (var stream = new System.IO.MemoryStream())
            using (var writer = new System.IO.BinaryWriter(stream))
            {
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new System.IO.MemoryStream())
                using (var innerWriter = new System.IO.BinaryWriter(innerStream))
                {
                    EncodeLength(innerWriter, parameters.Modulus.Length + parameters.Exponent.Length + 6);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, parameters.Modulus.Length + parameters.Exponent.Length + 2);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.Modulus.Length);
                    innerWriter.Write(parameters.Modulus);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.Exponent.Length);
                    innerWriter.Write(parameters.Exponent);
                    var innerBytes = innerStream.ToArray();
                    EncodeLength(writer, innerBytes.Length);
                    writer.Write(innerBytes);
                }
                return stream.ToArray();
            }
        }

        // 将私钥参数转换为ASN.1格式字节数组
        private byte[] ToAsn1PrivateKey(RSAParameters parameters)
        {
            using (var stream = new System.IO.MemoryStream())
            using (var writer = new System.IO.BinaryWriter(stream))
            {
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new System.IO.MemoryStream())
                using (var innerWriter = new System.IO.BinaryWriter(innerStream))
                {
                    EncodeLength(innerWriter, parameters.Modulus.Length + parameters.Exponent.Length + parameters.D.Length + parameters.P.Length + parameters.Q.Length + parameters.DP.Length + parameters.DQ.Length + parameters.InverseQ.Length + 17);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, 1);
                    innerWriter.Write((byte)0x00);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.Modulus.Length);
                    innerWriter.Write(parameters.Modulus);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.Exponent.Length);
                    innerWriter.Write(parameters.Exponent);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.D.Length);
                    innerWriter.Write(parameters.D);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.P.Length);
                    innerWriter.Write(parameters.P);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.Q.Length);
                    innerWriter.Write(parameters.Q);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.DP.Length);
                    innerWriter.Write(parameters.DP);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.DQ.Length);
                    innerWriter.Write(parameters.DQ);
                    innerWriter.Write((byte)0x02); // INTEGER
                    EncodeLength(innerWriter, parameters.InverseQ.Length);
                    innerWriter.Write(parameters.InverseQ);
                    var innerBytes = innerStream.ToArray();
                    EncodeLength(writer, innerBytes.Length);
                    writer.Write(innerBytes);
                }
                return stream.ToArray();
            }
        }

        // 编码长度
        private void EncodeLength(System.IO.BinaryWriter writer, int length)
        {
            if (length < 0x80)
            {
                writer.Write((byte)length);
            }
            else if (length < 0x100)
            {
                writer.Write((byte)0x81);
                writer.Write((byte)length);
            }
            else if (length < 0x10000)
            {
                writer.Write((byte)0x82);
                writer.Write((byte)(length >> 8));
                writer.Write((byte)(length));
            }
            else
            {
                throw new InvalidOperationException("Length too long to encode");
            }
        }
    }

    public class PemReader
    {
        private readonly System.IO.TextReader _reader;

        // 构造函数，初始化文本读取器
        public PemReader(System.IO.TextReader reader)
        {
            _reader = reader;
        }

        // 读取PEM格式并返回RSA参数
        public RSAParameters ReadObject(bool includePrivateKey)
        {
            string pem = _reader.ReadToEnd();
            string base64 = pem
                .Replace("-----BEGIN PUBLIC KEY-----", "")
                .Replace("-----END PUBLIC KEY-----", "")
                .Replace("-----BEGIN PRIVATE KEY-----", "")
                .Replace("-----END PRIVATE KEY-----", "")
                .Replace("\n", "")
                .Replace("\r", "");
            byte[] bytes = Convert.FromBase64String(base64);
            return includePrivateKey ? FromAsn1PrivateKey(bytes) : FromAsn1PublicKey(bytes);
        }

        // 从ASN.1格式字节数组中提取公钥参数
        private RSAParameters FromAsn1PublicKey(byte[] bytes)
        {
            using (var stream = new System.IO.MemoryStream(bytes))
            using (var reader = new System.IO.BinaryReader(stream))
            {
                reader.ReadByte(); // SEQUENCE
                ReadLength(reader);
                reader.ReadByte(); // SEQUENCE
                ReadLength(reader);
                reader.ReadByte(); // INTEGER
                var modulus = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var exponent = reader.ReadBytes(ReadLength(reader));
                return new RSAParameters { Modulus = modulus, Exponent = exponent };
            }
        }

        // 从ASN.1格式字节数组中提取私钥参数
        private RSAParameters FromAsn1PrivateKey(byte[] bytes)
        {
            using (var stream = new System.IO.MemoryStream(bytes))
            using (var reader = new System.IO.BinaryReader(stream))
            {
                reader.ReadByte(); // SEQUENCE
                ReadLength(reader);
                reader.ReadByte(); // INTEGER
                ReadLength(reader); // Skip version
                reader.ReadByte(); // INTEGER
                var modulus = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var exponent = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var d = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var p = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var q = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var dp = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var dq = reader.ReadBytes(ReadLength(reader));
                reader.ReadByte(); // INTEGER
                var inverseQ = reader.ReadBytes(ReadLength(reader));
                return new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent,
                    D = d,
                    P = p,
                    Q = q,
                    DP = dp,
                    DQ = dq,
                    InverseQ = inverseQ
                };
            }
        }

        // 读取长度
        private int ReadLength(System.IO.BinaryReader reader)
        {
            int length = reader.ReadByte();
            if (length == 0x81)
            {
                length = reader.ReadByte();
            }
            else if (length == 0x82)
            {
                length = (reader.ReadByte() << 8) | reader.ReadByte();
            }
            return length;
        }
    }


}
