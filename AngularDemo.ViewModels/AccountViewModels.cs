using System;
using System.Collections.Generic;

namespace AngularDemo.ViewModels
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public abstract class DataTableSearch : IDataTableSearch
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Guid? BranchId { get; set; }
    }

    public class Search : DataTableSearch { }

    public class DataTableResult<T> : IDataTableResult<T>
    {
        public DataTableResult() { }

        public DataTableResult(DataTableResult<T> dataTableResult)
        {
            Data = dataTableResult.Data;
            RecordsFiltered = dataTableResult.RecordsFiltered;
            Draw = dataTableResult.Draw;
        }

        public int Draw { get; set; }
        public int RecordsTotal => RecordsFiltered;
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }

    public class DropDown
    {
        public Guid Value { get; set; }
        public string Text { get; set; }
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string Type { get; set; }
        //public LookUpType? Type { get; set; }

        //public string TypeName => Type?.ToString().Replace('_', ' ');
    }
}
