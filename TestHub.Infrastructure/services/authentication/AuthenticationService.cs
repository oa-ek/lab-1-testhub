using System.IdentityModel.Tokens.Jwt;

namespace TestHub.Infrastructure.services.authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IMapper mapper, IUserRepository repository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseCommandResponse<RespondAuthenticationDto>> Login(RequestLoginDto? request)
    {
        if (request == null)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("RequestRegisterDto cannot be null.");

        var existingUser = await _repository.GetByEmail(request.Email);
        if (existingUser == null)
            return new NotFoundFailedStatusResponse<RespondAuthenticationDto>(request.Email);

        if (!existingUser.IsEmailVerified)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("The email is not verified.");

        var validationResult = await ValidateLoginRequest(request);
        if (!validationResult.IsValid)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>(validationResult.Errors);

        if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("Wrong password.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(existingUser.Token);
        if (decodedToken.ValidTo < DateTimeOffset.UtcNow)
        {
            var refreshToken = _jwtTokenGenerator.GenerateToken(existingUser.Name, existingUser.Email, existingUser.Role);
            existingUser = await _repository.SetRefreshToken(existingUser, refreshToken);
        }

        var respondUserDto = _mapper.Map<RespondUserDto>(existingUser);
        var respondAuthenticationDto = new RespondAuthenticationDto
        {
            RespondUserDto = respondUserDto,
            Token = existingUser.Token
        };
        return new OkSuccessStatusResponse<RespondAuthenticationDto>(respondAuthenticationDto);
    }

    public async Task<BaseCommandResponse<RespondAuthenticationDto>> Register(RequestRegisterDto? request)
    {
        if (request == null)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("RequestRegisterDto cannot be null.");

        var existingUser = await _repository.GetByEmail(request.Email);
        if (existingUser != null)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("User already exists in database.");

        var validationResult = await ValidateRegistrationRequest(request);
        if (!validationResult.IsValid)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>(validationResult.Errors);

        var token = _jwtTokenGenerator.GenerateToken(request.Name, request.Email, Roles.User);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = await RegisterUser(request, token, passwordHash);
        var respondUserDto = _mapper.Map<RespondUserDto>(user);
        var respondAuthenticationDto = new RespondAuthenticationDto
        {
            RespondUserDto = respondUserDto,
            Token = token
        };
        return new OkSuccessStatusResponse<RespondAuthenticationDto>(respondAuthenticationDto);
    }

    private static async Task<ValidationResult> ValidateRegistrationRequest(RequestRegisterDto request)
    {
        var validator = new RequestRegisterDtoValidator();
        return await validator.ValidateAsync(request);
    }

    private async Task<User> RegisterUser(RequestRegisterDto request, string token, string passwordHash)
    {
        var newUser = _mapper.Map<User>(request);
        newUser.Token = token;
        newUser.Password = passwordHash;
        newUser.Role = Roles.User;

        return await _repository.Add(newUser);
    }

    private static async Task<ValidationResult> ValidateLoginRequest(RequestLoginDto request)
    {
        var validator = new RequestLoginDtoValidator();
        return await validator.ValidateAsync(request);
    }
}