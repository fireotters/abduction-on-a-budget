using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCrank : MonoBehaviour
{
    public float rotateSpeed = 10f;
    private Rope rope;
    private int numLinks;
    public int minLinks = 3, maxLinks = 15;

    private void Awake()
    {
        rope = transform.parent.GetComponent<Rope>();
        numLinks = rope.numLinks;
    }

    public void Rotate(int direction)
    {
        if (direction > 0 && rope != null && numLinks <= maxLinks)
        {
            transform.Rotate(0, 0, direction * rotateSpeed);
            rope.AddLink();
            numLinks++;
        }
        else if (direction < 0 && rope != null && numLinks > minLinks)
        {
            transform.Rotate(0, 0, direction * rotateSpeed);
            rope.RemoveLink();
            numLinks--;
        }
    }
}
