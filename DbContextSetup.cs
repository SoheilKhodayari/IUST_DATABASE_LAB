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
        public class DbHelper
        {
            private BankDBContext _ctx;
            private static DbHelper _dbHelper = new DbHelper();

            private DbHelper()
            {
                _ctx = new BankDBContext();
            }

            public static DbHelper getInstance()
            {       
                return _dbHelper;
            }

            public enum ACCOUNT_TYPES
            {
                Saving_Account,
                Current_Account,
                Deposite_Account,
                Foreign_Currency_Account
            };

            public int openAccount(string name, string ssn, string gender, string birthdate, string phone,
                string mobile, string address, ACCOUNT_TYPES type, string balance, string branchid)
            {
                int id = -1;
                var x = _ctx.Database.SqlQuery<Person>("Select * from person where ssn=@ssn",
                                                        new SqlParameter("ssn", ssn)).FirstOrDefault();
                if (x == null)
                {
                    SqlParameter addressParam;
                    if (address == null) 
                        addressParam = new SqlParameter("address", DBNull.Value);
                    else
                        addressParam = new SqlParameter("address", address);

                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                        "insert into Person values (@name, @address, @gender, @birthdate, @ssn);select @@IDENTITY;",
                        new SqlParameter("name", name),
                        addressParam,
                        new SqlParameter("gender", int.Parse(gender)),
                        new SqlParameter("birthdate", birthdate),
                        new SqlParameter("ssn", ssn)).Single()
                        );
                    
                    
                    _ctx.Database.ExecuteSqlCommand(
                        "insert into Customer values (@cid)",
                        new SqlParameter("cid", id)
                        );

                        
                    if (phone != null)
                    {
                        _ctx.Database.ExecuteSqlCommand(
                            "insert into Person_Phone values (@cid, @phone)",
                            new SqlParameter("cid", id),
                            new SqlParameter("phone", phone)
                        );
                    }

                    if (mobile != null)
                    {
                        _ctx.Database.ExecuteSqlCommand(
                                "insert into Person_Phone values (@cid, @mobile)",
                                new SqlParameter("cid", id),
                                new SqlParameter("mobile", mobile)
                            );
                    }
                     
                }
                else
                {
                    id = x.PId;
                    var customer = _ctx.Database.SqlQuery<Customer>("Select * from Customer where Cid=@cid",
                                                        new SqlParameter("cid", id)).FirstOrDefault();
                    if (customer == null)
                    {
                        _ctx.Database.ExecuteSqlCommand(
                            "insert into Customer values (@cid)",
                            new SqlParameter("cid", id)
                            );

                        if (phone != null)
                        {
                            _ctx.Database.ExecuteSqlCommand(
                                    "insert into Person_Phone values (@cid, @phone)",
                                    new SqlParameter("cid", id),
                                    new SqlParameter("phone", phone)
                                );
                        }

                        if (mobile != null)
                        {
                            _ctx.Database.ExecuteSqlCommand(
                                    "insert into Person_Phone values (@cid, @mobile)",
                                    new SqlParameter("cid", id),
                                    new SqlParameter("mobile", mobile)
                                );
                        }
                    }
                }
                
                if (type == ACCOUNT_TYPES.Saving_Account)
                {
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                        "insert into SavingAccount values(@remainder, @opendate, @cid_fk, @branchid_fk);select @@IDENTITY;",
                        new SqlParameter("remainder", balance),
                        new SqlParameter("opendate", System.Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd")),
                        new SqlParameter("cid_fk", id),
                        new SqlParameter("branchid_fk", int.Parse(branchid))).Single()
                        );
                }
                else if (type == ACCOUNT_TYPES.Current_Account)
                {
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                        "insert into CurrentAccount values(@remainder, @opendate, @cid_fk, @branchid_fk);select @@IDENTITY;",
                        new SqlParameter("remainder", balance),
                        new SqlParameter("opendate", System.Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd")),
                        new SqlParameter("cid_fk", id),
                        new SqlParameter("branchid_fk", int.Parse(branchid))).Single()
                        );
                }
                else if (type == ACCOUNT_TYPES.Deposite_Account)
                {
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                        "insert into DepositAccount values(@remainder, @opendate, @cid_fk, @branchid_fk);select @@IDENTITY;",
                        new SqlParameter("remainder", balance),
                        new SqlParameter("opendate", System.Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd")),
                        new SqlParameter("cid_fk", id),
                        new SqlParameter("branchid_fk", int.Parse(branchid))).Single()
                        );
                }
                else if (type == ACCOUNT_TYPES.Foreign_Currency_Account)
                {
                    // -- insert
                }

                return id;
            }

            public SavingAccount getSavingAccDetails(string accNo)
            {
                var Acc = _ctx.Database.SqlQuery<SavingAccount>("Select * from DepositAccount where Aid=@accNo",
                                                        new SqlParameter("accNo", accNo)).FirstOrDefault();
                return Acc;
            }

            public DepositAccount getDepositAccDetails(string accNo)
            {
                var Acc = _ctx.Database.SqlQuery<DepositAccount>("Select * from DepositAccount where Aid=@accNo",
                                                        new SqlParameter("accNo", accNo)).FirstOrDefault();
                return Acc;
            }

            public CurrentAccount getCurrentAccDetails(string accNo)
            {
                var Acc = _ctx.Database.SqlQuery<CurrentAccount>("Select * from CurrentAccount where Aid=@accNo",
                                                        new SqlParameter("accNo", accNo)).FirstOrDefault();
                return Acc;
            }

            public int updateAccountOwnerDetails(string phone, string mobile, string address, string accNo, ACCOUNT_TYPES AccType)
            {
                int Cid = -1;
                if (AccType == ACCOUNT_TYPES.Saving_Account)
                {
                    Cid = getSavingAccDetails(accNo).CId_FK;
                }else if(AccType == ACCOUNT_TYPES.Deposite_Account)
                {
                    Cid = getDepositAccDetails(accNo).CId_FK;
                }
                else if (AccType == ACCOUNT_TYPES.Current_Account)
                {
                    Cid = getCurrentAccDetails(accNo).CId_FK;
                }

                _ctx.Database.ExecuteSqlCommand(
                        "update Person set address=@address where pid=@cid",
                        new SqlParameter("cid", Cid)
                    );

                _ctx.Database.ExecuteSqlCommand(
                        "DELETE FROM Person_Phone WHERE pid_fk=@cid",
                        new SqlParameter("cid", Cid)
                    );

                _ctx.Database.ExecuteSqlCommand(
                        "insert into Person_Phone values (@cid, @phone)",
                        new SqlParameter("cid", Cid),
                        new SqlParameter("phone", phone)
                    );
                
                _ctx.Database.ExecuteSqlCommand(
                        "insert into Person_Phone values (@cid, @mobile)",
                        new SqlParameter("cid", Cid),
                        new SqlParameter("mobile", mobile)
                    );

                return Cid;
            }

            public void removeAccount(string accNo, ACCOUNT_TYPES type)
            {
                if (type == ACCOUNT_TYPES.Saving_Account) 
                {
                    _ctx.Database.ExecuteSqlCommand(
                        "DELETE FROM SavingAccount WHERE AId=@accNo",
                        new SqlParameter("accNo", accNo)
                    );
                }
                else if (type == ACCOUNT_TYPES.Deposite_Account)
                {
                    _ctx.Database.ExecuteSqlCommand(
                        "DELETE FROM DepositAccount WHERE AId=@accNo",
                        new SqlParameter("accNo", accNo)
                    );
                }
                else if (type == ACCOUNT_TYPES.Current_Account)
                {
                    _ctx.Database.ExecuteSqlCommand(
                        "DELETE FROM CurrentAccount WHERE AId=@accNo",
                        new SqlParameter("accNo", accNo)
                    );
                }
            }

            public void withdraw(string AccNo, ACCOUNT_TYPES type, string money)
            {
                decimal amount = 0;
                if (type == ACCOUNT_TYPES.Saving_Account)
                {
                    amount = getSavingAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update SavingAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount-decimal.Parse(money))
                    );
                }
                else if (type == ACCOUNT_TYPES.Deposite_Account)
                {
                    amount = getDepositAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update DepositAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount - decimal.Parse(money))
                    );
                }
                else if (type == ACCOUNT_TYPES.Current_Account)
                {
                    amount = getCurrentAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update CurrentAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount - decimal.Parse(money))
                    );
                }
            }

            public void deposit(string AccNo, ACCOUNT_TYPES type, string money)
            {
                decimal amount = 0;
                if (type == ACCOUNT_TYPES.Saving_Account)
                {
                    amount = getSavingAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update SavingAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount + decimal.Parse(money))
                    );
                }
                else if (type == ACCOUNT_TYPES.Deposite_Account)
                {
                    amount = getDepositAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update DepositAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount + decimal.Parse(money))
                    );
                }
                else if (type == ACCOUNT_TYPES.Current_Account)
                {
                    amount = getCurrentAccDetails(AccNo).Remainder;
                    _ctx.Database.ExecuteSqlCommand(
                        "update CurrentAccount set money=@money where AID=@accNo",
                        new SqlParameter("money", amount + decimal.Parse(money))
                    );
                }
            }

            public Bank getBankOfBranch(string branchId)
            {
                int bankid = _ctx.Database.SqlQuery<int>("Select bankid_fk from Branch where branchid=@bid",
                                                        new SqlParameter("bid", branchId)).FirstOrDefault();
                
                Bank bank = _ctx.Database.SqlQuery<Bank>("Select * from Bnak where bnakid=@bankid",
                                                        new SqlParameter("bankid", bankid)).FirstOrDefault();
                return bank;
            }

            public Loan getLoanDetails(string loanId)
            {
                Loan loan = _ctx.Database.SqlQuery<Loan>("Select * from Loan where loanid=@lid",
                                                        new SqlParameter("lid", loanId)).FirstOrDefault();

                return loan;
            }

            public List<decimal> DespositAccountRatio(string accNo)
            {
                decimal money = 0;
                DepositAccount da = getDepositAccDetails(accNo);
                int? interest = getBankOfBranch(da.BranchId_FK.ToString()).Interest;
                if (interest!=null)
                {
                    money = (da.Remainder * decimal.Parse(interest.ToString())) / 100;
                }

                List<decimal> ratioes = new List<decimal>();
                ratioes.Add(money / 12);
                ratioes.Add(money);

                return ratioes;
            }

            public List<decimal> loanRatio(string loanId)
            {
                decimal money = 0;
                Loan loan = getLoanDetails(loanId);
                int? interest = loan.Interest;
                if (interest != null)
                {
                    money = (loan.Amount * decimal.Parse(interest.ToString())) / 100;
                }

                List<decimal> ratioes = new List<decimal>();
                ratioes.Add(money / 12);
                ratioes.Add(money);

                return ratioes;
            }
            
            public int checkInsurance(string accNo)
            {
                int id = -1;
                if (getCheckOfAccount(accNo) == null)
                {
                    DateTime dt = DateTime.Now;
                    string date = (dt.Year + 2).ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                        "insert into \"Check\" values(@aid,@expirationDate,@paperNo);select @@IDENTITY;",
                        new SqlParameter("aid", Int32.Parse(accNo)),
                        new SqlParameter("expirationDate", date),
                        new SqlParameter("PaperNumber", 10)).Single()
                    );
                }

                return id;
            }

            public Check getCheckOfAccount(string accNo)
            {
                Check check = _ctx.Database.SqlQuery<Check>("Select * from \"Check\" where Aid=@accNo and PaperNumber > 0",
                                                        new SqlParameter("accNo", accNo)).FirstOrDefault();
                return check;
            }

            public int controlCheckPaper(string accNo, string recvName, string recvDate, string Amount, string checkId = null)
            {
                int paperId = -1;
                CurrentAccount ca = getCurrentAccDetails(accNo);
                Check check = getCheckOfAccount(accNo);
                if ( check != null)
                {
                    if (ca.Remainder >= decimal.Parse(Amount))
                    {
                        _ctx.Database.ExecuteSqlCommand(
                            "update \"Check\" set PaperNumber=@paperNo where AID=@accNo and checkId=@checkId",
                            new SqlParameter("paperNo", check.PaperNumber-1),
                            new SqlParameter("accNo", accNo),
                            new SqlParameter("checkId", check.CheckId)
                        );
                        withdraw(accNo, ACCOUNT_TYPES.Current_Account, Amount);
                        paperId = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                            "insert into \"CheckPaper\" values(@amount,@receiver,@recvdate);select @@IDENTITY;",
                            new SqlParameter("amount", Amount),
                            new SqlParameter("receiver", recvName),
                            new SqlParameter("recvdate", System.Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"))).Single()
                        );
                    }
                }
                return paperId;
            }

            //-- currentAccount
            public void transferMoney(string srcAcc, string destAcc, string money)
            {
                withdraw(srcAcc, ACCOUNT_TYPES.Current_Account, money);
                deposit(destAcc, ACCOUNT_TYPES.Current_Account, money);
            }

            public int loanRequest(string amount, string intrest, string retDuration, string branchid_fk, string cid_fk)
            {
                int id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                            "insert into loan values(@amount,@intrest,@ret,@bid,@cid);select @@IDENTITY;",
                            new SqlParameter("amount", amount),
                            new SqlParameter("intrest", intrest),
                            new SqlParameter("ret", retDuration),
                            new SqlParameter("bid", branchid_fk),
                            new SqlParameter("cid", cid_fk)).Single()
                        );
                return id;
            }

            public int creditCradInsurance(int type, string number, string branchId)
            {
                int id = -1;
                DateTime dt = DateTime.Now;
                string date = (dt.Year + 6).ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
                Random rnd = new Random();
                int cvv2 = rnd.Next(10000, 100000);
                int spass = rnd.Next(10000, 100000);
                if(type == 0) // number is accNo
                {
                    decimal amount = getSavingAccDetails(number).Remainder;
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                            "insert into CreditCard values(@rem,@exp,@cvv2,@pass,@spass,@type,@aid);select @@IDENTITY;",
                            new SqlParameter("rem", amount),
                            new SqlParameter("exp", date),
                            new SqlParameter("cvv2", cvv2),
                            new SqlParameter("pass", "12345"),
                            new SqlParameter("spass", spass),
                            new SqlParameter("type", type),
                            new SqlParameter("aid", number)).Single()
                        );
                }
                else // number is money amount
                {
                    id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                            "insert into CreditCard values(@rem,@exp,@cvv2,@pass,@spass,@type,@aid);select @@IDENTITY;",
                            new SqlParameter("rem", number),
                            new SqlParameter("exp", date),
                            new SqlParameter("cvv2", cvv2),
                            new SqlParameter("pass", "12345"),
                            new SqlParameter("spass", spass),
                            new SqlParameter("type", type),
                            new SqlParameter("aid", DBNull.Value)).Single()
                        );
                }
                
                return id;
            }

            public List<string> getForeignCurrencies(string branchId)
            {
                List<string> currencies = _ctx.Database.SqlQuery<string>(
                                                "Select Currency from Available_Foreign_Currency where BranchId_FK=@bid",
                                                new SqlParameter("bid", branchId)
                                                ).ToList();

                return currencies;
            }

            public int addAbsence(string ssn)
            {
                Staff staff = getStaffBySSN(ssn);
                int success = -1;
                if (staff != null)
                {
                    success = _ctx.Database.ExecuteSqlCommand(
                        "update Staff set AbsenceCount=@abs where sid=@sid",
                        new SqlParameter("abs", staff.AbsenceCount+1),
                        new SqlParameter("sid", staff.SId)
                    );
                }
                else
                {
                    Boss boss = getBossBySSN(ssn);
                    success = _ctx.Database.ExecuteSqlCommand(
                        "update Staff set AbsenceCount=@abs where bid=@bid",
                        new SqlParameter("abs", boss.AbsenceCount + 1),
                        new SqlParameter("bid", boss.BId)
                    );
                }
                
                return success;
            }
            
            public Staff getStaffBySSN(string ssn)
            {
                Person p = getPersonBySSN(ssn);
                Staff staff = _ctx.Database.SqlQuery<Staff>("Select * from Staff where sid=@sid",
                                                        new SqlParameter("sid", p.PId)).FirstOrDefault();

                return staff;
            }

            public Boss getBossBySSN(string ssn)
            {
                Person p = getPersonBySSN(ssn);
                Boss boss = _ctx.Database.SqlQuery<Boss>("Select * from Boss where bid=@bid",
                                                        new SqlParameter("bid", p.PId)).FirstOrDefault();
                return boss;
            }
            public Person getPersonBySSN(string ssn)
            {
                Person p = _ctx.Database.SqlQuery<Person>("Select * from person where ssn=@ssn",
                                                        new SqlParameter("ssn", ssn)).FirstOrDefault();
                return p;
            }

            public Staff getStaffById(string id)
            {
                Person p = getPersonById(id);
                Staff staff = _ctx.Database.SqlQuery<Staff>("Select * from Staff where sid=@sid",
                                                        new SqlParameter("sid", p.PId)).FirstOrDefault();

                return staff;
            }

            public Boss getBossById(string id)
            {
                Person p = getPersonById(id);
                Boss boss = _ctx.Database.SqlQuery<Boss>("Select * from Boss where bid=@bid",
                                                        new SqlParameter("bid", p.PId)).FirstOrDefault();
                return boss;
            }
            public Person getPersonById(string id)
            {
                Person p = _ctx.Database.SqlQuery<Person>("Select * from person where pid=@pid",
                                                        new SqlParameter("pid", id)).FirstOrDefault();
                return p;
            }

            public int createLottery(string name, string date, string decp, string branchId)
            {
                int id = decimal.ToInt32(_ctx.Database.SqlQuery<decimal>(
                            "insert into Lottery values(@name,@date,@decp,@barnch);select @@IDENTITY;",
                            new SqlParameter("name", name),
                            new SqlParameter("date", date),
                            new SqlParameter("decp", decp),
                            new SqlParameter("barnch", branchId)).Single()
                        );
                
                return id;
            }

            public int addParticipator(string lotId, string AccNo)
            {
                int success = _ctx.Database.ExecuteSqlCommand(
                            "insert into Lottery_SavingAccount_Participate values(@aid,@lot)",
                            new SqlParameter("aid", AccNo),
                            new SqlParameter("lot", lotId)
                        );
                
                return success;
            }

            public void addAllAccountAsParticipator(string lotId)
            {
                Lottery l = _ctx.Database.SqlQuery<Lottery>("Select * from Lottery where LotteryId=@lid",
                                                        new SqlParameter("lid", lotId)).FirstOrDefault();

                List<int> savingAccounts = _ctx.Database.SqlQuery<int>(
                                                        "Select AId from SavingAccount where BranchId_FK=@bid",
                                                        new SqlParameter("bid", l.BranchId_FK)).ToList();

                foreach (var accNo in savingAccounts)
                {
                    addParticipator(lotId, accNo.ToString());
                }
            }

            public List<Lottery> getAllLotteries(string branchId)
            {
                List<Lottery> lotteries = _ctx.Database.SqlQuery<Lottery>(
                                                        "Select * from Lottery where BranchId_FK=@bid",
                                                        new SqlParameter("bid", branchId)).ToList();
                return lotteries;
            }

            public Lottery getLottery(string lotId)
            {
                Lottery lottery = _ctx.Database.SqlQuery<Lottery>(
                                                        "Select * from Lottery where LotteryId=@lid",
                                                        new SqlParameter("lid", lotId)).FirstOrDefault();
                return lottery;
            }

            public int addWinner(string lotId, string AccNo)
            {
                int success = _ctx.Database.ExecuteSqlCommand(
                            "insert into Lottery_SavingAccount_Wins values(@aid,@lot)",
                            new SqlParameter("aid", AccNo),
                            new SqlParameter("lot", lotId)
                        );
                
                return success;
            }

            public void addRandomWinnersToLottery(string lotId, int count=3)
            {
                Lottery lot = getLottery(lotId);

                List<int> participates = _ctx.Database.SqlQuery<int>(
                                                    "Select AId from Lottery_SavingAccount_Participate where LotteryId=@lid",
                                                    new SqlParameter("lid", lot.LotteryId)).ToList();
                IEnumerable<int> winners;
                if (count < participates.Count())
                {
                    Random r = new Random();
                    winners = participates.OrderBy(x => r.Next()).Take(count);
                }
                else
                {
                    winners = participates;
                }

                foreach (var accNo in winners)
                {
                    addWinner(lotId, accNo.ToString());
                }
            }

        }

}