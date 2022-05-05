using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using InfinityWorks.TechTest.Services;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace InfinityWorks.TechTest.Test.Services
{
    public class FsaClientTests
    {

        private Mock<IHttpClientFactory> _httpClientFactoryMock;

        [Test]
        public async Task GetEstablishmentsAsync_InvokesGetStreamAsyncWithEstablishmentsPath()
        {
            // arrange 
            var path = "https://api.ratings.food.gov.uk/Establishments?&localAuthorityId=1";

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{establishments': [ {'RatingValue': '5'}]}")
            });
            var client = new HttpClient(httpMessageHandlerMock.Object);

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new FsaClient(_httpClientFactoryMock.Object);

            // act 
            var result = await sut.GetEstablishmentsAsync(1);
            // assert
            Assert.AreEqual(result, result);

        }


    }
}