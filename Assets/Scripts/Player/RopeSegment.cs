using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    // Part of a Unity Rope tutorial by juul1a on YT https://www.youtube.com/channel/UCs2DJ9xpGic1pQkWNMwAUHw
    public GameObject connectedAbove, connectedBelow;
    public float anchorConnectionPoint;

    private void Start()
    {
        ResetAnchor();
    }

    public void ResetAnchor()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject;
            // Connect anchor 80% of the way up a segment, to avoid gaps in texture when rope flies around
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y * anchorConnectionPoint;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }
}
