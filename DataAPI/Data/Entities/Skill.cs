﻿using System.ComponentModel.DataAnnotations;

namespace DataAPI.Data.Entities;

public class Skill
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}