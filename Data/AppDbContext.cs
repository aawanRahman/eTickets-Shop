using eTickets.Models.Data.@enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Models.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {




        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);
            // Actor Data Seed
            modelBuilder.Entity<Actor>().HasData(
                new Actor()
                {
                    Id = 1,
                    FullName = "Actor 1",
                    Bio = "This is the Bio of the first actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-1.jpeg"

                },
                new Actor()
                {
                    Id = 2,
                    FullName = "Actor 2",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-2.jpeg"
                },
                new Actor()
                {
                    Id = 3,
                    FullName = "Actor 3",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-3.jpeg"
                },
                new Actor()
                {
                    Id = 4,
                    FullName = "Actor 4",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-4.jpeg"
                },
                new Actor()
                {
                    Id = 5,
                    FullName = "Actor 5",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/actors/actor-5.jpeg"
                });
            

            //Producers data seed.
            modelBuilder.Entity<Producer>().HasData(
                 new Producer()
                 {
                     Id = 1,
                     FullName = "Producer 1",
                     Bio = "This is the Bio of the first actor",
                     ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-1.jpeg"

                 },
                new Producer()
                {
                    Id = 2,
                    FullName = "Producer 2",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-2.jpeg"
                },
                new Producer()
                {
                    Id = 3,
                    FullName = "Producer 3",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-3.jpeg"
                },
                new Producer()
                {
                    Id = 4,
                    FullName = "Producer 4",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-4.jpeg"
                },
                new Producer()
                {
                    Id = 5,
                    FullName = "Producer 5",
                    Bio = "This is the Bio of the second actor",
                    ProfilePictureUrl = "http://dotnethow.net/images/producers/producer-5.jpeg"
                });

            
            // Data Seed for Cinemas.........
            modelBuilder.Entity<Cinema>().HasData(
                new Cinema()
                {
                    Id=1,
                    CinemaName = "Cinema 1",
                    CinemaImageUrl = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                    CinemaDescription = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id=2,
                    CinemaName = "Cinema 2",
                    CinemaImageUrl = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                    CinemaDescription = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id=3,
                    CinemaName = "Cinema 3",
                    CinemaImageUrl = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                    CinemaDescription = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id=4,
                    CinemaName = "Cinema 4",
                    CinemaImageUrl = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                    CinemaDescription = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id=5,
                    CinemaName = "Cinema 5",
                    CinemaImageUrl = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                    CinemaDescription = "This is the description of the first cinema"
                }
                );

            //Data Seed Movies.
            modelBuilder.Entity<Movie>().HasData(
                 new Movie()
                 {
                     Id = 1,
                     Name = "Life",
                     Description = "This is the Life movie description",
                     Price = 39.50,
                     ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                     StartDate = DateTime.Today.AddDays(-10).ToShortDateString(),
                     EndDate = DateTime.Today.AddDays(10).ToShortDateString(),
                     CinemaId = 3,
                     ProducerId = 3,
                     MovieCategory = MovieCategory.Documentary
                 },
                new Movie()
                {
                    Id = 2,
                    Name = "The Shawshank Redemption",
                    Description = "This is the Shawshank Redemption description",
                    Price = 29.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                    StartDate = DateTime.Today.ToShortDateString(),
                    EndDate = DateTime.Today.AddDays(3).ToShortDateString(),
                    CinemaId = 1,
                    ProducerId = 1,
                    MovieCategory = MovieCategory.Action
                },
                new Movie()
                {
                    Id= 3,
                    Name = "Ghost",
                    Description = "This is the Ghost movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                    StartDate = DateTime.Today.ToShortDateString(),
                    EndDate = DateTime.Today.AddDays(7).ToShortDateString(),
                    CinemaId = 4,
                    ProducerId = 4,
                    MovieCategory = MovieCategory.Horror
                },
                new Movie()
                {
                    Id = 4,
                    Name = "Race",
                    Description = "This is the Race movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                    StartDate = DateTime.Today.AddDays(-10).ToShortDateString(),
                    EndDate = DateTime.Today.AddDays(-5).ToShortDateString(),
                    CinemaId = 1,
                    ProducerId = 2,
                    MovieCategory = MovieCategory.Documentary
                },
                new Movie()
                {
                    Id = 5,
                    Name = "Scoob",
                    Description = "This is the Scoob movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                    StartDate = DateTime.Today.AddDays(-10).ToShortDateString(),
                    EndDate = DateTime.Today.AddDays(-2).ToShortDateString(),
                    CinemaId = 1,
                    ProducerId = 3,
                    MovieCategory = MovieCategory.Cartoon
                },
                new Movie()
                {
                    Id =  6,
                    Name = "Cold Soles",
                    Description = "This is the Cold Soles movie description",
                    Price = 39.50,
                    ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                    StartDate = DateTime.Today.AddDays(3).ToShortDateString(),
                    EndDate = DateTime.Today.AddDays(20).ToShortDateString(),
                    CinemaId = 1,
                    ProducerId = 5,
                    MovieCategory = MovieCategory.Drama
                });

            //Actor_Movie Data Seed.......

            modelBuilder.Entity<Actor_Movie>().HasData(
                new Actor_Movie()
                {
                    ActorId = 1,
                    MovieId = 1
                },
                new Actor_Movie()
                {
                    ActorId = 3,
                    MovieId = 1
                },

                new Actor_Movie()
                {
                    ActorId = 1,
                    MovieId = 2
                },
                new Actor_Movie()
                {
                    ActorId = 4,
                    MovieId = 2
                },

                new Actor_Movie()
                {
                    ActorId = 1,
                    MovieId = 3
                },
                new Actor_Movie()
                {
                    ActorId = 2,
                    MovieId = 3
                },
                new Actor_Movie()
                {
                    ActorId = 5,
                    MovieId = 3
                },


                new Actor_Movie()
                {
                    ActorId = 2,
                    MovieId = 4
                },
                new Actor_Movie()
                {
                    ActorId = 3,
                    MovieId = 4
                },
                new Actor_Movie()
                {
                    ActorId = 4,
                    MovieId = 4
                },


                new Actor_Movie()
                {
                    ActorId = 2,
                    MovieId = 5
                },
                new Actor_Movie()
                {
                    ActorId = 3,
                    MovieId = 5
                },
                new Actor_Movie()
                {
                    ActorId = 4,
                    MovieId = 5
                },
                new Actor_Movie()
                {
                    ActorId = 5,
                    MovieId = 5
                },


                new Actor_Movie()
                {
                    ActorId = 3,
                    MovieId = 6
                },
                new Actor_Movie()
                {
                    ActorId = 4,
                    MovieId = 6
                },
                new Actor_Movie()
                {
                    ActorId = 5,
                    MovieId = 6
                }
                );

            

            base.OnModelCreating(modelBuilder);
        


        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShopingCartItem> shopingCartItems { get; set; }


        //Seed Database Users and Roles.

        

       

      


        // End of Seed Users
    }
}
