using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab2FIFA.Models
{
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Grupo")]
        public string Group { get; set; }

        public static Comparison<Pais> CompareByName = delegate (Pais p1, Pais p2)
        {
            return p1.Name.CompareTo(p2.Name);
        };

        public static Comparison<Pais> CompareByGroup = delegate (Pais p1, Pais p2)
        {
            return p1.Group.CompareTo(p2.Group);
        };        
    }
}