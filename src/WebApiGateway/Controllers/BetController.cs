using AutoMapper;
using BetService.Grpc;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BetService;
using WebApiGateway.Models.BetService.Entities;
using static BetService.Grpc.BetService;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BetController : BaseAuthController
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        public BetController(IMapper mapper, GrpcClientFactory grpcClientFactory)
        {
            _mapper = mapper;
            _grpcClientFactory = grpcClientFactory;
        }

        [HttpGet("get-bets/{userId}")]
        public async Task<ActionResult<IEnumerable<BetViewModel>>> GetUserBets(Guid userId, int? page = 1, int? pageSize = 5)
        {
            var getUserBetsRequest = new GetUserBetsRequest()
            {
                UserId = userId,
                Page = page!.Value,
                PageSize = pageSize!.Value
            };
            var betServiceClient = _grpcClientFactory.CreateClient<BetServiceClient>(nameof(BetServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcRequest = new BetService.Grpc.GetUsersBetsRequset()
            {
                Page = getUserBetsRequest.Page,
                PageSize = getUserBetsRequest.PageSize,
                UserId = getUserBetsRequest.UserId.ToString()
            };

            var grpcResponse = await betServiceClient.GetUsersBetsAsync(grpcRequest, cancellationToken: token);

            var response = _mapper.Map<IEnumerable<BetViewModel>>(grpcResponse.Bets);

            return Ok(new ApiResponse<IEnumerable<BetViewModel>>(response));
        }
    }
}
