using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    private Vector3 gridSize;

    private float nodeSize;

    private List<Node> nodes;

    public GridCreator(Vector3 gridSize, float nodeSize)
    {
        this.gridSize = gridSize;
        this.nodeSize = nodeSize;  
    }

    public List<Node> CreateGrid()
    {
        nodes = new List<Node>();
        int nodeXAmount = (int)(gridSize.x/nodeSize);
        int nodeZAmount = (int)(gridSize.z / nodeSize);
        int nodeYAmount = (int)(gridSize.y / nodeSize);

        float nodeXPos = 0;
        float nodeZPos = 0;
        float nodeYPos = 0;
        for (int i = 0; i < nodeXAmount; i++)
        {
            for (int j = 0; j < nodeZAmount; j++)
            {
                for (int k = 0; k < nodeYAmount; k++)
                {
                    Node node = new Node(nodeXPos, nodeZPos, nodeYPos, i + j);
                    nodes.Add(node);
                    nodeYPos += nodeSize;
                }
                nodeYPos = 0;
                nodeZPos += nodeSize;
            }

            nodeZPos = 0;
            nodeXPos += nodeSize;
        }

        return nodes;
    }
}
