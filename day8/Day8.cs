using System.IO;
using System;
using System.Linq;

namespace AoC2025.day8;

public class Node(int[] coord){
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

    // public static void part1()
    // {
    //     string inputPath = Path.Combine("day8", "testInput");

    //     List<Node> nodes = new List<Node>();
    //     foreach (string line in File.ReadLines(inputPath))
    //     {
    //         var coords = line.Split(',').Select(int.Parse).ToArray();
    //         nodes.Add(new Node(coords));
    //     }

    //     for(int i = 0; i < nodes.Count; i++){
    //         Node currentNode = nodes[i];
    //         Node currentClosestNode;
    //         double distance = Double.MaxValue;
    //         foreach(Node alreadyConnectedNode in currentNode.ConnectedNodes){
    //             var currDist = euclideanDistance(currentNode.Coordinates, alreadyConnectedNode.Coordinates);
    //             if(currDist < distance){
    //                 distance = currDist;
    //                 currentClosestNode = alreadyConnectedNode;
    //             }
    //         }
    //         for(int j = i + 1; j < nodes.Count; j++){
    //             var nextNode = nodes[j];
    //             var nodeDistances = euclideanDistance(currentNode.Coordinates, nextNode.Coordinates);
    //             if(nodeDistances < distance){
    //                 distance = nodeDistances;
    //                 currentClosestNode = nextNode;
    //             }
    //         }

    //         currentNode.ConnectedNodes.Add(currentClosestNode);
    //     }
    // }

    public static void part1()
    {
        string inputPath = Path.Combine("day8", "input");

        List<Node> nodes = new List<Node>();
        foreach (string line in File.ReadLines(inputPath))
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            nodes.Add(new Node(coords));
        }

        // Get all of the node pairs and their distances
        List<NodePair> nodePairs = new List<NodePair>();
        for(int i = 0; i < nodes.Count; i++){
            Node currentNode = nodes[i];
            for(int j = i + 1; j < nodes.Count; j++){
                var nextNode = nodes[j];
                var distance = euclideanDistance(currentNode.Coordinates, nextNode.Coordinates);
                nodePairs.Add(new NodePair(distance, currentNode, nextNode));
            }
        }

        //Sort them
        nodePairs = nodePairs.OrderBy(pair => pair.Distance).ToList();

        // Pretty print the node pairs
        // Console.WriteLine("\n=== Node Pairs (sorted by distance) ===");
        // foreach (var pair in nodePairs)
        // {
        //     var coords1 = string.Join(", ", pair.Node1.Coordinates);
        //     var coords2 = string.Join(", ", pair.Node2.Coordinates);
        //     Console.WriteLine($"Node1: [{coords1}] <-> Node2: [{coords2}] | Distance: {pair.Distance:F2}");
        // }
        // Console.WriteLine();

        int amountOfConnections = 1000;
        HashSet<Node> nodesToCheck = new HashSet<Node>();

        //Find the X pairs of shortest distance
        for(int i = 0; i < amountOfConnections; i++){
            var pair = nodePairs[i];
            //if(nodesToCheck.Contains(pair.Node2) && nodesToCheck.Contains(pair.Node1)) continue;
            pair.Node1.ConnectedNodes.Add(pair.Node2);
            pair.Node2.ConnectedNodes.Add(pair.Node1);
            nodesToCheck.Add(pair.Node1);
            nodesToCheck.Add(pair.Node2);
        }

        List<int> connectionCount = new List<int>();
        foreach(Node node in nodesToCheck){
            if(!node.Checked){
                connectionCount.Add(findConnections(node));
            }
        }

        connectionCount = connectionCount.OrderByDescending(x => x).ToList();
        
        // Pretty print the connection counts
        Console.WriteLine("=== Connection Counts (sorted descending) ===");
        for(int i = 0; i < connectionCount.Count; i++){
            Console.WriteLine($"Group {i + 1}: {connectionCount[i]} nodes");
        }
        Console.WriteLine();

        long result = 1;
        for(int i = 0; i < 3; i++){
            result *= connectionCount[i];
        }

        Console.WriteLine("Part 1 result is " + result);
    }

    public static void part2()
    {
       string inputPath = Path.Combine("day8", "input");

        List<Node> nodes = new List<Node>();
        foreach (string line in File.ReadLines(inputPath))
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            nodes.Add(new Node(coords));
        }

        // Get all of the node pairs and their distances
        List<NodePair> nodePairs = new List<NodePair>();
        for(int i = 0; i < nodes.Count; i++){
            Node currentNode = nodes[i];
            for(int j = i + 1; j < nodes.Count; j++){
                var nextNode = nodes[j];
                var distance = euclideanDistance(currentNode.Coordinates, nextNode.Coordinates);
                nodePairs.Add(new NodePair(distance, currentNode, nextNode));
            }
        }

        //Sort them
        nodePairs = nodePairs.OrderBy(pair => pair.Distance).ToList();

        // Pretty print the node pairs
        // Console.WriteLine("\n=== Node Pairs (sorted by distance) ===");
        // foreach (var pair in nodePairs)
        // {
        //     var coords1 = string.Join(", ", pair.Node1.Coordinates);
        //     var coords2 = string.Join(", ", pair.Node2.Coordinates);
        //     Console.WriteLine($"Node1: [{coords1}] <-> Node2: [{coords2}] | Distance: {pair.Distance:F2}");
        // }
        // Console.WriteLine();

        HashSet<Node> nodesToCheck = new HashSet<Node>();
        int amountOfConnections = 0;
        bool connectionNotFound = true;
        while(connectionNotFound){
            amountOfConnections++;

            long lastJunction1 = 0;
            long lastJunction2 = 0;
            //Find the X pairs of shortest distance
            var pair = nodePairs[amountOfConnections];
            //if(nodesToCheck.Contains(pair.Node2) && nodesToCheck.Contains(pair.Node1)) continue;
            pair.Node1.ConnectedNodes.Add(pair.Node2);
            pair.Node2.ConnectedNodes.Add(pair.Node1);
            nodesToCheck.Add(pair.Node1);
            nodesToCheck.Add(pair.Node2);

            lastJunction1 = pair.Node1.Coordinates[0];
            lastJunction2 = pair.Node2.Coordinates[0];

            List<int> connectionCount = new List<int>();
            foreach(Node node in nodesToCheck){
                if(!node.Checked){
                    connectionCount.Add(findConnections(node));
                }
            }

            if(connectionCount[0] == 1000){
                long result = lastJunction1 * lastJunction2;
                Console.WriteLine($"Part 2 result is {result}");
                connectionNotFound = false;
            } else {
                foreach(Node node in nodesToCheck){
                    node.Checked = false;
                }
            }
        }
    }

    public static int findConnections(Node currentNode){
        int result = 1;
        currentNode.Checked = true;
        foreach(Node node in currentNode.ConnectedNodes){
            if(!node.Checked){
                result += findConnections(node);
            }
        }
        return result;
    }

    public static double euclideanDistance(int[] p1, int[] p2){
        return Math.Sqrt(Math.Pow(p2[0] - p1[0], 2) + Math.Pow(p2[1] - p1[1], 2) + Math.Pow(p2[2] - p1[2], 2));
    }
}