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
    }
}
