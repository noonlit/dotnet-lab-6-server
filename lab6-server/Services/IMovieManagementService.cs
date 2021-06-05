using Lab6.Errors;
using Lab6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab6.Services
{
	public interface IMovieManagementService
	{
		public Task<ServiceResponse<List<Movie>, IEnumerable<ModelError>>> GetMovies();
	}
}
