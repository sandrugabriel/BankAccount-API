using BankAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BankAccounts.Helpers
{
    public class TestBankAccountFactory
    {

        public static BankAccount CreateBankAccount(int id)
        {
            return new BankAccount
            {
                Id = id,
                Balance = (id*10) *5,
                Name = "test",
                Type = "salary"

            };
        }

        public static List<BankAccount> CreateBankAccounts(int count)
        {

            List<BankAccount> list = new List<BankAccount>();
            for (int i = 1; i < count; i++)
            {
                list.Add(CreateBankAccount(i));
            }

            return list;
        }
    }
}
