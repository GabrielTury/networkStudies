using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int id;
    public Vector3 WorldPosition { get; private set; }
    public float gCost { get; private set; }
    public float hCost { get; private set; }

    public bool isWall { get; private set; }

    public Node originNode { get; private set; }

    public Node(float x, float z, float y, int id, bool isWall)
    {
        WorldPosition = new Vector3(x, y, z);
        this.id = id;
        this.isWall = isWall;
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
