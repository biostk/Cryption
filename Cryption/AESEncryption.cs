using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtils
{
    public class AESEncryption
    {
        private readonly Aes _aes;

        // 构造函数，初始化 AES 实例
        public AESEncryption()
        {
            _aes = Aes.Create();
        }

        // 设置密钥
        public void SetKey(byte[] key)
        {
            _aes.Key = key;
        }

        // 设置初始化向量 (IV)
        public void SetIV(byte[] iv)
        {
            _aes.IV = iv;
        }

        // 设置加密模式
        public void SetMode(CipherMode mode)
        {
            _aes.Mode = mode;
        }

        // 设置填充模式
        public void SetPadding(PaddingMode padding)
        {
            _aes.Padding = padding;
        }

        // 加密字节数组
        public byte[] Encrypt(byte[] data)
        {
            using (var encryptor = _aes.CreateEncryptor())
            {
                return PerformCryptography(data, encryptor);
            }
        }

        // 解密字节数组
        public byte[] Decrypt(byte[] data)
        {
            using (var decryptor = _aes.CreateDecryptor())
            {
                return PerformCryptography(data, decryptor);
            }
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

        // 私有方法：执行加密或解密操作
        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }

                return memoryStream.ToArray();
            }
        }

        // 生成随机密钥
        public static byte[] GenerateRandomKey(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[size];
                rng.GetBytes(key);
                return key;
            }
        }

        // 生成随机 IV
        public static byte[] GenerateRandomIV(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[size];
                rng.GetBytes(iv);
                return iv;
            }
        }
    }
}