using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfinityWorks.TechTest.Model;

namespace InfinityWorks.TechTest.Services
{
	public interface IRatingCalulator
	{

		List<AuthorityRatingItem> GetRatingItems(List<FSAEstablishment> fSAEstablishments);

		IEnumerable<IGrouping<string, FSAEstablishment>> GetRatingsGroupedByName(List<FSAEstablishment> fSAEstablishments)
		{
			return fSAEstablishments.GroupBy(e => e.RatingValue);
		}

		List<FSAEstablishment> GetRatingListByKey(IEnumerable<IGrouping<string, FSAEstablishment>> ratingsGroupedByName, string ratingName)
		{
			return ratingsGroupedByName.FirstOrDefault(g => g.Key.ToLower() == ratingName.ToLower())?.ToList();
		}

		double CalulatePercentage(int ratingCount, int establishmentCount)
		{
			return ((double)ratingCount / establishmentCount) * 100;
		}

		public AuthorityRatingItem CreateAuthorityRatingItem(List<FSAEstablishment> fSAEstablishments, string ratingName)
		{

			var ratingsGroupedByName = GetRatingsGroupedByName(fSAEstablishments);

			var ratingListByKey = GetRatingListByKey(ratingsGroupedByName, ratingName);

			return new AuthorityRatingItem()
			{
				Name = ratingName,
				Value = ratingListByKey == null ? 0 : this.CalulatePercentage(ratingListByKey.Count(), fSAEstablishments.Count())
			};
		}

	}
}

