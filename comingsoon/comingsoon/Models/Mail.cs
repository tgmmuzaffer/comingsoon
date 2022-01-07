using System.ComponentModel.DataAnnotations;

namespace comingsoon.Models
{
    public class Mail
    {
        [Required(ErrorMessage = "Lütfen Doldurunuz")]
        [EmailAddress(ErrorMessage = "Lütfen Geçerli Bir E-Posta Adresi Giriniz")]
        public string emailAddress { get; set; }
        [Required(ErrorMessage = "Lütfen Doldurunuz")]
        [MinLength(3, ErrorMessage = "3 Karakterden Fazla Olmalı")]
        [MaxLength(200, ErrorMessage = "200 Karakterden Fazla Olmamalı")]
        public string message { get; set; }
    }
}
