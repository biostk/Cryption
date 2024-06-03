using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoUtils;

namespace Demo
{
    class Program
    {
        /*static void Main(string[] args)
        {

        Console.ReadLine();
        }*/

        static void Main(string[] args)
        {
            
            Console.ReadLine();
        }

        //Hashing算法Demo
        /*static void Main(string[] args)
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
            Console.ReadLine();
        }*/

        //AES算法Demo
        /*static void Main(string[] args)
        {
            AESEncryption aesEncryption = new AESEncryption();

            // 设置密钥和初始化向量 (IV)
            byte[] key = AESEncryption.GenerateRandomKey(32); // 例如，256 位密钥
            byte[] iv = AESEncryption.GenerateRandomIV(16);   // 例如，128 位 IV

            aesEncryption.SetKey(key);
            aesEncryption.SetIV(iv);

            // 设置加密模式和填充模式
            aesEncryption.SetMode(CipherMode.ECB);
            aesEncryption.SetPadding(PaddingMode.PKCS7);

            // 加密和解密示例
            string originalText = "Hello, AES!";
            string encryptedText = aesEncryption.Encrypt(originalText);
            string decryptedText = aesEncryption.Decrypt(encryptedText);

            Console.WriteLine("Original Text: " + originalText);
            Console.WriteLine("Encrypted Text: " + encryptedText);
            Console.WriteLine("Decrypted Text: " + decryptedText);
            Console.ReadLine();
        }*/

        //RSA算法Demo
        /*static void Main(string[] args)
        {
            RSAEncryption rsaEncryption = new RSAEncryption();
            rsaEncryption.GenerateKeys(2048);

            string publicKey = rsaEncryption.ExportPublicKeyToPEM();
            string privateKey = rsaEncryption.ExportPrivateKeyToPEM();

            Console.WriteLine("Public Key:");
            Console.WriteLine(publicKey);
            Console.WriteLine("Private Key:");
            Console.WriteLine(privateKey);

            // 加密和解密示例
            string originalText = "Hello, RSA!";
            string encryptedText = rsaEncryption.Encrypt(originalText);
            string decryptedText = rsaEncryption.Decrypt(encryptedText);

            Console.WriteLine("Original Text: " + originalText);
            Console.WriteLine("Encrypted Text: " + encryptedText);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            // 设置公钥的模数和指数示例
            byte[] modulus = rsaEncryption.HexModulusToByteArray(
                "D275807D2EAD211604D5A5EC42B9EB4F9BD7AE0624A8D1CBAF577CC9656CE5BEC2C31ABC4271C1368447EBC79B62F84117CE87DD00767DF8A3F506C3693843003AC55DA4745C48C70F3045A00CDF6F44A187FFDA5527B4F65CE2519AE11E2AD2907E78AE2B5C8B4F5F0FE680D93D40148893829A9188E4B1F5A74B96DFAFD5F5"); // replace with your modulus in hex
            byte[] exponent = rsaEncryption.HexModulusToByteArray("010001"); // replace with your exponent in hex

            // 示例：将字节数组转换为十六进制字符串
            string hexModulus = rsaEncryption.HexModulusToString(modulus);
            Console.WriteLine("Hex Modulus: " + hexModulus);

            rsaEncryption.SetPublicKeyFromModulusAndExponent(modulus, exponent);

            // 导出公钥模数为PEM格式
            publicKey = rsaEncryption.ExportPublicKeyToPEM();
            Console.WriteLine("Public Key:" + publicKey);

            // 导出公钥模数为十六进制字符串
            string publicKeyModulusHex = rsaEncryption.ExportPublicKeyModulusToHex();
            Console.WriteLine("Public Key Modulus in Hex: " + publicKeyModulusHex);
            Console.ReadLine();
        }*/
    }
}