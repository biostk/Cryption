using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryption
{
    public class RSAEncryption
    {
        private RSACryptoServiceProvider rsa;

        public RSAEncryption()
        {
            rsa = new RSACryptoServiceProvider();
        }

        public void GenerateKeys(int keySize)
        {
            rsa.KeySize = keySize;
        }

        public string ExportPublicKeyToPEM()
        {
            var parameters = rsa.ExportParameters(false);
            return ExportParametersToPEM(parameters, false);
        }

        public string ExportPrivateKeyToPEM()
        {
            var parameters = rsa.ExportParameters(true);
            return ExportParametersToPEM(parameters, true);
        }

        public void ImportPublicKeyFromPEM(string pem)
        {
            var parameters = ImportParametersFromPEM(pem);
            rsa.ImportParameters(parameters);
        }

        public void ImportPrivateKeyFromPEM(string pem)
        {
            var parameters = ImportParametersFromPEM(pem, true);
            rsa.ImportParameters(parameters);
        }

        public byte[] Encrypt(byte[] data)
        {
            return rsa.Encrypt(data, false);
        }

        public byte[] Decrypt(byte[] data)
        {
            return rsa.Decrypt(data, false);
        }

        public string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = Encrypt(dataBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string data)
        {
            byte[] dataBytes = Convert.FromBase64String(data);
            byte[] decryptedBytes = Decrypt(dataBytes);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

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

        public PemWriter(System.IO.TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteObject(RSAParameters parameters, bool includePrivateKey)
        {
            var base64 = Convert.ToBase64String(includePrivateKey ? parameters.ToAsn1PrivateKey() : parameters.ToAsn1PublicKey());
            _writer.WriteLine(includePrivateKey ? "-----BEGIN PRIVATE KEY-----" : "-----BEGIN PUBLIC KEY-----");
            for (int i = 0; i < base64.Length; i += 64)
            {
                _writer.WriteLine(base64.Substring(i, Math.Min(64, base64.Length - i)));
            }
            _writer.WriteLine(includePrivateKey ? "-----END PRIVATE KEY-----" : "-----END PUBLIC KEY-----");
        }
    }

    public class PemReader
    {
        private readonly System.IO.TextReader _reader;

        public PemReader(System.IO.TextReader reader)
        {
            _reader = reader;
        }

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
            return includePrivateKey ? RSAParametersExtensions.FromAsn1PrivateKey(bytes) : RSAParametersExtensions.FromAsn1PublicKey(bytes);
        }
    }

    public static class RSAParametersExtensions
    {
        public static byte[] ToAsn1PublicKey(this RSAParameters parameters)
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

        public static RSAParameters FromAsn1PublicKey(byte[] bytes)
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

        public static byte[] ToAsn1PrivateKey(this RSAParameters parameters)
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

        public static RSAParameters FromAsn1PrivateKey(byte[] bytes)
        {
            using (var stream = new System.IO.MemoryStream(bytes))
            using (var reader = new System.IO.BinaryReader(stream))
            {
                reader.ReadByte(); // SEQUENCE
                ReadLength(reader);
                reader.ReadByte(); // INTEGER
                ReadLength(reader);
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
                return new RSAParameters { Modulus = modulus, Exponent = exponent, D = d, P = p, Q = q, DP = dp, DQ = dq, InverseQ = inverseQ };
            }
        }

        private static void EncodeLength(System.IO.BinaryWriter writer, int length)
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

        private static int ReadLength(System.IO.BinaryReader reader)
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
