using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eltin_Buchard_Keller_Algorithm
{
    public abstract class BKTreeNode
    {
        private readonly Dictionary<Int32, BKTreeNode> _children;

        public BKTreeNode()
        {
            _children = new Dictionary<Int32, BKTreeNode>();
        }

        public virtual void Add(BKTreeNode node)
        {
            int distance = CalculateDistance(node);

            if (_children.ContainsKey(distance))
                _children[distance].Add(node);
            else
                _children.Add(distance, node);
        }

        public virtual int FindBestMatch(BKTreeNode node, int bestDistance, out BKTreeNode bestNode)
        {
            int distanceAtNode = CalculateDistance(node);
            bestNode = node;

            if (distanceAtNode < bestDistance)
            {
                bestDistance = distanceAtNode;
                bestNode = this;
            }

            int possibleBest = bestDistance;

            foreach (Int32 distance in _children.Keys)
                if (distance < distanceAtNode + bestDistance)
                {
                    possibleBest = _children[distance].FindBestMatch(node, bestDistance, out bestNode);
                    if (possibleBest < bestDistance)
                        bestDistance = possibleBest;
                }

            return bestDistance;
        }

        public virtual void Query(BKTreeNode node, int threshold, Dictionary<BKTreeNode, Int32> collected)
        {
            int distanceAtNode = CalculateDistance(node);

            if (distanceAtNode == threshold)
            {
                collected.Add(this, distanceAtNode);
                return;
            }

            if (distanceAtNode < threshold)
                collected.Add(this, distanceAtNode);

            for (int distance = (distanceAtNode - threshold); distance <= (threshold + distanceAtNode); distance++)
                if (_children.ContainsKey(distance))
                    _children[distance].Query(node, threshold, collected);
        }

        protected abstract int CalculateDistance(BKTreeNode node);
    }
}
