using System;
using System.Collections.Generic;

namespace JarvenFramework.EventModule
{
    public interface IEvent
    {

    };

    public class EventHandle
    {
        readonly Dictionary<Type, Action<IEvent>> _dicEvent = new Dictionary<Type, Action<IEvent>>();

        readonly Dictionary<Delegate, Action<IEvent>> _dicEventLookups = new Dictionary<Delegate, Action<IEvent>>();

        public void AddListener<T>(Action<T> evt) where T : IEvent
        {
            if (!_dicEventLookups.ContainsKey(evt))
            {
                Action<IEvent> newAction = (e) => evt((T)e);
                _dicEventLookups[evt] = newAction;

                if (_dicEvent.TryGetValue(typeof(T), out Action<IEvent> internalAction))
                    _dicEvent[typeof(T)] = internalAction += newAction;
                else
                    _dicEvent[typeof(T)] = newAction;
            }
        }

        public void RemoveListener<T>(Action<T> evt) where T : IEvent
        {
            if (_dicEventLookups.TryGetValue(evt, out var action))
            {
                if (_dicEvent.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        _dicEvent.Remove(typeof(T));
                    else
                        _dicEvent[typeof(T)] = tempAction;
                }

                _dicEventLookups.Remove(evt);
            }
        }


        public void Dispatch(IEvent evt)
        {
            if (_dicEvent.TryGetValue(evt.GetType(), out var action))
            {
                action.Invoke(evt);
            }
        }

        public void Clear()
        {
            _dicEvent.Clear();
            _dicEventLookups.Clear();
        }
    }
}