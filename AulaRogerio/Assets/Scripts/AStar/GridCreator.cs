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
        int nodeYAmount = (int)(gridSize.z / nodeSize);

        float nodeXPos = 0;
        float nodeZPos = 0;
        for (int i = 0; i < nodeXAmount; i++)
        {
            for (int j = 0; j < nodeYAmount; j++)
            {
                Node node = new Node(nodeXPos, nodeZPos, i+j);
                nodes.Add(node);
                nodeZPos += nodeSize;
            }

            nodeZPos = 0;
            nodeXPos += nodeSize;
        }

        return nodes;
    }
}
