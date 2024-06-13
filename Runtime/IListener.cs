namespace Bremity
{
    public interface IListener<in T> where T : ISignal
    {
        void React(T signal);
    }
}