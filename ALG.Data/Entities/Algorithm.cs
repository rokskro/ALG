namespace ALG.Data.Entities;
using System.ComponentModel.DataAnnotations;
using ALG.Data.Validators;


public class Algorithm
{
   
    
    [Required]
    public int Id {get; set;}

    [Required]
    public string Name {get;set;}
    [Required]
    public string Description {get;set;}

    [Required]
    [Range(1,5)]
    public int SpeedRating {get;set;}
    [Required]
    public string Speed {get;set;}
    
    [Required]
    [Range(1,5)]
    public int EfficiencyRating {get;set;}
    [Required]
    public string Efficiency {get;set;}

    [Required]
    [Range(1,5)]
    public int SecurityRating {get;set;}
    [Required]
    public string Security {get;set;}  

    [Required]
    [Url]
    [UrlResource]
    public string DiagramUrl {get;set;} 

    public String TypeClass {get;set;}
    
}

