namespace HW2
{
    public interface IBankAccountFactory
    {
        public BankAccount CreateBankAccount(int id, string name, decimal balance);
    }
}