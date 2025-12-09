using System.IO;
using System;
using System.Linq;

namespace AoC2025.day8;

public class Node(int[] coord)
{
    public int[] Coordinates { get; set; } = coord;
    public List<Node> ConnectedNodes { get; set; } = new List<Node>();
    public bool Checked { get; set; } = false;
}

public struct NodePair
{
    public double Distance;
    public Node Node1;
    public Node Node2;
    public NodePair(double distance, Node node1, Node node2)
    {
        Distance = distance;
        Node1 = node1;
        Node2 = node2;
    }
}

public class Day8
{
    public static void Run()
    {
        part1();
        part2();
    }

    public static void part1()
    {
        string inputPath = Path.Combine("day8", "input");

        var nodePairs = getSortedNodePairs(inputPath);

        int amountOfConnections = 1000;
        HashSet<Node> nodesToCheck = new HashSet<Node>();

        //Find the X pairs of shortest distance
        for (int i = 0; i < amountOfConnections; i++)
        {
            var pair = nodePairs[i];
            pair.Node1.ConnectedNodes.Add(pair.Node2);
            pair.Node2.ConnectedNodes.Add(pair.Node1);
            nodesToCheck.Add(pair.Node1);
            nodesToCheck.Add(pair.Node2);
        }

        List<int> connectionCount = new List<int>();
        foreach (Node node in nodesToCheck)
        {
            if (!node.Checked)
            {
                connectionCount.Add(findConnections(node));
            }
        }

        connectionCount = connectionCount.OrderByDescending(x => x).ToList();

        long result = 1;
        for (int i = 0; i < 3; i++)
        {
            result *= connectionCount[i];
        }

        Console.WriteLine("Part 1 result is " + result);
    }

    public static List<NodePair> getSortedNodePairs(string inputPath)
    {
        var nodes = File.ReadLines(inputPath).Select(line => {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            return new Node(coords);
        }).ToList();

        // Get all of the node pairs and their distances
        var nodePairs = nodes
            .SelectMany((currentNode, i) => 
                nodes.Skip(i + 1)
                    .Select(nextNode => new NodePair(
                        euclideanDistance(currentNode.Coordinates, nextNode.Coordinates),
                        currentNode,
                        nextNode
                    ))
            )
            .ToList();

        //Sort them
        return nodePairs.OrderBy(pair => pair.Distance).ToList();
    }

    public static void part2()
    {
        string inputPath = Path.Combine("day8", "input");

        var nodePairs = getSortedNodePairs(inputPath);

        HashSet<Node> nodesToCheck = new HashSet<Node>();
        int amountOfConnections = 0;
        bool connectionNotFound = true;
        while (connectionNotFound)
        {
            amountOfConnections++;

            long lastJunction1 = 0;
            long lastJunction2 = 0;
            //Find the X pairs of shortest distance
            var pair = nodePairs[amountOfConnections];
            pair.Node1.ConnectedNodes.Add(pair.Node2);
            pair.Node2.ConnectedNodes.Add(pair.Node1);
            nodesToCheck.Add(pair.Node1);
            nodesToCheck.Add(pair.Node2);

            lastJunction1 = pair.Node1.Coordinates[0];
            lastJunction2 = pair.Node2.Coordinates[0];

            List<int> connectionCount = new List<int>();
            foreach (Node node in nodesToCheck)
            {
                if (!node.Checked)
                {
                    connectionCount.Add(findConnections(node));
                }
            }

            if (connectionCount[0] == 1000)
            {
                long result = lastJunction1 * lastJunction2;
                Console.WriteLine($"Part 2 result is {result}");
                connectionNotFound = false;
            }
            else
            {
                foreach (Node node in nodesToCheck)
                {
                    node.Checked = false;
                }
            }
        }
    }

    public static int findConnections(Node currentNode)
    {
        int result = 1;
        currentNode.Checked = true;
        foreach (Node node in currentNode.ConnectedNodes)
        {
            if (!node.Checked)
            {
                result += findConnections(node);
            }
        }
        return result;
    }

    public static double euclideanDistance(int[] p1, int[] p2)
    {
        return Math.Sqrt(Math.Pow(p2[0] - p1[0], 2) + Math.Pow(p2[1] - p1[1], 2) + Math.Pow(p2[2] - p1[2], 2));
    }
}