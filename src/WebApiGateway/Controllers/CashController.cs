using AutoMapper;
using CashService.GRPC;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.CashService;
using static CashService.GRPC.CashService;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : BaseAuthController
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        private readonly ILogger<CashController> _logger;

        public CashController(ILogger<CashController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }


        [HttpGet("{id}/transactions", Name = nameof(GetTransactionsHistory))]
        public async Task<ActionResult<TransactionModelApi>> GetTransactionsHistory([FromRoute] BaseProfileRequstModel requstModel)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetTransactionsHistoryRequest()
            {
               ProfileId = requstModel.ProfileId
            };

            var result = await cashClient.GetTransactionsHistoryAsync(request, cancellationToken: token);

            var response = _mapper.Map<TransactionModel, TransactionModelApi>(result.Balance);

            return Ok(new ApiResponse<TransactionModelApi>(response));
        }

        [HttpGet("{id}", Name = nameof(GetBalance)) ]
        public async Task<ActionResult<TransactionModelApi>> GetBalance([FromRoute] BaseProfileRequstModel requstModel)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetBalanceRequest()
            {
                ProfileId = requstModel.ProfileId
            };

            var result = await cashClient.GetBalanceAsync(request, cancellationToken: token);

            var response = _mapper.Map<TransactionModel, TransactionModelApi>(result.Balance);

            return Ok(new ApiResponse<TransactionModelApi>(response));
        }


        [HttpPost("deposit", Name = nameof(Deposit))]
        public async Task<ActionResult> Deposit([FromBody] TransactionModelApi transactionModelApi)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<TransactionModelApi ,TransactionModel >(transactionModelApi);

            var request = new DepositRequest()
            {
                Deposit = requestModel
            };

            var result = await cashClient.DepositAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>());
        }

        [HttpPost("withdraw", Name = nameof(Withdraw))]
        public async Task<ActionResult<TransactionModelApi>> Withdraw([FromBody] TransactionModelApi transactionModelApi)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<TransactionModelApi, TransactionModel>(transactionModelApi);

            var request = new WithdrawRequest()
            {
                Withdrawrequest = requestModel
            };

            var result = await cashClient.WithdrawAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<TransactionModel, TransactionModelApi>(result.Withdrawresponse);

            return Ok(new ApiResponse<TransactionModelApi>(resultModel));
        }


        [HttpPost("depositrange", Name = nameof(DepositRange))]
        public async Task<ActionResult> DepositRange([FromBody] IEnumerable<TransactionModelApi> transactionModelApis)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map< IEnumerable<TransactionModelApi>, IEnumerable<TransactionModel>>(transactionModelApis);

            var request = new DepositRangeRequest();
            request.DepositRangeRequests.AddRange(requestModel);

            var result = await cashClient.DepositRangeAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>());
        }

        [HttpPost("withdrawrange", Name = nameof(WithdrawRange))]
        public async Task<ActionResult<IEnumerable<TransactionModelApi>>> WithdrawRange([FromBody] IEnumerable<TransactionModelApi> transactionModelApis)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<IEnumerable<TransactionModelApi>, List<TransactionModel>>(transactionModelApis);

            var request = new WithdrawRangeRequest();
            request.WithdrawRangeRequests.AddRange(requestModel);

            var result = await cashClient.WithdrawRangeAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<IEnumerable<TransactionModel>, List<TransactionModelApi>>(result.WithdrawRangeResponses);

            return Ok(new ApiResponse<IEnumerable<TransactionModelApi>>(resultModel));
        }
    }
}