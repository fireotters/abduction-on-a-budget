using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor_Related
{
    public class PlayerEnd : MonoBehaviour
    {
        
        private SpriteRenderer _sprite;
        private Collider2D _collider2D;

        void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            _sprite.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.i.LevelFinished();
        }
    }
}


