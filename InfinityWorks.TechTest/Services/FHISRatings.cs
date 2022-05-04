using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        public Task<AuthorityRatingItem> GetRatingItems(List<FSAEstablishment> fSAEstablishments)
        {
            throw new NotImplementedException();
        }
    }
}

