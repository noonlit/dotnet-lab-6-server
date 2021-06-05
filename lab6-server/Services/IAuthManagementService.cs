using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab6.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Lab6.Services
{
    public interface IAuthManagementService
    {
        Task<ServiceResponse<RegisterResponse, IEnumerable<IdentityError>>> RegisterUser(RegisterRequest registerRequest);
        Task<bool> ConfirmUserRequest(ConfirmUserRequest confirmUserRequest);
        Task<ServiceResponse<LoginResponse, string>> LoginUser(LoginRequest loginRequest);
    }
}
