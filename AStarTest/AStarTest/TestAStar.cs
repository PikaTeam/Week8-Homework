using System;
using System.Collections.Generic;
using System.Numerics;
using Debug = System.Diagnostics.Debug;
using System.Linq;

namespace AStarTest
{
    class TestAStar
    {
        public TestAStar()
        { }

        public void Run()
        {
            TestSimpleLinePath1();
            TestSimpleLinePath2();
            TestSimple2DPath1();
            TestSimple2DPath1WithL2();
        }

        public void TestSimpleLinePath1()
        {
            // create graph.
            var lineGraph = new WeightedLineGraph();
            // decide startNode, endNode
            var startNode = new Vector3(0, 0, 0);
            var endNode = new Vector3(-2, 0, 0);
            // manualy compute path.
            var expectedPath = new List<Vector3> {
                new Vector3(0,0,0),
                new Vector3(-1,0,0),
                new Vector3(-2,0,0)
            };
            // run A* to get path.
            var actualPath = AStar.GetPath(lineGraph, startNode, endNode, L1);
            // Assert Manual path = A* path.
            Debug.Assert(actualPath.SequenceEqual(expectedPath));
        }

        public void TestSimpleLinePath2()
        {
            // create graph.
            var lineGraph = new WeightedLineGraph();
            // decide startNode, endNode
            var startNode = new Vector3(0, 0, 0);
            var endNode = new Vector3(5, 0, 0);
            // manualy compute path.
            var expectedPath = new List<Vector3> {
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(2,0,0),
                new Vector3(3,0,0),
                new Vector3(4,0,0),
                new Vector3(5,0,0)
            };
            // run A* to get path.
            var actualPath = AStar.GetPath(lineGraph, startNode, endNode, L1);
            // Assert Manual path = A* path.
            Debug.Assert(actualPath.SequenceEqual(expectedPath));
        }

        private void TestSimple2DPath1()
        {
            // create graph.
            var lineGraph = new Weighted2DGraph();
            // decide startNode, endNode
            var startNode = new Vector3(1, 1, 0);
            var endNode = new Vector3(3, 3, 0);
            // manualy compute path.
            var optionalExpectedPath1 = new List<Vector3> {
                new Vector3(1,1,0),
                new Vector3(1,2,0),
                new Vector3(1,3,0),
                new Vector3(2,3,0),
                new Vector3(3,3,0),
            };
            var optionalExpectedPath2 = new List<Vector3> {
                new Vector3(1,1,0),
                new Vector3(2,1,0),
                new Vector3(3,1,0),
                new Vector3(3,2,0),
                new Vector3(3,3,0),
            };
            // run A* to get path.
            var actualPath = AStar.GetPath(lineGraph, startNode, endNode, L1);
            // Assert Manual path = A* path.
            Debug.Assert(
                actualPath.SequenceEqual(optionalExpectedPath1) 
                || actualPath.SequenceEqual(optionalExpectedPath2));
        }

        private void TestSimple2DPath1WithL2()
        {
            // create graph.
            var lineGraph = new Weighted2DGraph();
            // decide startNode, endNode
            var startNode = new Vector3(1, 1, 0);
            var endNode = new Vector3(3, 3, 0);
            // manualy compute path.
            var optionalExpectedPath1 = new List<Vector3> {
                new Vector3(1,1,0),
                new Vector3(1,2,0),
                new Vector3(1,3,0),
                new Vector3(2,3,0),
                new Vector3(3,3,0),
            };
            var optionalExpectedPath2 = new List<Vector3> {
                new Vector3(1,1,0),
                new Vector3(2,1,0),
                new Vector3(3,1,0),
                new Vector3(3,2,0),
                new Vector3(3,3,0),
            };
            // run A* to get path.
            var actualPath = AStar.GetPath(lineGraph, startNode, endNode, L2);
            // Assert Manual path = A* path.
            Debug.Assert(
                actualPath.SequenceEqual(optionalExpectedPath1)
                || actualPath.SequenceEqual(optionalExpectedPath2));
        }

        private static float L1(Vector3 v1, Vector3 v2)
        {
            return Math.Abs(v1.X - v2.X)
                   + Math.Abs(v1.Y - v2.Y)
                   + Math.Abs(v1.Z - v2.Z);
        }

        private static float L2(Vector3 v1, Vector3 v2)
        {
            return (float) Math.Sqrt(
                Math.Pow(v1.X - v2.X, 2)
                + Math.Pow(v1.Y - v2.Y, 2)
                + Math.Pow(v1.Z - v2.Z, 2));
        }
    }
}
