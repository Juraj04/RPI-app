namespace TemperatureDatabase
{
    using System.Data.Entity;

    public class TemperatureContext : DbContext
    {
        /// <summary>
        /// Database table of data from sensor
        /// </summary>
        public virtual DbSet<TemperatureData> TemperatureData { get; set; }

        public TemperatureContext()
            : base("name=TemperatureContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
