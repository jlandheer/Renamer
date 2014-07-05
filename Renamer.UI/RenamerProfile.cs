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
            .ForMember(i => i.Status, opt => opt.Ignore())
            .ForMember(i => i.Episodes, opt => opt.Ignore())
            .ForMember(i => i.ShowName, opt => opt.Ignore());

         CreateMap<Episode, EpisodeItem>();

         CreateMap<SearchResult, SearchResultItem>();
      }
   }
}