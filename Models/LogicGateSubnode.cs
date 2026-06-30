using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.WiredAssets;
using Wired.WiredInteractables;

namespace Wired.Models;

/// <summary>
/// This gets attached to Input_# on logic gates.
/// </summary>
public class LogicGateSubnode : MonoBehaviour, IElectricNode
{
    public uint InstanceID { get; set; }

    public IWiredAsset Asset { get; set; }

    public bool IsPowered { get; set; }

    public float Consumption { get; set; }

    public bool AllowPowerThrough { get; set; } = false;

    public Transform WireConnectPoint { get; set; }

    public LogicGate ParentNode { get; set; }

    public void SetPowered(bool powered)
    {
        IsPowered = powered;
    }

    private void Awake()
    {
        WireConnectPoint = this.transform;
        IsPowered = false;
        ParentNode = this.GetComponent<LogicGate>();
        var col = this.gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 0.1f;
    }

    public void Uninitialize()
    {

    }
}
