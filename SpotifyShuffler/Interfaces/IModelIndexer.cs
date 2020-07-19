using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpotifyShuffler.Interfaces
{
    public interface IModelIndexer
    {
        void Index<TModel>(List<TModel> models, Expression<Func<TModel, int>> expression);
    }
}