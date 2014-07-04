using AutoMapper;

namespace Renamer.UI
{
   public class RenamerProfile : Profile
   {
      public override string ProfileName
      {
         get { return "Renamer"; }
      }

      protected override void Configure()
      {
         CreateMap<ShowDirectory, ShowItem>()
            .ForMember(i => i.ShowName, opt => opt.MapFrom(s => "Nb"));
      }
   }
}