using UnityEngine;

public class ProjectileController : MonoBehaviour
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
            ObjectPooler.EnqueueObject(this, "Projectile");
        }

        lifetime -= 1;
    }

    void OnCollisionEnter (Collider other)
    {
        ObjectPooler.EnqueueObject(this, "Projectile");
    }
}