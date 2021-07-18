using Supermercado.Data;
using Supermercado.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Supermercado
{
    public class Repositor : Funcionario
    {
        public Repositor() : base()
        {

        }

        #region Menu Repositor
        public void MenuRepositor()
        {
            GestorProdutos.LerProduto();
            int escolha = 0;
            while (escolha != 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("####################################################");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#                 MENU - REPOSITOR                 #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("####################################################");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         1 - ADICIONAR PRODUTOS                   #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         2 - LIMPAR STOCK                         #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         3 - REMOVER PRODUTO                      #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         4 - LISTAR PRODUTOS                      #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         5 - EDITAR PRODUTOS                      #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         6 - PESQUISAR PRODUTO                    #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#--------------------------------------------------#");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("#         0 - SAIR                                 #");
                Console.WriteLine("#                                                  #");
                Console.WriteLine("####################################################");
                Console.ResetColor();

                escolha = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (escolha)
                {
                    case 1:
                        CreateProduct();
                        break;
                    case 2:
                        GestorProdutos.LimparLista();
                        break;
                    case 3:
                        GestorProdutos.EscolhaRemover();
                        break;
                    case 4:
                        GestorProdutos.EscreverListaConsola();
                        break;
                    case 5:
                        GestorProdutos.EscreverListaConsola();
                        GestorProdutos.EscolhaEditar();
                        break;
                    case 6:
                        GestorProdutos.PesquisaProdutos();
                        break;
                    case 0:
                        MenuInicial.InitialMenu();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        MenuRepositor();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        #endregion

        #region Inserir Produto
        private static void CreateProduct()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("#############################################");
                Console.WriteLine("#                                           #");
                Console.WriteLine("#                  PRODUCT                  #");
                Console.WriteLine("#                                           #");
                Console.WriteLine("#############################################");
                Console.ResetColor();
                Console.WriteLine("Barcode Number:");
                var barcodeNumber = Console.ReadLine();
                foreach (Produtos p in GestorProdutos.listaProdutos)
                {
                    while (GestorProdutos.listaProdutos.Any(x => x.barcodeNumber.ToLower() == barcodeNumber))
                    {
                        Console.WriteLine("Produto já existente!");
                        return;
                    }
                }
                while (string.IsNullOrEmpty(barcodeNumber))
                {
                    Console.WriteLine("Barcode must be filled!");
                    barcodeNumber = Console.ReadLine();
                }
                Console.WriteLine("Product Name:");
                var productName = Console.ReadLine();
                while (string.IsNullOrEmpty(productName))
                {
                    Console.WriteLine("Product name must be filled!");
                    productName = Console.ReadLine();
                }
                foreach (Produtos p in GestorProdutos.listaProdutos)
                {
                    while (GestorProdutos.listaProdutos.Any(x => x.productName.ToLower() == productName))
                    {
                        Console.WriteLine("Produto já existente!");
                        return;
                    }
                }
                Console.WriteLine("Unit Price:");
                var unitPrice = Console.ReadLine();
                while (string.IsNullOrEmpty(unitPrice) || unitPrice.Any(char.IsLetter) || Convert.ToDouble(unitPrice) <= 0)
                {
                    Console.WriteLine("Who do you think we are? \"Madre Teresa de Calcutá?\"");
                    unitPrice = Console.ReadLine();
                }

                Console.WriteLine("Stock:");
                double stock = Convert.ToDouble(Console.ReadLine());
                while (stock <= 0)
                {
                    Console.WriteLine("You do know you are trying to *ADD* a product right? Try again.");
                    stock = Convert.ToDouble(Console.ReadLine());
                }

                Console.WriteLine("Product type:");
                Console.WriteLine("1 - Congelados");
                Console.WriteLine("2 - Prateleira");
                Console.WriteLine("3 - Enlatados");
                var produto = Convert.ToInt32(Console.ReadLine());
                var produto_ = (EnumProductType)produto;
                bool active = true;

                Produtos a = new Produtos(barcodeNumber, productName, unitPrice, stock, active, produto_);
                GestorProdutos.listaProdutos.Add(a);
                GestorProdutos.GravarProduto();


                Console.WriteLine("\nProduct created successfully!");
            }
            catch (Exception a)
            {
                Console.WriteLine("Could not create this product. Reason: " + a.Message);
            }

        }
        #endregion
    }
}
        

