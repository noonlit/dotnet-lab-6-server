using Lab6.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Data
{
	public class SeedFavourites
	{
        private static Random random = new Random();

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            var usersCount = context.ApplicationUsers.Count();
            var moviesCount = context.Movies.Count();

            for (int i = 0; i < count; ++i)
            {
                // var user = serviceProvider.GetRequiredService<ApplicationDbContext>().ApplicationUsers.Skip(random.Next(1, usersCount)).Take(1).First();
                // var movies = serviceProvider.GetRequiredService<ApplicationDbContext>().Movies.Skip(random.Next(1, moviesCount)).Take(3).ToList();

                var favourites = new Models.Favourites
                {
                    Year = generateRandomInt(1950, DateTime.Now.Year),
                    UserId = "0025b610-79a2-4956-ab87-1f52764223a8",
                    Movies = new List<Movie>()
                };


                context.Favourites.Add(favourites);
            }

            context.SaveChanges();
        }

        private static int generateRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
