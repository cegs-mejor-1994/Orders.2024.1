﻿using Microsoft.AspNetCore.Identity;
using Orders.BackEnd.Repositories.Interfaces;
using Orders.BackEnd.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;

namespace Orders.BackEnd.UnitsOfWork.Implementations
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly IUsersRepository _usersRepository;

        public UsersUnitOfWork(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password) => await _usersRepository.AddUserAsync(user, password);
  
        public async Task AddUserToRoleAsync(User user, string roleName) => await _usersRepository.AddUserToRoleAsync(user, roleName);
  
        public async Task CheckRoleAsync(string roleName) => await _usersRepository.CheckRoleAsync(roleName);
  
        public async Task<User> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

        public async Task<bool> IsUserInRoleAsync(User user, string roleName) => await _usersRepository.IsUserInRoleAsync(user, roleName);

        public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usersRepository.LoginAsync(model);
 
        public async Task LogoutAsync() => await _usersRepository.LogoutAsync();
    }
}
