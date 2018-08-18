using System.ComponentModel.DataAnnotations;

namespace Web.Ararat.Model
{
    public class ContactModel
    {
        [Required]
        public string SendTo { get; set; }

        [Required]
        public string EmailFrom { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}