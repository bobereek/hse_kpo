namespace HW2
{
    public class Operation
    {
        public int Id { get; set; }
        public CategoryType Type { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        public Operation(int id, CategoryType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string? description)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }
    }
}