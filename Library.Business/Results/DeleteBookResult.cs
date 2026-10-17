using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Results
{
    public enum DeleteBookResult
    {
        Deleted,
        NotFound,
        HasLoans
    }
}
