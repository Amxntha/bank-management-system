using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    public class TransferTransaction : Transaction
    {
        //declaring instant variables//
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;


        //constructing them//
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._withdraw = new WithdrawTransaction(this._fromAccount, this._amount);
            this._deposit = new DepositTransaction(this._toAccount, this._amount);
        }

        //method to execute the transferring//
        public override void Execute()
        {
            //throw an error if it has already been executed//
            if (Executed)
            {
                throw new InvalidOperationException("Transfer attempted already");
            }
            // Calling the base execute method //
            base.Execute();

            Executed = true; //ADJUSTED //

            // Execute withdraw first
            this._withdraw.Execute(); //ADJUSTED: this will throw an invalidoperationexception when there isn't enough funds to do so//

            if (this._withdraw.Success)
            {
                // Only execute deposit if withdraw was successful
                this._deposit.Execute();

                if (this._deposit.Success)
                {  
                    Success = true; // ADJUSTED: Success only true when withdraw and deposit are successful respectively //
                    Console.WriteLine("Transfer transaction complete!");
                }
                else
                {
                    throw new InvalidOperationException("Deposit failed after successful withdrawal.");
                }
            }
            else
            {
                throw new InvalidOperationException("Withdrawal failed due to insufficient funds.");
            }
        }
        //method to reverse the transferring//
        public override void Rollback()
        {
            //throw an error if the transfer hasn't been executed//
            if (!Success) // ADJUSTED //
            {
                throw new InvalidOperationException("Transfer has not been successful. Therefore cannot rollback.");
            }
            //throw an error if the transfer has already been reversed//
            if (Reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed. Therefore cannot rollback again");
            }
            // Calling the base rollback method //
            base.Rollback();

            this._deposit.Rollback(); //ADJUSTED //

            // ADJUSTED //
            if(this._deposit.Reversed)
            {
                // if deposit reverse is true call the rollback for withdraw //
                this._withdraw.Rollback();
                if (this._withdraw.Reversed)
                {
                    Reversed = true;
                    Console.WriteLine("Transfer transaction rollback successful! \n");
                }
                else
                {
                    throw new InvalidOperationException("Withdraw rollback failed! \n");
                }
            }
            else
            {
                throw new InvalidOperationException("Deposit rollback failed! \n");
            }
         
        }
        //method to print transfer details//
        public override void Print()
        {
            Console.WriteLine();
            if (!this.Success)
            {
                this._amount = 0;
            }
            Console.WriteLine("Transfer Transaction details");
            Console.WriteLine($"Transaction date: {DateStamp}");
            Console.WriteLine($"Transferred {this._amount.ToString("C")} from {this._fromAccount.ID} {this._fromAccount.Name}’s account to " +
                $" {this._toAccount.ID} {_toAccount.Name}’s account.");
            Console.WriteLine($"Executed: {Executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {Reversed}");

            _withdraw.Print();
            _deposit.Print();
        }
    }
}
