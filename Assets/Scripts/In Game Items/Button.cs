using UnityEngine;

namespace In_Game_Items
{
    public class Button : MonoBehaviour
    {
        private Collider2D _triggerCollider;
        [SerializeField] private LockedGate attachedLock;
        private bool _usable = true;
        
        private void Start()
        {
            _triggerCollider = GetComponent<BoxCollider2D>();
            if (attachedLock == null)
            {
                Debug.LogError("Target LockedGate is null! Make sure its assigned in Inspector!");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_usable && other.CompareTag("Player"))
            {
                print("I have been triggered!");
                gameObject.GetComponent<AudioSource>().Play();
                attachedLock.DestroyLock();
                _triggerCollider.enabled = false;
                _usable = false;
            }
        }
    }
}