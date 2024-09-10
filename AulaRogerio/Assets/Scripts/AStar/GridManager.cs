using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 gridSize;
    [SerializeField]
    private float nodeSize;

    private List<Node> nodeList;

    private List<Node> openNodeList;

    private List<Node> closedNodeList;

    private List<Vector3> path;

    public static GridManager instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        GridCreator gc = new GridCreator(gridSize, nodeSize);
        nodeList = gc.CreateGrid();
    }

    public List<Vector3> CreatePath(Vector3 pos, Vector3 target)
    {
        openNodeList = new List<Node>();
        closedNodeList = new List<Node>();

        CalculateListHeuristicCost(target);
        Debug.Log("Start calculating path");
        return AStarSearch(GetNearestNode(pos), GetNearestNode(target));
    }

    private List<Vector3> AStarSearch(Node start, Node target)
    {

        openNodeList.Add(start);
        Node checkNode = start;
        checkNode.SetGCost(0);

        while(openNodeList.Count > 0)
        {
            openNodeList.Sort((n1, n2) => n1.hCost.CompareTo(n2.hCost));
            checkNode = openNodeList[0];

            if(checkNode == target)
            {
                List<Node> finalNodes = ReconstructPath(target);
                List<Vector3> path = new List<Vector3>();
                foreach(Node node in finalNodes)
                {
                    path.Add(node.WorldPosition);
                }
                Debug.Log("Created Path");
                return path;
            }
            openNodeList.Remove(checkNode);
            closedNodeList.Add(checkNode);

            List<Node> neighbors = GetNeighborNodes(checkNode);

            foreach (Node n in neighbors)
            {
                if (!closedNodeList.Contains(n))
                {
                    // Calculate tentative gCost (cost from start to this tile)
                    float tentativeGCost = checkNode.gCost - checkNode.hCost + 1;

                    if (!openNodeList.Contains(n) || tentativeGCost < n.gCost - n.hCost)
                    {                        
                        n.SetGCost(tentativeGCost + n.hCost);
                        n.SetOriginNode(checkNode);

                        if (!openNodeList.Contains(n))
                        {
                            openNodeList.Add(n);
                        }
                    }
                }
            }
        }

        return null;
    }

    private void CalculateListHeuristicCost(Vector3 target)
    {
        foreach (Node node in nodeList)
        {
            float totalDistance = Mathf.Abs(target.x - node.WorldPosition.x) + Mathf.Abs(target.y - node.WorldPosition.y); 
            node.SetHCost(totalDistance);
        }
    }

    private List<Node> GetNeighborNodes(Node Node)
    {
        List<Node> neighbors = new List<Node>();

        foreach (Node t in nodeList)
        {
            if (Mathf.Abs(t.WorldPosition.x - Node.WorldPosition.x) <= nodeSize+ 0.01f &&
                Mathf.Abs(t.WorldPosition.y - Node.WorldPosition.y) <= nodeSize + 0.01f)
            {
                if (t != Node) // Exclude the Node itself
                {
                    neighbors.Add(t);
                }
            }
        }

        return neighbors;
    }

    private Node GetNearestNode(Vector3 currentPosition)
    {
        Node nearest = null;
        float distance = 100;
        foreach (Node node in nodeList)
        {
            float currentDistance = Vector3.Distance(currentPosition, node.WorldPosition);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                nearest = node;
            }
        }

        return nearest;
    }
    private List<Node> ReconstructPath(Node current)
    {
        List<Node> path = new List<Node>();
        while (current != null)
        {
            path.Add(current);
            current = current.originNode;
        }
        path.Reverse(); // Reverse the path to get it from start to end

        //Debug.Log("Path successfully reconstructed");

        return path;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            foreach (Node node in nodeList)
            {
                Gizmos.DrawWireCube(node.WorldPosition, Vector3.one * nodeSize);
            }

        }
    }
#endif
}
