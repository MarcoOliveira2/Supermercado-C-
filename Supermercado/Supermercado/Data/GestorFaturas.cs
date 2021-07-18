using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Supermercado.Data
{
    [Serializable]
    class GestorFaturas
    {
        static public List<Fatura> listaFaturas = new List<Fatura>();
        public static string path { get; } = ConfigurationManager.AppSettings["faturasPath"];

        #region Gravar Faturas
        public static void GravarFaturas()
        {
            string fileLocation = Directory.GetCurrentDirectory();
            try
            {
                using(FileStream fileStream = File.Create(path))
                {
                    BinaryFormatter f = new BinaryFormatter();
                    f.Serialize(fileStream, GestorFaturas.listaFaturas);
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't access the file. Reason: " + a.Message);
            }
        }
        #endregion

        #region Ler Faturas
        public static void LerFaturas()
        {
            string location = Directory.GetCurrentDirectory();
            try
            {
                GestorFaturas.listaFaturas.Clear();
                if (File.Exists(path))
                {
                    using(FileStream fileStream = File.OpenRead(path))
                    {
                        BinaryFormatter f = new BinaryFormatter();
                        List<Fatura> g = f.Deserialize(fileStream) as List<Fatura>;
                        GestorFaturas.listaFaturas = g;
                    }
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't access the file! Reason: " + a.Message);
            }
        }
        #endregion

        #region Listar Faturas Console
        public static void ListarFaturasConsole()
        {
            LerFaturas();
            try
            {
                int size = 0;
                int option = -1;
                Console.WriteLine("Deseja ordenar as faturas? (1) - Sim | (0) - Não");
                option = Convert.ToInt32(Console.ReadLine());
                string row = "NOME FUNCIONARIO|NOME CLIENTE|NIF CLIENTE|NOME PRODUTO|BARCODE|PREÇO UNITÁRIO|QUANTIDADE";
                List<string> lista = new List<string>();
                lista.Add(row);
                List<Fatura> listAux = option == 1 ? GestorFaturas.listaFaturas.OrderBy(x => x.listaVendas.Sum(y => Convert.ToDouble(y.unitPrice) * y.quantity)).ToList() : GestorFaturas.listaFaturas.ToList();
                foreach (Fatura p in listAux)
                {
                    double valor = 0;
                    Console.OutputEncoding = System.Text.Encoding.Unicode;
                    foreach (Vendas v in p.listaVendas)
                    {
                        row = $"{p.nomeFuncionario}|{p.nomeCliente}|{p.nif}|{v.productName}|{v.barcodeCompra}|{v.unitPrice}€|{v.quantity}";
                        if(row.Length > size)
                        {
                            size = row.Length;
                        }
                        lista.Add(row);
                        valor += Convert.ToDouble(v.unitPrice) * v.quantity;
                    }
                    row = $"VALOR TOTAL||||||{valor}€";
                    lista.Add(row);
                    row = "||||||";
                    lista.Add(row);
                }
                ToolsTable.Print(size, lista);
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't print list! Reason: " + a.Message);
            }
        }
        #endregion

        #region Limpar Lista Faturas
        public static void LimparLista()
        {
            try
            {
                listaFaturas.Clear();
                string location = Directory.GetCurrentDirectory();

                if (File.Exists(path))
                {
                    File.WriteAllText(path, string.Empty);
                    Console.WriteLine("Lista de Faturas limpa!");
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't clear the list! Reason: " + a.Message);
            }
        }
        #endregion
    }
}
