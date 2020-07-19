using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Tests
{
    public class modelindexer_index_tests
    {
        class Model
        {
            public int Index { get; set; }
        }

        List<Model> list(int count)
        {
            List<Model> models = new List<Model>();
            
            for (int i = 0; i <= count; i++)
            {
                models.Add(new Model());
            }

            return models;
        }

        void exec(List<Model> models)
        {
            ModelIndexer indexer = new ModelIndexer();
            indexer.Index(models, x => x.Index);
        }

        [Test]
        public void dont_throws_exceptions()
        {
            List<Model> models = list(25);
            
            Assert.DoesNotThrow(() => exec(models));
        }

        [Test]
        public void first_item_has_Index_equals_to_0()
        {
            List<Model> models = list(25);
            
            exec(models);
            
            Assert.IsTrue(models.First().Index == 0);
        }

        [Test]
        public void last_item_has_Index_greater_than_0()
        {
            List<Model> models = list(25);
            
            exec(models);
            
            Assert.IsTrue(models.Last().Index > 0);
        }
    }
}