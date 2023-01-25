using Microsoft.EntityFrameworkCore;

namespace EFRelationsEntityApp
{
    /*
    public class City
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public Country? Country { set; get; }
    }
    public class Country
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public int?CapitalId { set; get; }
        public City? Capital { set; get; }
        public List<Company> Companies { get; set; } = new List<Company>();
    }
    public class Company
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public int CountryId { set; get; }
        public Country? Country { set; get; }
        public List<Employe> Employes { get; set; } = new List<Employe>();
    }
    public class Position
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public List<Employe> Employes { get; set; } = new List<Employe>();
    }
    public class Employe
    {
        public int Id { set; get; }
        public string? Name { set; get; } = null!;
        public DateTime BirthDate { set; get; }
        public int? CompanyId { set; get; } // свойство - внешний ключ
        public Company? Company { set; get; } // навигационное свойство
        public int? PositionId { set; get; } // свойство - внешний ключ
        public Position? Position { set; get; } // навигационное свойство

    }
    */

    public class Driver
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Car? Car { get; set; }
    }
    public class Car
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int DriverId { set; get; }
        public Driver? Driver { set; get; }
    }
    public class Mechanic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();

    }
    public class AppContext : DbContext
    {
        /*
        public DbSet<Employe> Employes { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        */
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public AppContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GarageDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relation one to one
            /*
            modelBuilder.Entity<City>()
                        .HasOne(city => city.Country)
                        .WithOne(country => country.Capital)
                        .HasForeignKey<Country>(country => country.CapitalId);
            */

            // relation on to one
            /*
            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.Car)
                        .WithOne(c => c.Driver)
                        .HasForeignKey<Car>(c => c.DriverId);
            */

            // relation on to one
            // 2 entity - 1 table
            /*
            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.Car)
                        .WithOne(c => c.Driver)
                        .HasForeignKey<Car>(dc => dc.Id);
            modelBuilder.Entity<Driver>().ToTable("DriversCars");
            modelBuilder.Entity<Car>().ToTable("DriversCars");
            */


        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            using(AppContext context = new())
            {
                //City beijing = new() { Title = "Beijing" };
                //context.Cities.Add(beijing);

                //Country country = new() { Title = "Chinese" };
                //context.Countries.Add(country);
                //context.SaveChanges();
                /*
                City chinaCapital = context.Cities.FirstOrDefault(c => c.Title == "Beijing");
                if(chinaCapital is not null)
                {
                    //china.Capital = chinaCapital;
                    Country china = context.Countries.FirstOrDefault(c => c.CapitalId == chinaCapital.Id);
                    china.Title = "China";
                    //context.Cities.Remove(chinaCapital);
                    context.SaveChanges();
                }
                */
                //foreach (City city in context.Cities.Include(cty => cty.Country).ToList())
                //{
                //    Console.WriteLine($"{city.Title} {city?.Country?.Title}");
                //}

                Driver driver1 = new() { Name = "Bob" };
                context.Drivers.Add(driver1);
                Car car1 = new() { Number = "AB123C", Driver = driver1 };
                context.Cars.Add(car1);
                context.SaveChanges();

            }
        }
    }
}