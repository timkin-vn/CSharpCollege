using Minesweeper.Business.Mappers;
using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Repositories;
using Minesweeper.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserResponse GetOrCreateUser(string username)
        {
            try
            {
                if (!ValidateUsername(username))
                    throw new Exception("Invalid username");

                var existingUser = _userRepository.GetByUsername(username);
                if (existingUser != null)
                {
                    var model = UserMapper.ToModel(existingUser);
                    return UserMapper.ToResponse(model);
                }

                var newUser = new UserDto
                {
                    Username = username,
                    CreatedAt = DateTime.Now,
                    TotalGamesPlayed = 0,
                    GamesWon = 0
                };

                var createdUser = _userRepository.Create(newUser);
                var createdModel = UserMapper.ToModel(createdUser);

                return UserMapper.ToResponse(createdModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting or creating user: {ex.Message}");
            }
        }

        public UserResponse GetUser(int userId)
        {
            try
            {
                var userDto = _userRepository.GetById(userId);
                if (userDto == null)
                    throw new Exception("User not found");

                var userModel = UserMapper.ToModel(userDto);
                return UserMapper.ToResponse(userModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user: {ex.Message}");
            }
        }

        public UserResponse GetUserByUsername(string username)
        {
            try
            {
                var userDto = _userRepository.GetByUsername(username);
                if (userDto == null)
                    throw new Exception("User not found");

                var userModel = UserMapper.ToModel(userDto);
                return UserMapper.ToResponse(userModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by username: {ex.Message}");
            }
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAll();
                return users.Select(u =>
                {
                    var model = UserMapper.ToModel(u);
                    return UserMapper.ToResponse(model);
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: {ex.Message}");
            }
        }

        public IEnumerable<UserResponse> GetTopPlayers(int limit = 10)
        {
            try
            {
                var users = _userRepository.GetTopPlayers(limit);
                return users.Select(u =>
                {
                    var model = UserMapper.ToModel(u);
                    return UserMapper.ToResponse(model);
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting top players: {ex.Message}");
            }
        }

        public void UpdateUserStats(int userId, bool gameWon)
        {
            try
            {
                _userRepository.UpdateStats(userId, gameWon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user stats: {ex.Message}");
            }
        }

        public bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            if (username.Length < 3 || username.Length > 50)
                return false;

            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c) && c != '_' && c != '-')
                    return false;
            }

            return true;
        }
    }
}