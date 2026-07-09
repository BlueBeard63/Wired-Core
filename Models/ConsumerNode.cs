using SDG.Unturned;
using System.Linq;
using UnityEngine;
using Wired.WiredAssets;
using Wired.Wrappers;

namespace Wired.Models
{
    public class ConsumerNode : MonoBehaviour, IElectricNode
    {
        public uint InstanceID {  get; private set; }
        public IWiredAsset Asset { get; set; }
        public bool IsPowered { get; set; }
        public float Consumption { get; set; }
        public bool AllowPowerThrough { get; set; } = true;
        public Transform WireConnectPoint { get; set; }
        public BarricadeDrop barricade { get; set; }

        private ConsumerInteractable Interactable;
        public void SetPowered(bool powered)
        {
            Interactable ??= new ConsumerInteractable(this.transform);
            IsPowered = powered;
            if(Interactable.IsPowered != powered)
                Interactable.SetPowered(powered);
        }
        private void Awake()
        {
            InstanceID = BarricadeManager.FindBarricadeByRootTransform(this.transform).instanceID;

            var p = transform.Find("WireConnectPoint");
            if (p != null) WireConnectPoint = p;
            else WireConnectPoint = this.transform;
        }

        public void Uninitialize()
        {
            Destroy(this);
        }
    }
}
