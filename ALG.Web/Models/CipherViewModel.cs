using System.ComponentModel.DataAnnotations;
public class CipherViewModel
{
    public string InputText { get; set; }
    public int Shift { get; set; }
    public string EncryptedText { get; set; }
    public string DecryptedText { get; set; }

    public string EncryptionKey { get; set; }
}