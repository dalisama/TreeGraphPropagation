using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
