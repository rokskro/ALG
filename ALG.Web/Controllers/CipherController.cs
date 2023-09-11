using System;
using Microsoft.AspNetCore.Mvc;
using ALG.Data.Services;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Microsoft.AspNetCore.Authorization;
using ALG.Data.Entities;
using ALG.Web.Models;


namespace ALG.Web.Controllers;
public class CipherController : BaseController
{

    //----------- Database functionality  --------------------
    private IAlgorithmService svc;
    public CipherController()
    {
        svc = new AlgorithmService();
    }//controller

    public IActionResult Index(CipherViewModel search)
    {
        search.Algorithms = svc.SearchAlgorithms(search.Query);
        return View(search);
    }//get algorithims

    public IActionResult Details(int id)
    {
        var algorithim = svc.GetAlgorithm(id);
       
        if (algorithim is null) {
            Alert("algorithim not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }
        return View(algorithim);
    }//algo detail page

    public IActionResult Guide()
    {
        return View();
    }//guide page 

    public IActionResult Match()
    {
        return View();
    }//matchmaking page


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
            }//aes creation
            return View("_Sym", model);
        }//if
        return View("_Sym", model);
    }//decrypt aes post

    //----------- A1Z26 --------------------

    public ActionResult _Cipher2(){
        return View(new CipherViewModel());
    }//view 

    [HttpPost]
    public ActionResult A1ZCipherEncrypt(CipherViewModel model)
    {
        if (!ModelState.IsValid){
            return View("_Cipher", model);
        }//if
        model.EncryptedText = A1ZEncrypt(model.InputText);

        return View("_Cipher", model);
    }//encrypt + decrypt

    [HttpPost]
    public ActionResult A1ZCipherDecrypt(CipherViewModel model)
    {
        if (!ModelState.IsValid){
            return View("_Cipher", model);
        }//if
        model.DecryptedText = DecryptA1Z(model.InputText);

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
     public ActionResult _Cipher(){
        return View(new CipherViewModel());
    }//view 

    [HttpPost]
    public ActionResult Encrypt(CipherViewModel model)
    {
        if (ModelState.IsValid){
            model.EncryptedText = EncryptTextCaesar(model.InputText, model.Shift);
        }//if
        return View("_Cipher", model);
    }//encrypt post


    [HttpPost]
    public ActionResult DecryptC(CipherViewModel model)
    {
        if (ModelState.IsValid){
            model.DecryptedText = DecryptTextCaesar(model.EncryptedText, model.Shift);
        }//if
        return View("_Cipher", model);
    }//decrypt post

    private string EncryptTextCaesar(string text, int shiftKey)
    {
        StringBuilder encryptedText = new StringBuilder();
        foreach (char c in text){
            if (char.IsLetter(c)){
                char shiftedChar = (char)(((c - 'A' + shiftKey) % 26) + 'A');
                encryptedText.Append(shiftedChar);
            }//if
            else{
                encryptedText.Append(c);
            }//else
        }//for each
        return encryptedText.ToString();
    }//encrypt caesar logic

    private string DecryptTextCaesar(string text, int shiftKey)
    {
        return EncryptTextCaesar(text, 26 - shiftKey);
    }//decrypt caesar logic

    //----------- Asym view --------------------
     public ActionResult _Asym(){
        return View(new CipherViewModel());
    }//view only for asymmetrical algs

    [HttpPost]
    public ActionResult RSA(CipherViewModel model)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()){
            string message = model.InputText;
            byte[] encryptedMessage = EncryptMessage(message, rsa);
            string decryptedMessage = DecryptMessage(encryptedMessage, rsa);

            string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            model.SpareEncryptionKey = publicKey;
            model.EncryptionKey = privateKey;

            model.EncryptedText = Convert.ToBase64String(encryptedMessage);
            model.DecryptedText = decryptedMessage;
        }
        return View("_Asym", model);
    }

    static byte[] EncryptMessage(string message, RSACryptoServiceProvider rsa)
    {
        byte[] encodedMessage = Encoding.UTF8.GetBytes(message);
        byte[] encryptedMessage = rsa.Encrypt(encodedMessage, true);

        return encryptedMessage;
    }

    static string DecryptMessage(byte[] encryptedMessage, RSACryptoServiceProvider rsa)
    {
        byte[] decryptedMessage = rsa.Decrypt(encryptedMessage, true);
        string originalMessage = Encoding.UTF8.GetString(decryptedMessage);

        return originalMessage;
    }

    
    


        
}