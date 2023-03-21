using AuthService.Grpc;
using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.BaseModels;
using static AuthService.Grpc.AuthService;
using static WebApiGateway.Models.Constants.PolicyConstants;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseAuthController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] BasicUserModel basicUserModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = _mapper.Map<BasicUserModel, AuthenticateRequest>(basicUserModel);

            var result = await authClient.AuthenticateAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<Token>(result.Token));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken([FromBody] BasicTokenModel basicTokenModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new RefreshRequest()
            {
                RefreshToken = basicTokenModel.AuthToken
            };

            var result = await authClient.RefreshAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<Token>(result.Token));
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCreateUserRequest = _mapper.Map<CreateUserRequest>(createUserModel);

            var createUserResponse = await authClient.CreateUserAsync(grpcCreateUserRequest, cancellationToken: token);

            var userSimpleModel = _mapper.Map<UserModel>(createUserResponse.User);

            return Created("create-user", new ApiResponse<UserModel>(userSimpleModel));
        }

        [HttpPost("get-user")]
        public async Task<ActionResult<string>> GetUser([FromBody] BaseUserRequestModel requestModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetUserRequest()
            {
                UserId = requestModel.UserId.ToString(),
            };

            var result = await authClient.GetUserAsync(request, cancellationToken: token);

            var response = _mapper.Map<User, UserModel>(result.User);

            return Ok(new ApiResponse<UserModel>(response));
        }

        [HttpGet("get-all-roles")]
        [Authorize(Policy = AdminPolicy)]
        public async Task<ActionResult<string>> GetAllRoles()
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetAllRolesRequest();

            var getAllRolesResponse = await authClient.GetAllRolesAsync(request, cancellationToken: token);

            var allRoles = _mapper.Map<IEnumerable<RoleModel>>(getAllRolesResponse.Roles);

            return Ok(new ApiResponse<IEnumerable<RoleModel>>(allRoles));
        }
    }
}