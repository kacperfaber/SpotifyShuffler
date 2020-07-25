﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Spencer.NET;

namespace SpotifyShuffler.Interface.Tests
{
    public class querygenerator_generate_tests
    {
        string exec(string url, params KeyValuePair<string, object>[] pairs)
        {
            QueryGenerator queryGenerator = new QueryGenerator(new QueryParameterGenerator());
            return queryGenerator.Generate(url, new Dictionary<string, object>(pairs));
        }

        [Test]
        public void dont_throws_exceptions()
        {
            Assert.DoesNotThrow(() => exec("hello", new KeyValuePair<string, object>("x", 0)));
        }

        [TestCase("localhost:2222", "value_x", 0)]
        [TestCase("localhost", "name", "kacper")]
        [TestCase("http://www.google.com/redirect", "x", false)]
        [TestCase("http://youtube.pl/", "max_", 251)]
        [TestCase("localhost:25252", "is_public", true)]
        public void returns_string_are_equals_to_expecteds(string url, string keyName, object value)
        {
            string s = exec(url, new KeyValuePair<string, object>(keyName, value));

            Assert.AreEqual(s, $@"{url}?{keyName}={value}");
        }

        [TestCase("localhost:2222", "value_x", 0, "helo", "world")]
        [TestCase("localhost", "name", "kacper", "update", false)]
        [TestCase("http://www.google.com/redirect", "x", false, "y", 0)]
        [TestCase("http://youtube.pl/", "max_", 251, "xxx", false)]
        [TestCase("localhost:25252", "is_public", true, "x", "d")]
        public void returns_string_are_equals_to_expecteds_when_parameters_count_is_two(string url, string k1, object v1, string k2, object v2)
        {
            string s = exec(url, new KeyValuePair<string, object>(k1, v1), new KeyValuePair<string, object>(k2, v2));

            Assert.AreEqual(s, $@"{url}?{k1}={v1}&{k2}={v2}");
        }

        [Test]
        public async Task xxx()
        {
            SpotifyAuthorization auth = SpotifyAuthorization.Create("BQCUJuM_EhTL0pWWXzLyczpYBSecMzf_zHILjXdbcELWjSIHf0T5plhwL-BD6wOefKwrZ86isgb1lLBSd5ncHbPyDwn4sgVfgZ2XjUxa3I4M4dCUrvX2PORrmL1AEKzPAOxxTwcnZRcRWk1xpqcQH6Xqt3RzmFv0lof6y5xV8Uyr_6pTU4AXWYONthni_mUxu1O4zMfsXRef3gChvfsAmVHZUdpfmp92q9R7yMwGCkwSbDB-ybB4SIIiKDyqdyISr2cbbyAJxrugbz6pw3aq6eX_ZN7_68kB");
            
            PlaylistService service = await ContainerFactory
                .Container()
                .ResolveOrAuto<SpotifyService>()
                .GetAsync<PlaylistService>(auth);
            
            SpotifyPlaylist playlist = await service.GetPlaylist("0J7cbNQx2M9yjjkABfMUjZ");

            await service.Clear(playlist);
        }
    }
}