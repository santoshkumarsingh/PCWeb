using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PCWeb.Models
{
    public class RSAUtility
    {
        public static RSAViewModel rsa2(RSAViewModel model)
        {
            try
            {
                int keySize = 1024;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);

                RSAParameters publickey = rsa.ExportParameters(false); // don't export private key 
                RSAParameters privatekey = rsa.ExportParameters(true); // export private key 
                //\b 123\b0 
                model.PublicKey = "e=" + ByteToString(publickey.Exponent) + Environment.NewLine + "n=" + ByteToString(publickey.Modulus);
                model.PrivateKey = "d=" + ByteToString(privatekey.D) + Environment.NewLine + "n=" + ByteToString(publickey.Modulus);

                rsa.ImportParameters(publickey);
                byte[] encryptedData = rsa.Encrypt(StringToByte(model.PlainText), true);

                model.CipherText=ByteToString(encryptedData);

                rsa.ImportParameters(privatekey);
                byte[] decryptedData = rsa.Decrypt(encryptedData, true);

                model.DecryptedText = ByteToAscii(decryptedData);
             
            }
            catch (CryptographicException ex)
            {
                


            }
            return model;
        }

        private static string ByteToAscii(byte[] decryptedData)
        {
            return Encoding.UTF8.GetString(decryptedData);
        }
        private static byte[] StringToByte(string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }
        private static string ByteToString(byte[] p)
        {
            return BitConverter.ToString(p).Replace("-", "");
        }
    }
}