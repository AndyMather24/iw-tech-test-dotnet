using System;
using InfinityWorks.TechTest.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityWorks.TechTest.Services
{
	public class RatingCalulatorResolver : IRatingCalulatorResolver
	{

        private readonly IServiceProvider _serviceProvider;

        public RatingCalulatorResolver(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public IRatingCalulator Resolve(RatingSchema ratingSchema)
        { 
            switch (ratingSchema)
            {
                case RatingSchema.FHRS:
                    return _serviceProvider.GetService<FHRSRatings>();
                case RatingSchema.FHIS:
                    return _serviceProvider.GetService<FHISRatings>();
                default:
                    return null;
            
            }
        }
    }
}

