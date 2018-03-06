using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public abstract class Damageable : MonoBehaviour {
        
        public float MaxHealth = 20f;
        public float StartingHealth = 20f;

        [Tooltip("Object takes critical damage")]
        public bool IsCritical = false;

        float health;

        protected virtual void Awake() {
            health = StartingHealth;
        }

        protected virtual void Update() {
            if (HasDied()) {
                try {
                    Die();
                } catch (NullReferenceException) {
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public float GetHealth() {
            return health;
        }

        public float GetHealthPercentage() {
            // Avoid dividing by zero o.o
            if (MaxHealth == 0) {
                return 0;
            }
            return health / MaxHealth;
        }

        public float Heal(float amount) {
            if (HasDied()) {
                return health;
            }
            health += amount;
            if (health > MaxHealth) {
                health = MaxHealth;
            }
            return health;
        }

        public float Damage(float amount) {
            health -= amount;

            if (health < 0) {
                health = 0;
            }

            return health;
        }

        public bool HasDied() {
            return health <= 0;
        }

        protected abstract void Die();
    }
}