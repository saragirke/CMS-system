using System.ComponentModel.DataAnnotations;

namespace cmsSystem.Models
{

    public class Message
    {
        public int Id { get; set; }

        [Display(Name = "Förnamn:")]
        [MaxLength(120, ErrorMessage = "Max 120 tecken")]
        [Required(ErrorMessage = "Obligatoriskt fält")]
        public string? FirstName { get; set; }

        [Display(Name = "Förnamn:")]
        [MaxLength(120, ErrorMessage = "Max 120 tecken")] 
        [Required(ErrorMessage = "Obligatoriskt fält")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Obligatoriskt")]
        [Display(Name = "Meddelande:")]
        public string? MessageText { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [EmailAddress(ErrorMessage = "Felaktig e-postadress")]
        [Display(Name = "Email:")]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; init; } = DateTime.Now;


    }
}