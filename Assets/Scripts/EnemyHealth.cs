using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject dropItemPrefabs;
    [SerializeField] private Vector3 dropOffset = new Vector3(0, 0.5f, 0);
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log(name + "남은 체력 " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (dropItemPrefabs != null)
        {
            Instantiate(dropItemPrefabs, transform.position + dropOffset, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
