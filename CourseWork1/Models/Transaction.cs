using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork1.Models
{
//creating transaction class 
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionName { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
        public string Tags { get; set; }
        public DateTime DebtDueDate { get; set; }
        public string DebtSource { get; set; }
        public int User_id { get; set; }
    }
}
