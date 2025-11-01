namespace HW2
{
    public interface IBankAccountStorage
    {
        void AddBankAccount(int id, string name, decimal balance);
        void DeleteBankAccount(int id);
        BankAccount? GetBankAccount(int id);

        List<BankAccount> GetAllBankAccounts();
    }
}