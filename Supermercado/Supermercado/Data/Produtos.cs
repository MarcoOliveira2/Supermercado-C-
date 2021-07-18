using Supermercado.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Supermercado
{
    [Serializable]
    public class Produtos
    {
        static public List<Produtos> productList = new List<Produtos>();
        static public string path { get; } = ConfigurationManager.AppSettings["produtosPath"];

        #region Properties
        public int id { get; set; }
        public string barcodeNumber { get; set; }
        public string productName { get; set; }
        public string unitPrice { get; set; }
        public double stock { get; set; }
        public EnumProductType produto { get; set; }
        #endregion

        #region Set Random ID
        public int RandomID()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 3000);
            return id;
        }
        #endregion

        #region Constructors
        public Produtos()
        {
        }
        public Produtos(string barcodeNumber ,string productName, string unitPrice, double stock, bool active, EnumProductType produto)
        {
            this.id = RandomID();
            this.barcodeNumber = barcodeNumber;
            this.productName = productName;
            this.unitPrice = unitPrice;
            this.stock = stock;
            active = true;
            this.produto = produto;
        }
        #endregion
    }
}