using AuthService.Grpc;
using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.AuthService;
using static AuthService.Grpc.AuthService;
using static WebApiGateway.Models.Constants.PolicyConstants;


namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
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

        [HttpGet("test2")]
        [Authorize]
        public ActionResult<string> Test2()
        {
            return Ok("test2");
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthenticateModel authenticateModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = _mapper.Map<AuthenticateModel,AuthenticateRequest>(authenticateModel);

            var result = await authClient.AuthenticateAsync(request, cancellationToken: token);

            return Ok(result.Token);
        }

        [HttpPost("refresh-token")]
        public  async Task<ActionResult<string>> RefreshToken([FromBody] string authToken)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new RefreshRequest()
            {
               RefreshToken = authToken
            };

            var result = await authClient.RefreshAsync(request, cancellationToken: token);
            
            return Ok(result.Token);
        }


        [HttpPost("create-user")]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = _mapper.Map<CreateUserModel, CreateUserRequest>(createUserModel);

            var result = await authClient.CreateUserAsync(request, cancellationToken: token);

            var responce = _mapper.Map<CreateUserResponse, UserModel>(result);

            return Ok(responce);
        }


        [HttpPost("get-user")]
        public async Task<ActionResult<string>> GetUser([FromBody] string userId)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetUserRequest()
            {
                UserId = userId,
            };

            var result = await authClient.GetUserAsync(request, cancellationToken: token);

            var responce = _mapper.Map<GetUserResponse, UserModel>(result);

            return Ok(result);
        }
    }
}