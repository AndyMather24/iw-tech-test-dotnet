using System.Collections.Generic;
using System.Linq;

using System.Text.Json.Serialization;
using InfinityWorks.TechTest;
using InfinityWorks.TechTest.Enum;

namespace InfinityWorks.TechTest.Model
{
    public class FSAEstablishmentList
    {

        public FSAEstablishmentList()
        {
            FSAEstablishments = new List<FSAEstablishment>();

        }

        [JsonPropertyName("establishments")]
        public List<FSAEstablishment> FSAEstablishments { get; set; }

       
        public RatingSchema RatingSchema {

            get

            {
                if(!FSAEstablishments.Any())
                {
                    return RatingSchema.FHRS;
                }

                var schemaString = FSAEstablishments.Select(x => x.RatingKey.Substring(0, 4)).First().ToUpper();
                return System.Enum.Parse<RatingSchema>(schemaString);
            }
        }

    }
}