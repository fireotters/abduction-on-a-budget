using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCrank : MonoBehaviour
{
    // Part of a Unity Rope tutorial by juul1a on YT https://www.youtube.com/channel/UCs2DJ9xpGic1pQkWNMwAUHw
    [Header("Changable Attributes")]
    public int minLinks = 2, maxLinks = 15;

    [Header("References")]
    private Rope rope;

    [Header("Private Vars")]
    private int numLinks;

    private void Awake()
    {
        rope = transform.parent.GetComponent<Rope>();
        numLinks = rope.initialNumOfLinks;
    }

    public void Rotate(int direction)
    {
        if (direction > 0 && rope != null && numLinks <= maxLinks)
        {
            rope.AddLink();
            numLinks++;
        }
        else if (direction < 0 && rope != null && numLinks > minLinks)
        {
            rope.RemoveLink();
            numLinks--;
        }
    }
}
