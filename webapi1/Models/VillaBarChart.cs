using System.ComponentModel.DataAnnotations;

namespace webapi1.Models
{
    public class VillaBarChart
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }
        
    }
}
