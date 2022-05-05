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
					Name = "1",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "2",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "3",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "4",
					Value = 0.0
				},
				new AuthorityRatingItem()
				{
					Name = "5",
					Value = 0.0
				}
			};

			var sut = new FHRSRatings();

			// Act
			var result = sut.GetRatingItems(new List<FSAEstablishment>());

			// Assert
			Assert.AreEqual(result[0].Name, authorityRatingItems[0].Name);
			Assert.AreEqual(result[1].Name, authorityRatingItems[1].Name);
			Assert.AreEqual(result[2].Name, authorityRatingItems[2].Name);
			Assert.AreEqual(result[0].Value, authorityRatingItems[0].Value);
			Assert.AreEqual(result[1].Value, authorityRatingItems[1].Value);
			Assert.AreEqual(result[2].Value, authorityRatingItems[2].Value);
		}



	[Test]
	public void GetRatingItems_ReturnsRatingItemsWithValuePercentage()
        {
			//Arrange
			var authorityRatingItems = new List<AuthorityRatingItem>()
			{
				new AuthorityRatingItem()
				{
					Name = "1",
					Value = 25.0
				},
					new AuthorityRatingItem()
				{
					Name = "2",
					Value = 75.0
				},
							new AuthorityRatingItem()
				{
					Name = "3",
					Value = 0.0
				}
			};

			var establishments = new List<FSAEstablishment>()
			{
				new FSAEstablishment()
				{
					RatingValue = "1"

				},
				new FSAEstablishment()
				{
					RatingValue = "2"
				},
				new FSAEstablishment()
				{
					RatingValue = "2"
				},
				new FSAEstablishment()
				{
					RatingValue = "2"
				}
			};

			var sut = new FHRSRatings();

			// Act
			var result = sut.GetRatingItems(establishments);

			// Assert
			Assert.AreEqual(authorityRatingItems[0].Name, result[0].Name);
			Assert.AreEqual(authorityRatingItems[1].Name, result[1].Name);
			Assert.AreEqual(authorityRatingItems[2].Name, result[2].Name);
			Assert.AreEqual(authorityRatingItems[0].Value, result[0].Value);
			Assert.AreEqual(authorityRatingItems[1].Value, result[1].Value);
			Assert.AreEqual(authorityRatingItems[2].Value, result[2].Value);


		}
	}
}

