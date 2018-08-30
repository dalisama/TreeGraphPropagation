using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TreeGraphPropagation.Core;

namespace TreeGraphPropagation.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FastestTreeGraphPropagationTest
    {
        private class Node : IEqualityComparer<Node>
        {
            public int NodeId { get; set; }
            // override object.Equals
            public override bool Equals(object obj)
            {

                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }


                return ((Node)obj).NodeId == this.NodeId;
            }



            public bool Equals(Node x, Node y)
            {
                return x.NodeId == y.NodeId;
            }

            // override object.GetHashCode


            public int GetHashCode(Node obj)
            {
                return obj.GetHashCode();
            }

            public override int GetHashCode()
            {
                return -1065341352 + NodeId.GetHashCode();
            }
        }

        [TestMethod]
        public void Should_ReturnEmptyRoot_When_TreeIsEmpty()
        {
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(null);
            Assert.IsNull(treeGraphPropagation.GetFastOptimisedRoot());
        }
        [TestMethod]
        public void Should_ReturnTWoRoot_When_TreeHasTwoElements()
        {
            var edgeList = new List<Tuple<Node, Node>>();
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 1 }, new Node { NodeId = 2 }));
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edgeList);
            
            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 1);
            Assert.AreEqual(result[1].RootNode.NodeId, 2);
            Assert.AreEqual(result[1].Propagation, 1);
        }
        [TestMethod]
        public void Should_ReturnTWORoot_When_TreeHasElements()
        {
            var edgeList = new List<Tuple<Node, Node>>();
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 1 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 5 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 6 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 8 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 7 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 9 }, new Node { NodeId = 7 }));
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edgeList);
            
            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 3);
            Assert.AreEqual(result[0].Propagation, 3);
            Assert.AreEqual(result[1].RootNode.NodeId, 2);
            Assert.AreEqual(result[1].Propagation, 3);

        }


        [TestMethod]
        public void Should_ReturnOneRoot_When_TreeHasElements()
        {
            var edgeList = new List<Tuple<Node, Node>>();
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 1 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 2 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 5 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 4 }, new Node { NodeId = 6 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 8 }));
            edgeList.Add(new Tuple<Node, Node>(new Node { NodeId = 3 }, new Node { NodeId = 7 }));

            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edgeList);
            
            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId,2);
            Assert.AreEqual(result[0].Propagation, 2);
       

        }


        [TestMethod]

        public void Should_ReturnTwoRoot_When_TestFile01()
        {
            var edges = GetDataFromFile(@"TestFile\Test01.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 2);
            Assert.AreEqual(result[1].RootNode.NodeId, 2);
            Assert.AreEqual(result[1].Propagation, 2);
        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile02()
        {
            var edges = GetDataFromFile(@"TestFile\Test02.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 2);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile03()
        {
            var edges = GetDataFromFile(@"TestFile\Test03.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 0);
            Assert.AreEqual(result[0].Propagation, 3);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile04()
        {
            var edges = GetDataFromFile(@"TestFile\Test04.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 78);
            Assert.AreEqual(result[0].Propagation, 5);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile05()
        {
            var edges = GetDataFromFile(@"TestFile\Test05.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 479);
            Assert.AreEqual(result[0].Propagation, 5);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile06()
        {
            var edges = GetDataFromFile(@"TestFile\Test06.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 495);
            Assert.AreEqual(result[0].Propagation, 7);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile07()
        {
            var edges = GetDataFromFile(@"TestFile\Test07.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1327);
            Assert.AreEqual(result[0].Propagation, 15);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile08()
        {
            var edges = GetDataFromFile(@"TestFile\Test08.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 8226);
            Assert.AreEqual(result[0].Propagation, 9);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile09()
        {
            var edges = GetDataFromFile(@"TestFile\Test09.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 41078);
            Assert.AreEqual(result[0].Propagation, 15);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile10()
        {
            var edges = GetDataFromFile(@"TestFile\Test10.txt");
            var treeGraphPropagation = new FastestTreeGraphPropagation<Node>(edges);

            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 5);

        }
        private List<Tuple<Node,Node>> GetDataFromFile(string Path)
        {
            var data = File.ReadAllLines(Path);

            var result = new List<Tuple<Node, Node>>();

            foreach (var item in data)
            {
                var arrayItem = item.Split(' ');
                var item1 = int.Parse(arrayItem[0]);
                var item2 = int.Parse(arrayItem[1]);
                result.Add(new Tuple<Node, Node>(new Node { NodeId = item1 }, new Node { NodeId = item2 }));

            }
            return result;
        }
    }
}
