using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Comment {

    public int Id {get; set;}

    [Display (Name = "Namn:")]
    [MaxLength(120, ErrorMessage = "Max 120 tecken")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? CommentName {get; set;}

    [Display (Name = "Inlägg:")]
    [Required(ErrorMessage = "Obligatoriskt fält")]
    public string? CommentPost {get; set;}

    [Display (Name = "Datum:")]
    [DataType(DataType.Date)]
    public DateOnly? DateCreated {get; init;} = DateOnly.FromDateTime(DateTime.Now); //Endast Datum

    public int? NewsId  {get; set;}
    public News? News {get; set;}

}

}