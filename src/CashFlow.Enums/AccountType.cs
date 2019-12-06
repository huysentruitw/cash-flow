using System;
using System.Linq.Expressions;

namespace CashFlow.Enums
{
    public enum AccountType
    {
        Unknown = 0,
        CashAccount = 1,
        CurrentAccount = 2,
        SavingsAccount = 3
    }

    //public class Account
    //{
    //    public string Name { get; set; }
        
    //    public AccountType AccountType { get; set; }
    //}

    //public abstract class View<T>
    //{
    //    public abstract void ConfigureView();

    //    protected object ForField(Expression<Func<T, object>> expression)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class AccountView : View<Account>
    //{
    //    public override void ConfigureView()
    //    {
    //        ForField(x => x.AccountType).AsDropDown();
    //    }
    //}
}
