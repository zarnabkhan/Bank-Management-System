using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

namespace BankApp
{
    class BankAccount
    {
        private double balance;
        public string AccountNumber { get; private set; }

        public BankAccount(string accountNumber, double initilbalance=0)
        {
            AccountNumber = accountNumber;
            balance= initilbalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Enter postive amount");
            }

            balance += amount;
        }

        public void Withdraw(double amount)
        {
            if(amount <=0)
            {
                throw new ArgumentException("Amount should be greater than zero. Try again!");
            }
            if(amount > balance)
            {
                throw new InvalidOperationException("Insuficient funds");
            }

            balance -= amount;
        }

        public double GetBalance() { return balance; }

    }
    class Program
    {
        static List<BankAccount> accounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Bank Menu ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Show Balance");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateAccount();
                            break;
                        case "2":
                            DepositMoney();
                            break;
                        case "3":
                            WithdrawMoney();
                            break;
                        case "4":
                            ShowBalance();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                }
            }
        }
        static void CreateAccount()
        {
            Console.WriteLine("Enter Account Number");
            string accnum= Console.ReadLine();
            var account= new BankAccount(accnum);
            accounts.Add(account);
            Console.WriteLine("Account created succesfully");
        }

        static void DepositMoney()
        {
            Console.WriteLine("Enter Account number");
            string accNum= Console.ReadLine();
            var account= accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found, Try again!");
                return;
            }
            Console.WriteLine("Enter Deposit Amount");
            double amount= Convert.ToDouble(Console.ReadLine());
            account.Deposit(amount);
            Console.WriteLine("Deposit succesfully");
        }

        static void WithdrawMoney()
        {
            Console.WriteLine("Enter Account Number");
            string accNum= Console.ReadLine();
            var account= accounts.Find(a => a.AccountNumber == accNum);
            if (account == null)
            {
                Console.WriteLine("Account not found. Try again!");
            }
            Console.WriteLine("Enter Withdrawamount");
            double amount= Convert.ToDouble(Console.ReadLine());
            account.Withdraw(amount);
            Console.WriteLine("Withdrawl successfull");
        }

        static void ShowBalance()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine();
            var account = accounts.Find(a => a.AccountNumber == accNum);
            if (account == null) { Console.WriteLine("Account not found."); return; }

            Console.WriteLine($"Balance: {account.GetBalance()}");
        }

    }
}
