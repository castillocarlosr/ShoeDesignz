using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeDesignz.Data
{
    public class ShoeDesignzDbContext : DbContext
    {
        public ShoeDesignzDbContext(DbContextOptions<ShoeDesignzDbContext> options) : base(options)
        {

        }
    }
}
