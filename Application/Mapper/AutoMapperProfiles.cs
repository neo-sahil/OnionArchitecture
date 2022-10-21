using Application.EntityDTO;
using Application.Extentions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberUserDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(scr => scr.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(scr => scr.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.MapGender()));
            //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == 'M' ? "Male": src.Gender == 'F' ? "Female" : "Other"));
            //.ForMember(dest => dest.Gender, opt => opt.MapFrom((src, dest) =>
            //{
            //    string gender = "";
            //    if (src.Gender == 'F' || src.Gender == 'f')
            //    {
            //        gender = "Female";
            //    }
            //    else if(src.Gender == 'M' || src.Gender == 'm')
            //    {
            //        gender = "Male";
            //    }
            //    else
            //    {
            //        gender = "Other";
            //    }
            //    return gender;
            //}));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
