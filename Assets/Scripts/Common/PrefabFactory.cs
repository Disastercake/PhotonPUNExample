// Copyright (c) 2021 Disastercake MIT License (https://opensource.org/licenses/MIT)
// Latest version can be found at: https://gist.github.com/Disastercake/7a5960517c6b32cef1c4b500b8ae061c

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefabFactorySystem
{
    /// <summary>
    /// Allows for an object to be used with PrefabFactory.
    /// </summary>
    public interface IPoolableGameObject
    {
        /// <summary>
        /// If true, then this object is pooled.
        /// </summary>
        bool IsPooled { get; }
        
        /// <summary>
        /// Most common usage:
        /// IsPooled = true;
        /// gameObject.SetActive(false);
        /// </summary>
        void OnReturnToPool();
        
        /// <summary>
        /// Most common usage:
        /// IsPooled = false;
        /// gameObject.SetActive(true);
        /// </summary>
        void OnRemoveFromPool();

        /// <summary>
        /// The transform of this GameObject to avoid unnecessary casting.
        /// </summary>
        Transform GetTransform();
    }
    
    /// <summary>
    /// Optimized pooling and instantiation system for MonoBehaviours.
    /// </summary>
    /// <typeparam name="T">The Type of the MonoBehaviour.</typeparam>
    public sealed class PrefabFactory <T> where T : MonoBehaviour, IPoolableGameObject
    {
        public PrefabFactory(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly List<T> _pool = new List<T>();

        /// <summary>
        /// Get an object from the pool, or create a new one if none are available.
        /// </summary>
        /// <param name="canCreate">If true, will create a new object if none are available.</param>
        /// <returns>An unused object.  Null if there are none available and canCreate is false.</returns>
        public T Get(bool canCreate = true)
        {
            for (int i = _pool.Count-1; i >= 0; i--)
            {
                try
                {
                    if (_pool[i].IsPooled)
                    {
                        // If the object's parent was changed at any point, set it back to the default parent.
                        if (_pool[i].GetTransform().parent != _parent)
                            _pool[i].GetTransform().SetParent(_parent);
                        
                        _pool[i].OnRemoveFromPool();
                        return _pool[i];
                    }
                }
                catch (Exception e)
                {
                    // If there was an error, then it was probably null.  So remove the object.
                    if (Debug.isDebugBuild)
                        Debug.LogError(e);

                    _pool.RemoveAt(i);
                }
            }

            // If can't create more, quit early here.
            if (!canCreate)
                return null;
            
            var instance = UnityEngine.Object.Instantiate(_prefab, _parent);
            _pool.Add(instance);
            instance.OnRemoveFromPool();

            return instance;
        }

        /// <summary>
        /// The states an object can be in.
        /// </summary>
        [System.Serializable]
        public enum ObjectState
        {
            Any = 0, Pooled = 1, InUse = 2
        }

        /// <summary>
        /// Appends the supplied list with every object in the pool, both active and not active.
        /// Note: This is expensive since it checks for nulls and removes them.
        /// </summary>
        public void Get(List<T> pool, ObjectState matching = ObjectState.Any)
        {
            for (int i = _pool.Count - 1; i >= 0; i--)
            {
                var o = _pool[i];

                if (o == null)
                {
                    _pool.RemoveAt(i);
                    continue;
                }

                switch (matching)
                {
                    case ObjectState.Any:
                        pool.Add(o);
                        break;
                    case ObjectState.Pooled:
                        if (o.IsPooled)
                            pool.Add(_pool[i]);
                        break;
                    case ObjectState.InUse:
                        if (!o.IsPooled)
                            pool.Add(_pool[i]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(matching), matching, null);
                }
            }
        }

        public void PoolAll()
        {
            for (int i = _pool.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (!_pool[i].IsPooled)
                        _pool[i].OnReturnToPool();
                }
                catch (Exception e)
                {
                    if (Debug.isDebugBuild)
                        Debug.LogError(e);

                    _pool.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Add an instantiated object to the factory pool.
        /// This fails quietly if the item is already in this factory's pool list.
        /// </summary>
        public void AddToFactory(T item)
        {
            if (item == null || _pool.Contains(item))
                return;
            
            _pool.Add(item);
        }

        /// <summary>
        /// Add items to the factory pool.
        /// This fails quietly if any item is already in this factory's pool list.
        /// </summary>
        public void AddToFactory(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (item == null || _pool.Contains(item))
                    continue;
                
                _pool.Add(item);
            }
        }

        public List<T> GetPool()
        {
            return _pool;
        }
    }
}
