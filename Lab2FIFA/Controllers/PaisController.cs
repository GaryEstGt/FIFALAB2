using Lab2FIFA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2FIFA.Controllers
{
    public class PaisController : Controller
    {
        public ActionResult ElegirTipodeDato()
        {
            return View();
        }

        // POST: Jugador/ElegirLista
        [HttpPost]
        public ActionResult ElegirTipodeDato(string submitButton)
        {
            try
            {
                String retorno="Index";
                switch (submitButton)
                {
                    case "Texto":
                        Data<Texto>.instance.tipoDato = 2;
                        Data<Pais>.instance.tipoDato = 2;
                        Data<Entero>.instance.tipoDato = 2;
                        retorno = "IndexTexto";
                        break;
                    case "Entero":
                        Data<Texto>.instance.tipoDato = 1;
                        Data<Pais>.instance.tipoDato = 1;
                        Data<Entero>.instance.tipoDato = 1;
                        retorno = "IndexEntero";
                        break;
                    case "Pais":
                        Data<Pais>.instance.tipoDato = 0;
                        Data<Texto>.instance.tipoDato = 0;
                        Data<Entero>.instance.tipoDato = 0;
                        break;
                }
                return RedirectToAction(retorno);
            }
            catch
            {
                return View();
            }
        }
        // GET: Pais
        

        
     
        public ActionResult Index(string submitButton)
        {
            switch (submitButton)
            {
                case "PreOrden":
                    Data<Pais>.instance.orden = 1;
                    break;
                case "InOrden":
                    Data<Pais>.instance.orden = 0;
                    break;
                case "PostOrden":
                    Data<Pais>.instance.orden = 2;
                    break;
                case "Equilibrio":
                    Data<Pais>.instance.orden = 3;
                    break;
            }
            if (Data<Pais>.instance.orden == 0)
            {
                Data<Pais>.instance.lista.Clear();
                Data<Pais>.instance.Arbol.MostrarInOrden(ref Data<Pais>.instance.lista);
            }
            else if(Data<Pais>.instance.orden == 1)
            {
                Data<Pais>.instance.lista.Clear();
                Data<Pais>.instance.Arbol.MostrarPreOrden(ref Data<Pais>.instance.lista);
            }
            else if(Data<Pais>.instance.orden == 2)
            {
                Data<Pais>.instance.lista.Clear();
                Data<Pais>.instance.Arbol.MostrarPostOrden(ref Data<Pais>.instance.lista);
            }
            else
            {
                Data<Pais>.instance.lista.Clear();
                if (Data<Pais>.instance.Arbol.Equilibrio() == null)
                {
                    Data<Pais>.instance.Arbol.MostrarInOrden(ref Data<Pais>.instance.lista);
                }
                else
                {
                    Data<Pais>.instance.lista.Add(Data<Pais>.instance.Arbol.Equilibrio());
                }

            }
            return View(Data<Pais>.instance.lista);
        }
        public ActionResult IndexEntero(string submitButton)
        {
            switch (submitButton)
            {
                case "PreOrden":
                    Data<Entero>.instance.orden = 1;
                    break;
                case "InOrden":
                    Data<Entero>.instance.orden = 0;
                    break;
                case "PostOrden":
                    Data<Entero>.instance.orden = 2;
                    break;
            }
            if (Data<Entero>.instance.orden == 0)
            {
                Data<Entero>.instance.lista.Clear();
                Data<Entero>.instance.Arbol.MostrarInOrden(ref Data<Entero>.instance.lista);
            }
            else if (Data<Entero>.instance.orden == 1)
            {
                Data<Entero>.instance.lista.Clear();
                Data<Entero>.instance.Arbol.MostrarPreOrden(ref Data<Entero>.instance.lista);
            }
            else
            {
                Data<Entero>.instance.lista.Clear();
                Data<Entero>.instance.Arbol.MostrarPostOrden(ref Data<Entero>.instance.lista);
            }
            return View(Data<Entero>.instance.lista);
        }
        public ActionResult IndexTexto(string submitButton)
        {
            switch (submitButton)
            {
                case "PreOrden":
                    Data<Texto>.instance.orden = 1;
                    break;
                case "InOrden":
                    Data<Texto>.instance.orden = 0;
                    break;
                case "PostOrden":
                    Data<Texto>.instance.orden = 2;
                    break;
            }
            if (Data<Texto>.instance.orden == 0)
            {
                Data<Texto>.instance.lista.Clear();
                Data<Texto>.instance.Arbol.MostrarInOrden(ref Data<Texto>.instance.lista);
            }
            else if (Data<Texto>.instance.orden == 1)
            {
                Data<Texto>.instance.lista.Clear();
                Data<Texto>.instance.Arbol.MostrarPreOrden(ref Data<Texto>.instance.lista);
            }
            else
            {
                Data<Texto>.instance.lista.Clear();
                Data<Texto>.instance.Arbol.MostrarPostOrden(ref Data<Texto>.instance.lista);
            }
            return View(Data<Texto>.instance.lista);
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pais/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pais/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CrearPorArchivo()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult CrearPorArchivo(HttpPostedFileBase postedFile)
        {
            try
            {
                string todoeltexto="";
                string filePath = string.Empty;
                if (postedFile != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    int contLinea = 0;
                    string csvData = System.IO.File.ReadAllText(filePath);
                   /* foreach (string row in csvData.Split('}'))
                    {*/
                        
                           
                               /* if (!string.IsNullOrEmpty(row))
                                {*/
                                if (Data<Pais>.instance.tipoDato == 0)
                                {
                                    Pais[] pais = JsonConvert.DeserializeObject<Pais[]>(csvData);
                                        if (pais.Length == 1)
                                        {
                                            Data<Pais>.instance.Arbol.Insertar(pais[0], Pais.CompareByName);
                                        }
                                        else
                                        {
                                            for (int i = 0; i <= pais.Length - 1; i++)
                                            {
                                                Data<Pais>.instance.Arbol.Insertar(pais[i], Pais.CompareByName);
                                            }
                                        }
                                   
                                }
                                else if (Data<Entero>.instance.tipoDato == 1)
                                {
                                    Entero[] entero = JsonConvert.DeserializeObject<Entero[]>(csvData);
                                        if (entero.Length == 1)
                                        {
                                            Data<Entero>.instance.Arbol.Insertar(entero[0], Entero.CompareByValor);
                                        }
                                        else
                                        {
                                            for (int i = 0; i <= entero.Length - 1; i++)
                                            {
                                                Data<Entero>.instance.Arbol.Insertar(entero[i], Entero.CompareByValor);
                                            }
                                        }    
                                }
                                else
                                {
                                    Texto[] texto = JsonConvert.DeserializeObject<Texto[]>(csvData);
                                    if (texto.Length == 1)
                                    {
                                        Data<Texto>.instance.Arbol.Insertar(texto[0], Texto.CompareByText);
                                    }
                                    else
                                    {
                                        for (int i = 0; i <= texto.Length - 1; i++)
                                        {
                                            Data<Texto>.instance.Arbol.Insertar(texto[i], Texto.CompareByText);
                                        }
                                    }    
                                }

                            }
                //  Pais p = new Pais {Id = 1, Name = "Brasil", Group = "A"};
                //Data<Pais>.instance.Arbol.removeNodo(p, Pais.CompareByName);

                //}
                // }
                String retorno = "Index";
                if(Data<Pais>.instance.tipoDato==0 || Data<Texto>.instance.tipoDato == 0|| Data<Entero>.instance.tipoDato == 0)
                {
                    retorno = "Index";
                }
                else if(Data<Pais>.instance.tipoDato == 1 || Data<Texto>.instance.tipoDato == 1 || Data<Entero>.instance.tipoDato == 1)
                {
                    retorno = "IndexEntero";
                }
                else if(Data<Pais>.instance.tipoDato == 2 || Data<Texto>.instance.tipoDato == 2 || Data<Entero>.instance.tipoDato == 2)
                {
                    retorno = "IndexTexto";
                }
                return RedirectToAction(retorno);
            }
            catch
            {
                return View();
            }
        }
    }
}
