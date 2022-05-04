using System;
using InfinityWorks.TechTest.Enum;

namespace InfinityWorks.TechTest.Services
{
	public interface IRatingCalulatorResolver
	{
		public IRatingCalulator Resolve(RatingSchema ratingSchema);
	}
}

