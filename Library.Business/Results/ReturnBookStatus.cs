using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Results
{
    public enum ReturnBookStatus
    {
        Success,
        LoanNotFound,
        NotOwner,
        AlreadyReturned
    }
}
