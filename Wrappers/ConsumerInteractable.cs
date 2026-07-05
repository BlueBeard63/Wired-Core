using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using UnityEngine;
using Wired.Utilities;
using Wired.WiredInteractables;

namespace Wired.Wrappers
{
    public class ConsumerInteractable(Transform barricade)
    {
        private readonly InteractableSpot _spot = barricade.GetComponent<InteractableSpot>();
        private readonly InteractableOven _oven = barricade.GetComponent<InteractableOven>();
        private readonly InteractableOxygenator _oxygenator = barricade.GetComponent<InteractableOxygenator>();
        private readonly InteractableSafezone _safezone = barricade.GetComponent<InteractableSafezone>();
        private readonly InteractableCharge _charge = barricade.GetComponent<InteractableCharge>();
        private readonly IWiredInteractable _wiredInteractable = barricade.GetComponent<IWiredInteractable>();

        public void SetPowered(bool powered)
        {
            if (_wiredInteractable != null)
            {
                _wiredInteractable.SetPowered(powered);
                WiredLogger.Info($"Set wired interactable {(_wiredInteractable.interactable != null ? _wiredInteractable.interactable.name : "null")} to {(powered ? "ON" : "OFF")}");
                return;
            }
            if (_spot != null)
            {
                BarricadeManager.ServerSetSpotPowered(_spot, powered);

                BarricadeFinder finder = new(position: _spot.transform.position);
                if (finder.GetBarricadesInRadius(radius: 256).Any(b => b.asset.GUID == new Guid("101d13181ef1407ca583686f36663a0f")))
                {
                    Barricade bar = new(Plugin.Instance.Resources.generator_technical);
                    Transform gen = BarricadeManager.dropNonPlantedBarricade(bar, _spot.transform.position, _spot.transform.rotation, 0, 0);
                    if (gen != null)
                    {
                        BarricadeManager.sendFuel(gen, 512);
                        BarricadeManager.ServerSetGeneratorPowered(gen.GetComponent<InteractableGenerator>(), true);
                    }
                }
                return;
            }
            if (_oven != null)
                BarricadeManager.ServerSetOvenLit(_oven, powered);
            if (_oxygenator != null)
                BarricadeManager.ServerSetOxygenatorPowered(_oxygenator, powered);
            if (_safezone != null)
                BarricadeManager.ServerSetSafezonePowered(_safezone, powered);
            if (_charge != null && powered == true)
                _charge.Detonate(null);
        }

        
        public void Uninitialize()
        {
            _wiredInteractable?.Uninitialize();
        }
    }
}
