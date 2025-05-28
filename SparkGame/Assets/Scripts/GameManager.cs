using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public ProjectileController projectilePrefab;
        public EnemyTank enemyPrefab;
        public EnemyProjectile enemyProjectilePrefab;
        public Transform spawn;
        public int NumOfEnemies = 10;

        #endregion

        #region BuildInMethods
        public void Awake()
        {
            SetupPool();
            InvokeRepeating("SpawnEnemy", 10f, 5f);
        }

        #endregion

        #region CustomMethods
        private void SetupPool()
        {
            ObjectPooler.SetupPool(projectilePrefab, 10, "Projectile");
            ObjectPooler.SetupPool(enemyPrefab, NumOfEnemies, "Enemy");
            for (int i = 0; i < NumOfEnemies; i++) {
                ObjectPooler.SetupPool(enemyProjectilePrefab, 5, "EnemyProjectile");
            }
        }

        private void SpawnEnemy()
        {
            EnemyTank instance = ObjectPooler.DequeueObject<EnemyTank>("Enemy");
            instance.transform.position = spawn.position;
            instance.gameObject.SetActive(true);
        }

        #endregion
    }
}

