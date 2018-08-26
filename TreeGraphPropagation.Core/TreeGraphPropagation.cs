using System.Collections.Generic;
using System.Linq;

namespace TreeGraphPropagation.Core
{
    public class TreeGraphPropagation<TNode> where TNode : class
    {
        private readonly Dictionary<TNode, List<TNode>> Tree;


        public TreeGraphPropagation(Dictionary<TNode, List<TNode>> tree)
        {
            Tree = tree;
        }

    

        public Root<TNode>? GetOptimisedRoot()
        {
            if (Tree == null || !Tree.Any()) return null;
            if (Tree.Count == 1) return new Root<TNode>(Tree.Keys.FirstOrDefault(), 0);
            return null;
        }

    }
    public struct Root<TNode> where TNode:class
    {
        public TNode RootNode { get;}
        public int Propagation { get; }

        public Root(TNode rootNode, int propagation)
        {
            RootNode = rootNode;
            Propagation = propagation;
        }
    }
}
