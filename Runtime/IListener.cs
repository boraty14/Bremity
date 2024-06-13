namespace Bremity
{
    public interface IListener<in T> where T : struct, ISignal
    {
        void React(T signal);
    }
}