using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    class Bank
    {
        // Declaring list to store bank accounts //
        private List<Account> _accounts;
        // Declaring list to record transaction history //
        private List<Transaction> _transactions;

        // Constructing //
        public Bank()
        {
            this._accounts = new List<Account>();
            this._transactions = new List<Transaction>();
        }

        // property to get transactions //
        public List<Transaction> Transactions
        {
            get {return _transactions;}
        }

        // Method to add an account to the Bank //
        public void AddAccount(Account account)
        {
            this._accounts.Add(account);
        }
        // Method to get an account by the provided name //
        public Account GetAccount(String name)
        {
            // Iterating through the list of accounts //
            foreach (Account account in this._accounts)
            {
                // Comparing the passed name with the list of accounts //
                // StringComparison is an enumeration type that is used to specify how the comparison is done //
                // OrdinalIgnoreCase is used to make the search character insensitive //
                if (account.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return account;
                }
            }
            // Else it returns a null //
            return null;
        }

        // Method to get the transaction by index //
        public Transaction GetTransaction(int index)
        {
            // Check if the index is within the bounds of the transaction list //
            if (index >= 0 && index < this._transactions.Count)
            {
                return this._transactions[index];
            }
            // Else it returns a null //
            return null;
        }

        // Method to determine if an account exists for the ID //
        public bool GetAccountByID(int id)
        {
            // Iterate through the list of accounts
            foreach (Account account in this._accounts)
            {
                if (account.ID == id)
                {

                    return true;
                }
            }
            return false;
        }

        // Method to execute the transaction //
        public void ExecuteTransaction(Transaction transaction)
        {
            // Adding the transaction to the list //
            this._transactions.Add(transaction);

            // Executing the transaction //
            transaction.Execute();
        }

        // Method to rollback a transaction //
        public void RollbackTransaction(Transaction transaction)
        {
            // Rollback the transaction //
            transaction.Rollback();
        }

        // Method to iterate over the transactions //
        public void PrintTransactionHistory()
        {
            Console.WriteLine("Transaction History: \n");

            // Saving the current console color //
            var originalColor = Console.ForegroundColor;

            // Setting the color to blue //
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int i = 0; i < this._transactions.Count; i++)
            {
                Console.WriteLine($"Transaction {i + 1}: ");

                // Reset the color after printing header //
                Console.ForegroundColor = originalColor;

                // Print the transaction details //
                this._transactions[i].Print();

                // Set the color back to blue again for the header //
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
            }

            // Reset to original color //
            Console.ForegroundColor = originalColor;
        }
        
    }
}
