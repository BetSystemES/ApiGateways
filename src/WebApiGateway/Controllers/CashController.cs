using AutoMapper;
using CashService.GRPC;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.CashService;
using WebApiGateway.Models.CashService.ViewModel;
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

        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<TransactionModelViewModel>> GetTransactionsHistory(string id)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetTransactionsHistoryRequest()
            {
                ProfileId = id
            };

            var result = await cashClient.GetTransactionsHistoryAsync(request, cancellationToken: token);

            var response = _mapper.Map<TransactionModelViewModel>(result.Balance);

            return Ok(new ApiResponse<TransactionModelViewModel>(response));
        }

        [HttpGet("get-paged-transaction-history")]
        [SwaggerResponse(200, "Successfully get bonus(es)", typeof(List<TransactionModelCreateModel>))]
        public async Task<ActionResult<BasePagedResponseModel<TransactionModelCreateModel>>> GetPagedTransactionHistory([FromQuery] CashServiceRequestModel requstModel)
        {
            var profileClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetTransactionHistoryWithFilterRequest()
            {
                ProfileId = requstModel.ProfileId,
                TransactionHistoryFilter = new TransactionHistoryFilter()
                {
                    ColumnName = requstModel.ColumnName,
                    OrderDirection = (OrderDirection)(requstModel.OrderDirection ?? 0),
                    PageNumber = requstModel.PageNumber ?? -1,
                    PageSize = requstModel.PageSize ?? -1,
                    StartDate = Timestamp.FromDateTimeOffset(requstModel.StartDate ?? DateTimeOffset.MinValue),
                    EndDate = Timestamp.FromDateTimeOffset(requstModel.EndDate ?? DateTimeOffset.MinValue),
                }
            };

            var result = await profileClient.GetPagedTransactionHistoryAsync(request, cancellationToken: token);

            List<TransactionModelCreateModel> transactionModels = _mapper.Map<IEnumerable<TransactionModel>, List<TransactionModelCreateModel>>(result.Transactions);

            var response = new BasePagedResponseModel<TransactionModelCreateModel>()
            {
                Data = transactionModels,
                TotalCount = result.TotalCount
            };

            return Ok(new ApiResponse<BasePagedResponseModel<TransactionModelCreateModel>>(response));
        }

        [HttpGet("get-balance/{id}")]
        public async Task<ActionResult<TransactionModelCreateModel>> GetBalance(string id)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetBalanceRequest()
            {
                ProfileId = id
            };

            var response = await cashClient.GetBalanceAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>(response.Balance.ToString()));
        }

        [HttpPost("deposit", Name = nameof(Deposit))]
        public async Task<ActionResult> Deposit([FromBody] TransactionModelCreateModel transactionModelApi)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var transactionRequestModel = new TransactionRequestModel() 
            { 
                ProfileId = transactionModelApi.ProfileId
            };

            var transactions = _mapper.Map<IEnumerable<Transaction>>(transactionModelApi.TransactionApis);
            transactionRequestModel.Transactions.AddRange(transactions);

            var request = new DepositRequest()
            {
                Deposit = transactionRequestModel
            };

            var result = await cashClient.DepositAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<TransactionModel, TransactionModelCreateModel>(result.Depositresponse);

            return Ok(new ApiResponse<TransactionModelCreateModel>(resultModel));
        }

        [HttpPost("withdraw", Name = nameof(Withdraw))]
        public async Task<ActionResult<TransactionModelCreateModel>> Withdraw([FromBody] TransactionModelCreateModel transactionModelApi)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var transactionRequestModel = new TransactionRequestModel()
            {
                ProfileId = transactionModelApi.ProfileId
            };

            var transactions = _mapper.Map<IEnumerable<Transaction>>(transactionModelApi.TransactionApis);
            transactionRequestModel.Transactions.AddRange(transactions);

            var request = new WithdrawRequest()
            {
                Withdrawrequest = transactionRequestModel
            };

            var result = await cashClient.WithdrawAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<TransactionModel, TransactionModelCreateModel>(result.Withdrawresponse);

            return Ok(new ApiResponse<TransactionModelCreateModel>(resultModel));
        }

        [HttpPost("depositrange", Name = nameof(DepositRange))]
        public async Task<ActionResult> DepositRange([FromBody] IEnumerable<TransactionModelCreateModel> transactionModelApis)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<IEnumerable<TransactionModelCreateModel>, IEnumerable<TransactionModel>>(transactionModelApis);

            var request = new DepositRangeRequest();
            request.DepositRangeRequests.AddRange(requestModel);

            var result = await cashClient.DepositRangeAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>());
        }

        [HttpPost("withdrawrange", Name = nameof(WithdrawRange))]
        public async Task<ActionResult<IEnumerable<TransactionModelCreateModel>>> WithdrawRange([FromBody] IEnumerable<TransactionModelCreateModel> transactionModelApis)
        {
            var cashClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<IEnumerable<TransactionModelCreateModel>, List<TransactionModel>>(transactionModelApis);

            var request = new WithdrawRangeRequest();
            request.WithdrawRangeRequests.AddRange(requestModel);

            var result = await cashClient.WithdrawRangeAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<IEnumerable<TransactionModel>, List<TransactionModelCreateModel>>(result.WithdrawRangeResponses);

            return Ok(new ApiResponse<IEnumerable<TransactionModelCreateModel>>(resultModel));
        }
    }
}