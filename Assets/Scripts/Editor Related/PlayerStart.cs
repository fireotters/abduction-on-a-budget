using Cinemachine;
using Player;
using UnityEngine;

namespace Editor_Related
{
    public class PlayerStart : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private GameObject player = null;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
        }

        private void Start()
        {
            _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _spriteRenderer.enabled = false;
            
            // Spawns a player.
            if (player != null)
            {
                var spawnedPlayer = Instantiate(player, gameObject.transform.position, Quaternion.identity);
                _cinemachineVirtualCamera.m_Follow =
                    spawnedPlayer.GetComponentInChildren<UfoController>().transform;
            }
            else // This error shouldn't appear, but it's better to be safe than sorry.
            {
                Debug.LogError("You must set the Player prefab in the Inspector!");
            }

            Destroy(gameObject);
        }
    }

}

