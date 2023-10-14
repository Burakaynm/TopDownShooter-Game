using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    private Animator animator;
    private EnemyController enemyController;

    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        if(isDead)
            return;

        currentHealth -= damage;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Gethit"))
            animator.SetTrigger("GetHit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        enemyController.enabled = false;
    }

}

