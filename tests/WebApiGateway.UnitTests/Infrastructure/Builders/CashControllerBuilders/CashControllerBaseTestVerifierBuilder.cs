using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Grpc.Net.ClientFactory;
using AutoMapper;
using Moq;
using WebApiGateway.Controllers;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Mapper.CashService;
using WebApiGateway.UnitTests.Infrastructure.Builders.BonusControllerBuilders;
using WebApiGateway.UnitTests.Infrastructure.Verifiers;

using static CashService.GRPC.CashService;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.CashControllerBuilders
{
    public abstract class CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
        where TRequest : class, new()
        where TResponse : class, new()
        where TExpectedResult : class
    {
        protected CashController? _cashController;

        protected TRequest _cashServiceClientRequest = new();
        protected TResponse _cashServiceClientResponse = new();

        protected ApiResponse<TExpectedResult> _expectedResult = new();
        protected Mock<CashServiceClient> _mockCashServiceClient = new();

        protected Mock<GrpcClientFactory> _mockGrpcClientFactory = new();

        public CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult> Prepare()
        {
            var mockLogger = new Mock<ILogger<CashController>>();

            var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.AddProfile<TransactionModelApiMap>()));

            _cashController = new(
                mockLogger.Object,
                _mockGrpcClientFactory.Object,
                mapper);

            _cashController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestAborted = CancellationToken.None
            };

            return this;
        }

        public abstract CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetProfileServiceClientRequest(params string[] paramsStrings);

        public abstract CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetProfileServiceClientResponse(params string[] paramsStrings);

        public abstract CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetupProfileServiceClientResponse();

        public abstract CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetExpectedResult(params string[] paramsStrings);

        public CashControllerTestVerifier<TRequest, TExpectedResult> Build()
        {
            if (_cashController is null)
                throw new InvalidOperationException(
                    $"{nameof(BonusControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>)} setup is wrong. Use Prepare method before build");

            return new CashControllerTestVerifier<TRequest, TExpectedResult>(
                _cashController,
                _cashServiceClientRequest,
                _mockGrpcClientFactory,
                _mockCashServiceClient,
                _expectedResult);
        }

        public CashControllerBaseTestVerifierBuilder<TRequest, TResponse, TExpectedResult>
            SetupProfileServiceClientGrpcFactory()
        {
            _mockGrpcClientFactory
                .Setup(f => f.CreateClient<CashServiceClient>(It.IsAny<string>()))
                .Returns(_mockCashServiceClient.Object)
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
