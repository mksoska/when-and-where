using Microsoft.EntityFrameworkCore;
using WhenAndWhereDAL.Models;

namespace WhenAndWhereDAL.Data
{
    public static class DataInitializer
    {
        //Specifying IDs is mandatory if seeding db through OnModelCreating method
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // TODO seed data
        }
    }
}
