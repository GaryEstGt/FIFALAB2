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
                        retorno = "IndexTexto";
                        break;
                    case "Entero":
                        Data<Entero>.instance.tipoDato = 1;
                        retorno = "IndexEntero";
                        break;
                    case "Pais":
                        Data<Pais>.instance.tipoDato = 0;
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
        

        
     
        public ActionResult Index()
        {
            Pais country = new Pais();
            List<Pais> lista = new List<Pais>();
            country.Id = 1;
            country.Name = "hola";
            lista.Add(country);
            return View(lista);
        }
        public ActionResult IndexEntero()
        {
            List<Entero> lista = new List<Entero>();
            lista.Add(new Entero{ valor=1});
            return View(lista);
        }
        public ActionResult IndexTexto()
        {
            List<Texto> lista = new List<Texto>();
            lista.Add(new Texto { texto = "hola" });
            return View(lista);
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
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (contLinea != 0)
                        {
                            if (Data<Pais>.instance.tipoDato == 0)
                            {
                                if (!string.IsNullOrEmpty(row))
                                {
                                    Pais pais = JsonConvert.DeserializeObject<Pais>(row);
                                    Data<Pais>.instance.Arbol.Insertar(pais, Pais.CompareByName);
                                }
                            }
                            else if (Data<Entero>.instance.tipoDato == 1)
                            {
                                if (!string.IsNullOrEmpty(row))
                                {
                                    Entero entero = JsonConvert.DeserializeObject<Entero>(row);
                                    Data<Entero>.instance.Arbol.Insertar(entero, Entero.CompareByValor);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(row))
                                {
                                    Texto texto = JsonConvert.DeserializeObject<Texto>(row);
                                    Data<Texto>.instance.Arbol.Insertar(texto, Texto.CompareByText);
                                }
                            }
                        }
                        contLinea++;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
