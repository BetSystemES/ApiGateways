using System.ComponentModel.DataAnnotations;
using WebApiGateway.Models.CashService.Enums;

namespace WebApiGateway.Models.CashService.ViewModel
{
    public class TransactionViewModel
    {
        [Key] public Guid Id { get; set; }
        public CashTypeApi CashType { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}