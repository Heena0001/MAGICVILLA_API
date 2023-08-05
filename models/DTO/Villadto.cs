﻿using System.ComponentModel.DataAnnotations;

namespace MAGICVILLA_API.models.DTO
{
    public class Villadto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
    }
}