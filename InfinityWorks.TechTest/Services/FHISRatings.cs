using System.Collections.Generic;
using InfinityWorks.TechTest.Model;

namespace InfinityWorks.TechTest.Services
{
	public class FHISRatings : IRatingCalulator
	{

		
		private readonly List<string> _ratingNames;

        private IRatingCalulator _ratingCalulator;

		public FHISRatings()
		{
			_ratingNames = new List<string> { "Pass and Eat Safe", "Pass", "Needs Improvement" };
      
		}
		
        public List<AuthorityRatingItem> GetRatingItems(List<FSAEstablishment> fSAEstablishments)
        {

			_ratingCalulator = this;

			var authorityRatings = new List<AuthorityRatingItem>();

            for (int i = 0; i < _ratingNames.Count; i++)
            {
                authorityRatings.Add(_ratingCalulator.CreateAuthorityRatingItem(fSAEstablishments,_ratingNames[i]));
            };
            
			return authorityRatings;
		   
        }

   

    }
}

