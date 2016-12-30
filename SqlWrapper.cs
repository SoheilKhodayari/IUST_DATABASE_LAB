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
            public List<decimal> DespositAccountRatio(string accNo)
            {
                decimal money = 0;
                List<decimal> ratioes = new List<decimal>();
                DepositAccount da = _ctx.DepositAccounts.Where(x => x.AId.ToString() == accNo).FirstOrDefault();
                if(da == null)
                {
                    return ratioes;
                }

                int? interest = da.Branch.Bank.Interest;
                if (interest == null) interest = 20;
                if (interest != null)
                {
                    money = (da.Remainder * decimal.Parse(interest.ToString())) / 100;
                }

                ratioes.Add((decimal)interest);
                ratioes.Add(money / 12);
                ratioes.Add(money);
                return ratioes;
            }
            public void transferMoney(string srcAcc, ACCOUNT_TYPES src, string destAcc, ACCOUNT_TYPES dest, string money)
            {
                this.withdraw(srcAcc, src, money);
                this.deposit(destAcc, dest, money);
            }


            private static Random random = new Random();
            public static string RandomString(int length)
            {
                const string chars = "0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            public List<string> creditCradIssuance(int type, string number)
            {
                int id = -1;
                List<string> result = new List<string>();
                DateTime dt = DateTime.Now;
                string date = (dt.Year + 6).ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
                Random rnd = new Random();
                int cvv2 = rnd.Next(100, 9999);
                int spass = rnd.Next(10000, 100000);
                if (type == 0) // number is accNo
                {
                    SavingAccount acc = _ctx.SavingAccounts.Where(x => x.AId.ToString() == number).First();

                    CreditCard c = new CreditCard
                    {
                        CardNumber = RandomString(16),
                        Remainder = acc.Remainder,
                        ExpirationDate = new DateTime().AddYears(5),
                        CVV2 = cvv2.ToString(),
                        Password = "12345",
                        SecondaryPassword = spass.ToString(),
                        CardType = true,
                        AId_FK = acc.AId
                    };
                    _ctx.CreditCards.Add(c);
                    _ctx.SaveChanges();

                    result.AddRange(new string[] { c.CardNumber.ToString(), c.Remainder.ToString(), c.ExpirationDate.ToString(), cvv2.ToString(), "12345", spass.ToString() });
                }
                else // number is money amount
                {
                    CreditCard c = new CreditCard
                    {
                        CardNumber = RandomString(16),
                        Remainder = decimal.Parse(number),
                        ExpirationDate = new DateTime().AddYears(5),
                        CVV2 = cvv2.ToString(),
                        Password = "12345",
                        SecondaryPassword = spass.ToString(),
                        CardType = false,
                        AId_FK = null
                    };
                    _ctx.CreditCards.Add(c);
                    _ctx.SaveChanges();
                    result.AddRange(new string[] { id.ToString(), number.ToString(), date.ToString(), cvv2.ToString(), "12345", spass.ToString() });
                }

                return result;
            }


    }
}
