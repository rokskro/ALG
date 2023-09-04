using System.ComponentModel.DataAnnotations;
using ALG.Data.Entities;
public class CipherViewModel
{

    public IList<Algorithm> Algorithms {get; set;} = new List<Algorithm>();
    public string Query {get; set;} = "";
    

    public string InputText { get; set; }
    public int Shift { get; set; }
    public string EncryptedText { get; set; }
    public string DecryptedText { get; set; }

    public string EncryptionKey { get; set; }
    public string SpareEncryptionKey { get; set; }
    public string Error { get; set; }
}