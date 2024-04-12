using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.Models
{
    public class MenuItem
    {   
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double Price { get; set; }
        [Display(Name="Food Type")]
        public int FoodtypeId { get; set; }
        [ForeignKey("FoodtypeId")]
        public Foodtype Foodtype { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
