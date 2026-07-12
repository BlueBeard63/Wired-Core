using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Models;
using Wired.Utilities;
using Wired.WiredAssets;

namespace Wired.WiredInteractables;

public class Sprinkler : MonoBehaviour, IWiredInteractable
{
    public static List<Sprinkler> SprinklersRegistry = [];
    public Interactable interactable {  get; private set; }
    public bool IsOn {  get; private set; }
    public float Radius { get; private set; }

    private void Awake()
    {
        if (TryGetComponent(out InteractableSpot spot))
        {
            interactable = spot;
        }
        
        Radius = ((SprinklerAsset)GetComponent<ConsumerNode>().Asset).Radius;
        SprinklersRegistry.Add(this);
    }

    public void SetPowered(bool state)
    {
        IsOn = state;
        if (interactable == null) return;
        if (interactable is InteractableSpot spot && spot != null)
        {
            if (spot.isPowered != state)
            {
                BarricadeManager.ServerSetSpotPowered(spot, state);
            }
        }
    }

    public void Uninitialize()
    {
        SprinklersRegistry.Remove(this);
        Destroy(this);
    }

    public static void HandleSprinklers()
    {
        Stopwatch sw = Stopwatch.StartNew();
        foreach(var sprinkler in SprinklersRegistry)
        {
            if(!sprinkler.IsOn) continue;
            BarricadeFinder finder = new(sprinkler.transform.position);
            var crops = finder.GetBarricadesInRadius(sprinkler.Radius);
            foreach(var crop in crops)
            {
                if (!crop.model.TryGetComponent(out InteractableFarm farm)) continue;
                if(farm.IsFullyGrown) continue;

                var newplanted = farm.planted - 10 > 10 ? farm.planted - 10 : 1;
                BarricadeManager.updateFarm(farm.transform, newplanted, true);
            }
        }
        sw.Stop();
        WiredLogger.Info($"Handled sprinklers, took {sw.ElapsedTicks /10000f} ms");
    }
}
