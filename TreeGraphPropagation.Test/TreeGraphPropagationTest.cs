using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TreeGraphPropagation.Core;


namespace TreeGraphPropagation.Test
{
    [TestClass]
    public class TreeGraphPropagationTest
    {

        private class Node: IEquatable<Node>
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
            var treeGraphPropagation = new TreeGraphPropagation<Node>(null);
            Assert.IsNull(treeGraphPropagation.GetOptimisedRoot());
        }
        [TestMethod]
        public void Should_ReturnTheOnlyElement_When_TreeHasOneElement()
        {
            var tree = new Dictionary<Node, List<Node>>();
            tree[new Node { NodeId = 1 }] = new List<Node>();
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);
            Assert.IsNotNull(treeGraphPropagation.GetOptimisedRoot());
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 0);
        }

        [TestMethod]
        public void Should_ReturnTWoRoot_When_TreeHasTwoElements()
        {
            var tree = new Dictionary<Node, List<Node>>();
            tree[new Node { NodeId = 1 }] = new List<Node> { new Node { NodeId = 2 } };
            tree[new Node { NodeId = 2 }] = new List<Node> { new Node { NodeId = 1 } };
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);
            Assert.IsNotNull(treeGraphPropagation.GetOptimisedRoot());
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 1);
            Assert.AreEqual(result[1].RootNode.NodeId, 2);
            Assert.AreEqual(result[1].Propagation, 1);
        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_Tree()
        {
            var tree = new Dictionary<Node, List<Node>>();
            tree[new Node { NodeId = 1 }] = new List<Node> { new Node { NodeId = 2 } };
            tree[new Node { NodeId = 2 }] = new List<Node> { new Node { NodeId = 1 } , new Node { NodeId = 4 } , new Node { NodeId = 3 } };
            tree[new Node { NodeId = 3 }] = new List<Node> { new Node { NodeId = 7 }, new Node { NodeId = 8 }, new Node { NodeId = 2 } };
            tree[new Node { NodeId = 4 }] = new List<Node> { new Node { NodeId = 5 }, new Node { NodeId = 6 }, new Node { NodeId = 2 } };
            tree[new Node { NodeId = 5 }] = new List<Node> { new Node { NodeId = 4 } };
            tree[new Node { NodeId = 6 }] = new List<Node> { new Node { NodeId = 4 } };
            tree[new Node { NodeId = 7 }] = new List<Node> { new Node { NodeId = 3 } };
            tree[new Node { NodeId = 8 }] = new List<Node> { new Node { NodeId = 3 } };
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);
            Assert.IsNotNull(treeGraphPropagation.GetOptimisedRoot());
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 2);
            Assert.AreEqual(result[0].Propagation, 2);
      
        }

        [TestMethod]
        public void Should_ReturnTwoRoot_When_Tree()
        {
            var tree = new Dictionary<Node, List<Node>>();
            tree[new Node { NodeId = 1 }] = new List<Node> { new Node { NodeId = 2 } };
            tree[new Node { NodeId = 2 }] = new List<Node> { new Node { NodeId = 1 }, new Node { NodeId = 4 }, new Node { NodeId = 3 } };
            tree[new Node { NodeId = 3 }] = new List<Node> { new Node { NodeId = 7 }, new Node { NodeId = 8 }, new Node { NodeId = 2 } };
            tree[new Node { NodeId = 4 }] = new List<Node> { new Node { NodeId = 5 }, new Node { NodeId = 6 }, new Node { NodeId = 2 } };
            tree[new Node { NodeId = 5 }] = new List<Node> { new Node { NodeId = 4 } };
            tree[new Node { NodeId = 6 }] = new List<Node> { new Node { NodeId = 4 } };
            tree[new Node { NodeId = 7 }] = new List<Node> { new Node { NodeId = 3 } };
            tree[new Node { NodeId = 8 }] = new List<Node> { new Node { NodeId = 3 }, new Node { NodeId = 9 } };
            tree[new Node { NodeId = 9 }] = new List<Node> { new Node { NodeId = 8 } };
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);
            Assert.IsNotNull(treeGraphPropagation.GetOptimisedRoot());
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 2);
            Assert.AreEqual(result[0].Propagation, 3);
            Assert.AreEqual(result[1].RootNode.NodeId, 3);
            Assert.AreEqual(result[1].Propagation, 3);

        }
    }

}
