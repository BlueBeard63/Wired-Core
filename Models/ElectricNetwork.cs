using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired.Utilities;

namespace Wired.Models
{
    public class ElectricNetwork
    {
        public HashSet<IElectricNode> Nodes { get; private set; } = [];
        public HashSet<NodeConnection> Connections { get; private set; } = [];


        public float TotalSupply { get; private set; }
        public float TotalConsumption { get; private set; }

        /// <summary>
        /// Resets to false every <see cref="Plugin.Update"/>, to limit recalculations to 1 per frame.
        /// A measure to prevent lag machines.
        /// </summary>
        private bool _frameLocked = false;
        private bool _recalculationPending = false;
        public void ResetFrameLock()
        {
            _frameLocked = false;
            if(_recalculationPending)
                RecalculatePower();
        }

        public static event Action<ElectricNetwork, float> PowerUpdated;

        public void AddNode(IElectricNode node) => Nodes.Add(node);
        public void AddConnection(NodeConnection conn) => Connections.Add(conn);

        public void RecalculatePower()
        {
            if (_frameLocked)
            {
                _recalculationPending = true;
                return;
            }
            _recalculationPending = false;
            _frameLocked = true;

            Stopwatch sw = new();
            sw.Start();

            HashSet<IElectricNode> visited = [];

            var adjacencyMap = BuildAdjacencyMap();

            foreach (var startNode in Nodes)
            {
                if (visited.Contains(startNode))
                    continue;

                List<IElectricNode> islandNodes = [];
                float currentIslandSupply = 0f;
                float currentIslandConsumption = 0f;

                Queue<IElectricNode> queue = new();
                queue.Enqueue(startNode);
                visited.Add(startNode);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    islandNodes.Add(current);

                    if (current is SupplierNode sup && sup.IsPowered) currentIslandSupply += sup.Supply;
                    if (current is ConsumerNode cons) currentIslandConsumption += cons.Consumption;

                    if (!current.AllowPowerThrough)
                        continue;

                    if (adjacencyMap.ContainsKey(current))
                    {
                        foreach (var neighbor in adjacencyMap[current])
                        {
                            if (!visited.Contains(neighbor))
                            {
                                visited.Add(neighbor);
                                queue.Enqueue(neighbor);
                            }
                        }
                    }
                }

                bool hasEnoughPower = currentIslandSupply > 0 &&
                                      currentIslandSupply >= currentIslandConsumption;

                foreach (var node in islandNodes)
                {
                    if(node is TimerNode timer)
                    {
                        if (hasEnoughPower)
                            timer.StartTimer();
                        else
                            timer.StopIfRunning();
                        continue;
                    }
                    if (node.IsPowered != hasEnoughPower)
                    {
                        node.SetPowered(hasEnoughPower);
                    }
                }
            }

            PowerUpdated?.Invoke(this, sw.ElapsedTicks/10000f);

            sw.Stop();
            //WiredLogger.Info($"Recalculated power flow in a {Nodes.Count}-node network, took {sw.ElapsedTicks / 10000f} ms.");
        }

        private Dictionary<IElectricNode, List<IElectricNode>> BuildAdjacencyMap()
        {
            var map = new Dictionary<IElectricNode, List<IElectricNode>>();

            foreach (var conn in Connections)
            {
                if (!map.ContainsKey(conn.Node1)) map[conn.Node1] = [];
                if (!map.ContainsKey(conn.Node2)) map[conn.Node2] = [];

                map[conn.Node1].Add(conn.Node2);
                map[conn.Node2].Add(conn.Node1);
            }
            return map;
        }
    }
}