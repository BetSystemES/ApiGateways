using AutoMapper;
using CashService.GRPC;
using Google.Protobuf.WellKnownTypes;
using WebApiGateway.Mapper.Extensions;
using WebApiGateway.Models.CashService;
using WebApiGateway.Models.CashService.Enums;
using WebApiGateway.Models.CashService.ViewModel;

namespace WebApiGateway.Mapper.CashService
{
    public class TransactionModelApiMap : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="TransactionModelApiMap" /> class.</summary>
        public TransactionModelApiMap()
        {
            CreateMap<DateTimeOffset, Timestamp>()
                .ConvertUsing((x, res) => res = x.ToTimestamp());
            CreateMap<Timestamp, DateTimeOffset>()
                .ConvertUsing((x, res) => res = x.ToDateTimeOffset());

            CreateMap<Transaction, TransactionCreateModel>()
                .ReverseMap()
                .Ignore(e => e.Id);

            CreateMap<TransactionModel, TransactionModelCreateModel>()
                .ReverseMap();

            CreateMap<TransactionModelCreateModel, TransactionModel>()
                .ForMember(x => x.Transactions, y => y.MapFrom(z => z.TransactionApis))
                .Ignore(e => e.Amount)
                .ReverseMap();

            CreateMap<CashType, CashTypeApi>()
                .ReverseMap();

            CreateMap<TransactionViewModel, Transaction>()
                .ReverseMap();

            CreateMap<TransactionModel, TransactionModelViewModel>()
                .ReverseMap()
                .Ignore(e => e.Amount)
                .Ignore(e => e.ProfileId);
        }
    }
}