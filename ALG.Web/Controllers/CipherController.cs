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
    public ActionResult AESEncrypt(CipherViewModel model)
    {
        if (ModelState.IsValid){
            byte[] inputBytes = Encoding.UTF8.GetBytes(model.InputText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(model.EncryptionKey);

            using (Aes aesAlg = Aes.Create()){
                //set same key for encryption and decryption
                aesAlg.Key = keyBytes;
                aesAlg.IV = aesAlg.Key;

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor()){
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    model.EncryptedText = Convert.ToBase64String(encryptedBytes);
                }//set encryptor
                
                using (ICryptoTransform  decryptor = aesAlg.CreateDecryptor()){
                    byte[] encryptedBytes = Convert.FromBase64String(model.EncryptedText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                    return View("_Sym", model);
                }//set decryptor
            }//AES spawn
        }//model state checks
        return View("_Sym", model);
    }//AES encrypt and decrypt



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


    
        
    
        
}