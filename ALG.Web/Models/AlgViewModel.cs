using ALG.Data.Entities;
namespace ALG.Web.Models;

public class AlgViewModel
{
    public IList<Algorithm> Algorithms {get; set;} = new List<Algorithm>();
    public string Query {get; set;} = "";
    
}