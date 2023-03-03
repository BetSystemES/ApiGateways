using AutoMapper;
using CashService.GRPC;
using WebApiGateway.Models.CashService;
using WebApiGateway.Models.CashService.Enums;

namespace WebApiGateway.Mapper.CashService
{
    public class TransactionModelApiMap : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="TransactionModelApiMap" /> class.</summary>
        public TransactionModelApiMap()
        {
            CreateMap<Transaction, TransactionApi>().ReverseMap();

            CreateMap<TransactionModel, TransactionModelApi>().ReverseMap();

            CreateMap<TransactionModelApi, TransactionModel>()
                .ForMember(x => x.Transactions, y => y.MapFrom(z => z.TransactionApis))
                .ReverseMap();

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));

            CreateMap<CashType, CashTypeApi>().ReverseMap();
        }

    }
}
