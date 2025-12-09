using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectV4
{
    abstract class Transaction
    {
        // variables that are inherited by subclassess //
        protected decimal _amount;
        protected bool _success;

        // variables limited to this class //
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        // constructor //
        public Transaction(decimal amount)
        {
            this._amount = amount;
            this._success = false;
            this._executed = false;
            this._reversed = false;
            this._dateStamp = DateTime.Now;
        }

        // property to make success read only //
        public virtual bool Success
        {
            get { return _success; }
            // protected set to ensure that this can be changed only within this class and its inherited classes //
            protected set { _success = value; }
        }

        // property to make executed read only //
        public bool Executed
        {
            get { return _executed; }
            // protected set makes sure that this value can only be changed within this class and its inherited classes //
            protected set
            {
                _executed = value;
                // Update the datestamp when executed
                if (value) _dateStamp = DateTime.Now;  
            }
        }

        // property to make reversed read only. it uses similar implementation as Executed //
        public bool Reversed
        {
            get { return _reversed; }
            protected set
            {
                _reversed = value;
                if (value) _dateStamp = DateTime.Now;  
            }
        }

        // property to make datestamp read only //
        public DateTime DateStamp
        {
            get { return _dateStamp; }
        }

        // virtual print method to be overriden. it was italicized in the UML hence why its virtual //
        public virtual void Print()
        {
            Console.WriteLine($"Transaction Date: {DateStamp}");
            Console.WriteLine($"Amount: {_amount.ToString("C")}");
            Console.WriteLine($"Executed: {Executed}");
            Console.WriteLine($"Success: {Success}");
            Console.WriteLine($"Reversed: {Reversed}");
        }

        // virtual execute method to be overriden //
        public virtual void Execute()
        {
            Console.WriteLine("Transaction executing...");
        }    

        // virtual rollback method to be overriden //
        public virtual void Rollback()
        {
            Console.WriteLine("Transaction rolling back...");
        }
    }
}
