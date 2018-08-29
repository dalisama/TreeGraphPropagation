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
        private class Node : IEquatable<Node>
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

            public bool Equals(Node other)
            {
                return other.NodeId == this.NodeId;
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {

                return base.GetHashCode();
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
    }
}
