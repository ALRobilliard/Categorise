using System;

namespace Categorise.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public int AccountType { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}