using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public ProjectileController projectilePrefab;

        #endregion

        #region BuildInMethods
        public void Awake()
        {
            SetupPool();
        }

        #endregion

        #region CustomMethods
        private void SetupPool()
        {
            ObjectPooler.SetupPool(projectilePrefab, 10, "Projectile");
        }
        #endregion
    }
}

