using Supermercado.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Supermercado
{
    [Serializable]

    public class Funcionario
    {
        #region Properties
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public DateTime birthDate { get; set; }
        public decimal salary { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public EnumCargo cargo { get; set; }
        #endregion

        #region Constructors
        public Funcionario()
        {
        }

        public Funcionario(string firstName, string lastName, string phoneNumber, string address, DateTime birthDate, decimal salary, string userName, string password, EnumCargo cargo)
        {
            this.id = RandomID();
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.birthDate = birthDate;
            this.salary = salary;
            this.userName = userName;
            this.password = password;
            this.cargo = cargo;
        }
        #endregion

        #region Set Random ID
        public int RandomID()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 3000);
            return id;
        }
        #endregion

        #region Override ToString
        public override string ToString()
        {
            return firstName + " " + lastName;
        }
        #endregion

        #region Login
        public static void LoginForm()
        {
            Gerente g = new Gerente();
            Repositor r = new Repositor();
            Caixa c = new Caixa();

            bool successfull = false;
            Console.Clear();
            try
            {
                while (successfull != true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("###########################################");
                    Console.WriteLine("#                                         #");
                    Console.WriteLine("#                   LOGIN                 #");
                    Console.WriteLine("#                                         #");
                    Console.WriteLine("###########################################");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Username:");
                    var username = Console.ReadLine();
                    Console.Write("Password:");
                    var password = ReadPassword();
                    Console.WriteLine("###########################################");
                    Console.ResetColor();
                    foreach (Funcionario funcionario in GestorFuncionario.listaFuncionarios)
                    {
                        if (username == funcionario.userName && password == funcionario.password)
                        {
                            Console.WriteLine("Login bem sucedido!");
                            successfull = true;
                            Console.Clear();
                            if (EnumHelper.GetDescription(funcionario.cargo) == "Gerente")
                            {
                                g.MenuGerente(funcionario);
                            }
                            else if (EnumHelper.GetDescription(funcionario.cargo) == "Caixa")
                            {
                                c.MenuCaixa(funcionario);
                            }
                            else if (EnumHelper.GetDescription(funcionario.cargo) == "Repositor")
                            {
                                r.MenuRepositor();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Username ou Password incorretos.");
                            Console.Clear();
                        }
                    }
                }
                Console.ReadKey();
                Console.Clear();
            }
            catch(Exception a)
            {
                Console.WriteLine("Could not login! Reason:" + a.Message);
            }
        }
        #endregion

        #region Password
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // Remove um caractere da password, becz backspace
                        password = password.Substring(0, password.Length - 1);
                        // Vê a localizaçao do cursor
                        int pos = Console.CursorLeft;
                        // Move o cursor 1 espaço pra esquerda
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // Altera por um espaço
                        Console.Write(" ");
                        // Move o cursor 1 espaço pra esquerda
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // Adiciona uma nova linha por causa do enter
            Console.WriteLine();
            return password;
        }
        #endregion
    }
}