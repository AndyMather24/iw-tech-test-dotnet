using System;
using System.Collections.Generic;
using InfinityWorks.TechTest.Model;
using InfinityWorks.TechTest.Services;
using NUnit.Framework;

namespace InfinityWorks.TechTest.Test.Services
{
	public class FHRSRatingsTests
	{


	[Test]
	public void GetRatingItems_EmptyEstablishmentsList_ReturnsListOfRatingItemsPerRatingName()
       {
			// Arrange
			var authorityRatingItems = new List<AuthorityRatingItem>()
			{
				
				new AuthorityRatingItem()
				{
					Name = "5",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "4",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "3",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "2",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "1",
					Value = 0.0
				},
				new AuthorityRatingItem()
                {
					Name = "Exempt",
					Value = 0.0
				}

			};

			var sut = new FHRSRatings();

			// Act
			var result = sut.GetRatingItems(new List<FSAEstablishment>());

			// Assert
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[0].Name));
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[1].Name));
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[2].Name));
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[3].Name));
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[4].Name));
			Assert.That(result,
				Has.Exactly(1).Matches<AuthorityRatingItem>(r => r.Name == authorityRatingItems[5].Name));
		}



		[Test]
		public void GetRatingItems_ReturnsRatingItemsWithValuePercentage()
		{
			//Arrange
			var authorityRatingItems = new List<AuthorityRatingItem>()
			{

				new AuthorityRatingItem()
				{
					Name = "5",
					Value = 75.0
				},
				new AuthorityRatingItem()
				{
					Name = "4",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "3",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "2",
					Value = 25.0
				},
				new AuthorityRatingItem()
				{
					Name = "1",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "Exempt",
					Value = 0.0
				}

			};

			var establishments = new List<FSAEstablishment>()
			{
				new FSAEstablishment()
				{
					RatingValue = "5"

				},
				new FSAEstablishment()
				{
					RatingValue = "5"
				},
				new FSAEstablishment()
				{
					RatingValue = "2"
				},
				new FSAEstablishment()
				{
					RatingValue = "5"
				}
			};
			var sut = new FHRSRatings();

			// Act
			var result = sut.GetRatingItems(establishments);

			// Assert
			Assert.AreEqual(authorityRatingItems[0].Value, result[0].Value);
			Assert.AreEqual(authorityRatingItems[1].Value, result[1].Value);
			Assert.AreEqual(authorityRatingItems[2].Value, result[2].Value);
			Assert.AreEqual(authorityRatingItems[3].Value, result[3].Value);
			Assert.AreEqual(authorityRatingItems[4].Value, result[4].Value);
		}

	}
}

