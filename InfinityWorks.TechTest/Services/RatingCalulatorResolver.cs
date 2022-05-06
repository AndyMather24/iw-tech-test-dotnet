using System;
using InfinityWorks.TechTest.Enum;

using Microsoft.Extensions.DependencyInjection;

namespace InfinityWorks.TechTest.Services
{
	public class RatingCalulatorResolver : IRatingCalulatorResolver
	{

        private readonly IServiceProvider _serviceProvider;

        private IRatingCalulator _ratingCalulator;


        public RatingCalulatorResolver(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public IRatingCalulator Resolve(RatingSchema ratingSchema)
        { 
            switch (ratingSchema)
            {
                case RatingSchema.FHRS:
                    _ratingCalulator = _serviceProvider.GetRequiredService<FHRSRatings>();
                    break;
                case RatingSchema.FHIS:
                    _ratingCalulator = _serviceProvider.GetRequiredService<FHISRatings>();
                    break;
                default:
                    throw new NotSupportedException();

            }

            return _ratingCalulator;
        }

    }
}

