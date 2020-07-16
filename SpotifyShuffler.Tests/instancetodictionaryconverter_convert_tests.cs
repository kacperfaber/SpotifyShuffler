using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Tests
{
    public class instancetodictionaryconverter_convert_tests
    {

        Dictionary<string, object> exec(object o)
        {
            return new InstanceToDictionaryConverter().Convert(o);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            Assert.DoesNotThrow(() => exec(new {Hello = 0}));
        }

        [Test]
        public void returns_items_count_equals_to_object_properties()
        {
            Assert.IsTrue(exec(new {Age = 0, Id = 5}).Count == 2);
        }

        [Test]
        public void returns_field_name_equals_to_Age_if_Age_was_gived()
        {
            Assert.NotNull(exec(new {Age = 1}).SingleOrDefault(x => x.Key == "Age"));
        }

        [TestCase(5)]
        [TestCase(53)]
        [TestCase(68)]
        [TestCase(25)]
        public void returns_excepted_Age_value_gived_in_anonymous_object(int ageParam)
        {
            object age = exec(new {Age = ageParam}).SingleOrDefault(x => x.Key == "Age").Value;
            
            Assert.IsTrue((int) age == ageParam);
        }
    }
}