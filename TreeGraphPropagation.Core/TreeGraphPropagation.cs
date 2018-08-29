using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeGraphPropagation.Core
{
    public class TreeGraphPropagation<TNode> where TNode : class, IEquatable<TNode>
    {
        private readonly Dictionary<TNode, List<TNode>> Tree;


        public TreeGraphPropagation(Dictionary<TNode, List<TNode>> tree)
        {
            Tree = tree;
        }



        public Root<TNode>[] GetOptimisedRoot()
        {

            if (Tree == null || !Tree.Any()) return null;
            if (Tree.Count == 1) return new Root<TNode>[1] { new Root<TNode>(Tree.Keys.FirstOrDefault(), 0) };
         //   if (Tree.Count == 2) return new Root<TNode>[2] { new Root<TNode>(Tree.Keys.FirstOrDefault(), 1), new Root<TNode>(Tree.Keys.LastOrDefault(), 1) };



            var indexPropagation = 0;
            // cloning the tree to keep a working copy
            var treeCopy = Tree.ToDictionary(x => x.Key, x => x.Value.Select(y => y).ToList());
            while (treeCopy.Count > 2)
            {
                // getting extremum 
                var extremum = treeCopy.Where(x => x.Value.Count == 1).Select(x => x.Key).ToList();
                foreach (var item in extremum)
                {
                    treeCopy.Remove(item);
                    foreach (var key in treeCopy.Keys)
                    {
                        treeCopy[key].Remove(item);
                    }
                }
                indexPropagation++;

            }
            if(treeCopy.Count==2) indexPropagation++;
            return treeCopy.Keys.Select(x => new Root<TNode>(x, indexPropagation)).ToArray();
          
        }

    }
    public struct Root<TNode> where TNode : class
    {
        public TNode RootNode { get; }
        public int Propagation { get; }

        public Root(TNode rootNode, int propagation)
        {
            RootNode = rootNode;
            Propagation = propagation;
        }
    }

   
}
