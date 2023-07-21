using AutoMapper;
using CompetitionService.Grpc;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiGateway.Models.API.Requests;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.CompetitionService.Requests;
using static CompetitionService.Grpc.CompetitionService;
using BusinessModels = WebApiGateway.Models.CompetitionService;
using CreateModels = WebApiGateway.Models.CompetitionService.CreateModels;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionController : BaseAuthController
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        public CompetitionController(IMapper mapper, GrpcClientFactory grpcClientFactory)
        {
            _mapper = mapper;
            _grpcClientFactory = grpcClientFactory;
        }

        [HttpGet("get-competitions-dota2/{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<CompetitionDota2>>> GetCompetitionsDota2(int page, int pageSize)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcRequest = new GetCompetitionsDota2Request()
            {
                Page = page,
                PageSize = pageSize
            };

            var grpcResponse = await competitionClient.GetCompetitionsDota2Async(grpcRequest, cancellationToken: token);

            var response = _mapper.Map<IEnumerable<BusinessModels.Entities.CompetitionDota2>>(grpcResponse.CompetitionsDota2);

            return Ok(new ApiResponse<IEnumerable<BusinessModels.Entities.CompetitionDota2>>(response));
        }

        [HttpGet("competition/{id}")]
        public async Task<ActionResult<BusinessModels.Entities.CompetitionDota2>> GetCompetitionDota2(string id)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcRequest = new GetCompetitionDota2Request()
            {
                Id = id,
            };

            var grpcResponse = await competitionClient.GetCompetitionDota2Async(grpcRequest, cancellationToken: token);
            var competition = _mapper.Map<BusinessModels.Entities.CompetitionDota2>(grpcResponse.CompetitionDota2);

            return Ok(new ApiResponse<BusinessModels.Entities.CompetitionDota2>(competition));
        }

        [HttpPost("create-competition-dota2", Name = nameof(CreateCompetitionDota2))]
        public async Task<ActionResult> CreateCompetitionDota2(
            [FromBody] CreateCompetitionDota2RequestModel<CreateModels.CompetitionDota2.CompetitionDota2CreateModel> requestModel)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCompetition = _mapper.Map<CompetitionDota2CreateModel>(requestModel.CompetitionModel);

            var grpcRequest = new CreateCompetitionDota2Request()
            {
                CompetitionDota2CreateModel = grpcCompetition,
            };

            var grpcResponse = await competitionClient.CreateCompetitionDota2Async(grpcRequest, cancellationToken: token);

            // todo: return competition from grpc service
            return Ok();
        }

        [HttpPost("create-competition-base", Name = nameof(CreateCompetitionBase))]
        public async Task<ActionResult<BusinessModels.Entities.CompetitionBase>> CreateCompetitionBase([FromBody] CreateModels.CompetitionBaseCreateModel competitionBaseCreateModel)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCompetitionBase = _mapper.Map<CompetitionBaseCreateModel>(competitionBaseCreateModel);

            var grpcRequest = new CreateCompetitionBaseRequest()
            {
                CompetitionBaseCreateModel = grpcCompetitionBase,
            };

            var grpcResponse = await competitionClient.CreateCompetitionBaseAsync(grpcRequest, cancellationToken: token);

            var competitionBase = _mapper.Map<BusinessModels.Entities.CompetitionBase>(grpcResponse.CompetitionBase);

            return Ok(competitionBase);
        }

        [HttpPost("create-outcome", Name = nameof(CreateOutcome))]
        public async Task<ActionResult> CreateOutcome([FromBody] CreateModels.CoefficientGroupCreateModel coefficientGroupCreateModel)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCoefficientGroup = _mapper.Map<CoefficientGroupCreateModel>(coefficientGroupCreateModel);

            var grpcRequest = new CreateOutcomeRequest()
            {
                CoefficientGroupCreateModel = grpcCoefficientGroup,
            };

            var grpcResponse = await competitionClient.CreateOutcomeAsync(grpcRequest, cancellationToken: token);

            // todo: return competition from grpc service
            return Ok();
        }

        [HttpPost("create-coefficient", Name = nameof(CreateCoefficient))]
        public async Task<ActionResult> CreateCoefficient([FromBody] CreateModels.CoefficientCreateModel coefficientCreateModel)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCoefficient = _mapper.Map<CoefficientCreateModel>(coefficientCreateModel);

            var grpcRequest = new CreateCoefficientRequest()
            {
                CoefficientCreateModel = grpcCoefficient,
            };

            var grpcResponse = await competitionClient.CreateCoefficientAsync(grpcRequest, cancellationToken: token);

            // todo: return competition from grpc service
            return Ok();
        }

        [HttpPost("block-competition", Name = nameof(BlockCompetitionBaseById))]
        public async Task<ActionResult> BlockCompetitionBaseById([FromBody] Guid id)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcRequest = new BlockCompetitionBaseByIdRequest()
            {
                Id = id.ToString()
            };

            var grpcResponse = await competitionClient.BlockCompetitionBaseByIdAsync(grpcRequest, cancellationToken: token);

            return Ok();
        }

        [HttpPost("deposit-to-coefficient", Name = nameof(DepositToCoefficientById))]
        public async Task<ActionResult> DepositToCoefficientById([FromBody] DepositToCoefficientModel depositToCoefficientModel)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcRequest = new DepositToCoefficientByIdRequest()
            {
                CoefficientId = depositToCoefficientModel.CoefficientId,
                Amount = depositToCoefficientModel.Amount,
                UserId = depositToCoefficientModel.UserId.ToString()
            };

            var grpcResponse = await competitionClient.DepositToCoefficientByIdAsync(grpcRequest, cancellationToken: token);

            // todo: return competition from grpc service
            return Ok(new ApiResponse<string>(grpcResponse.Rate.ToString()));
        }

        [HttpPost("update-coefficient", Name = nameof(UpdateCoefficient))]
        public async Task<ActionResult> UpdateCoefficient([FromBody] BusinessModels.UpdateModels.CoefficientUpdateModel coefficient)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCoefficient = _mapper.Map<CoefficientUpdateModel>(coefficient);

            var grpcRequest = new UpdateCoefficientRequest()
            {
                CoefficientUpdateModel = grpcCoefficient,
            };

            var grpcResponse = await competitionClient.UpdateCoefficientAsync(grpcRequest, cancellationToken: token);

            // todo: return coefficient from grpc service
            return Ok();
        }

        [HttpPost("update-outcome", Name = nameof(UpdateOutcome))]
        public async Task<ActionResult> UpdateOutcome([FromBody] BusinessModels.UpdateModels.CoefficientGroupUpdateModel coefficientGroup)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCoefficientGroup = _mapper.Map<CoefficientGroupUpdateModel>(coefficientGroup);

            var grpcRequest = new UpdateOutcomeRequest()
            {
                OutcomeUpdateModel = grpcCoefficientGroup,
            };

            var grpcResponse = await competitionClient.UpdateOutcomeAsync(grpcRequest, cancellationToken: token);

            // todo: return coefficient from grpc service
            return Ok();
        }

        [HttpPost("update-competition-base", Name = nameof(UpdateCompetitionBase))]
        public async Task<ActionResult> UpdateCompetitionBase([FromBody] BusinessModels.UpdateModels.CompetitionBaseUpdateModel competitionBase)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCompetitionBase = _mapper.Map<CompetitionBaseUpdateModel>(competitionBase);

            var grpcRequest = new UpdateCompetitionBaseRequest()
            {
                CompetitionBaseUpdateModel = grpcCompetitionBase,
            };

            var grpcResponse = await competitionClient.UpdateCompetitionBaseAsync(grpcRequest, cancellationToken: token);

            // todo: return coefficient from grpc service
            return Ok();
        }

        [HttpPost("update-competition-dota2", Name = nameof(UpdateCompetitionDota2))]
        public async Task<ActionResult> UpdateCompetitionDota2([FromBody] BusinessModels.UpdateModels.CompetitionDota2UpdateModel competitionDota2)
        {
            var competitionClient = _grpcClientFactory.CreateClient<CompetitionServiceClient>(nameof(CompetitionServiceClient));
            var token = HttpContext.RequestAborted;

            var grpcCompetitionDota2UpdateModel = _mapper.Map<CompetitionDota2UpdateModel>(competitionDota2);


            var grpcRequest = new UpdateCompetitionDota2Request()
            {
                CompetitionDota2UpdateModel = grpcCompetitionDota2UpdateModel,
            };

            var grpcResponse = await competitionClient.UpdateCompetitionDota2Async(grpcRequest, cancellationToken: token);

            // todo: return coefficient from grpc service
            return Ok();
        }
    }
}