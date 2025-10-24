using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetAllBillings
{
    public const string Query = @"
        SELECT 
            id, 
            date,
            status
        FROM billings";
}
