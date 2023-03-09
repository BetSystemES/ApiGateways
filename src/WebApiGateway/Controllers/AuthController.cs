using AuthService.Grpc;
using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.AuthService;
using static AuthService.Grpc.AuthService;

namespace WebApiGateway.Controllers
{
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

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] AuthenticateModel authenticateModel)
        {
            var authClient = _grpcClientFactory.CreateClient<AuthServiceClient>(nameof(AuthServiceClient));
            var token = HttpContext.RequestAborted;

            var request = _mapper.Map<AuthenticateModel,AuthenticateRequest>(authenticateModel);

            var result = await authClient.AuthenticateAsync(request, cancellationToken: token);

            return Ok(result.Token);
        }

        [HttpPost]
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
    }
}