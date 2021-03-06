using AutoMapper;
using Lab6.Data;
using Lab6.Models;
using Lab6.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lab6.Services;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    public class FavouritesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly IFavouritesManagementService _favouritesService;

		public FavouritesController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IFavouritesManagementService favouritesService
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _favouritesService = favouritesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouritesForUserViewModel>>> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                return NotFound();
            }

            var serviceResponse = await _favouritesService.GetFavourites(user.Id);
            var favourites = serviceResponse.ResponseOk;

            return _mapper.Map<List<Favourites>, List<FavouritesForUserViewModel>>(favourites);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFavourites(NewFavouritesForUserViewModel newFavouriteRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var favouritesForYearResponse = await _favouritesService.GetFavouritesForYear(user.Id, newFavouriteRequest.Year);

            Favourites favouritesForYear = favouritesForYearResponse.ResponseOk;

            if (favouritesForYear != null)
            {
                return BadRequest($"You already have a favourites list for year {newFavouriteRequest.Year}.");
            }

            var serviceResponse = await _favouritesService.CreateFavourites(user.Id, newFavouriteRequest);

            if (serviceResponse.ResponseError == null)
			{
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateFavourites(UpdateFavouritesForUserViewModel updateFavouritesRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var favouritesWithIdResponse = await _favouritesService.GetFavourite(user.Id, updateFavouritesRequest.Id);

            Favourites favourites = favouritesWithIdResponse.ResponseOk;

            if (favourites == null)
            {
                return BadRequest("There is no favourites list with this ID.");
            }

            var serviceResponse = await _favouritesService.UpdateFavourites(favourites, updateFavouritesRequest);

            if (serviceResponse.ResponseError == null)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavourites(int id)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var favouritesResponse = await _favouritesService.GetFavourite(user.Id, id);
            var favourites = favouritesResponse.ResponseOk;

            if (favourites == null)
            {
                return NotFound();
            }

            var result = await _favouritesService.DeleteFavourites(id);

            if (result.ResponseError == null)
            {
                return NoContent();
            }


            return StatusCode(500);
        }

        [HttpDelete("Year/{year}")]
        public async Task<IActionResult> DeleteFavouritesByYear(int year)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!_favouritesService.FavouritesForYearExists(user.Id, year))
            {
                return NotFound();
            }

            var result = await _favouritesService.DeleteFavouritesByYear(user.Id, year);

            if (result.ResponseError == null)
            {
                return NoContent();
            }


            return StatusCode(500);
        }
    }
}
