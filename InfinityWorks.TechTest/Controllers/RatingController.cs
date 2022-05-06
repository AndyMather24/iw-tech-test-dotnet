using System;
using System.Linq;
using System.Threading.Tasks;
using InfinityWorks.TechTest.Model;
using InfinityWorks.TechTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfinityWorks.TechTest.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly IFsaClient _fSaClient;
        private readonly IRatingCalulatorResolver _ratingCalulatorResolver;

        public RatingController(IFsaClient fSaClient, IRatingCalulatorResolver ratingCalulatorResolver)
        {
            _fSaClient = fSaClient;
            _ratingCalulatorResolver = ratingCalulatorResolver;

        }

        /// <summary>
        /// Produces a list of authorities, for the select dropdown
        /// </summary>
        /// <returns>
        /// List of authorities
        /// </returns>
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var fsaAuthorities = await _fSaClient.GetAuthorities();
            var authorityList = fsaAuthorities.Authorities.Select(authority => new Authority { Id = authority.LocalAuthorityId, Name = authority.Name });
            return Json(authorityList);
        }

        /// <summary>
        /// Produces the ratings for a specific authority for the table
        /// </summary>
        /// <param name="authorityId">The authority to calculate ratings for</param>
        /// <returns>
        /// The ratings to display
        /// </returns>
        [HttpGet("{authorityId}")]
        public async Task<JsonResult> GetRatingsAsync(int authorityId)
        {
            try
            {

                var establishments = await _fSaClient.GetEstablishmentsAsync(authorityId);

                var ratingCalulator = _ratingCalulatorResolver.Resolve(establishments.RatingSchema);

                var ratingItems = ratingCalulator.GetRatingItems(establishments.FSAEstablishments);

                return Json(ratingItems);
            } catch(Exception e)
            {

                //TODO: once inject a logger into this controller I can then log the exception details
                throw; 
            }

        }
    }
}