using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _starpos;
    private Camera _cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        _starpos = transform.position.x;
        _length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    private void Update()
    {
        var currentPos = transform.position;
        var cameraPos = _cam.transform.position;
        var temp = (cameraPos.x * (1 - parallaxEffect));
        var dist = (cameraPos.x * parallaxEffect);
        
        transform.position = new Vector3(_starpos + dist, currentPos.y, currentPos.z);
        
        if (temp > _starpos + _length)
            _starpos += _length;
        else if (temp < _starpos - _length)
            _starpos -= _length;
    }
}
