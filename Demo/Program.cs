using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryption;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {

        }

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