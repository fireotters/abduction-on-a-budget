using UnityEngine;

namespace In_Game_Items
{
    public class LockedGate : MonoBehaviour
    {
        private SpriteRenderer[] _spriteRenderers;
        private Collider2D _collider2D;
        public bool unlockable = true;

        private void Awake()
        {
            _spriteRenderers = GetComponents<SpriteRenderer>();
            if (_spriteRenderers.Length == 0)
            {
                _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            }
            _collider2D = GetComponent<Collider2D>();
        }

        public void DestroyLock()
        {
            _collider2D.enabled = false;
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
            unlockable = false;
            Invoke(nameof(CompleteDestructionAfterSound), 0.2f);
        }

        private void CompleteDestructionAfterSound()
        {
            Destroy(gameObject);
        }
    }
}
