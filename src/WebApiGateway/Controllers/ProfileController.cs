using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using ProfileService.GRPC;
using WebApiGateway.Filters;
using WebApiGateway.Middleware;
using WebApiGateway.Models.ProfileService;
using static ProfileService.GRPC.ProfileService;

namespace WebApiGateway.Controllers
{
    // TODO: remove all unnecessary empty lines (make file clean and pretty)
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }


        // GET api/profile/"guid"
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileModel>> Get([FromRoute] string id)
        {
            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var request = new GetPersonalDataByIdRequest()
            {
                Profilebyidrequest = new ProfileByIdRequest()
                {
                    Id = id
                },
            };

            var result = await profileClient.GetPersonalDataByIdAsync(request, cancellationToken: token);

            var response = _mapper.Map<PersonalProfile, ProfileModel>(result.Personalprofile);

            return Ok(response);
        }


        // POST api/profile
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProfileModel profileModel)
        {
            // TODO: remove this check and throw. Exception should be thrown in global model state filter
            if (profileModel is null)
            {
                throw new FilterException("Model is null");
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<ProfileModel, PersonalProfile>(profileModel);

            var request = new AddPersonalDataRequest()
            {
                Personalprofile = requestModel
            };

            // TODO: remove unused variable result
            var result = await profileClient.AddPersonalDataAsync(request, cancellationToken: token);

            return Ok();
        }

        // PUT api/profile/
        [HttpPut]
        // TODO: remove Validate Model Filter. Global Filter should check Model State.
        [ValidateModelFilter]
        public async Task<ActionResult> Put([FromBody] ProfileModel profileModel)
        {
            // TODO: remove this check and throw. Exception should be thrown in global model state filter
            if (profileModel is null)
            {
                throw new FilterException("Model is null");
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>(nameof(ProfileServiceClient));
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<ProfileModel, PersonalProfile>(profileModel);

            var request = new UpdatePersonalDataRequest()
            {
                Personalprofile = requestModel
            };

            // TODO: remove unused variable result
            var result = await profileClient.UpdatePersonalDataAsync(request, cancellationToken: token);

            return Ok();
        }


    }

}
