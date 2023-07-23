using System.ComponentModel.DataAnnotations;

namespace LEDGER_BOOK.Models
{
    public class CreditStatement
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Product { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
    }
}
