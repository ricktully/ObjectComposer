namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public interface IObjectComposer
    {
        T Compose<S, T>(S source, params object[] optionalParams)
            where S : new()
            where T : new();

        T Compose<S, T>(S source, T target, params object[] optionalParams)
            where S : new()
            where T : new();
    }
}
