using System.ComponentModel.DataAnnotations;

namespace EventHubWebsite.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }


        [Required]
        public string Description { get; set; }

        [Display(Name = "HTML Content")]
        public string HTMLContent { get; set; }

        [Display(Name = "Added At")]
        public DateTime Added { get; set; }

        [Display(Name = "Categories")]
        public int CategoryId { get; set; }

        public List<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

    }
}
