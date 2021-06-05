using AutoMapper;
using Lab6.Models;
using Lab6.ViewModels;
namespace Lab6.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Movie, MovieWithCommentsViewModel>();
            CreateMap<Favourites, FavouritesForUserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
