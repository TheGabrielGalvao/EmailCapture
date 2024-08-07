﻿using Domain.Interface.Repository.Auth;
using Domain.Interface.Service.Auth;
using Domain.Model.Auth;
using Util;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _autenticacaoRepository;
        private readonly IUserRepository _userRepository;

        public AuthService(IAuthRepository autenticacaoRepository, IUserRepository repository)
        {
            _autenticacaoRepository = autenticacaoRepository;
            _userRepository = repository;
        }

        public async Task<AuthResponse> ExecuteAuth(AuthRequest request)
        {
            request.Password = Encryption.HashPassword(request.Password);
            return await _autenticacaoRepository.ExecuteAuth(request);
        }

        public async Task<AuthResponse> ExecuteInternalAuth(AuthRequest request)
        {
            var user = await _userRepository.Get(request);

            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Usuário ou Senha inválido" };
            }

            var token = await _autenticacaoRepository.GenerateToken(user);

            var response = new AuthResponse
            {
                AccessToken = token,
                Success = true,

            };

            return response;
        }
    }
}
