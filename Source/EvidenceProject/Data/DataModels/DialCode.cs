﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace EvidenceProject.Data.DataModels
{
    public class DialCode
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? name { get; set; }

        [NotMapped]
        public Color? color { get; set; }

        public string? description { get; set; }

        [Required]
        public virtual DialInfo? dialInfo { get; set; }
    }
}
