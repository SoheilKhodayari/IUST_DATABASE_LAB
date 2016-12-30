using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Models;


namespace WpfApplication1
{
    public class SqlWrapper
    {
            private BankDBContext _ctx;
            private static SqlWrapper _wrapper = new SqlWrapper();

            private SqlWrapper()
            {
                _ctx = new BankDBContext();
            }

            public static SqlWrapper getInstance()
            {
                return _wrapper;
            }
            public BankDBContext getDbContext()
            {
                return _ctx;
            }

            public void removeAccount(string accNo, ACCOUNT_TYPES type)
            {
                int accountId = Int32.Parse(accNo);
                switch(type)
                {
                    case ACCOUNT_TYPES.Saving_Account:
                        SavingAccount account1 = _ctx.SavingAccounts.Find(accountId);
                        _ctx.SavingAccounts.Remove(account1);
                        break;
                    case ACCOUNT_TYPES.Current_Account:
                        CurrentAccount account2 = _ctx.CurrentAccounts.Find(accountId);
                        _ctx.CurrentAccounts.Remove(account2);
                        break;
                    case ACCOUNT_TYPES.Foreign_Currency_Account:
                        ForeignCurrencyAccount account3 = _ctx.ForeignCurrencyAccounts.Find(accountId);
                        _ctx.ForeignCurrencyAccounts.Remove(account3);
                        break;
                    case ACCOUNT_TYPES.Deposite_Account:
                        DepositAccount account = _ctx.DepositAccounts.Find(accountId);
                        _ctx.DepositAccounts.Remove(account);
                        break;
                }
                _ctx.SaveChanges();
            }
            public int updateAccountOwnerDetails(string phone, string mobile, string address, string accountId, ACCOUNT_TYPES type)
            {
                int ret = 0;
                return ret;
            }
            
            public void withdraw(string AccNo, ACCOUNT_TYPES type, string money)
            {
                int accountId = Int32.Parse(AccNo);
                switch (type)
                {
                    case ACCOUNT_TYPES.Saving_Account:
                        SavingAccount account1 = _ctx.SavingAccounts.Find(accountId);
                        account1.Remainder -= decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Current_Account:
                        CurrentAccount account2 = _ctx.CurrentAccounts.Find(accountId);
                        account2.Remainder -= decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Foreign_Currency_Account:
                        ForeignCurrencyAccount account3 = _ctx.ForeignCurrencyAccounts.Find(accountId);
                        account3.Remainder -= decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Deposite_Account:
                        DepositAccount account4 = _ctx.DepositAccounts.Find(accountId);
                        account4.Remainder -= decimal.Parse(money);
                        break;
                }
                _ctx.SaveChanges();
            }
            public void deposit(string AccNo, ACCOUNT_TYPES type, string money)
            {
                int accountId = Int32.Parse(AccNo);
                switch (type)
                {
                    case ACCOUNT_TYPES.Saving_Account:
                        SavingAccount account1 = _ctx.SavingAccounts.Find(accountId);
                        account1.Remainder += decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Current_Account:
                        CurrentAccount account2 = _ctx.CurrentAccounts.Find(accountId);
                        account2.Remainder += decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Foreign_Currency_Account:
                        ForeignCurrencyAccount account3 = _ctx.ForeignCurrencyAccounts.Find(accountId);
                        account3.Remainder += decimal.Parse(money);
                        break;
                    case ACCOUNT_TYPES.Deposite_Account:
                        DepositAccount account4 = _ctx.DepositAccounts.Find(accountId);
                        account4.Remainder += decimal.Parse(money);
                        break;
                }
                _ctx.SaveChanges();
            }


    }
}
