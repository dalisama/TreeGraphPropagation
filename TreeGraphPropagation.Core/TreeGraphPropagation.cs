using System.Collections.Generic;

namespace TreeGraphPropagation.Core
{
    public class TreeGraphPropagation<TNode> where TNode : class
    {
        private readonly Dictionary<TNode, List<TNode>> Tree;


        public TreeGraphPropagation(Dictionary<TNode, List<TNode>> tree)
        {
            Tree = tree;
        }

        public struct Root
        {
            public TNode RootNode { get; }
            public int Propagation { get; }

            public Root(TNode rootNode, int propagation)
            {
                RootNode = rootNode;
                Propagation = propagation;
            }
        }

        public Root? GetOptimisedRoot()
        {
            return null;
        }

    }

}
