using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace In_Game_Items
{
    public class LockedGate : MonoBehaviour
    {

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;
        public bool unlockable = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        public void DestroyLock()
        {
            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            unlockable = false;
            Invoke(nameof(Die), 0.2f);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
