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
                BarricadeManager.ServerSetSpotPowered(_spot, powered);
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
