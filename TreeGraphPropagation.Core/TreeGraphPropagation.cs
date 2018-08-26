using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraphPropagation.Core
{
    public class TreeGraphPropagation<TNode> where  TNode : class
    {
        private readonly Dictionary<TNode, List<TNode>> Tree;

        public TreeGraphPropagation(Dictionary<TNode, List<TNode>> tree)
        {
            Tree = tree;
        }

    }
}
