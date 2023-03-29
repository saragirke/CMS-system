using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cmsSystem.Models {

public class Footer {

    public int Id {get; set;}

    [Display (Name = "Bakgrundsfärg på footer:")]
    public string? FooterColor {get; set;}

    [Display (Name = "Font-färg:")]
    public string? FontColor {get; set;}

    [Display (Name = "Adress:")]
    public string? FooterAdress {get; set;}

    [Display (Name = "Telefon-nummer:")]
    public string? FooterPhone {get; set;}

    [Display (Name = "Email:")]
    [EmailAddress(ErrorMessage = "Felaktig e-postadress")]
    public string? FooterEmail {get; set;}

        internal static IEnumerable<Footer> setListTempSource()
        {
            throw new NotImplementedException();
        }
    }

}