
using UnityEngine;
using ZOMBOM.UI;

namespace ZOMBOM.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float startingHealth = 100f;
        [SerializeField] HealthBarDisplay healthBar = null;
        float currentHealth;
        private void Start()
        {
            currentHealth = startingHealth;
        }

        public void GetDamage(float damage)
        {
            currentHealth = Mathf.Max(0f, currentHealth - damage);
            if (healthBar) healthBar.UpdateSlider(GetPercentHealth());
            Die();
            
        }
        public void GetHealed(float health)
        {
            currentHealth = Mathf.Min(startingHealth , currentHealth + health);
        }
        public void Die()
        {
            if (currentHealth == 0.0f)
            {
                Destroy(gameObject);
            }
            
        }
        public float GetPercentHealth()
        {
            return currentHealth / startingHealth;
        }
    }

}