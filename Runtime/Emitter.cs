using System.Collections.Generic;

namespace Bremity
{
    public static class Emitter<T> where T : class, ISignal
    {
        private static readonly List<IListener<T>> _listeners = new();
        private static readonly Stack<int> _nullIndices = new();

        public static void AddListener(IListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public static void RemoveListener(IListener<T> listener)
        {
            _listeners.Remove(listener);
        }

        public static void Emit(T signal = null)
        {
            int i = -1;
            foreach (var listener in _listeners)
            {
                i++;
                if (listener == null)
                {
                    _nullIndices.Push(i);
                    continue;
                }

                listener.React(signal);
            }
            
            while (_nullIndices.Count > 0)
            {
                _listeners.RemoveAt(_nullIndices.Pop());
            }
        }
    }
}