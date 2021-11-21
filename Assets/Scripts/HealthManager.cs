using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health;
    public Text EnemyHealth;

    public void AddDamage(int damage)
    {
        health -= damage;
        EnemyHealth.text = $"Enemy Health: {health}";
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}