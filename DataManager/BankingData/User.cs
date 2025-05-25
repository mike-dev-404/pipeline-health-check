namespace BankingData
{
    public class User
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public DateOnly Birthday { get; set; }

        public required string Email { get; set; }

        public required string Nationality { get; set; }
    }
}
