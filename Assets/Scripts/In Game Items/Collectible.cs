using UnityEngine;

namespace In_Game_Items
{
    public abstract class Collectible : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        public Collider2D _triggerCollider;
        private AttachedSoundEffect _sfx;

        internal virtual void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _triggerCollider = GetComponent<Collider2D>();
            _sfx = GetComponent<AttachedSoundEffect>();
        }

        public virtual void DestroyCollectible()
        {
            if (_sprite != null)
                _sprite.enabled = false;
            _triggerCollider.enabled = false;
            _sfx.PlaySound();
            Invoke(nameof(CompleteDestructionAfterSound), 5f);
        }

        private void CompleteDestructionAfterSound()
        {
            Destroy(gameObject);
        }
    }
}

