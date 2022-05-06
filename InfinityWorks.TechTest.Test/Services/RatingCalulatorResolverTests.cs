using System;
using InfinityWorks.TechTest.Enum;
using InfinityWorks.TechTest.Services;
using Moq;
using NUnit.Framework;

namespace InfinityWorks.TechTest.Test.Services
{
	[TestFixture]
	public class RatingCalulatorResolverTests
	{
		private Mock<IServiceProvider> _serviceProviderMock;


		[SetUp]
		public void SetUp()
        {
			_serviceProviderMock = new Mock<IServiceProvider>();
			_serviceProviderMock.Setup(x => x.GetService(typeof(FHRSRatings))).Returns(new FHRSRatings());
			_serviceProviderMock.Setup(x => x.GetService(typeof(FHISRatings))).Returns(new FHISRatings());

		}

		[Test]
		public void Resolve_FHRSRatingSchema_ReturnsFHRSRatingType()
        {
			// Arrange
			var resolver = new RatingCalulatorResolver(_serviceProviderMock.Object);

			// Act
			var result = resolver.Resolve(RatingSchema.FHRS);

            // Assert
            Assert.IsInstanceOf<FHRSRatings>(result);

		}


		[Test]
		public void Resolve_FHISRatingSchema_ReturnsFHISRatingType()
		{
			// Arrange
			var resolver = new RatingCalulatorResolver(_serviceProviderMock.Object);

			// Act
			var result = resolver.Resolve(RatingSchema.FHIS);

			// Assert
			Assert.IsInstanceOf<FHISRatings>(result);

		}

		[Test]
		public void Resolver_UnSuportedRatingSchema_ThrowsNotSupportedException()
        {
			// Arrange
			var resolver = new RatingCalulatorResolver(_serviceProviderMock.Object);
			// Act
		
			// Assert
			Assert.Throws<NotSupportedException>(() => resolver.Resolve((RatingSchema)3));

        }
	}
}

