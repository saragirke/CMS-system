using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class News {

    public int Id {get; set;}

    [Display (Name = "Titel:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Title {get; set;}

    [Display (Name = "Inlägg:")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Post {get; set;}

    [Display (Name = "Bild:")]
    public string? ImageName {get; set;}

    [Display (Name = "Alt-Text till bild:")]
    public string? AltText{get; set;}

    [Display (Name = "Datum:")]
    [DataType(DataType.Date)]
    public DateOnly? DateCreated {get; init;} = DateOnly.FromDateTime(DateTime.Now); //Endast Datum

    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Bild")]
    public IFormFile? ImageFile {get; set;}

     public Comment? Comment {get; set;}
}

}