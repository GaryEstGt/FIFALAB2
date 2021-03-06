﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2FIFA
{
    public class Data<T>
    {
        private static Data<T> Instance;
        public static Data<T> instance
        {

            get
            {
                if (Instance == null)
                {
                    Instance = new Data<T>();
                }
                return Instance;
            }
            set { Instance = value; }
        }

        public Biblioteca.ArbolBB<T> Arbol = new Biblioteca.ArbolBB<T>();
        public int tipoDato;
        public int orden = 0;
        public List<T> lista = new List<T>();        
        
    }
}