using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Part of a Unity Rope tutorial by juul1a on YT https://www.youtube.com/channel/UCs2DJ9xpGic1pQkWNMwAUHw
    [Header("Changable Attributes")]
    public int initialNumOfLinks = 5;
    [SerializeField] private float InitialDistFromUfo = 1.9f, AddedUfoDistPerSegment = 0.21f;

    [Header("References")]
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public Player.UfoController plr1;
    public Plr2Controller plr2;
    public HingeJoint2D top;

    private void Start()
    {
        GenerateRope();
    }

    private void Update()
    {
        transform.position = plr1.transform.position;
    }

    private void GenerateRope()
    {
        plr1.GetComponent<DistanceJoint2D>().distance = InitialDistFromUfo;

        Rigidbody2D prevBod = hook;
        for (int i = 0; i < initialNumOfLinks; i++)
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
            if (i == initialNumOfLinks - 1)
            {
                plr2.GetComponent<HingeJoint2D>().connectedBody = newSeg.GetComponent<Rigidbody2D>();
            }
        }
    }

    public void AddLink()
    {
        plr1.GetComponent<DistanceJoint2D>().distance += AddedUfoDistPerSegment;

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
        plr1.GetComponent<DistanceJoint2D>().distance -= AddedUfoDistPerSegment;

        HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        newTop.connectedBody = hook;
        newTop.gameObject.transform.position = hook.gameObject.transform.position;
        newTop.GetComponent<RopeSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = newTop;
    }
}
