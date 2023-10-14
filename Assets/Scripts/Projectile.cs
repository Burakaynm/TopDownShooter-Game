using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        Health enemy = collision.collider.GetComponent<Health>();
        if(enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }

        Destroy(gameObject); 
    }

    private void OnEnable()
    {
        Destroy(gameObject, 7.5f);
    }
}

