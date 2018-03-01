using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab2FIFA.Models
{
    public class Entero
    {
        [Key]
        public int Id { get; set; }
        [Display (Name = "Valor")]
        public int valor { get; set; }

        public static Comparison<Entero> CompareByValor = delegate (Entero p1, Entero p2)
        {
            return p1.valor > p2.valor ? 1: p1.valor == p2.valor ? 0 : -1;
        };
    }
}