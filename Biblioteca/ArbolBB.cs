using System;
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

        public Nodo<T> findWhere(Delegate delegado, T datos, Nodo<T> raiz)
        {
            if (raiz != null)
            {
                findWhere(delegado, datos, raiz.Izquierda);
                if ((int)delegado.DynamicInvoke(raiz.info, datos) == 0)
                {
                    return raiz;
                }                
                findWhere(delegado, datos, raiz.Derecha);
            }
            return null;
            
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

        public int VerAltura()
        {
            altura = 0;
            VerAltura(Raiz, altura);
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

        private void PostOrden(Nodo<T> nAux, ref List<T> lista)
        {
            if (nAux != null)
            {
                InOrden(nAux.Izquierda, ref lista);
                InOrden(nAux.Derecha, ref lista);
                lista.Add(nAux.info);
            }
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
        
public bool removeNodo(T dato, Delegate delegado)
        {
            Nodo<T> nodo = findWhere(delegado, dato, Raiz);
            /* Creamos variables para saber si tiene hijos izquierdo y derecho */
            bool tieneNodoDerecha = nodo.getDerecha() != null ? true : false;
            bool tieneNodoIzquierda = nodo.getIzquierda() != null ? true : false;

            if (!tieneNodoDerecha && !tieneNodoIzquierda)
            {
                return removeNodoCaso1(nodo);
            }

      
            if (tieneNodoDerecha && !tieneNodoIzquierda)
            {
                return removeNodoCaso2(nodo);
            }

  
            if (!tieneNodoDerecha && tieneNodoIzquierda)
            {
                return removeNodoCaso2(nodo);
            }

            /* Caso 3: Tiene ambos hijos */
            if (tieneNodoDerecha && tieneNodoIzquierda)
            {
                return removeNodoCaso3(nodo);
            }

            return false;
        }
        

private bool removeNodoCaso1(Nodo<T> nodo)
        {
            
            return false;
        }
        private bool removeNodoCaso2(Nodo<T> nodo)
        {
            return false;
        }
        private bool removeNodoCaso3(Nodo<T> nodo)
        {
            return false;
        }
    }
}
