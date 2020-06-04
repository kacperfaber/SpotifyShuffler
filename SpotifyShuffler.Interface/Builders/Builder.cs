using System;

namespace SpotifyShuffler.Interface
{
    public class Builder<TModel, TBuilder>
        where TModel : class
        where TBuilder : class
    {
        TModel Instance;

        public Builder(TModel instance = null)
        {
            Instance = instance == null ? Activator.CreateInstance<TModel>() : instance;
        }

        TModel Build() => Instance;

        protected TBuilder Update(Action<TModel> action)
        {
            action(Instance);
            return this as TBuilder;
        }
    }
}