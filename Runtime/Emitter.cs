using System;
using System.Collections.Generic;

namespace Bremity
{
    public static class Emitter
    {
        private static readonly Dictionary<Type, object> _listeners = new();
        private static readonly Stack<int> _nullIndices = new();

        public static void Init()
        {
            _listeners.Clear();
            _nullIndices.Clear();
        }
        
        public static void AddListener<T>(IListener<T> listener) where T : ISignal
        {
            var signalType = typeof(T);
            if(!_listeners.ContainsKey(signalType))
            {
                _listeners[signalType] = new List<IListener<T>>();
            }

            List<IListener<T>> signalListeners = (List<IListener<T>>)_listeners[signalType];
            signalListeners.Add(listener);
        }
        
        public static void RemoveListener<T>(IListener<T> listener) where T : ISignal
        {
            var signalType = typeof(T);
            if (!_listeners.ContainsKey(signalType))
            {
                return;
            }
            List<IListener<T>> signalListeners = (List<IListener<T>>)_listeners[signalType];
            signalListeners.Remove(listener);
        }

        public static void Emit<T>(T signal = default) where T : ISignal
        {
            var signalType = typeof(T);
            if (!_listeners.ContainsKey(signalType))
            {
                return;
            }
            
            List<IListener<T>> signalListeners = (List<IListener<T>>)_listeners[signalType];
            int i = -1;
            foreach (var listener in signalListeners)
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
                signalListeners.RemoveAt(_nullIndices.Pop());
            }
        }
    }
}