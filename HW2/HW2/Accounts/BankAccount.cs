namespace HW2
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(int id, string name, decimal balance) => (Id, Name, Balance) = (id, name, balance);
        public void EditName(string name) => Name = name;
        public void ApplyOperation(Operation operation)
        {
            if (operation.Type == CategoryType.Income)
            {
                Balance += operation.Amount;
            }
            else
            {
                Balance -= operation.Amount;
            }
        }
        public void RevertOperation(Operation operation)
        {
            if (operation.Type == CategoryType.Income)
            {
                Balance -= operation.Amount;
            }
            else
            {
                Balance += operation.Amount;
            }
        }
    }
}