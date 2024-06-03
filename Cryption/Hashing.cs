using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtils
{
    public class Hashing
    {
        // 计算 MD5 哈希：字符串重载
        public string ComputeMD5(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return ComputeMD5(dataBytes);
        }

        // 计算 MD5 哈希：字节数组重载
        public string ComputeMD5(byte[] data)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(data);
                return ConvertToHexString(hashBytes);
            }
        }

        // 计算 SHA-1 哈希：字符串重载
        public string ComputeSHA1(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return ComputeSHA1(dataBytes);
        }

        // 计算 SHA-1 哈希：字节数组重载
        public string ComputeSHA1(byte[] data)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(data);
                return ConvertToHexString(hashBytes);
            }
        }

        // 计算 SHA-256 哈希：字符串重载
        public string ComputeSHA256(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return ComputeSHA256(dataBytes);
        }

        // 计算 SHA-256 哈希：字节数组重载
        public string ComputeSHA256(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(data);
                return ConvertToHexString(hashBytes);
            }
        }

        // 计算 SHA-384 哈希：字符串重载
        public string ComputeSHA384(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return ComputeSHA384(dataBytes);
        }

        // 计算 SHA-384 哈希：字节数组重载
        public string ComputeSHA384(byte[] data)
        {
            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] hashBytes = sha384.ComputeHash(data);
                return ConvertToHexString(hashBytes);
            }
        }

        // 计算 SHA-512 哈希：字符串重载
        public string ComputeSHA512(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return ComputeSHA512(dataBytes);
        }

        // 计算 SHA-512 哈希：字节数组重载
        public string ComputeSHA512(byte[] data)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(data);
                return ConvertToHexString(hashBytes);
            }
        }

        // 私有方法：将字节数组转换为十六进制字符串
        private string ConvertToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}