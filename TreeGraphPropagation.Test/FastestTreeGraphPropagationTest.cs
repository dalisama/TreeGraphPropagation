using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Assert.IsNotNull(treeGraphPropagation.GetFastOptimisedRoot());
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
            Assert.IsNotNull(treeGraphPropagation.GetFastOptimisedRoot());
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
            Assert.IsNotNull(treeGraphPropagation.GetFastOptimisedRoot());
            var result = treeGraphPropagation.GetFastOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId,2);
            Assert.AreEqual(result[0].Propagation, 2);
       

        }
    }
}
