using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraphPropagation.Core
{
    public class FastestTreeGraphPropagation<TNode> where TNode :class, IEqualityComparer<TNode>
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
            var itemList = EdgesList.SelectMany(x => new List<TNode> { x.Item1, x.Item2 }).ToList();
            var indexPropagation = 0;
            while(itemList.Count>2)
            {
                var extremum = itemList.GroupBy(c => c).Where(x => x.Count() == 1).Select(x=>x.Key).ToList();
                if(extremum.Count+1==itemList.Distinct().Count())
                {
                    indexPropagation++;
                    break;
                }
                foreach (var item in extremum)
                {

                    if (!itemList.Any()) break;

                    var index = itemList.IndexOf(item);


                    itemList.RemoveRange(index - (index % 2), 2);

                }
                indexPropagation++;
            }

            if (itemList.Count == 2) indexPropagation++;
            if (itemList.Count > 2) itemList = itemList.GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            return itemList.Select(x => new Root<TNode>(x, indexPropagation)).ToArray();
        }
    }
}
