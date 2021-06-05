using Lab6.Data;
using Lab6.Errors;
using Lab6.Models;
using Lab6.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Services
{
	public class MovieManagementService : IMovieManagementService
	{
		public ApplicationDbContext _context;
		public MovieManagementService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<List<Movie>, IEnumerable<ModelError>>> GetMovies()
		{
			var movies = await _context.Movies.ToListAsync();
			var serviceResponse = new ServiceResponse<List<Movie>, IEnumerable<ModelError>>();
			serviceResponse.ResponseOk = movies;
			return serviceResponse;
		}
	}
}
