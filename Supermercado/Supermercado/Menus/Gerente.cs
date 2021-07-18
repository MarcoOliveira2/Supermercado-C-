using Supermercado.Data;
using Supermercado.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Supermercado
{
    [Serializable]

    public class Gerente : Funcionario
    {
        
        public Gerente() : base()
        {
        }

        #region Menu Gerente
        public void MenuGerente(Funcionario funcionario)
        {
            int escolha = 0;
            while (escolha != 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("##################################################");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#                 MENU - GERENTE                 #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("##################################################");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         1 - CRIAR FUNCIONÁRIO                  #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         2 - APAGAR FUNCIONÁRIO                 #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         3 - VENDER PRODUTOS                    #");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#------------------------------------------------#");
                Console.WriteLine("#                                                #");
                Console.WriteLine("#         4 - LISTAR FATURAS                     #");
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
                        CreateEmployee();
                        break;
                    case 2:
                        EscolhaRemover();
                        break;
                    case 3:
                        Vendas.Venda(funcionario);
                        break;
                    case 4:
                        GestorFaturas.ListarFaturasConsole();
                        break;
                    case 0:
                        MenuInicial.InitialMenu();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        MenuGerente(funcionario);
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        #endregion

        #region Criar Funcionario
        private void CreateEmployee()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("###########################################");
                Console.WriteLine("#                                         #");
                Console.WriteLine("#                REGISTAR                 #");
                Console.WriteLine("#                                         #");
                Console.WriteLine("###########################################");
                Console.ResetColor();
                Console.WriteLine("Your First Name:");
                var firstName = Console.ReadLine();
                while (string.IsNullOrEmpty(firstName) || firstName.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid name!");
                    firstName = Console.ReadLine();
                }
                Console.WriteLine("Your Last Name:");
                var lastName = Console.ReadLine();
                while (string.IsNullOrEmpty(lastName) || lastName.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid name!");
                    lastName = Console.ReadLine();
                }
                Console.WriteLine("Your contact information:");
                var phoneNumber = Console.ReadLine();
                while (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Any(char.IsLetter) || phoneNumber.Length != 9)
                {
                    Console.WriteLine("Invalid phone number!");
                    phoneNumber = Console.ReadLine();
                }
                Console.WriteLine("Your address:");
                var address = Console.ReadLine();
                while (string.IsNullOrEmpty(address))
                {
                    Console.WriteLine("Please enter your address!");
                    address = Console.ReadLine();
                }
                Console.WriteLine("Your salary: ");
                decimal salary;
                bool result = decimal.TryParse(Console.ReadLine(), out salary);
                if (result)
                {
                    while (salary < 600)
                    {
                        Console.WriteLine("Don't be a liar!");
                        salary = Convert.ToDecimal(Console.ReadLine());
                    }
                }
                else
                {
                    Console.WriteLine("That's not a number!");
                    return;
                }
                Console.WriteLine("Your birth date: (dd/MM/YYYY)");
                DateTime birthDate = Convert.ToDateTime(Console.ReadLine());
                while (birthDate.Year <= 1950 || birthDate.Year >= 2006)
                    if (birthDate.Year <= 1950)
                    {
                        Console.WriteLine("Aren't you too old to work here?");
                        birthDate = Convert.ToDateTime(Console.ReadLine());
                    }
                    else
                    {
                        if (birthDate.Year >= 2006)
                        {
                            Console.WriteLine("Aren't you too young to work here?");
                            birthDate = Convert.ToDateTime(Console.ReadLine());
                        }
                    }
                Console.WriteLine("Wanted username:");
                var username = Console.ReadLine();
                foreach (Funcionario f in GestorFuncionario.listaFuncionarios)
                {
                    while (f.userName == username)
                    {
                        Console.WriteLine("Username already taken! Please try another one!");
                        username = Console.ReadLine();
                    }
                }
                Console.WriteLine("Wanted password(min. 8 char):");
                var password = ReadPassword();
                while (string.IsNullOrEmpty(password) || password.Length < 8)
                {
                    Console.WriteLine("Invalid password format! Please try another one!");
                    password = Console.ReadLine();
                }
                Console.WriteLine("What's your role in the company?");
                Console.WriteLine("1 - Gerente");
                Console.WriteLine("2 - Repositor");
                Console.WriteLine("3 - Caixa");
                var cargo = Convert.ToInt32(Console.ReadLine());
                var cargo_ = (EnumCargo)cargo;

                Funcionario a = new Funcionario(firstName, lastName, phoneNumber, address, birthDate, salary, username, password, cargo_);
                GestorFuncionario.listaFuncionarios.Add(a);
                GestorFuncionario.GravarFuncionario();


                Console.WriteLine("\nUser created successfully!");
            }
            catch (Exception a)
            {
                Console.WriteLine("Error creating user! Reason: " + a.Message);
            }
        }
        #endregion

        #region Remover Funcionario
        private void EscolhaRemover()
        {
            try
            {
                GestorFuncionario.EscreverListaConsola();
                Console.Write("Username do funcionário que pretende remover:");
                string contactoAEliminarNome = Console.ReadLine();
                bool resultado = removeFromContacs(contactoAEliminarNome);
                if (resultado)
                {
                    Console.WriteLine("Funcionário eliminado com sucesso");
                    GestorFuncionario.GravarFuncionario();

                }
                else
                {
                    Console.WriteLine("Falhou");
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't eliminate employee. Reason: " + a.Message);
            }
        }

        private bool removeFromContacs(string userName)
        {
            try
            {
                int indexAremover = -1;
                for (int i = 0; i < GestorFuncionario.listaFuncionarios.Count; i++)
                {

                    if (GestorFuncionario.listaFuncionarios[i].userName.ToLower().Equals(userName.ToLower()))
                    {
                        indexAremover = i;
                    }
                }
                if (indexAremover != -1)
                {
                    GestorFuncionario.listaFuncionarios.RemoveAt(indexAremover);
                    return true;
                }

                return false;
            }
            catch (Exception a)
            {
                Console.WriteLine("Couldn't remove employee. Reason: " + a.Message);
            }
            return false;
        }

        #endregion
    }
}
