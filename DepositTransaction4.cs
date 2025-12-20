using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    public class DepositTransaction : Transaction
    {
        //Declaring instance variables//
        private Account _account;

        //constructing them//
        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }
      
        //Method to execute deposit//
        public override void Execute()
        {
            //check if the transaction was already attempted//
            if (Executed)
            {
                throw new InvalidOperationException("Transaction already attempted");

            }
            // Calling the base execute method //
            base.Execute();

            //update status of executed//
            Executed = true;

            //deposit this amount into account and change success into true if it worked//
            Success = this._account.Deposit(this._amount);

            //indicate status to the user//
            if (Success)
            {
                Console.WriteLine("Deposit call succeeded!");
            }
            else
            {
                Console.WriteLine("Deposit call failed!");
            }
        }
        //Method to rollback deposit//
        public override void Rollback()
        {
            //throw an exception if deposit is unsuccessful//
            if (!Success)
            {
                throw new InvalidOperationException("Deposit transaction has not been successfull to carry out rollback");
            }
            //throw an error if deposit has already been reversed//
            if (Reversed)
            {
                throw new InvalidOperationException("Deposit transaction has already been reversed so cannot rollback again");
            }
            // calling the base rollback method //
            base.Rollback();    

            //withdraw amount from the account and change status of reversed to true//
            Reversed = this._account.Withdraw(this._amount);

            //indicate status to the user//
            if (Reversed)
            {
                Console.WriteLine("Deposit rollback succeeded! \n");
            }
            else
            {
                Console.WriteLine("Deposit rollback failed! \n");
            }
        }
        // Method to print the details of the transaction
        public override void Print()
        {
            Console.WriteLine();
            Console.WriteLine("Deposit Transaction details:");
            Console.WriteLine($"Transaction date: {DateStamp}");
            this._account.Print();
            if (Success)
            {
                Console.WriteLine($"Deposited amount: {this._amount.ToString("C")}");
            }
            else
            {
                Console.WriteLine($"Deposited amount: {0.ToString("C")}");
            }
            Console.WriteLine($"Executed: {Executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {Reversed}");
        }
    }
}
