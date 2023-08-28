using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;

namespace ALG.Web.Controllers;
public class CipherController : BaseController
{
    //----------- Symmetrical View --------------------
    public ActionResult _Sym(){
        return View(new CipherViewModel());
    }//view only for symmetrical algs

    //----------- Symmetrical (Blowfish) View --------------------
    public ActionResult _Sym2(){
        return View(new CipherViewModel());
    }//view for blowfish

    [HttpPost]
    public ActionResult BlowFishEncrypt(CipherViewModel model)
    {
        if(ModelState.IsValid){
            byte[] keyBytes = Encoding.UTF8.GetBytes(model.EncryptionKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(model.InputText);

            BlowfishEngine fish = new BlowfishEngine();
            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(fish), new Pkcs7Padding());
            KeyParameter keyParam = new KeyParameter(keyBytes);

            cipher.Init(true, keyParam);
            byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            int length = cipher.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
            cipher.DoFinal(outputBytes, length);

            string encryptedText = Convert.ToBase64String(outputBytes);
            model.EncryptedText = encryptedText;
            return View("_Sym2", model);
        }
        return View("_Sym2", model);
    }

    public ActionResult BlowFishDecrypt(CipherViewModel model)
    {
        if (ModelState.IsValid){
            byte[] keyBytes = Encoding.UTF8.GetBytes(model.EncryptionKey);
            byte[] encryptedBytes = Convert.FromBase64String(model.InputText);

            BlowfishEngine fish = new BlowfishEngine();
            BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(fish), new Pkcs7Padding());
            KeyParameter keyParam = new KeyParameter(keyBytes);

            cipher.Init(false, keyParam);
            byte[] outputBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
            int length = cipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, outputBytes, 0);
            cipher.DoFinal(outputBytes, length);

            string decryptedText = Encoding.UTF8.GetString(outputBytes).TrimEnd('\0');
            model.DecryptedText = decryptedText;
            return View("_Sym2", model);
        }
        return View("_Sym2", model);
    }


    
    //----------- AES View --------------------

    [HttpPost]
    public ActionResult AESEncrypt(CipherViewModel model)
    {
        if (ModelState.IsValid){
            using (Aes aesAlg = Aes.Create()){
                aesAlg.Key = Encoding.UTF8.GetBytes(model.EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(model.EncryptionKey);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] plaintextBytes = Encoding.UTF8.GetBytes(model.InputText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                string encryptedText = Convert.ToBase64String(encryptedBytes);

                model.EncryptedText = encryptedText;
            }
            return View("_Sym", model);
        }
    return View("_Sym", model);
    }

    [HttpPost]
    public ActionResult AESDecrypt(CipherViewModel model)
    {
        if(ModelState.IsValid){
            using (Aes aesAlg = Aes.Create()){
                aesAlg.Key = Encoding.UTF8.GetBytes(model.EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(model.EncryptionKey);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = Convert.FromBase64String(model.InputText);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                model.DecryptedText = decryptedText;
            }
            return View("_Sym", model);
        }
        return View("_Sym", model);
    }

    //----------- Cipher view --------------------
    public ActionResult _Cipher(){
        return View(new CipherViewModel());
    }//view only for ciphers

    //----------- A1Z26 --------------------

    [HttpPost]
    public ActionResult A1ZCipher(CipherViewModel model)
    {
        if (!ModelState.IsValid){
            return View("_Cipher", model);
        }//if
        model.EncryptedText = A1ZEncrypt(model.InputText);
        model.DecryptedText = DecryptA1Z(model.EncryptedText);

        return View("_Cipher", model);
    }//encrypt + decrypt
    public static string A1ZEncrypt(string input){
        input = input.ToUpper();
        string encryptedText = "";

        foreach (char x in input){
            if (char.IsLetter(x)){
                int position = x -'A' +1;
                encryptedText += position + " ";
            }//if
            else{
                encryptedText += x;
            }//else
        }//foreach
        return encryptedText.Trim();
    }//encrypts for A1Z

    public static string DecryptA1Z(string encryptedText){
        string[] parts = encryptedText.Split(' ');
        string decryptedtext ="";

        foreach(string part in parts){
            if(int.TryParse(part, out int position) && position >= 1 && position <=26){
                char x = (char)('A' + position -1);
                decryptedtext += x;
            }//if
            else {
                decryptedtext += parts;
            }//else
        }//foreach
        return decryptedtext;
    }//decrypts A1Z
  
    //----------- Caesar --------------------
    [HttpPost]
    public ActionResult EncryptDecrypt(CipherViewModel model)
    {
        if (!ModelState.IsValid){
            return View("_Cipher", model);
        }//if
        model.EncryptedText = CEncipher(model.InputText, model.Shift);
        model.DecryptedText = CDecipher(model.EncryptedText, model.Shift);

        return View("_Cipher", model);
    }//encrypt + decrypt

    private static char Cipher(char ch, int key)
    {
	    if (!char.IsLetter(ch)){
		    return ch;
        }//if
	    char offset = char.IsUpper(ch) ? 'A' : 'a';
	    return (char)((((ch + key) - offset) % 26) + offset);
    }//provides cipher for caesar

    public static string CEncipher(string input, int key)
    {
        string output = string.Empty;
        foreach (char ch in input){
            output += Cipher(ch, key);
        }//foreach
        return output;
    }//enciphers caesar

    public static string CDecipher(string input, int key)
    {
        return CEncipher(input, 26 - key);
    }//deciphers caesar

    //----------- Asym view --------------------
     public ActionResult _Asym(){
        return View(new CipherViewModel());
    }//view only for asymmetrical algs
    

    [HttpPost]
    public ActionResult RSAE(CipherViewModel model){
        if (ModelState.IsValid){
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()){
                string publicKey = rsa.ToXmlString(false);

                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(model.InputText);
                byte[] encryptedDataByte = rsa.Encrypt(dataToEncrypt, false);
                string encryptedText = Convert.ToBase64String(encryptedDataByte);

                model.EncryptedText = encryptedText;
                model.EncryptionKey = publicKey;
            }
            return View("_Asym", model);
        }
    return View("_Asym", model);
    }
    
    [HttpPost]
    public ActionResult RSAD(CipherViewModel model){
        if(ModelState.IsValid){
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()){
                rsa.FromXmlString(model.EncryptionKey);

                byte[] encryptedData = Convert.FromBase64String(model.InputText);
                byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                string decryptedText = Encoding.UTF8.GetString(decryptedData);

                model.DecryptedText = decryptedText;
            }
            return View("_Asym", model);
        }
        return View("_Asym", model);
    }
    
        
}