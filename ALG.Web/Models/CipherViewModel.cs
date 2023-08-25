using System.ComponentModel.DataAnnotations;
public class CipherViewModel
{
    public string InputText { get; set; }
    public int Shift { get; set; }
    public string EncryptedText { get; set; }
    public string DecryptedText { get; set; }

    [StringLength(32, MinimumLength = 32)]
    public string EncryptionKey { get; set; }
}