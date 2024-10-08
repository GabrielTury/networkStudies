using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int id;
    public Vector3 WorldPosition { get; private set; }
    public Vector3 MinPos {  get; private set; }
    public Vector3 MaxPos { get; private set; }
    public float gCost { get; private set; }
    public float hCost { get; private set; }

    public float nodeSize {  get; private set; }

    public bool isWall { get; private set; }

    public Node originNode { get; private set; }

    public Node[] subNodeList { get; private set; }

    public Node parent {  get; private set; }

    int mult;

    public Node(float x, float z, float y, int id, bool isWall, float nodeSize, int mult)
    {
        WorldPosition = new Vector3(x, y, z);
        this.id = id;
        this.isWall = isWall;
        this.nodeSize = nodeSize;
        MinPos = new Vector3(WorldPosition.x - nodeSize / 2, WorldPosition.y - nodeSize / 2, WorldPosition.z - nodeSize / 2);
        MaxPos = new Vector3(WorldPosition.x + nodeSize / 2, WorldPosition.y + nodeSize / 2, WorldPosition.z + nodeSize / 2);

        this.mult = mult;
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

    public void SubdivideNode()
    {
        
        
        int NodeXAmount = (int)nodeSize;
        int NodeYAmount = (int)nodeSize;
        int NodeZAmount = (int)nodeSize;

        Node[] addnodes = new Node[NodeXAmount*NodeYAmount*NodeZAmount];

        float nodeXPos = 0;
        float nodeZPos = 0;
        float nodeYPos = 0;
        int Index = 0;
        for (int i = 0; i < NodeXAmount; i++)
        {
            for (int j = 0; j < NodeZAmount; j++)
            {
                for (int k = 0; k < NodeYAmount; k++)
                {
                    //int rand = Random.Range(0, 5);
                    bool wall = false;
                    /*if(rand == 0)
                    {
                        wall = true;
                    }*/
                    Node node = new Node(MinPos.x + (nodeSize / mult)/2 + nodeXPos, MinPos.y + (nodeSize / mult) / 2 + nodeYPos, MinPos.z + (nodeSize / mult) / 2 + nodeZPos,
                        i + j, wall, nodeSize/mult, mult);
                    //Set parent
                    node.parent = this;
                    //Add to array
                    addnodes[Index] = node;
                    Index++;
                    nodeYPos += nodeSize/mult;
                }
                nodeYPos = 0;
                nodeZPos += nodeSize/mult;
            }

            nodeZPos = 0;
            nodeXPos += nodeSize/mult;
        }

        subNodeList = addnodes;
    }
}
