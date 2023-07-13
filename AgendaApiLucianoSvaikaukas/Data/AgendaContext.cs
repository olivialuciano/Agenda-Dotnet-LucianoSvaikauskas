
using AgendaApiLucianoSvaikaukas.Entities;
using Microsoft.EntityFrameworkCore;


namespace AgendaApiLucianoSvaikaukas.Data
{

    public class AgendaContext : DbContext //heredamos de dbcontext
    {

        //Esto genera las tablas users y contacts por ef.
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }  //agg tabla a bbdd group


        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) //Heredamos del constructor de DbContext
        {

        }

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
                Id = 1,
                Name = "Natacion",
                Contacts = contacts
            };


            base.OnModelCreating(modelBuilder);

            // Relación uno a muchos: Usuario - Contacto
            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Contact>()
        .HasMany(c => c.Groups)
        .WithMany(g => g.Contacts)
        .UsingEntity<Dictionary<string, object>>(
            "ContactGroup",
            cg => cg.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
            cg => cg.HasOne<Contact>().WithMany().HasForeignKey("ContactId")
        );
        //.HasData(new[]
        //                {
        //                    new { StudentsId = 1, SubjectsAttendedId = 1},
        //                    new { StudentsId = 1, SubjectsAttendedId = 2},
        //                };

            //METEMOS LO HARCODEADO EN LA BBDD
            modelBuilder.Entity<User>()
                .HasData(ka, lu);
            modelBuilder.Entity<Contact>()
                .HasData(contacts);

            //modelBuilder.

            //modelBuilder.Entity<Group>()
            //    .HasData(natacionG);



            //// Relación muchos a muchos: Contacto - Grupo
            //modelBuilder.Entity<ContactGroup>()
            //    .HasKey(cg => new { cg.ContactId, cg.GroupId });

            //modelBuilder.Entity<ContactGroup>()
            //    .HasOne(cg => cg.Contact)
            //    .WithMany()
            //    .HasForeignKey(cg => cg.ContactId);

            //modelBuilder.Entity<ContactGroup>()
            //    .HasOne(cg => cg.Group)
            //    .WithMany()
            //    .HasForeignKey(cg => cg.GroupId);

            // Remove any ambiguous entity type mapping for ContactoGrupo
            //modelBuilder.Ignore<Dictionary<string, object>>("ContactoGrupo");
            //    modelBuilder.Entity<User>()
            //      .HasMany<Contact>(u => u.Contacts)
            //      .WithOne(c => c.User);
            //    ////////////////////////////


            //    /////////para las relaciones de contact y group

            //    modelBuilder.Entity<ContactGroup>()
            //    .HasKey(cg => new { cg.ContactId, cg.GroupId });


            //    base.OnModelCreating(modelBuilder);
            //}
        }
    }
}

