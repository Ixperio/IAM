using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAM_CORE.Common;
using IAM_CORE.Interfaces;


namespace IAM_CORE.Features.Authentication.Login
{
    public record LoginCommand(string Email, string Password);

    public record LoginResult(string AccessToken, string RefreshToken);

    public class LoginHandler
    {
        private readonly IUserReposiotry _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginHander(
            IUserReposiotry userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;

        }

        public async Task<Result<LoginResult>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (user == null || !user.IsActive)
            {
                return Result.Failure<LoginResult>("Authentication.InvalidCredentials", "Nieprawidłowy email lub hasło.");
            }

            if(!user.IsActive)
            {
                return Result.Failure<LoginResult>("Authentication.AccountDeactivated", "Twoje konto jest zdezaktywowane.");
            }

            var isPasswordValid = _passwordHasher.Verify(command.Password, user.PasswordHash);
            if(!isPasswordValid)
            {
                return Result.Failure<LoginResult>("Authentication.InvalidCredentials", "Nieprawidłowy email lub hasło.");
            }

            var accesstoken = _tokenService.GetAccessToken();
            var refreshToken = _tokenService.GetRefreshToken();


            return Result.Success<LoginResult>(accessToken, refreshToken);

        }

    }
}
