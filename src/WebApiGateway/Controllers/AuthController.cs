using AuthService.Grpc;
using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;
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

        [HttpGet("test1")]
        [Authorize(Policy = AdminPolicy)]
        public ActionResult<string> Test1()
        {
            return Ok("test1");
        }

        [HttpGet("test2/{UserId:guid}")]
        public ActionResult<string> Test2([FromRoute] BaseUserRequestModel requestModel)
        {
            return Ok("test2");
        }

        [HttpGet("test3/{UserId:guid}")]
        [AllowAnonymous]
        public ActionResult<string> Test3([FromRoute] BaseUserRequestModel requestModel)
        {
            return Ok("test3");
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
        public async Task<ActionResult<string>> RefreshToken([FromBody] string authToken)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new RefreshRequest()
            {
                RefreshToken = authToken
            };

            var result = await authClient.RefreshAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<Token>(result.Token));

        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser([FromBody] BasicUserModel basicUserModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var getAllRolesRequest = new GetAllRolesRequest();
            var getAllRolesResponce = await authClient.GetAllRolesAsync(getAllRolesRequest, cancellationToken: token);

            var roleId = getAllRolesResponce?.Roles?.FirstOrDefault(x => x.Name.ToLower() == AuthRole.User.GetDescription())?.Id;

            CreateUserModel createUserModel = new CreateUserModel(basicUserModel);
            createUserModel.RoleIds.Add(roleId);

            var request = _mapper.Map<CreateUserModel, CreateUserRequest>(createUserModel);

            var result = await authClient.CreateUserAsync(request, cancellationToken: token);

            var responce = _mapper.Map<User, UserModel>(result.User);

            return Ok(new ApiResponse<UserModel>(responce));
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

            var responce = _mapper.Map<User, UserModel>(result.User);

            return Ok(new ApiResponse<UserModel>(responce));
        }
    }
}