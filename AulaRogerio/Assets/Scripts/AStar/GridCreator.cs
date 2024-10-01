using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    private Vector3 gridSize;

    private float nodeSize;

    private Node[] nodes;

    private int optimizationMult;

    public GridCreator(Vector3 gridSize, float nodeSize, int opt)
    {
        this.gridSize = gridSize;
        this.nodeSize = nodeSize;  
        this.optimizationMult = opt;
    }

    public Node[] CreateGrid()
    {
        float nSize = nodeSize * optimizationMult; 
        int nodeXAmount = (int)(gridSize.x / nSize);
        int nodeZAmount = (int)(gridSize.z / nSize);
        int nodeYAmount = (int)(gridSize.y / nSize);

        int totalTiles = nodeXAmount * nodeZAmount * nodeYAmount;
        nodes = new Node[totalTiles];

        float nodeXPos = 0;
        float nodeZPos = 0;
        float nodeYPos = 0;

        int Index = 0;
        for (int i = 0; i < nodeXAmount; i++)
        {
            for (int j = 0; j < nodeZAmount; j++)
            {
                for (int k = 0; k < nodeYAmount; k++)
                {
                    //int rand = Random.Range(0, 5);
                    bool wall = false;
                    /*if(rand == 0)
                    {
                        wall = true;
                    }*/
                    Node node = new Node(nodeXPos, nodeZPos, nodeYPos, i + j, wall, nSize, optimizationMult);
                    nodes[Index] = node;
                    Index++;
                    nodeYPos += nSize;
                }
                nodeYPos = 0;
                nodeZPos += nSize;
            }

            nodeZPos = 0;
            nodeXPos += nSize;
        }

        return nodes;
    }

}
