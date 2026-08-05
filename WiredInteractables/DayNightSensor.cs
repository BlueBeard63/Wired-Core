using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Models;
using Wired.WiredAssets;
using Wired.WiredAssets.Placeables;

namespace Wired.WiredInteractables
{
    public class DayNightSensor : MonoBehaviour, IWiredInteractable
    {
        public Interactable interactable { get; private set; }
        
        public bool IsOn { get; private set; }

        private DayNightSensorAsset _asset;
        private GateNode _gate;
        public void Start()
        {
            _gate = gameObject.GetComponent<GateNode>();
            _asset = (DayNightSensorAsset)_gate.Asset;

            LightingManager.onDayNightUpdated += onDayNightUpdated;
        }

        private void onDayNightUpdated(bool isDaytime)
        {
            SetPowered(isDaytime == (_asset.Mode == DayNightSensorMode.Day));
        }

        public void SetPowered(bool state)
        {
            
        }

        public void Uninitialize()
        {
            
        }
    }
}
