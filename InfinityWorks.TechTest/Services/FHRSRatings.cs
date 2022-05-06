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
		private IRatingCalulator _ratingCalulator;

		public FHRSRatings()
		{
			_ratingNames = new List<string> { "5", "4", "3", "2", "1", "Exempt" };
		}

        public List<AuthorityRatingItem> GetRatingItems(List<FSAEstablishment> fSAEstablishments)
        {
			_ratingCalulator = this;

			var authorityRatings = new List<AuthorityRatingItem>();

			for (int i = 0; i < _ratingNames.Count; i++)
			{
				authorityRatings.Add(_ratingCalulator.CreateAuthorityRatingItem(fSAEstablishments, _ratingNames[i]));
			};

			return authorityRatings;

		}
	}
}

