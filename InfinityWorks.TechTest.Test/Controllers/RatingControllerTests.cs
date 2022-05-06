using InfinityWorks.TechTest.Controllers;
using InfinityWorks.TechTest.Enum;
using InfinityWorks.TechTest.Model;
using InfinityWorks.TechTest.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfinityWorks.TechTest.Test.Controllers
{
    class RatingControllerTests
    {

        private Mock<IFsaClient> _fsaClientMock;
        private Mock<IRatingCalulatorResolver> _ratingCalulatorResolver;
        private RatingController _sut;

        private const int AuthorityId = 1;

        [SetUp]
        public void SetUp()
        {
            _fsaClientMock = new Mock<IFsaClient>();
            _ratingCalulatorResolver = new Mock<IRatingCalulatorResolver>();
            _sut = new RatingController(_fsaClientMock.Object, _ratingCalulatorResolver.Object);

            _ratingCalulatorResolver.Setup(x => x.Resolve(RatingSchema.FHIS)).Returns(new FHISRatings());

            _ratingCalulatorResolver.Setup(x => x.Resolve(RatingSchema.FHRS)).Returns(new FHRSRatings());

        }

        [Test]
        public async Task GetAsync_ReturnsAllAuthorities()
        {
            // Arrange
            var authorityList = new FsaAuthorityList();
            authorityList.Authorities = new List<FsaAuthority>
            {
                new FsaAuthority { Name = "authority1", LocalAuthorityId = 1 },
                new FsaAuthority { Name = "authority2", LocalAuthorityId = 2 }
            };


            _fsaClientMock.Setup(c => c.GetAuthorities()).ReturnsAsync(authorityList);
         
            // Act
            var jsonResult = await _sut.GetAsync();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Authority>>(jsonResult.Value);
            var authorities = ((IEnumerable<Authority>)jsonResult.Value).ToArray();
            Assert.AreEqual(authorities.Length, 2);
            Assert.AreEqual(authorities[0].Name, "authority1");
            Assert.AreEqual(authorities[0].Id, 1);
            Assert.AreEqual(authorities[1].Name, "authority2");
            Assert.AreEqual(authorities[1].Id, 2);
        }


        [Test]
        public async Task GetRatingsAsync_AuthorityId_InvokesGetEstablishmentsWithAuthorityIdOnce()
        {

            // Act
            var establishments = new FSAEstablishmentList();
            
            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "5", RatingKey = "fhrs_5_en-gb" });
            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "1", RatingKey = "fhrs_5_en-gb" });

            _fsaClientMock.Setup(x => x.GetEstablishmentsAsync(AuthorityId)).ReturnsAsync(establishments);

            var jsonResult = await _sut.GetRatingsAsync(AuthorityId);

            // Assert
            _fsaClientMock.Verify(x => x.GetEstablishmentsAsync(AuthorityId), Times.Once);

        }

        [Test]
        public async Task GetRatingAsync_AuthorityId_InvokesResolverWithEstablishmentRatingKeyOnce()
        {
            // Arrange
            var establishments = new FSAEstablishmentList();
            _fsaClientMock.Setup(x => x.GetEstablishmentsAsync(AuthorityId)).ReturnsAsync(establishments);
            _fsaClientMock.Setup(x => x.GetEstablishmentsAsync(AuthorityId)).ReturnsAsync(establishments);

            // Act
            var result =_sut.GetRatingsAsync(AuthorityId);

            // Assert
            _ratingCalulatorResolver.Verify(x => x.Resolve(establishments.RatingSchema), Times.Once);
  
        }


        [Test]
        public async Task GetRatingAsync_FHRSRatings_ReturnsRatingItemsInCorrectOrder()
        {
            // Arrange
            var establishments = new FSAEstablishmentList();

            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "5", RatingKey = "fhrs_5_en-gb" });
            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "1", RatingKey = "fhrs_5_en-gb" });

            var authorityRatingItems = new List<AuthorityRatingItem>()
            {
                new AuthorityRatingItem() {Name = "5" , Value = 50.00},
                new AuthorityRatingItem() {Name = "4", Value = 0 },
                new AuthorityRatingItem() {Name = "3", Value = 0 },
                new AuthorityRatingItem() {Name = "2", Value = 0 },
                new AuthorityRatingItem() {Name = "1", Value = 50.00 }
            };

            _fsaClientMock.Setup(x => x.GetEstablishmentsAsync(AuthorityId)).ReturnsAsync(establishments);

            // Act
            var result = await _sut.GetRatingsAsync(AuthorityId);

            // Assert
            Assert.IsInstanceOf<IEnumerable<AuthorityRatingItem>>(result.Value);
            var ratingItems = ((IEnumerable<AuthorityRatingItem>)result.Value).ToArray();
            Assert.AreEqual(authorityRatingItems[0].Name, ratingItems[0].Name);
            Assert.AreEqual(authorityRatingItems[1].Name, ratingItems[1].Name);
            Assert.AreEqual(authorityRatingItems[2].Name, ratingItems[2].Name);
            Assert.AreEqual(authorityRatingItems[3].Name, ratingItems[3].Name);
            Assert.AreEqual(authorityRatingItems[4].Name, ratingItems[4].Name);
            Assert.AreEqual(authorityRatingItems[0].Value, ratingItems[0].Value);
            Assert.AreEqual(authorityRatingItems[1].Value, ratingItems[1].Value);
            Assert.AreEqual(authorityRatingItems[2].Value, ratingItems[2].Value);
            Assert.AreEqual(authorityRatingItems[3].Value, ratingItems[3].Value);
            Assert.AreEqual(authorityRatingItems[4].Value, ratingItems[4].Value);

        }


        [Test]
        public async Task GetRatingAsync_FHISRatings_ReturnsRatingItemsInCorrectOrder()
        {
            // Arrange
            var establishments = new FSAEstablishmentList();

            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "Pass", RatingKey = "fhis_pass_en-gb" });
            establishments.FSAEstablishments.Add(new FSAEstablishment { RatingValue = "Pass", RatingKey = "fhis_pass_en-gb" });

            var authorityRatingItems = new List<AuthorityRatingItem>()
            {
                new AuthorityRatingItem() {Name = "Pass and Eat Safe" , Value = 0.0},
                new AuthorityRatingItem() {Name = "Pass", Value = 100.0 },
                new AuthorityRatingItem() {Name = "Needs Improvement", Value = 0.0 }
            };

         

            _fsaClientMock.Setup(x => x.GetEstablishmentsAsync(AuthorityId)).ReturnsAsync(establishments);

            // Act
            var result = await _sut.GetRatingsAsync(AuthorityId);

            // Assert
            Assert.IsInstanceOf<IEnumerable<AuthorityRatingItem>>(result.Value);
            var ratingItems = ((IEnumerable<AuthorityRatingItem>)result.Value).ToArray();
            Assert.AreEqual(authorityRatingItems[0].Name, ratingItems[0].Name);
            Assert.AreEqual(authorityRatingItems[1].Name, ratingItems[1].Name);
            Assert.AreEqual(authorityRatingItems[2].Name, ratingItems[2].Name);
            Assert.AreEqual(authorityRatingItems[0].Value, ratingItems[0].Value);
            Assert.AreEqual(authorityRatingItems[1].Value, ratingItems[1].Value);
            Assert.AreEqual(authorityRatingItems[2].Value, ratingItems[2].Value);

        }

    }
}
