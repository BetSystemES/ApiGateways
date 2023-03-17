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

        // TODO:  Why role ids not in body?
        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser([FromBody] BasicUserModel basicUserModel)
        {
            // TODO: Add new CreateUserRequest model and inherit it from BasicUserModel.
            // TODO: add field IEnumerable<Guid> roleIds and validate for count only (count > 0)
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            // TODO: for what reason we should get roles from auth service in that case?
            var getAllRolesRequest = new GetAllRolesRequest();
            var getAllRolesResponse = await authClient.GetAllRolesAsync(getAllRolesRequest, cancellationToken: token);

            // TODO: Why User Role is default? We should have role ids in request model and just transfer it to auth service.
            var roleId = getAllRolesResponse?.Roles?.FirstOrDefault(x => string.Equals(x.Name.ToLower(), AuthRole.User.GetDescription()))?.Id;

            CreateUserModel createUserModel = new CreateUserModel(basicUserModel);
            // TODO: take role ids from request model
            createUserModel.RoleIds.Add(roleId);

            var request = _mapper.Map<CreateUserModel, CreateUserRequest>(createUserModel);

            var result = await authClient.CreateUserAsync(request, cancellationToken: token);

            var response = _mapper.Map<User, UserModel>(result.User);

            // TODO: Created response with status 201
            return Ok(new ApiResponse<UserModel>(response));
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

        // TODO: Add api get roles endpoint (take roles from auth server)???
    }
}