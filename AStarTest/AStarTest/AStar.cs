using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace AStarTest
{
    public class AStar
    {

        /**
         * Description: returns a path from a starting point to the goal using the A*-heuristic algorithm.
         * Input:
         * <param name="graph"> a weighted graph to search a path on</param>
         * <param name="startNode"> The starting point. </param>
         * <param name="endNode"> The goal to get path to. </param>
         * <param name="heuristic"> a function that accepts 2 nodes and returns a distance estimation between. </param>
         * <param name="maxiterations"> Maximum iterations to run A* algorithm. </param>
         * Output:
         * <returns> sequence of nodes to go through as a path </returns>
         */
        public static List<NodeType> GetPath<NodeType>(IWeightedGraph<NodeType> graph, NodeType startNode, NodeType endNode, Func<NodeType, NodeType, float> heuristic, int maxiterations = 1000)
        {
            List<NodeType> path = new List<NodeType>();
            FindPath(graph, startNode, endNode, path, heuristic, maxiterations);
            return path;
        }

        /**
         * Description: Finds a path and to endNode and stores it outputPath as an out-param.
         * Input:
         * <param name="graph"> a weighted graph to search a path on</param>
         * <param name="startNode"> The starting point. </param>
         * <param name="endNode"> The goal to get path to. </param>
         * <param name="outputPath"> OUT-PARAM that stores the collection of nodes as a path </param>
         * <param name="heuristic"> a function that accepts 2 nodes and returns a distance estimation between. </param>
         * <param name="maxiterations"> Maximum iterations to run A* algorithm. </param>
         * Output:
         *      None.
         */
        public static void FindPath<NodeType>(
            IWeightedGraph<NodeType> graph,
            NodeType startNode,
            NodeType endNode,
            List<NodeType> outputPath,
            Func<NodeType, NodeType, float> heuristic,
            int maxiterations = 1000)
        {
            HashSet<EnrichedNode<NodeType>> exploredNodes = new HashSet<EnrichedNode<NodeType>>(); // "closed list"
            SimplePriorityQueue<EnrichedNode<NodeType>> scannedNodes = new SimplePriorityQueue<EnrichedNode<NodeType>>(); // "open list"
            
            // add startNode to the scannedNodes
            var enrichedStartNode = new EnrichedNode<NodeType>(startNode, 0, heuristic(startNode, endNode));
            scannedNodes.Enqueue(
                enrichedStartNode,
                enrichedStartNode.fScore);

            // start counting iterations, as we are limited to maxiterations.
            int i = 0;

            while (scannedNodes.Count > 0 && i++ < maxiterations) // while there are still scanned nodes that we need to explore.
            {
                // get best scanned node (respective to f(n)
                var currentNode = scannedNodes.Dequeue();

                // if its the endNode then we are done.
                if (currentNode.node.Equals(endNode))
                {
                    PathBackTracking(currentNode, outputPath);
                    return;
                }

                // scan its neighbors.
                foreach (var nodeInScan in graph.Neighbors(currentNode.node))
                {
                    var enrichedNodeInScan = new EnrichedNode<NodeType>(nodeInScan.Key);
                    if (scannedNodes.Contains(enrichedNodeInScan)) // if already scanned
                    {
                        var fullyEnrichedNodeInScan = scannedNodes.Select(n => n)
                                                                  .Where(n => n == enrichedNodeInScan)
                                                                  .First();
                        if (fullyEnrichedNodeInScan.distanceFromStart >= currentNode.distanceFromStart + nodeInScan.Value) // if found a better path to it via currentNode.
                        {
                            // update path cost in distanceFromStart
                            fullyEnrichedNodeInScan.distanceFromStart = currentNode.distanceFromStart + nodeInScan.Value;
                            // update the priority
                            scannedNodes.UpdatePriority(fullyEnrichedNodeInScan, fullyEnrichedNodeInScan.fScore);
                            // update lastUpdatedFrom
                            fullyEnrichedNodeInScan.lastUpdatedFrom = currentNode;
                        }
                        else continue;
                    }    
                    else if (exploredNodes.Contains(enrichedNodeInScan)) // if already visited at and explored path through it.
                    {
                        var fullyEnrichedNodeInScan = exploredNodes.Select(n => n)
                                                                   .Where(n => n == enrichedNodeInScan)
                                                                   .First();
                        if (fullyEnrichedNodeInScan.distanceFromStart >= currentNode.distanceFromStart + nodeInScan.Value) // if found a better path to it via currentNode.
                        {
                            // update path cost in distanceFromStart
                            fullyEnrichedNodeInScan.distanceFromStart = currentNode.distanceFromStart + nodeInScan.Value;
                            // update lastUpdatedFrom
                            fullyEnrichedNodeInScan.lastUpdatedFrom = currentNode;
                            // put it back to the scannedNodes. and remove it from explored.
                            scannedNodes.Enqueue(fullyEnrichedNodeInScan, fullyEnrichedNodeInScan.fScore);
                            exploredNodes.Remove(fullyEnrichedNodeInScan);
                        }
                        else continue;
                    }
                    else // if the neghibor was never scanned/looked at before, then do the following:
                    {
                        // Add it to scanned nodes.
                        var fullyEnrichedNodeInScan = new EnrichedNode<NodeType>(
                                nodeInScan.Key,
                                currentNode.distanceFromStart + nodeInScan.Value,
                                heuristic(nodeInScan.Key, endNode),
                                currentNode
                            );
                        scannedNodes.Enqueue(fullyEnrichedNodeInScan, fullyEnrichedNodeInScan.fScore);
                    }

                }

                exploredNodes.Add(currentNode);
            }



        }

        /**
         * Description: After reaching the goal with A* algorithm, the PathBackTracking go through the parents of the nodes to
         * find the actual path.
         * Input:
         * <param name="endNode"> The goal to get path to - A* algoirhm ends here and starts backtracking </param>
         * <param name="outputPath"> OUT-PARAM that stores the collection of nodes as a path </param>
         * Output:
         *      None.
         */
        private static void PathBackTracking<NodeType>(EnrichedNode<NodeType> endNode, List<NodeType> outputPath)
        {
            // backtrack
            var currentNode = endNode;
            while (currentNode != null)
            {
                outputPath.Add(currentNode.node);
                currentNode = currentNode.lastUpdatedFrom;
            }

            // reverse backtracking result - as the path we find was from end --> start, and by reversing
            // we get the required start --> end.
            outputPath.Reverse();
        }



        // Node enriched with additional information
        private class EnrichedNode<NodeType> : IEquatable<EnrichedNode<NodeType>>
        {
            public EnrichedNode(NodeType node, float distanceFromStart = -1, float heuristiceDistance = -1, EnrichedNode<NodeType> lastUpdatedFrom = null)
            {
                this.node = node;
                this.distanceFromStart = distanceFromStart;
                this.lastUpdatedFrom = lastUpdatedFrom;
                this.heuristiceDistance = heuristiceDistance;
            }


            public NodeType node { get; } // Note: readonly, this is the ID of EnrichedNode.
            public float distanceFromStart { get; set; } // g(x)
            public EnrichedNode<NodeType> lastUpdatedFrom { get; set; } // parent
            public float heuristiceDistance { get; } // h(x), note: readonly, assumes heuristics doesnt change.
            public float fScore { get { return distanceFromStart + heuristiceDistance; } } // f(x) = g(x) + h(x)

            public bool Equals(EnrichedNode<NodeType> other)
            {
                return (!object.ReferenceEquals(other, null) && node.Equals(other.node));
            }

            public override bool Equals(object other)
            {
                if (other == null || other.GetType() != this.GetType())
                    return false;

                var otherNode = other as EnrichedNode<NodeType>;

                return this.Equals(other);
            }

            public override int GetHashCode()
            {
                return node.GetHashCode();
            }

            public static bool operator ==(EnrichedNode<NodeType> node1, EnrichedNode<NodeType> node2)
            {
                if (object.ReferenceEquals(node1, null) && object.ReferenceEquals(node2, null))
                    return true;

                return !object.ReferenceEquals(node1, null) && node1.Equals(node2);
            }
            public static bool operator !=(EnrichedNode<NodeType> node1, EnrichedNode<NodeType> node2)
            {
                return !(node1 == node2);
            }
        }




    }



}
