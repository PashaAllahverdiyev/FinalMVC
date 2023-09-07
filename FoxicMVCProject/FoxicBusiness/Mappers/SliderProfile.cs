using AutoMapper;
using Foxic.Business.ViewModels.SliderViewModels;
using Foxic.Core.Entities.AreaEntityController;

namespace Foxic.Business.Mappers;

public class SliderProfile:Profile
{
	public SliderProfile()
	{
		CreateMap<Slider,SliderPostVM>().ReverseMap();
		CreateMap<SliderUploadVM,Slider>().ReverseMap();
	}

}
