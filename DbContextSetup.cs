using System;
using System.Collections.Generic;
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

            public void checkInsurance(string AccountNo)
            {
                _ctx.Database.ExecuteSqlCommand(
                    "insert into \"Check\" values(@aid,@expirationDate,@paperNo)",
                    new SqlParameter("aid", Int32.Parse(AccountNo)),
                    new SqlParameter("expirationDate", "2015-09-22"),
                    new SqlParameter("paperNo", 10)
                );
            }

            public void creditCardIssue_AccountCard(string accountNo)
            {
                var x = _ctx.Database.SqlQuery<Person>("Select * from person").ToArray();
            }

            
        }

}

