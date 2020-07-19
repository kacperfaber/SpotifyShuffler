using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ModelIndexer : IModelIndexer
    {
        public void Index<TModel>(List<TModel> models, Expression<Func<TModel, int>> expression)
        {
            try
            {
                PropertyInfo property = ((MemberExpression) expression.Body).Member as PropertyInfo;

                for (int i = 0; i <= models.Count; i++)
                {
                    property.SetValue(models[i], i);
                }
            }

            catch (ArgumentOutOfRangeException)
            {
            }
        }
    }
}