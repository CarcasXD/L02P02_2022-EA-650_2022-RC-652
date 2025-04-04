using Microsoft.EntityFrameworkCore;
namespace L02P02_2022_EA_650_2022_RC_652.Models

{
    public class libroContext : DbContext
    {
        public libroContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
