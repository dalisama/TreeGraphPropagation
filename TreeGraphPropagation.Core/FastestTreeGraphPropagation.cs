using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraphPropagation.Core
{
    public class FastestTreeGraphPropagation<TNode> where TNode : class, IEquatable<TNode>
    {
        private readonly List<Tuple<TNode,TNode>> EdgesList;
   

        public FastestTreeGraphPropagation(List<Tuple<TNode, TNode>> edgesList)
        {
            EdgesList = edgesList;
        }

        public Root<TNode>[] GetFastOptimisedRoot()
        {
            if (EdgesList == null || !EdgesList.Any()) return null;
            if(EdgesList.Count==1) return new Root<TNode>[2] { new Root<TNode>(EdgesList.FirstOrDefault().Item1, 1), new Root<TNode>(EdgesList.FirstOrDefault().Item2, 1) };
            return null;
        }
    }
}
