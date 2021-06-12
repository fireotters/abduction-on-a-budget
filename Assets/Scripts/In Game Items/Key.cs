using UnityEngine;

namespace In_Game_Items
{
    public class Key : MonoBehaviour
    {
        private SpriteRenderer _keySprite;
        private Collider2D _triggerCollider;
        
        private void Awake()
        {
            _keySprite = GetComponent<SpriteRenderer>();
            _triggerCollider = GetComponent<Collider2D>();
        }

        public void DestroyKey()
        {
            _keySprite.enabled = false;
            _triggerCollider.enabled = false;
            // TODO: insert sfx maybe?
            Invoke(nameof(Die), 1f);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

