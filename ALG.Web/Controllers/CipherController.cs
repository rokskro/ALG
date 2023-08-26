using System;
using Microsoft.AspNetCore.Mvc;
using ALG.Web.Models;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ALG.Web.Controllers;
public class CipherController : BaseController
{
    //----------- Symmetrical View --------------------
    public ActionResult _Sym(){
        return View(new CipherViewModel());
    }//view only for symmetrical algs
    
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
    public ActionResult RSAEncrypt(CipherViewModel model)
    {
        if (ModelState.IsValid){
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.FromXmlString(model.EncryptionKey);

                byte[] plaintextBytes = Encoding.UTF8.GetBytes(model.InputText);
                byte[] encryptedBytes = rsa.Encrypt(plaintextBytes, false);
                string encryptedText = Convert.ToBase64String(encryptedBytes);

                model.EncryptedText = encryptedText;
            }
            return View("_Asym", model);
        }
        return View("_Asym", model);
    }//RSA encryption

    [HttpPost]
    public ActionResult Decrypt(CipherViewModel model)
    {
        if(ModelState.IsValid){
            using (var rsa = new RSACryptoServiceProvider()){
                rsa.FromXmlString(model.EncryptionKey);

                byte[] encryptedBytes = Convert.FromBase64String(model.InputText);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                model.DecryptedText = decryptedText;
            }
            return View("_Asym", model);
        }
       return View("_Asym", model);
    }
    
        
}