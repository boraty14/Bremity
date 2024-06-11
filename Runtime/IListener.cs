namespace Bremity
{
    public interface IListener<in T> where T : class, ISignal
    {
        void React(T signal);
    }
}