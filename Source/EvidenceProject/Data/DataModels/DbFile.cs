﻿using System.ComponentModel.DataAnnotations;

namespace EvidenceProject.Data.DataModels;

public class DbFile
{
    //Any file - https://stackoverflow.com/questions/2579373/saving-any-file-to-in-the-database-just-convert-it-to-a-byte-array
    //Image - http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
    [Key]
    public Guid id { get; set; }
    
    /// <summary> Název souboru
    /// </summary>
    [Required]
    public string fileName { get; set; }
    
    /// <summary> Data souboru
    /// </summary>
    [Required]
    public byte[] fileData { get; set; }
    
    /// <summary>
    /// Typ souboru
    /// </summary>
    [Required]
    public string fileType { get; set; }
}

