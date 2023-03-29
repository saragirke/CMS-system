using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Header {

    public int Id {get; set;}

    [Display (Name = "Titel i header:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    public string? Title {get; set;}


    [Display (Name = "Typsnitt:")]
    public string? Font {get; set;}


    [Display (Name = "Header-bild:")]
    public string? HeaderName {get; set;}


    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Header-Bild")]
    public IFormFile? HeaderFile {get; set;}

    [Display (Name = "Logotyp:")]
    public string? LogoName {get; set;}


    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Header-Bild")]
    public IFormFile? LogoFile {get; set;}
}

}