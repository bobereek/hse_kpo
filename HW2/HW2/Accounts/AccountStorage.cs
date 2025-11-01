namespace HW2
{
    public class AccountStorage: IBankAccountStorage
    {
        private Dictionary<int, BankAccount> BankAccounts = new Dictionary<int, BankAccount>();
        private IBankAccountFactory bankFactory;
        public AccountStorage(IBankAccountFactory bankFactory)
        {
            this.bankFactory = bankFactory;
        }

        public void AddBankAccount(int id, string name, decimal balance)
        {
            if (id <= 0)
                throw new ArgumentException("Account ID must be positive", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account name cannot be empty", nameof(name));
            if (balance < 0)
                throw new ArgumentException("Initial balance cannot be negative", nameof(balance));

            if (BankAccounts.ContainsKey(id))
                throw new InvalidOperationException($"Bank account with id {id} already exists");

            BankAccounts.Add(id, bankFactory.CreateBankAccount(id, name, balance));
        }

        public void DeleteBankAccount(int id)
        {
            if (!BankAccounts.ContainsKey(id))
                throw new ArgumentException($"Bank account with id {id} not found", nameof(id));

            BankAccounts.Remove(id);
        }

        public BankAccount? GetBankAccount(int id)
        {
            return BankAccounts.TryGetValue(id, out var bankAccount) ? bankAccount : null;
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            return BankAccounts.Values.ToList();
        }
    }
}