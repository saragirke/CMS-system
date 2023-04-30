using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class About {

    public int Id {get; set;}

    [Display (Name = "Titel:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    public string? AboutTitle {get; set;}

    [Display (Name = "Text:")]
    public string? AboutText {get; set;}

    [Display (Name = "Bild:")]
    public string? AboutImage {get; set;}

    [Display (Name = "Alt-Text till bild:")]
    public string? AltText{get; set;}


    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Bild")]
    public IFormFile? AboutFile {get; set;}
}

}