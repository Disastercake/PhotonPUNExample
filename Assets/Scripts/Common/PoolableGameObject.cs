using PrefabFactorySystem;
using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public class PoolableGameObject : MonoBehaviour, IPoolableGameObject
    {

        /// <summary>
        /// Ran when the object is put back into the pool.
        /// Use this to revert to default values and cleanup or release necessary objects.
        /// </summary>
        protected virtual void SetStateToPooled()
        {
            
        }

        /// <summary>
        /// Ran when the object is removed from the pool.
        /// Use it to initialize important values before the object is returned to the caller.
        /// </summary>
        protected virtual void SetStateToInUse()
        {
            
        }

        #region IPoolableGameObject

        public bool IsPooled { get; set; }
        
        public void OnReturnToPool()
        {
            IsPooled = true;
            SetStateToPooled();
            gameObject.SetActive(false);
        }

        public void OnRemoveFromPool()
        {
            IsPooled = false;
            SetStateToInUse();
            gameObject.SetActive(true);
        }

        public Transform GetTransform()
        {
            return GetComponent<RectTransform>();
        }
        
        #endregion
        
    }
}
