using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMS.Infrastructure.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public long InvoiceId { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public ICollection<InvoiceEntry> InvoiceEntries { get; } = new List<InvoiceEntry>();
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime IssueDate { get; set; }
        //ToDo: Хранить время истечения инвойса с учетом банковских дней
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
