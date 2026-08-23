using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Utilities;

namespace Wired.WiredInteractables
{
    public class WindTurbine : MonoBehaviour, IWiredInteractable
    {
        public Interactable interactable { get; private set; }

        public bool IsOn { get; private set; }

        private void Awake()
        {
            Plugin.OnTimeOfDayUpdated += OnTimeOfDayUpdated;
        }

        private void OnTimeOfDayUpdated(uint timeOfDay, float timefraction)
        {
            var wind = Plugin.Instance.Services.WindService.GetWindAt(transform.position);
            WiredLogger.Info($"Current wind at {transform.position}: {wind.Intensity}, {wind.Direction}");
        }

        public void SetPowered(bool state)
        {

        }

        public void Uninitialize()
        {
            
        }
    }
}
