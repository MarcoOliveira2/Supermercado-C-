using Supermercado.Data;
using Supermercado.Menus;
using System;
using System.Collections.Generic;

namespace Supermercado
{
    public class Caixa : Funcionario
    {
        public Caixa() : base()
        {
        }

        #region MenuCaixa
        public void MenuCaixa(Funcionario funcionario)
        {
            int escolha = 0;
            while (escolha != 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("##################################################");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#                 MENU - CAIXA                   #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("##################################################");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         1 - VENDER                             #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         2 - LISTAR FATURAS                     #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         3 - LISTAR PRODUTOS                    #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         4 - LIMPAR LISTA FATURAS               #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         0 - SAIR                               #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("##################################################");
                Console.ResetColor();
                escolha = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (escolha)
                {
                    case 1:
                        Vendas.Venda(funcionario);
                        break;
                    case 2:
                        GestorFaturas.ListarFaturasConsole();
                        break;
                    case 3:
                        GestorProdutos.EscreverListaConsola();
                        break;
                    case 4:
                        GestorFaturas.LimparLista();
                        break;
                    case 0:
                        MenuInicial.InitialMenu();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        MenuCaixa(funcionario);
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        #endregion

        #region Edit Stock
        static public Produtos EditaStock(string barcodeCompra, double novoStock)
        {
            try
            {
                Produtos produtoAEditar = FindProduct(barcodeCompra);

                if (produtoAEditar != null)
                {
                    if (!novoStock.Equals(""))
                    {
                        produtoAEditar.stock = produtoAEditar.stock - novoStock;
                        GestorProdutos.GravarProduto();
                    }
                    return produtoAEditar;
                }
                return null;
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't edit stock. Reason: " + a.Message);
            }
            return null;
        }
        #endregion

        #region Find Product
        static public Produtos FindProduct(string barcodeCompra)
        {
            try
            {
                foreach (Produtos p in GestorProdutos.listaProdutos)
                {
                    if (p.barcodeNumber.ToLower().Equals(barcodeCompra.ToLower()))
                    {
                        return p;
                    }
                }
                return null;
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't find product. Reason: " + a.Message);
            }
            return null;
        }
        #endregion
    }
}
