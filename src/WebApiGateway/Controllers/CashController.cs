﻿using AutoMapper;
using CashService.GRPC;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiGateway.AppDependencies;
using WebApiGateway.Models.CashService;
using static CashService.GRPC.Casher;


namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashController : ControllerBase
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

        //[HttpGet("{id}")]
        [HttpGet("{id}/transactions", Name = nameof(GetTransactionsHistory))]
        
        public async Task<ActionResult<TransactionModelApi>> GetTransactionsHistory([FromRoute] string id)
        {
            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var request = new GetTransactionsHistoryRequest()
            {
               Profileid = id
            };

            var result = await cashClient.GetTransactionsHistoryAsync(request, cancellationToken: token);

            var response = _mapper.Map<TransactionModel, TransactionModelApi>(result.Balance);

            return Ok(response);
        }

        
        [HttpGet("{id}", Name = nameof(GetBalance)) ]
        public async Task<ActionResult<TransactionModelApi>> GetBalance([FromRoute] string id)
        {
            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var request = new GetBalanceRequest()
            {
                Profileid = id
            };

            var result = await cashClient.GetBalanceAsync(request, cancellationToken: token);

            var response = _mapper.Map<TransactionModel, TransactionModelApi>(result.Balance);

            return Ok(response);
        }


        [HttpPost("deposit", Name = nameof(Deposit))]
        public async Task<ActionResult> Deposit([FromBody] TransactionModelApi transactionModelApi)
        {
            if (transactionModelApi == null)
            {
                return BadRequest();
            }

            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<TransactionModelApi ,TransactionModel >(transactionModelApi);

            var request = new DepositRequest()
            {
                Deposit = requestModel
            };

            var result = await cashClient.DepositAsync(request, cancellationToken: token);

            return Ok();
        }

        [HttpPost("withdraw", Name = nameof(Withdraw))]
        public async Task<ActionResult<TransactionModelApi>> Withdraw([FromBody] TransactionModelApi transactionModelApi)
        {
            if (transactionModelApi == null)
            {
                return BadRequest();
            }

            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<TransactionModelApi, TransactionModel>(transactionModelApi);

            var request = new WithdrawRequest()
            {
                Withdrawrequest = requestModel
            };

            var result = await cashClient.WithdrawAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<TransactionModel, TransactionModelApi>(result.Withdrawresponce);

            return Ok(resultModel);
        }


        [HttpPost("depositrange", Name = nameof(DepositRange))]
        public async Task<ActionResult> DepositRange([FromBody] IEnumerable<TransactionModelApi> transactionModelApis)
        {
            if (transactionModelApis == null)
            {
                return BadRequest();
            }

            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map< IEnumerable<TransactionModelApi>, IEnumerable<TransactionModel>>(transactionModelApis);

            var request = new DepositRangeRequest();
            request.Depositrangerequest.AddRange(requestModel);

            var result = await cashClient.DepositRangeAsync(request, cancellationToken: token);

            return Ok();
        }

        [HttpPost("withdrawrange", Name = nameof(WithdrawRange))]
        public async Task<ActionResult<IEnumerable<TransactionModelApi>>> WithdrawRange([FromBody] IEnumerable<TransactionModelApi> transactionModelApis)
        {
            if (transactionModelApis == null)
            {
                return BadRequest();
            }

            var cashClient = _grpcClientFactory.CreateClient<CasherClient>(ClientNames.CashClient);
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<IEnumerable<TransactionModelApi>, List<TransactionModel>>(transactionModelApis);

            var request = new WithdrawRangeRequest();
            request.Withdrawrangerequest.AddRange(requestModel);

            var result = await cashClient.WithdrawRangeAsync(request, cancellationToken: token);

            var resultModel = _mapper.Map<IEnumerable<TransactionModel>, List<TransactionModelApi>>(result.Withdrawrangeresponce);

            return Ok(resultModel);
        }

    }

}
