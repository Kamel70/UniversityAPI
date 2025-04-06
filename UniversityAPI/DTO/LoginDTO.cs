﻿using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
