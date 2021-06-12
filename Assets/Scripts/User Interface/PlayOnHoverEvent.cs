using UnityEngine;
using UnityEngine.EventSystems;

public class PlayOnHoverEvent : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        _audioSource = MusicManager.i.sfxDemo;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_audioSource.enabled)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}
