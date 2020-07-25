using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotifyShuffler.Interface
{
    public class QueryGenerator : IQueryGenerator
    {
        public IQueryParameterGenerator QueryParameterGenerator;

        public QueryGenerator(IQueryParameterGenerator queryParameterGenerator)
        {
            QueryParameterGenerator = queryParameterGenerator;
        }

        public string Generate(string url, Dictionary<string, object> queryParameters)
        {
            StringBuilder stringBuilder = new StringBuilder(url);
            List<KeyValuePair<string, object>> list = queryParameters
                .Where(x => x.Value != null)
                .ToList();

            for (int i = 0; i < queryParameters.Count; i++)
            {
                if (i == 0)
                    stringBuilder.Append("?");

                else if (i < queryParameters.Count) stringBuilder.Append("&");

                stringBuilder.Append(QueryParameterGenerator.Generate(list[i]));
            }

            return stringBuilder.ToString();
        }
    }
}