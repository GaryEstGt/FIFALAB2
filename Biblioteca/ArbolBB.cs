﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{        
    public class ArbolBB<T>
    {
        public Nodo<T> Raiz;
        public List<T> nodosHoja;
        int altura;
        public ArbolBB()
        {
            Raiz = null;
        }

        public void Insertar(T datos, Delegate delegado)
        {
            Nodo<T> nuevo = new Nodo<T>(datos);            

            if (Raiz == null)
            {
                Raiz = nuevo;
            }
            else
            {
                Nodo<T> aux = Raiz;
                Nodo<T> Padre = Raiz;
                bool derecha = false;

                while (aux != null)
                {
                    Padre = aux;
                    if ((int)delegado.DynamicInvoke(nuevo.info, aux.info) == 1)
                    {
                        aux = aux.Derecha;
                        derecha = true;
                    }
                    else
                    {
                        aux = aux.Izquierda;
                        derecha = false;
                    }
                }

                if (derecha)
                {
                    Padre.Derecha = nuevo;                   
                }
                else
                {
                    Padre.Izquierda = nuevo;
                }

                nuevo.Padre = Padre;                                
            }
        }

        public void findWhere(Delegate delegado, T datos, Nodo<T> raiz, ref Nodo<T> nodo)
        {
            if ((raiz != null)&&(nodo == null))
            {
                findWhere(delegado, datos, raiz.Izquierda, ref nodo);
                if ((int)delegado.DynamicInvoke(raiz.info, datos) == 0)
                {
                    nodo = raiz;
                }                
                findWhere(delegado, datos, raiz.Derecha, ref nodo);
            }                        
        }

        public List<T> BuscarHojas()
        {
            nodosHoja = null;            
            BuscarHojas(Raiz);
            return nodosHoja;
        }

        public void BuscarHojas(Nodo<T> aux)
        {
            if (aux != null)
            {
                if ((aux.Derecha == null)&&(aux.Izquierda == null))
                {
                    nodosHoja.Add(aux.info);
                }
                BuscarHojas(aux.Izquierda);
                BuscarHojas(aux.Derecha);
            }
        }

        public int VerAltura(Nodo<T> Nodo)
        {
            altura = 0;
            VerAltura(Nodo, altura);
            return altura;
        }

        public void VerAltura(Nodo<T> aux, int nivel)
        {
            if (aux != null)
            {
                VerAltura(aux.Izquierda, nivel + 1);
                if (nivel>altura)
                {
                    altura = nivel;
                }
                VerAltura(aux.Derecha, nivel + 1);
            }
        }       
        private void InOrden(Nodo<T> nAux, ref List<T> lista)
        {
            if (nAux != null)
            {
                InOrden(nAux.Izquierda, ref lista);
                lista.Add(nAux.info);
                InOrden(nAux.Derecha, ref lista);
            }
        }
        public void MostrarInOrden(List<T> lista)
        {
            InOrden(Raiz, ref lista);
        }

        private void PostOrden(Nodo<T> nAux, ref List<T> lista)
        {
            if (nAux != null)
            {
                InOrden(nAux.Izquierda, ref lista);
                InOrden(nAux.Derecha, ref lista);
                lista.Add(nAux.info);
            }
        }
        public void MostrarPostOrden(List<T> lista)
        {
            PostOrden(Raiz, ref lista);
        }

        private void PreOrden(Nodo<T> nAux, ref List<T> lista)
        {
            if (nAux != null)
            {
                lista.Add(nAux.info);
                InOrden(nAux.Izquierda, ref lista);
                InOrden(nAux.Derecha, ref lista);
            }
        }
        public void MostrarPreOrden(List<T> lista)
        {
            PreOrden(Raiz, ref lista);
        }
        public void removeNodo(T dato, Delegate delegado)
        {
            Nodo<T> nodo = null;
            findWhere(delegado, dato, Raiz, ref nodo);
            /* Creamos variables para saber si tiene hijos izquierdo y derecho */
            bool tieneNodoDerecha = nodo.getDerecha() != null ? true : false;
            bool tieneNodoIzquierda = nodo.getIzquierda() != null ? true : false;

            if (!tieneNodoDerecha && !tieneNodoIzquierda)
            {
                removeNodoCaso1(nodo);
            }

      
            if (tieneNodoDerecha && !tieneNodoIzquierda)
            {
                removeNodoCaso2(nodo);
            }

  
            if (!tieneNodoDerecha && tieneNodoIzquierda)
            {
                removeNodoCaso2(nodo);
            }

            /* Caso 3: Tiene ambos hijos */
            if (tieneNodoDerecha && tieneNodoIzquierda)
            {
                removeNodoCaso3(nodo);
            }            
        }
        

        private void removeNodoCaso1(Nodo<T> nodo)
        {
            if (nodo != Raiz)
            {
                if (nodo.Padre.Derecha == nodo)
                    nodo.Padre.Derecha = null;
                else
                    nodo.Padre.Izquierda = null;
            }
            else
            {
                Raiz = null;
            }
            
        }
        private void removeNodoCaso2(Nodo<T> nodo)
        {
            if (nodo != Raiz)
            {
                bool derecha = nodo.Padre.Derecha == nodo ? true : false;

                if (nodo.Derecha != null)
                {
                    nodo.Derecha.Padre = nodo.Padre;

                    if (derecha)
                        nodo.Padre.Derecha = nodo.Derecha;
                    else
                        nodo.Padre.Izquierda = nodo.Derecha;
                }
                else
                {
                    nodo.Izquierda.Padre = nodo.Padre;
                    if (derecha)
                        nodo.Padre.Derecha = nodo.Izquierda;
                    else
                        nodo.Padre.Izquierda = nodo.Izquierda;
                }
            }
            else
            {
                if (nodo.Derecha != null)
                {
                    Raiz = nodo.Derecha;
                    nodo.Derecha.Padre = null;
                }                                  
                else
                {
                    Raiz = nodo.Izquierda;
                    nodo.Izquierda.Padre = null;
                }                                                    
            }
        }
        private void removeNodoCaso3(Nodo<T> nodo)
        {
            Nodo<T> aux = nodo.Izquierda;

            while (aux.Derecha != null)
            {
                aux = aux.Derecha;
            }

            if (aux.Padre != nodo)
            {
                aux.Padre.Derecha = aux.Izquierda;                
            }
            else
            {
                aux.Padre.Izquierda = aux.Izquierda;                
            }

            if (aux.Izquierda != null)
                aux.Izquierda.Padre = aux.Padre;

            aux.Derecha = nodo.Derecha;
            if (nodo.Derecha != null)            
                nodo.Derecha.Padre = aux;                       
            aux.Izquierda = nodo.Izquierda;
            if (nodo.Izquierda != null)
                nodo.Izquierda.Padre = aux;

            if (nodo != Raiz)
            {
                aux.Padre = nodo.Padre;
                bool derecha = nodo.Padre.Derecha == nodo ? true : false;

                if (derecha)
                    nodo.Padre.Derecha = aux;
                else
                    nodo.Padre.Izquierda = aux;
            }                
            else
            {
                aux.Padre = null;
                Raiz = aux;
            }
                
            
        }
        public Nodo<T> verEquilibrio(Nodo<T> nodo)
        {
            int valorDer = 0;
            int valorIzq = 0;
            if(nodo.Derecha==null && nodo.Izquierda == null)
            {
                return null;
            }
            else
            {
                if (nodo.Izquierda == null)
                {
                    valorIzq = 0;
                    valorDer = VerAltura(nodo.Derecha);
                }
                else if (nodo.Derecha == null)
                {
                    valorIzq = VerAltura(nodo.Izquierda);
                    valorDer = 0;
                }
                else
                {
                    valorIzq = VerAltura(nodo.Izquierda);
                    valorDer = VerAltura(nodo.Derecha);
                }
                if ((valorDer - valorIzq) > 1 || (valorDer - valorIzq) < -1)
                {
                    return nodo;
                }
                else
                {
                    if (nodo.Derecha == null)
                    {
                        verEquilibrio(nodo.Izquierda);
                    }
                    else if (nodo.Izquierda == null)
                    {
                        verEquilibrio(nodo.Derecha);

                    }
                  
                    
                }
            }
            return null;
        }
        public T Equilibrio()
        {
            return verEquilibrio(Raiz).info;
        }
    }
}
