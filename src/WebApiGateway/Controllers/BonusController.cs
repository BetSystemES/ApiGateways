using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.GRPC;
using Swashbuckle.AspNetCore.Annotations;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.BaseModels;
using WebApiGateway.Models.ProfileService;
using static ProfileService.GRPC.ProfileService;
using static WebApiGateway.Models.Constants.PolicyConstants;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusController : BaseAuthController
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<BonusController> _logger;

        public BonusController(ILogger<BonusController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }

        // GET api/bonus/"guid"
      
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Successfully get bonus(es)", typeof(List<DiscountModel>))]
        public async Task<ActionResult<List<DiscountModel>>> Get([FromRoute] BaseProfileRequstModel requstModel)
        {
            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetDiscountsRequest()
            {
                ProfileByIdRequest = new ProfileByIdRequest()
                {
                    Id = requstModel.ProfileId
                },
            };

            var result = await profileClient.GetDiscountsAsync(request, cancellationToken: token);

            List<DiscountModel> response = _mapper.Map<IEnumerable<Discount>, List<DiscountModel>>(result.Discounts);

            return Ok(new ApiResponse<List<DiscountModel>>(response));
        }

        // POST api/bonus
        [Authorize(Policy = AdminPolicy)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiscountModel discountModel)
        {
           var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<DiscountModel, Discount>(discountModel);

            var request = new AddDiscountRequest()
            {
                Discount = requestModel
            };

            var result = await profileClient.AddDiscountAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>());
        }

        // PUT api/bonus/
        [Authorize(Policy = AdminPolicy)]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] DiscountModel discountModel)
        {
            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<DiscountModel, Discount>(discountModel);

            var request = new UpdateDiscountRequest()
            {
                Discount = requestModel
            };

            var result = await profileClient.UpdateDiscountAsync(request, cancellationToken: token);

            return Ok(new ApiResponse<string>());
        }
    }
}