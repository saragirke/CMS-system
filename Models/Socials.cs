using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Socials {

    public int Id {get; set;}

    [Display (Name = "Facebook:")]
    public string? Facebook {get; set;}


    [Display (Name = "LinkedIn:")]
    public int? Linkedin {get; set;}


    [Display (Name = "Instagram:")]
    public string? Instagram {get; set;}


}

}