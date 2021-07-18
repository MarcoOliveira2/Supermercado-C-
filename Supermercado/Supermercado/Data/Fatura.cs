using System;
using System.Collections.Generic;
using System.Text;

namespace Supermercado.Data
{
    [Serializable]
    class Fatura
    {
        #region Properties
        public string nomeFuncionario { get; set; }
        public string nomeCliente { get; set; }

        public List<Vendas> listaVendas = new List<Vendas>();
        public long nif { get; set; }
        #endregion

        #region Costructor
        public Fatura(string nomeFuncionario,string nomeCliente, List<Vendas> listaVendas, long nif)
        {
            this.nomeFuncionario = nomeFuncionario;
            this.nomeCliente = nomeCliente;
            this.listaVendas = listaVendas;
            this.nif = nif;
        }
        #endregion
    }
}
