using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using InfinityWorks.TechTest.Model;

namespace InfinityWorks.TechTest.Services
{
	public class FHRSRatings : IRatingCalulator
	{

		private readonly List<string> _ratingNames; 

		public FHRSRatings()
		{
			_ratingNames = new List<string> { "1", "2", "3", "4", "5" };
		}

        public List<AuthorityRatingItem> GetRatingItems(List<FSAEstablishment> fSAEstablishments)
        {
			IRatingCalulator ratingCalulator = this;
			var authorityRatings = new List<AuthorityRatingItem>();

			var ratingsGroupedByName = fSAEstablishments.GroupBy(e => e.RatingValue);

			for (int i = 0; i < _ratingNames.Count; i++)
			{
				var ratingGroupList = ratingsGroupedByName.FirstOrDefault(g => g.Key.ToLower() == _ratingNames[i].ToLower())?.ToList();

				authorityRatings.Add(new AuthorityRatingItem()
				{
					Name = _ratingNames[i],
					Value = ratingGroupList == null ? 0 : ratingCalulator.CalulatePercentage(ratingGroupList.Count(), fSAEstablishments.Count())
				});
			};

			return authorityRatings;

		}
	}
}

