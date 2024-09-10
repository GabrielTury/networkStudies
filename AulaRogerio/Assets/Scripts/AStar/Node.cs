using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int id;
    public Vector3 WorldPosition { get; private set; }
    public float gCost { get; private set; }
    public float hCost { get; private set; }

    public Node originNode { get; private set; }

    public Node(float x, float z, int id)
    {
        WorldPosition = new Vector3(x, 0, z);
        this.id = id;
    }

    public void SetOriginNode(Node n)
    {
        originNode = n;
    }

    public void SetGCost(float cost)
    {
        gCost = cost;
    }

    public void SetHCost(float cost) 
    {
        hCost = cost;
    }
}
