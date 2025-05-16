using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float speed = 0.0f;
    float lifetime = 0.0f;

    public void Initialize(float initialSpeed, float initialifetime)
    {
        speed = initialSpeed;
        lifetime = initialifetime;
    }

    void Update()
    {
        float speed_factor = Mathf.InverseLerp(0, speed, Mathf.Abs(lifetime));
        transform.position += transform.up * speed * speed_factor * Time.deltaTime;

        if (lifetime < 0.0f)
        {
            ObjectPooler.EnqueueObject(this, "Projectile");
        }

        lifetime -= 1;
    }

    void OnCollision(Collider other)
    {
        ObjectPooler.EnqueueObject(this, "Projectile");
    }
}