using Lab6.Errors;
using Lab6.Models;
using Lab6.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab6.Services
{
	public interface IFavouritesManagementService
	{
		Task<ServiceResponse<List<Favourites>, IEnumerable<EntityManagementError>>> GetFavourites(string userId);
		Task<ServiceResponse<Favourites, IEnumerable<EntityManagementError>>> GetFavourite(string userId, int id);
		Task<ServiceResponse<Favourites, IEnumerable<EntityManagementError>>> GetFavouritesForYear(string userId, int year);
		Task<ServiceResponse<Favourites, IEnumerable<EntityManagementError>>> CreateFavourites(string userId, NewFavouritesForUserViewModel newFavouritesRequest);
		Task<ServiceResponse<Favourites, IEnumerable<EntityManagementError>>> UpdateFavourites(Favourites favourites, UpdateFavouritesForUserViewModel updateFavouritesRequest);
		Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeleteFavourites(int id);
		public Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeleteFavouritesByYear(string userId, int year);
		bool FavouritesForYearExists(string userId, int year);
		bool FavouritesExists(string userId, int favouritesId);
	}
}
