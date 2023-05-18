using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Service {

    public int Id {get; set;}

    [Display (Name = "Titel:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Title {get; set;}

    [Display (Name = "Om tjänsten:")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Description {get; set;}

    [Display (Name = "Pris:")]
    public string? Price {get; set;}


    [Display (Name = "Bild:")]
    public string? ImageName {get; set;}


    [Display (Name = "Alt-Text till bild:")]
     [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? AltText{get; set;}


    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Bild")]
    public IFormFile? ImageFile {get; set;}
}

}