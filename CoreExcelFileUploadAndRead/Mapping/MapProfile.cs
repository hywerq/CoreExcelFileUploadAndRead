using AutoMapper;
using CoreExcelFileUploadAndRead.Database.Entities;
using ExcelDataReader;

namespace CoreExcelFileUploadAndRead.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<IExcelDataReader, ExcelFile>()
				.ForMember(dest => dest.OpeningBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(1)))
				.ForMember(dest => dest.OpeningBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(2)))
				.ForMember(dest => dest.TurnoverDebit, x => x.MapFrom(src => (decimal)src.GetDouble(3)))
				.ForMember(dest => dest.TurnoverCredit, x => x.MapFrom(src => (decimal)src.GetDouble(4)))
				.ForMember(dest => dest.ClosingBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(5)))
				.ForMember(dest => dest.ClosingBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(6)));
			
			CreateMap<IExcelDataReader, Class>()
				.ForMember(dest => dest.Title, x => x.MapFrom(src => src.GetString(0)))
				.ForMember(dest => dest.OpeningBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(1)))
				.ForMember(dest => dest.OpeningBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(2)))
				.ForMember(dest => dest.TurnoverDebit, x => x.MapFrom(src => (decimal)src.GetDouble(3)))
				.ForMember(dest => dest.TurnoverCredit, x => x.MapFrom(src => (decimal)src.GetDouble(4)))
				.ForMember(dest => dest.ClosingBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(5)))
				.ForMember(dest => dest.ClosingBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(6)));

			CreateMap<IExcelDataReader, ClassGroup>()
				.ForMember(dest => dest.Number, x => x.MapFrom(src => (int)src.GetDouble(0)))
				.ForMember(dest => dest.OpeningBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(1)))
				.ForMember(dest => dest.OpeningBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(2)))
				.ForMember(dest => dest.TurnoverDebit, x => x.MapFrom(src => (decimal)src.GetDouble(3)))
				.ForMember(dest => dest.TurnoverCredit, x => x.MapFrom(src => (decimal)src.GetDouble(4)))
				.ForMember(dest => dest.ClosingBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(5)))
				.ForMember(dest => dest.ClosingBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(6)));

			CreateMap<IExcelDataReader, BalanceAccount>()
				.ForMember(dest => dest.Number, x => x.MapFrom(src => int.Parse(src.GetString(0))))
				.ForMember(dest => dest.OpeningBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(1)))
				.ForMember(dest => dest.OpeningBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(2)))
				.ForMember(dest => dest.TurnoverDebit, x => x.MapFrom(src => (decimal)src.GetDouble(3)))
				.ForMember(dest => dest.TurnoverCredit, x => x.MapFrom(src => (decimal)src.GetDouble(4)))
				.ForMember(dest => dest.ClosingBalanceActive, x => x.MapFrom(src => (decimal)src.GetDouble(5)))
				.ForMember(dest => dest.ClosingBalancePassive, x => x.MapFrom(src => (decimal)src.GetDouble(6)));
		}
	}
}
