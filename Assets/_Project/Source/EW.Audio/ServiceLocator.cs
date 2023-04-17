using System;
using System.Collections.Generic;
using UnityEngine;

namespace EW.Patterns
{
    public class ServiceLocator
    {
        private IDictionary<object, object> _services;
        private static ServiceLocator _current;

        public static ServiceLocator Current
        {
            get
            {
                if (_current != null)
                {
                    return _current;
                }
                else
                {
                    _current = new ServiceLocator();
                    return _current;
                }
            }
        }

        public T Get<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }

        public void Register<T>(T service)
        {
            object key = typeof(T);

            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key.GetType()} which is already registered with the {GetType().Name}.");
                return;
            }

            _services.Add(key, service);
        }

        public void Unregister<T>()
        {
            object key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key.GetType()} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }

        private ServiceLocator()
        {
            _services = new Dictionary<object, object>();
        }
    }
}