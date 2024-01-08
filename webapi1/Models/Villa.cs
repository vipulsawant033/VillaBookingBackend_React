using System.ComponentModel.DataAnnotations;

namespace webapi1.Models
{
    public class Villa
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }
        public string? Description { get; set; }
        
        public double Price { get; set; }
        public int Sqft { get; set; }
        
        public int Occupancy { get; set; }
    }
}
