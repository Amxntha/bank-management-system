namespace BankProjectV4
{
    internal class BankSystem
    {
        static void Main(string[] args)
        {
            // Creating a bank object //
            Bank bank = new Bank();
            // Creating account objects //
            //Account myAccount = new Account("Amantha Kulathunga", 1090007851, 12500);
            //Account savingsAccount = new Account("Hiranthi Kulathunga", 109300784, 5000);

            //assigning the returned value from option to a variable of the same data type//
            MenuOption userSelection;
            do
            {
                //assinging the returned value from the readuseroption method into a variable//
                userSelection = ReadUserOption();
                //clearing the terminal//
                Console.Clear();
                Console.WriteLine($"You have selected {(int)userSelection}). {userSelection}, page");

                //switch case to execute corresponding method of the chosen screen//
                switch (userSelection)
                {
                    case MenuOption.AddAccount:
                        AddAccount(bank);
                        break;
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;
                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;
                    case MenuOption.Print:
                        DoPrint(bank);
                        break;
                    case MenuOption.TransactionHistory:
                        DoRollback(bank);
                        break;
                }
            } while (userSelection != MenuOption.Quit);
        }
        static MenuOption ReadUserOption()
        {
            //declare a variable option and initialise it to the value of 'quit + 1' (7)//
            MenuOption selection = MenuOption.Quit + 1;
            do
            {
                //options on the menu//
                Console.WriteLine("Welcome to the main menu of your online Banking account!");
                Console.WriteLine("1). Add an Account");
                Console.WriteLine("2). Withdraw");
                Console.WriteLine("3). Deposit");
                Console.WriteLine("4). Transfer");
                Console.WriteLine("5). Print Account Details");
                Console.WriteLine("6). Print Transaction History");
                Console.WriteLine("7). Quit");
                Console.Write("Please select one of the menu options: ");

                //using try parse to check if the selection is an integer and Enum.IsDefined to see if it matches the integers mapped 
                //to each of the Enumerators//
                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(MenuOption), choice))
                {
                    //if both are true, assigning the choice to a selection variable by converting it to MenuOption type//
                    selection = (MenuOption)choice;
                }
                else
                {
                    //else print an error message//
                    Console.WriteLine("Invalid choice. Please enter an integer between 1-7");
                }
                Console.WriteLine();
                //loop continues until the input matches an integer mapped to an enumerator//
            } while (!Enum.IsDefined(typeof(MenuOption), (int)selection));
            return selection;
        }
        //method to carry out withdrawal functions//
        static void DoWithdraw(Bank bank)
        {
            Account accountReturned = FindAccount(bank);

            if (accountReturned != null)
            {
                Console.Write("Please enter the amount you want to withdraw: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                {
                    WithdrawTransaction transaction = new WithdrawTransaction(accountReturned, withdrawAmount);
                    //executing the transaction and handling it in the case of an exception//
                    try
                    {
                        bank.ExecuteTransaction(transaction);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Transaction failed: {e.Message}");
                        Console.WriteLine();
                    }
                    //Printing the transaction to the user//
                    transaction.Print();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Please enter an integer amount.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Withdraw transaction failed!");
                Console.WriteLine();
            }
        }
        //method to carry out deposit functions//
        static void DoDeposit(Bank bank)
        {
            Account accountReturned = FindAccount(bank);

            if (accountReturned != null)
            {
                Console.Write("Please enter the amount you want to deposit: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                {
                    DepositTransaction transaction = new DepositTransaction(accountReturned, depositAmount);

                    //executing the transaction and handling it in the case of an exception//
                    try
                    {
                        bank.ExecuteTransaction(transaction);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Transaction failed: {e.Message}");
                        Console.WriteLine();

                    }
                    //printing the transaction to the user//
                    transaction.Print();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Please enter an integer amount.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Deposit transaction failed!");
                Console.WriteLine();
            }
        }
        // Method to carry out transfer functions // 
        static void DoTransfer(Bank bank)
        {
            // Obtaining the from account //
            Console.Write("From Account (Credit): ");
            Account fromAccount = FindAccount(bank);

            if (fromAccount != null)
            {
                // Obtaining the to account //
                Console.Write("To Account (Debit): ");
                Account toAccount = FindAccount(bank);

                if (toAccount != null)
                {
                    //asking the user to enter the amount to transfer//
                    Console.Write("Please enter the amount you want to transfer: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
                    {
                        TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, transferAmount);
                        Console.WriteLine();
                        //asking the transaction to print and handling it incase of an exception//
                        try
                        {
                            bank.ExecuteTransaction(transaction);
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine($"Transaction failed: {e.Message}");
                            Console.WriteLine();
                        }
                        //printing the transaction to the user//
                        transaction.Print();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Please enter an integer amount.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Enter a valid debit account. Closing Transfer transaction.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Enter a valid credit account. Closing Transfer transaction");
                Console.WriteLine();
            }

        }
        // Method to add a new account //
        static void AddAccount(Bank bank)
        {
            Account validName = null;
            string name;

            // Prompt the user to enter the account name //
            Console.Write("Please enter the new account name: ");
            do
            {
                name = Console.ReadLine();
                validName = bank.GetAccount(name);

                if (validName != null)
                {
                    Console.Write("An account exists under this name. Try again: ");
                }
                else if (name == "")
                {
                    Console.Write("Please enter a valid name. Try again: ");
                }
            } while ((validName != null) || (name == ""));
           
            //Prompt the user to enter the account ID //
            Console.Write("Please enter the account ID: ");
            bool validID;
            int id;

            do
            {
                while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                {
                    Console.Write("Please enter a valid integer ID. Try again: ");
                }

                // checking if an account exists for that ID //
                validID = bank.GetAccountByID(id);

                if (validID)
                {
                    Console.Write("ID already in use. Please try again: ");
                }
            
            } while (validID);


            //Prompt the user to enter the starting balance //
            Console.Write("Please enter the starting balance: ");
            decimal balance;

            // Validate balance input
            while (!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0)
            {
                Console.Write("Please enter a valid positive number for the balance. Try again:");
            }

            // Create a new Account object with the obtained details //
            Account newAccount = new Account(name, id, balance);

            // Add the account to the bank using its AddAccount method //
            bank.AddAccount(newAccount);

            Console.WriteLine("New account added successfully..!");
            Console.WriteLine();
        }
        // Method to find account //
        private static Account FindAccount(Bank bank)
        {
            // Prompt the user to enter the account name for the transaction//
            Console.Write("Enter the account name related to this function: ");
            string name = Console.ReadLine();

            // Search for the account using the Bank object's GetAccount method //
            Account account = bank.GetAccount(name);

            // if and else statement to notify the user about the search outcome //
            if (account != null)
            {
                Console.WriteLine($"Account search successful...!");
            }
            else
            {
                Console.WriteLine("Account search unsuccessful...!");
                Console.WriteLine();
            }

            // Return the result of the search operation
            return account;
        }
        //method to print information//
        static void DoPrint(Bank bank)
        {
            // Obtaining the account //
            Account returnedAccount = FindAccount(bank);

            // Error message in case account not returned //
            if (returnedAccount != null)
            {
                returnedAccount.Print();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Print procedure failed! Enter a valid account name.");
                Console.WriteLine();
            }
        }
        // Method to print transaction history and rollback //
        static void DoRollback(Bank bank)
        {
            bool failure = true;
            // Printing transaction history //
            bank.PrintTransactionHistory();
            if (bank.Transactions.Count > 0)
            {
                // Prompting the user if they want to rollback a transaction //
                Console.Write("Do you want to rollback a specific transaction? (yes/no): ");
                string response = Console.ReadLine().Trim().ToLower();
                while (failure)
                {
                    if (response == "yes")
                    {
                        Console.Write("Enter the index of the transaction to rollback: ");
                        if (int.TryParse(Console.ReadLine(), out int transactionIndex) && transactionIndex > 0)
                        {
                            // Attempting to find the transaction //
                            var returnedTransaction = bank.GetTransaction(transactionIndex - 1);
                            try
                            {
                                if (returnedTransaction != null)
                                {
                                    // Attempting to rollback the transaction 
                                    bank.RollbackTransaction(returnedTransaction);
                                    failure = false;
                                }
                                else
                                {
                                    Console.WriteLine("No transaction corresponding to the index found. Returning to main menu.");
                                    Console.WriteLine();
                                    failure = false;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Rollback failed: {e.Message}");
                                Console.WriteLine();
                                failure = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please ensure the index entered is an integer and above 0.");
                            Console.WriteLine();
                        }
                    }
                    else if (response == "no")
                    {
                        Console.WriteLine();
                        failure = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Returning to main menu.");
                        Console.WriteLine();
                        failure = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("No transactions have been carried out. Returning to main menu");
                Console.WriteLine();
            }
        }
        enum MenuOption
        {
            AddAccount = 1,
            Withdraw = 2,
            Deposit = 3,
            Transfer = 4,
            Print = 5,
            TransactionHistory = 6,
            Quit = 7,
        }
    }
}
