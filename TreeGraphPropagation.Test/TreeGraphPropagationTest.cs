using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TreeGraphPropagation.Core;


namespace TreeGraphPropagation.Test
{
    [TestClass]
    public class TreeGraphPropagationTest
    {

        public class Node
        {
            public int NodeId { get; set; }
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
            Assert.AreEqual(((Root<Node>)result).RootNode.NodeId, 1);
            Assert.AreEqual(((Root<Node>)result).Propagation, 0);
        }
    }
}
