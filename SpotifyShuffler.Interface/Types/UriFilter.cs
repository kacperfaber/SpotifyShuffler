using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public class UriFilter : IUriFilter
    {
        public IUriValidator UriValidator;

        public UriFilter(IUriValidator uriValidator)
        {
            UriValidator = uriValidator;
        }

        public IEnumerable<string> Filter(IEnumerable<string> uris)
        {
            foreach (string uri in uris)
            {
                if (UriValidator.ValidateAsync(uri).Result)
                {
                    yield return uri;
                }
            }
        }
    }
}