using AgendaApiLucianoSvaikaukas.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaApiLucianoSvaikaukas.Data
{
    public class AgendaContext : DbContext
    {  
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }

        //Heredamos del constructor de DbContext
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User ka = new User()
            {
                Id = 1,
                Name = "Karen",
                LastName = "Lasot",
                Password = "Pa$$w0rd",
                Email = "karenbailapiola@gmail.com",
            };
            User lu = new User()
            {
                Id=2,
                Name = "Luis Gonzalez",
                LastName = "Gonzales",
                Password = "lamismadesiempre",
                Email = "elluismidetotoras@gmail.com",
            };
            var contacts = new List<Contact>
            {
                new Contact { Id=1,
                Name = "Jaimito",
                CelularNumber = 341457896,
                Description = "Plomero",
                TelephoneNumber = 66,
                UserId = ka.Id,},
                new Contact { Id = 2,
                Name = "Pepe",
                CelularNumber = 34156978,
                Description = "Papa",
                TelephoneNumber = 422568,
                UserId = ka.Id, },
                new Contact {Id = 3,
                Name = "Maria",
                CelularNumber = 011425789,
                Description = "Jefa",
                TelephoneNumber = 7656,
                UserId = lu.Id, }
            };
            Group natacionG = new Group()
            {
                Id=1,
                Name="Natacion",
                UserId=ka.Id,
            };

            // Relación uno a muchos: Usuario - Contacto
            modelBuilder.Entity<User>()
                    .HasMany(x => x.Contacts)
                    .WithOne(x => x.User);

            // Relación uno a muchos: Usuario - Grupo
            modelBuilder.Entity<User>()
              .HasMany(u => u.Groups)
              .WithOne(c => c.User);

            // Creación de la tabla-relación ContactGroup
            modelBuilder.Entity<Contact>()
               .HasMany(x => x.Groups)
               .WithMany(x => x.Contacts)
               .UsingEntity(j => j
                   .ToTable("ContactGroup")
                   .HasData(new[]{ //metemos a la tabla relacion
                            new { GroupsId = 1, ContactsId = 1},
                            new { GroupsId= 1, ContactsId = 3},}));


            modelBuilder.Entity<User>().HasData(ka, lu);
            modelBuilder.Entity<Contact>().HasData(contacts);
            modelBuilder.Entity<Group>().HasData(natacionG);

            base.OnModelCreating(modelBuilder);


        }



    }
}







