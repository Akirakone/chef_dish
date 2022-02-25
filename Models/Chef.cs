using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace chef_dish.Models
{
    public class Chef
    {
        [Key]
        public int Chefid { get; set; }

        [Required]
        [MinLength(2)]
        public string  firstname { get; set; }

        [Required]
        [MinLength(2)]
        public string lastname { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; } 

        public List<Dish> AllDishes {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}