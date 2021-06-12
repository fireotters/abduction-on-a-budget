using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor_Related
{
    public class PlayerEnd : MonoBehaviour
    {
        
        private SpriteRenderer _sprite;
        private Collider2D _collider2D;
        public string levelToLoad;

        private void Awake()
        {
            if (levelToLoad == string.Empty)
            {
                Debug.LogError("You must input a scene name to load in the Inspector!");
            }
        }

        void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            _sprite.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            // StartCoroutine(UsefulFunctions.FadeScreenBlack("to", 3f));
            Invoke(nameof(ChangeLevel), 5f);
        }

        private void ChangeLevel()
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}


