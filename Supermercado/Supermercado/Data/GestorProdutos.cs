using Supermercado.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Supermercado.Data
{
    [Serializable]
    class GestorProdutos
    {
        static public List<Produtos> listaProdutos = new List<Produtos>();
        static public string path { get; } = ConfigurationManager.AppSettings["produtosPath"];

        #region Gravar Produto
        public static void GravarProduto()
        {
            string fileLocation = Directory.GetCurrentDirectory();

            try
            {
                using(FileStream fileStream = File.Create(path))
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Serialize(fileStream, GestorProdutos.listaProdutos);
                }
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't access the file! Reason:" + a.Message);
            }
        }
        #endregion

        #region Ler Produto
        public static void LerProduto()
        {
            string location = Directory.GetCurrentDirectory();

            try
            {
                GestorProdutos.listaProdutos.Clear();
                if (File.Exists(path))
                {
                    using(FileStream fileStream = File.OpenRead(path))
                    {
                        BinaryFormatter f = new BinaryFormatter();


                        List<Produtos> g = f.Deserialize(fileStream) as List<Produtos>;
                        GestorProdutos.listaProdutos = g;
                    }
                }
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't access the file! Reason:" + a.Message);
            }
        }
        #endregion

        #region Listar na Console
        public static void EscreverListaConsola()
        {
            LerProduto();
            try
            {
                List<string> lista = new List<string>();
                string row = "ID|NOME PRODUTO|BARCODE|PREÇO|STOCK|TIPO";
                lista.Add(row);
                int size = 0;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                foreach (Produtos p in listaProdutos)
                {
                    if(p.stock >= 0)
                    {
                        row = $"{p.id}|{p.productName}|{p.barcodeNumber}|{p.unitPrice}€|{p.stock}|{p.produto}";
                        if (row.Length > size)
                        {
                            size = row.Length;
                        }
                        lista.Add(row);
                    }
                }
                ToolsTable.Print(size, lista);
            }
            catch (Exception a)
            {
                Console.WriteLine(" Couldn't print list! Reason: " + a.Message);
            }
        }
        #endregion

        #region Remover Produto

        public static void EscolhaRemover()
        {
            try
            {
                EscreverListaConsola();
                Console.Write("Product Name do produto que pretende remover:");
                string contactoAEliminarNome = Console.ReadLine();
                bool resultado = removeFromProducts(contactoAEliminarNome);
                if (resultado)
                {
                    Console.WriteLine("Produto eliminado com sucesso");
                    GravarProduto();

                }
                else
                {
                    Console.WriteLine("Falhou");
                }
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't eliminate product. Reason: " + a.Message);
            }
        }

        public static bool removeFromProducts(string productName)
        {
            try
            {
                int indexAremover = -1;
                for (int i = 0; i < listaProdutos.Count; i++)
                {

                    if (listaProdutos[i].productName.ToLower().Equals(productName.ToLower()))
                    {
                        indexAremover = i;
                    }
                }
                if (indexAremover != -1)
                {
                    listaProdutos.RemoveAt(indexAremover);
                    return true;
                }
                return false;
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't remove product. Reason: " + a.Message);
            }
            return false;
        }

        #endregion

        #region Editar Produto 
        public static void EscolhaEditar()
        {
            try
            {
                Console.WriteLine("Nome do Produto a Editar:");
                string nomeAProcurar = Console.ReadLine();

                Console.Write("Nome Produto: ");
                string novoProductName = Console.ReadLine();
                foreach(Produtos p in GestorProdutos.listaProdutos)
                {
                    while(p.productName.ToLower().Contains(novoProductName.ToLower()))
                    {
                        Console.WriteLine("Produto já existente!");
                        return;
                    }
                }
                Console.Write("Barcode Produto: ");
                string novoBarcode = Console.ReadLine();
                foreach (Produtos p in GestorProdutos.listaProdutos)
                {
                    while (p.barcodeNumber.ToLower().Contains(novoBarcode.ToLower()))
                    {
                        Console.WriteLine("Produto já existente!");
                        return;
                    }
                }
                Console.Write("Unit Price: ");
                string novoUnitPrice = Console.ReadLine();
                Console.Write("Stock: ");
                double novoStock = Convert.ToDouble(Console.ReadLine());

                EditProduct(nomeAProcurar, novoProductName, novoBarcode, novoUnitPrice, novoStock);
                GravarProduto();
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't edit Product. Reason: " + a.Message);
            }
        }
        #region Edit Product
        static public Produtos EditProduct(string nome, string novoProductName, string novoBarcode, string novoUnitPrice, double novoStock)
        {
            try
            {
                Produtos contactoAEditar = FindProduct(nome);

                if (contactoAEditar != null)
                {
                    if (!novoProductName.Equals(""))
                    {
                        contactoAEditar.productName = novoProductName;
                    }
                    if (!novoBarcode.Equals(""))
                    {
                        contactoAEditar.barcodeNumber = novoBarcode;
                    }
                    if (!novoUnitPrice.Equals(""))
                    {
                        contactoAEditar.unitPrice = novoUnitPrice;
                    }
                    if (!novoStock.Equals(""))
                    {
                        contactoAEditar.stock = novoStock;
                    }
                    return contactoAEditar;
                }
                return null;
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't edit product. Reason: " + a.Message);
            }
            return null;
        }
        #endregion

        #region Find Product
        static public Produtos FindProduct(string productName)
        {
            try
            {
                foreach (Produtos p in listaProdutos)
                {
                    if (p.productName.ToLower().Equals(productName.ToLower()))
                    {

                        return p;
                    }
                }
                return null;
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't find product! Reason: " + a.Message);
            }
            return null;
        }
        #endregion

        #endregion

        #region Limpar Stock
        public static void LimparLista()
        {
            try
            {
                listaProdutos.Clear();
                string location = Directory.GetCurrentDirectory();

                if (File.Exists(path))
                {
                    File.WriteAllText(path, string.Empty);

                }
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't clear the list! Reason: " + a.Message);
            }
        }
        #endregion

        #region Pesquisar Produtos
        public static void PesquisaProdutos()
        {
            try
            {
                LerProduto();
                Console.WriteLine("Insira o nome produto que quer pesquisar: ");
                string nomeProduto = Console.ReadLine();
                List<string> lista = new List<string>();
                string row = "ID|NOME PRODUTO|BARCODE|PREÇO|STOCK|TIPO";
                lista.Add(row);
                int size = 0;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                foreach (Produtos p in listaProdutos)
                {
                    if (p.productName.ToLower().Equals(nomeProduto.ToLower()))
                    {
                        row = $"{p.id}|{p.productName}|{p.barcodeNumber}|{p.unitPrice}€|{p.stock}|{p.produto}";
                        if (row.Length > size)
                        {
                            size = row.Length;
                        }
                        lista.Add(row);
                    }
                }
                ToolsTable.Print(size, lista);
            }
            catch(Exception a)
            {
                Console.WriteLine("Impossível encontrar produto. Razão: " + a.Message);
            }
        }
        #endregion

    }
}
