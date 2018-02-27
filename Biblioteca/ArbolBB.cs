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
            }
        }

        public Nodo<T> findWhere(Func<T, bool> delegado, T datos, Nodo<T> raiz)
        {
            if (raiz != null)
            {
                findWhere(delegado, datos, raiz.Izquierda);
                if (delegado.Invoke(raiz.info))
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
    }
}
