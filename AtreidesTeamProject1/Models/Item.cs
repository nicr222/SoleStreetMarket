using System.ComponentModel.DataAnnotations;

namespace AtreidesTeamProject1.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
