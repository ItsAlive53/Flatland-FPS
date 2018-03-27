using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public class Enemy : Damageable {
        private LevelManager attachedLevelMgr;
        
        protected override void Awake() {
            base.Awake();
        }

        public void SetLevelManager(LevelManager levelManager) {
            attachedLevelMgr = levelManager;
        }

        protected override void Die() {
            if (attachedLevelMgr) {
                attachedLevelMgr.IncrementScore();
            }

            Destroy(gameObject);
        }
    }
}
