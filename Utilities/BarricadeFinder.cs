using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace Wired
{
    public class BarricadeFinder
    {
        private readonly Vector3 _position;

        public BarricadeFinder(Vector3 position = new Vector3())
        {
            _position = position;
        }
        public BarricadeDrop GetBarricadeByInstanceID(uint instanceID)
        {
            foreach(BarricadeRegion reg in BarricadeManager.regions)
            {
                foreach(BarricadeDrop bar in reg.drops)
                {
                    if(bar.instanceID == instanceID)
                        return bar;
                }
            }
            return null;
        }

        public List<BarricadeDrop> GetBarricadesInRadius(float radius = 0)
        {
            List<BarricadeDrop> result = new List<BarricadeDrop>();
            BarricadeRegion[,] regions = BarricadeManager.regions;
            if (radius == 0)
            {
                foreach (var region in regions)
                {
                    foreach (var drop in region.drops)
                    {
                        result.Add(drop);
                    }
                }
                return result;
            }

            radius *= radius;
            var search = new List<RegionCoordinate>();
            Regions.getRegionsInRadius(_position, radius, search);

            for (int i = 0; i < search.Count; i++)
            {
                RegionCoordinate regionCoordinate = search[i];
                if (regions[regionCoordinate.x, regionCoordinate.y] == null)
                {
                    continue;
                }

                foreach (BarricadeDrop drop in regions[regionCoordinate.x, regionCoordinate.y].drops)
                {
                    Transform model = drop.model;
                    if (!(model == null) && (model.position - _position).sqrMagnitude < radius)
                    {
                        result.Add(drop);
                    }
                }
            }
            return result;
        }
        public List<BarricadeDrop> GetBarricadesOfType<T>() where T: Component
        {
            List<BarricadeDrop> result = [];

            foreach(BarricadeRegion reg in BarricadeManager.regions)
            {
                foreach(BarricadeDrop drop in reg.drops)
                {
                    if (drop.model.TryGetComponent(out T _))
                        result.Add(drop);
                }
            }
            return result;
        }
        public Queue<T> GetBarricadesOfTypeQueue<T>() where T: Component
        {
            Queue<T> result = new();

            foreach (BarricadeRegion reg in BarricadeManager.regions)
            {
                foreach (BarricadeDrop drop in reg.drops)
                {
                    if (drop.model.TryGetComponent(out T component))
                        result.Enqueue(component);
                }
            }
            return result;
        }
    }
}