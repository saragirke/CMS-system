using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Staff {

    public int Id {get; set;}

    [Display (Name = "Namn:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Name {get; set;}

    [Display (Name = "Titel:")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? Title {get; set;}

    [Required(ErrorMessage = "Obligatoriskt fält")]
    [EmailAddress(ErrorMessage = "Felaktig e-postadress")]
    [Display(Name = "Email:")]
    public string? Email { get; set; }

    [Display (Name = "Telefonnummer:")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression("([0-9]+)", ErrorMessage = "Endast siffror!")]
    public string? Number { get; set; }


    [Display (Name = "Bild:")]
    public string? ImageName {get; set;}


    [Display (Name = "Alt-Text till bild:")]
    public string? AltText{get; set;}


    [NotMapped] //När en migration görs kommer detta inte skapas i databasen, endast gränssnittet
    [Display(Name = "Bild")]
    public IFormFile? ImageFile {get; set;}
}

}