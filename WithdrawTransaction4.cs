using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    public class WithdrawTransaction : Transaction
    {
        //declaring instance variables//
        private Account _account;
   

        //constructing them//
        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            this._account = account;
        }

        //Method to execute withdrawal//
        public override void Execute()
        {
            //throws an exception if already attempted//
            if (Executed)
            {
                throw new InvalidOperationException("Withdrawal transaction has already been attempted");
            }
            // calling the base execute method //
            base.Execute();

            //updates executed//
            Executed = true;

            //deposits money into account and changes success to true if it worked//
            Success = this._account.Withdraw(this._amount);

            //status indicator at the end//
            if (Success)
            {
                Console.WriteLine("Withdraw call succeeded");
            }
            else
            {
                Console.WriteLine("Withdraw call failed!");
            }
        }
        // Method to perform a rollback of the withdrawal transaction
        public override void Rollback()
        {
            //throw exceptions if the withdraw is unsuccessful//
            if (!Success)
            {
                throw new InvalidOperationException("Withdrawal transaction has not been successfull to carry out rollback.");

            }
            //throw an exception if the transaction has already been reversed//
            if (Reversed)
            {
                throw new InvalidOperationException("Withdrawal transaction has already been reversed so cannot rollback again.");
            }
            // calling the base rollback method //
            base.Rollback();

            //Attempting to deposit the amount back into the account//
            Reversed = this._account.Deposit(this._amount);

            //status indicator at the end//
            if (Reversed)
            {
                Console.WriteLine("Withdraw rollback succeded! \n");
            }
            else
            {
                Console.WriteLine("Withdraw rollback failed! \n");
            }
        }
        // Method to print the details of the transaction
        public override void Print()
        {
            Console.WriteLine();
            Console.WriteLine("Withdraw Transaction details:");
            Console.WriteLine($"Transaction date: {DateStamp}");
            this._account.Print();
            if (this._success)
            {
               Console.WriteLine($"Withdrawn amount: {this._amount.ToString("C")}");
            }
            else
            {
               Console.WriteLine($"Withdrawn amount: {0.ToString("C")}");
            }
            Console.WriteLine($"Executed: {Executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {Reversed}");
        }
    }
}
