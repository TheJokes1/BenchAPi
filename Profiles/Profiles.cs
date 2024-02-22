using AutoMapper;
using CommonDTO.DTO;
using Entities.MainEntities;

namespace BenchAPI.Profiles
{
	public class Profiles : Profile
	{
		public Profiles()
		{
			CreateMap<CafeDTO, Cafe>().ReverseMap();

			CreateMap<Order, OrderDTO>()
				.ForMember(dest => dest.CafeId, opt => opt.MapFrom(src => src.CafeId ?? 0))
				.ForMember(dest => dest.OrderState, opt => opt.MapFrom(src => src.OrderState))
				.ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines)) 
				.ReverseMap();

			CreateMap<OrderlineDTO, Orderline>().ReverseMap();

			CreateMap<BeerDTO, Beer>().ReverseMap();

			CreateMap<OrderlineDetail, OrderlineDetailDTO>().ReverseMap();

			CreateMap<Order, OrderToReturnDTO>().ReverseMap();
		}

	}
}
