using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float speed = 0.0f;
    float lifetime = 0.0f;

    public void Initialize(float initialSpeed, float initialLifetime)
    {
        speed = initialSpeed;
        lifetime = initialLifetime;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        if (lifetime < 0.0f)
        {
            ObjectPooler.EnqueueObject(this, "EnemyProjectile");
        }

        lifetime -= 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
        } else {
            ObjectPooler.EnqueueObject(this, "EnemyProjectile");
        }
    }
}