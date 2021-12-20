using System;
using System.Collections.Generic;
using FinanceAccounting.Models;

namespace FinanceAccounting.DataAccess.Initialization
{
    public static class TestData
    {
        public static List<BookkeepingUser> Users => new()
        {
            new BookkeepingUser { Id = 1 },
            new BookkeepingUser { Id = 2 },
            new BookkeepingUser { Id = 3 }
        };

        public static List<Transaction> Transactions => new()
        {
            new Transaction { Id = 1, UserId = 1, Date = new DateTime(2021, 12, 1), CategoryId = 1, Sum = 10000 },
            new Transaction { Id = 2, UserId = 1, Date = new DateTime(2021, 11, 5), CategoryId = 2, Sum = 5000 },
            new Transaction { Id = 3, UserId = 2, Date = new DateTime(2021, 12, 14), CategoryId = 1, Sum = 15000 },
            new Transaction { Id = 4, UserId = 2, Date = new DateTime(2021, 11, 23), CategoryId = 5, Sum = 3000 },
            new Transaction { Id = 5, UserId = 1, Date = new DateTime(2021, 11, 20), CategoryId = 5, Sum = 1300 },
            new Transaction { Id = 6, UserId = 3, Date = new DateTime(2021, 11, 21), CategoryId = 3, Sum = 2000 },
            new Transaction { Id = 7, UserId = 1, Date = new DateTime(2021, 12, 16), CategoryId = 7, Sum = 2300 },
            new Transaction { Id = 8, UserId = 2, Date = new DateTime(2021, 11, 14), CategoryId = 5, Sum = 800 },
            new Transaction { Id = 9, UserId = 3, Date = new DateTime(2021, 12, 1), CategoryId = 11, Sum = 1300 },
            new Transaction { Id = 10, UserId = 1, Date = new DateTime(2021, 12, 1), CategoryId = 15, Sum = 2000 },
            new Transaction { Id = 11, UserId = 1, Date = new DateTime(2021, 12, 2), CategoryId = 19, Sum = 1500 },
            new Transaction { Id = 12, UserId = 1, Date = new DateTime(2021, 12, 3), CategoryId = 13, Sum = 1000 },
            new Transaction { Id = 13, UserId = 1, Date = new DateTime(2021, 12, 4), CategoryId = 17, Sum = 1700 },
            new Transaction { Id = 14, UserId = 1, Date = new DateTime(2021, 12, 5), CategoryId = 16, Sum = 1100 },
            new Transaction { Id = 15, UserId = 2, Date = new DateTime(2021, 12, 3), CategoryId = 18, Sum = 3500 },
            new Transaction { Id = 16, UserId = 2, Date = new DateTime(2021, 12, 4), CategoryId = 26, Sum = 600 },
            new Transaction { Id = 17, UserId = 2, Date = new DateTime(2021, 12, 5), CategoryId = 17, Sum = 1200 },
            new Transaction { Id = 18, UserId = 3, Date = new DateTime(2021, 12, 8), CategoryId = 13, Sum = 500 },
            new Transaction { Id = 19, UserId = 3, Date = new DateTime(2021, 12, 9), CategoryId = 19, Sum = 800 },
            new Transaction { Id = 20, UserId = 1, Date = new DateTime(2021, 12, 4), CategoryId = 24, Sum = 1400 },
            new Transaction { Id = 21, UserId = 1, Date = new DateTime(2021, 12, 7), CategoryId = 19, Sum = 500 },
            new Transaction { Id = 22, UserId = 2, Date = new DateTime(2021, 12, 6), CategoryId = 15, Sum = 2600 },
            new Transaction { Id = 23, UserId = 2, Date = new DateTime(2021, 12, 9), CategoryId = 14, Sum = 1800 },
            new Transaction { Id = 24, UserId = 3, Date = new DateTime(2021, 12, 14), CategoryId = 15, Sum = 700 }
        };
    }
}
