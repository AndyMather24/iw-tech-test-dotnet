using System;
using InfinityWorks.TechTest.Enum;
using InfinityWorks.TechTest.Services;
using Microsoft.Extensions.DependencyInjection;
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
	}
}

