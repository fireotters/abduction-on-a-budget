using UnityEngine;

namespace In_Game_Items
{
    public class Button : MonoBehaviour
    {
        private Collider2D _triggerCollider;
        private AttachedSoundEffect _attachedSoundEffect;
        [SerializeField] private LockedGate attachedLock;
        private bool _usable = true;
        
        private void Start()
        {
            _triggerCollider = GetComponent<BoxCollider2D>();
            _attachedSoundEffect = GetComponent<AttachedSoundEffect>();
            if (attachedLock == null)
            {
                Debug.LogError("Target LockedGate is null! Make sure its assigned in Inspector!");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_usable && other.CompareTag("Player"))
            {
                _attachedSoundEffect.PlaySound();
                attachedLock.DestroyLock();
                _triggerCollider.enabled = false;
                _usable = false;
            }
        }
    }
}