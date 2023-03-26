using AutoMapper;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Mapper.ProfileService;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.UnitTests.Infrastructure.Verifiers;

using static ProfileService.GRPC.ProfileService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders
{
    public abstract class BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        where TRequest : class, new()
        where TResponse : class, new()
        where TExpectedResult : class
    {
        protected BonusController? _bonusController;

        protected TRequest _bonusServiceClientRequest = new();
        protected TResponse _bonusServiceClientResponse = new();

        protected ApiResponse<TExpectedResult> _expectedResult = new();
        protected Mock<ProfileServiceClient> _mockProfileServiceClient = new();

        protected Mock<GrpcClientFactory> _mockGrpcClientFactory = new();

        public BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult> Prepare()
        {
            var mockLogger = new Mock<ILogger<BonusController>>();

            var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.AddProfile<DiscountModelMap>()));

            _bonusController = new(
                mockLogger.Object,
                _mockGrpcClientFactory.Object,
                mapper);

            _bonusController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestAborted = CancellationToken.None
            };

            return this;
        }

        public abstract BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetProfileServiceClientRequest(params string[] paramsStrings);

        public abstract BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetProfileServiceClientResponse(params string[] paramsStrings);

        public abstract BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetupProfileServiceClientResponse();

        public abstract BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetExpectedResult(params string[] paramsStrings);

        public BonusControllerTestVerifier<TRequest, TExpectedResult> Build()
        {
            if (_bonusController is null)
                throw new InvalidOperationException(
                    $"{nameof(BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>)} setup is wrong. Use Prepare method before build");

            return new BonusControllerTestVerifier<TRequest, TExpectedResult>(
                _bonusController,
                _bonusServiceClientRequest,
                _mockGrpcClientFactory,
                _mockProfileServiceClient,
                _expectedResult);
        }

        public BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetupProfileServiceClientGrpcFactory()
        {
            _mockGrpcClientFactory
                .Setup(f => f.CreateClient<ProfileServiceClient>(It.IsAny<string>()))
                .Returns(_mockProfileServiceClient.Object)
                .Verifiable();

            return this;
        }

        protected AsyncUnaryCall<T> GrpcAsyncUnaryCallBuilder<T>(T result) where T : class
        {
            var asyncUnaryCall = new AsyncUnaryCall<T>(
                Task.FromResult(result),
                null,
                null,
                null,
                null,
                null
            );

            return asyncUnaryCall;
        }
    }
}
