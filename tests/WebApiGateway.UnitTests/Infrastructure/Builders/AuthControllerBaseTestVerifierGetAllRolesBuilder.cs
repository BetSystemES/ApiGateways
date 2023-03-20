using System.Collections;
using AuthService.Grpc;
using FizzWare.NBuilder;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Moq;
using System.Collections.Generic;
using WebApiGateway.Models.API.Responses;
using WebApiGateway.Models.AuthService;
using WebApiGateway.Models.AuthService.Enums;
using WebApiGateway.Models.AuthService.Extensions;

namespace WebApiGateway.UnitTests.Infrastructure.Builders;

public class AuthControllerBaseTestVerifierGetAllRolesBuilder : AuthControllerBaseTestVerifierBuilder<Empty, GetAllRolesResponse, IEnumerable<RoleModel>>
{
    public override AuthControllerBaseTestVerifierBuilder<Empty, GetAllRolesResponse, IEnumerable<RoleModel>>
        SetAuthServiceClientRequest(params string[] paramsStrings)
    {
       return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<Empty, GetAllRolesResponse, IEnumerable<RoleModel>>
        SetAuthServiceClientResponse(params string[] paramsStrings)
    {
        _authServiceClientResponse = Builder<GetAllRolesResponse>
            .CreateNew()
            .Build();

        _authServiceClientResponse.Roles.AddRange(_authServiceClientRoles);

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<Empty, GetAllRolesResponse, IEnumerable<RoleModel>>
        SetupAuthServiceClientResponse()
    {
        var grpcResponse = GrpcAsyncUnaryCallBuilder(_authServiceClientResponse);

        _mockAuthServiceClient
            .Setup(f => f.GetAllRolesAsync(
                It.IsAny<GetAllRolesRequest>(),
                null,
                null,
                It.IsAny<CancellationToken>()))
            .Returns(grpcResponse);

        return this;
    }

    public override AuthControllerBaseTestVerifierBuilder<Empty, GetAllRolesResponse, IEnumerable<RoleModel>>
        SetExpectedResult(params string[] paramsStrings)
    {

        var authServiceClientRoles = Builder<RoleModel>
            .CreateListOfSize(2)
            .TheFirst(1)
            .With(x => x.Id = Guid.NewGuid())
            .With(x => x.Name = AuthRole.Admin.GetDescription())
            .TheNext(1)
            .With(x => x.Id = Guid.NewGuid())
            .With(x => x.Name = AuthRole.User.GetDescription())
            .Build();

        _expectedResult = Builder<ApiResponse<IEnumerable<RoleModel>>>
            .CreateNew()
            .With(x => x.Data = authServiceClientRoles)
            .Build();

        return this;
    }
}