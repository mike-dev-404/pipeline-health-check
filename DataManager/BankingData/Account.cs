namespace BankingData
{
    public class Account
    {
        public required User User { get; set; }

        public int AuthorizationCode { get; set; }
    }
}
