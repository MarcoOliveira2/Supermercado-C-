using System;
using System.Collections.Generic;
using System.Text;

namespace Supermercado.Data
{
    [Serializable]
    class Vendas
    {

        #region Properties
        public string productName { get; set; }
        public int quantity { get; set; }
        public string barcodeCompra { get; set; }
        public string unitPrice { get; set; }
        #endregion

        #region Constructors
        public Vendas(string productName, int quantity, string barcodeCompra, string unitPrice)
        {
            this.productName = productName;
            this.quantity = quantity;
            this.barcodeCompra = barcodeCompra;
            this.unitPrice = unitPrice;
        }
        #endregion

        #region Venda
        public static void Venda(Funcionario funcionario)
        {
            try
            {
                List<Vendas> listaTemp = new List<Vendas>();
                int resposta;
                GestorFaturas.LerFaturas();

                GestorProdutos.EscreverListaConsola();
                Console.Write("Cliente: ");
                string nomeCliente = Console.ReadLine();
                string price = "";
                string name = "";
                do
                {
                    Console.Write("\nBarcode do produto a comprar: ");
                    string barcodecompra = Console.ReadLine();

                    Console.Write("\nQuantidade a comprar: ");
                    int quantidade = Convert.ToInt32(Console.ReadLine());

                    foreach (Produtos p in GestorProdutos.listaProdutos)
                    {
                        if (p.barcodeNumber.ToLower().Equals(barcodecompra.ToLower()))
                        {
                            while(p.stock <= 0 || p.stock < quantidade || quantidade<0)
                            {
                                Console.WriteLine("Produto esgotado! Escolha outro produto: ");
                                barcodecompra = Console.ReadLine();
                                Console.WriteLine("Quantidade a comprar: ");
                                quantidade = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                    }

                    Caixa.EditaStock(barcodecompra, quantidade);

                    Console.WriteLine("Deseja comprar mais produtos? (1) - Sim | (0) - Não");
                    resposta = Convert.ToInt32(Console.ReadLine());
                    
                    foreach (Produtos p in GestorProdutos.listaProdutos)
                    {
                        if (p.barcodeNumber.ToLower().Equals(barcodecompra.ToLower()))
                        {
                            if (p.stock > 0)
                            {
                                name = p.productName;
                                price = p.unitPrice;
                            }
                        }
                    }
                    Vendas venda = new Vendas(name, quantidade, barcodecompra, price);
                    listaTemp.Add(venda);
                } while (resposta == 1 && resposta != 0);

                double valor = 0;

                Console.WriteLine("Compra:");
                foreach (Vendas v in listaTemp)
                {
                    valor += Convert.ToDouble(v.unitPrice) * v.quantity;
                    Console.WriteLine("{0} | {1} | {2}", v.productName, v.barcodeCompra, v.quantity);
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("###########################################");
                Console.WriteLine("#                                         #");
                Console.WriteLine("#                 COMPRA                  #");
                Console.WriteLine("#                                         #");
                Console.WriteLine("###########################################");
                Console.ResetColor();
                Console.OutputEncoding = System.Text.Encoding.Unicode; //Para dar display corretamente ao simbolo "€"
                Console.WriteLine("Valor da compra: {0}€", valor);
                Console.WriteLine("Insira o dinheiro recebido: ");
                double pagamento = Convert.ToDouble(Console.ReadLine());
                while (pagamento < valor)
                {
                    double valorEmFalta = valor - pagamento;
                    Console.WriteLine("Está em falta: {0}€", Math.Round(valorEmFalta,2));
                    Console.WriteLine("Receber mais dinheiro: ");
                    double novopagamento = Convert.ToDouble(Console.ReadLine());
                    pagamento = pagamento + novopagamento;
                }
                if (pagamento > valor)
                {
                    double trocoAReceber = pagamento - valor;
                    Console.WriteLine("Troco: {0}€", Math.Round(trocoAReceber, 2));
                }
                string nif_;
                int nif;
                Console.Write("\nDeseja número de contribuinte?");
                Console.Write("\n1 - SIM");
                Console.WriteLine("\n2 - NÃO");
                int op = Convert.ToInt32(Console.ReadLine());
                if (op == 1)
                {
                    Console.WriteLine("Número de contribuinte: ");
                    nif_ = Console.ReadLine();
                    if(string.IsNullOrEmpty(nif_))
                    {
                        nif = 000000000;
                    }
                    else
                    {
                        nif = Convert.ToInt32(nif_);
                    }
                }
                else
                {
                    nif = 000000000;
                }
                Fatura fatura = new Fatura(funcionario.ToString(), nomeCliente, listaTemp, nif);
                GestorFaturas.listaFaturas.Add(fatura);
                GestorProdutos.GravarProduto();
                GestorFaturas.GravarFaturas();
            }
            catch(Exception a)
            {
                Console.WriteLine("Couldn't finish this purchase. Reason: " + a.Message);
            }
        }
        #endregion
    }
}
