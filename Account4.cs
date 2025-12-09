using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    class Account
    {
        //instance variables//
        private string _name;
        private decimal _balance;
        private int _id;

        //constructor//
        public Account(string name, int id, decimal balance)
        {
            this._name = name;
            this._id = id;
            this._balance = balance;
        }

        //method to make _name a read only property//
        public string Name
        {
            get { return _name; }
        }
        //method to make _id a read only property//
        public int ID
        {
            get { return _id; }
        }
       
        //method to deposit money by user input//
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Please enter a valid deposit amount");
                return false;
            }
            this._balance += amount;
            return true;
        }
        //method to withdraw money by user input//
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Please enter a valid withdrawal amount");
                return false;
            }
            else if (this._balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }
            this._balance -= amount;
            return true;
        }
        //method to print account content to the screen//
        public void Print()
        {
            Console.WriteLine($"Account name: {_name} \nAccount ID: {_id} \nAccount balance: {_balance.ToString("C")}");
        }
    }
}
