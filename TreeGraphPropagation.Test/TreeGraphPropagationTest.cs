using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 2);
            Assert.AreEqual(result[0].Propagation, 3);
            Assert.AreEqual(result[1].RootNode.NodeId, 3);
            Assert.AreEqual(result[1].Propagation, 3);

        }


        [TestMethod]

        public void Should_ReturnTwoRoot_When_TestFile01()
        {
            var tree = GetDataFromFile(@"TestFile\Test01.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);
        
            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 2);
            Assert.AreEqual(result[1].RootNode.NodeId, 2);
            Assert.AreEqual(result[1].Propagation, 2);
        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile02()
        {
            var tree = GetDataFromFile(@"TestFile\Test02.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 2);
        
        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile03()
        {
            var tree = GetDataFromFile(@"TestFile\Test03.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 0);
            Assert.AreEqual(result[0].Propagation, 3);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile04()
        {
            var tree = GetDataFromFile(@"TestFile\Test04.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 78);
            Assert.AreEqual(result[0].Propagation, 5);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile05()
        {
            var tree = GetDataFromFile(@"TestFile\Test05.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId,479);
            Assert.AreEqual(result[0].Propagation, 5);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile06()
        {
            var tree = GetDataFromFile(@"TestFile\Test06.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 495);
            Assert.AreEqual(result[0].Propagation, 7);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile07()
        {
            var tree = GetDataFromFile(@"TestFile\Test07.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1327);
            Assert.AreEqual(result[0].Propagation, 15);

        }

        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile08()
        {
            var tree = GetDataFromFile(@"TestFile\Test08.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 8226);
            Assert.AreEqual(result[0].Propagation, 9);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile09()
        {
            var tree = GetDataFromFile(@"TestFile\Test09.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 41078);
            Assert.AreEqual(result[0].Propagation, 15);

        }
        [TestMethod]
        public void Should_ReturnOneRoot_When_TestFile10()
        {
            var tree = GetDataFromFile(@"TestFile\Test10.txt");
            var treeGraphPropagation = new TreeGraphPropagation<Node>(tree);

            var result = treeGraphPropagation.GetOptimisedRoot();
            Assert.AreEqual(result[0].RootNode.NodeId, 1);
            Assert.AreEqual(result[0].Propagation, 5);

        }
        private Dictionary<Node, List<Node>> GetDataFromFile(string Path)
        {
            var data = File.ReadAllLines(Path);

            var result = new Dictionary<Node, List<Node>>();

            foreach (var item in data)
            {
                var arrayItem = item.Split(' ');
                var item1 = int.Parse(arrayItem[0]);
                var item2 = int.Parse(arrayItem[1]);
                if (result.Keys.Select(x => x.NodeId).Contains(item1))
                {
                    result.FirstOrDefault(x => x.Key.NodeId == item1).Value.Add(new Node { NodeId = item2 });
                }
                else
                {
                    result[new Node { NodeId = item1 }] = new List<Node> { new Node { NodeId = item2 } };
                }
                if (result.Keys.Select(x => x.NodeId).Contains(item2))
                {
                    result.FirstOrDefault(x => x.Key.NodeId == item2).Value.Add(new Node { NodeId = item1 });
                }
                else
                {
                    result[new Node { NodeId = item2 }] = new List<Node> { new Node { NodeId = item1 } };
                }
            }
            return result;
        }
    }


}
