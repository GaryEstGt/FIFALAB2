using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab2FIFA.Models
{
    public class Texto
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Texto")]
        public string texto { get; set; }

        public static Comparison<Texto> CompareByText = delegate (Texto p1, Texto p2)
        {
            return p1.texto.CompareTo(p2.texto);
        };
    }
}