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
            RSAEncryption rsaEncryption = new RSAEncryption();
            rsaEncryption.GenerateKeys(2048);

            string publicKey = rsaEncryption.ExportPublicKeyToPEM();
            string privateKey = rsaEncryption.ExportPrivateKeyToPEM();

            Console.WriteLine("Public Key:");
            Console.WriteLine(publicKey);
            Console.WriteLine("Private Key:");
            Console.WriteLine(privateKey);

            // Encrypt and decrypt example
            string originalText = "Hello, RSA!";
            string encryptedText = rsaEncryption.Encrypt(originalText);
            string decryptedText = rsaEncryption.Decrypt(encryptedText);

            Console.WriteLine("Original Text: " + originalText);
            Console.WriteLine("Encrypted Text: " + encryptedText);
            Console.WriteLine("Decrypted Text: " + decryptedText);
            Console.ReadLine();
        }
    }
}
