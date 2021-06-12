using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 5;
    public Plr2Controller plr2;

    public HingeJoint2D top;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();

            // First rope segment is the top segment, to be deleted/create rope when player climbs/lowers on rope.
            if (i == 0)
            {
                top = hj;
            }
            // Last rope segment is always attached to Player 2
            if (i == numLinks - 1)
            {
                plr2.GetComponent<HingeJoint2D>().connectedBody = newSeg.GetComponent<Rigidbody2D>();
            }
        }
    }

    public void AddLink()
    {
        int index = Random.Range(0, prefabRopeSegs.Length);
        GameObject newLink = Instantiate(prefabRopeSegs[index]);
        newLink.transform.parent = transform;
        newLink.transform.position = transform.position;
        HingeJoint2D hj = newLink.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;
        newLink.GetComponent<RopeSegment>().connectedBelow = top.gameObject;
        top.connectedBody = newLink.GetComponent<Rigidbody2D>();
        top.GetComponent<RopeSegment>().ResetAnchor();
        top = hj;
    }

    public void RemoveLink()
    {
        HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        newTop.connectedBody = hook;
        newTop.gameObject.transform.position = hook.gameObject.transform.position;
        newTop.GetComponent<RopeSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = newTop;
    }
}
