﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(5)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [Required]
        public string Degree { get; set; }
    }
}
