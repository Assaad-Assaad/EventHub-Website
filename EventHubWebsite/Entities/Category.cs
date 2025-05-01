using System.ComponentModel.DataAnnotations;

namespace EventHubWebsite.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Event Categories")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Display(Name = "Events")]
        public List<Event> Events { get; set; } = new List<Event>();

    }
}
