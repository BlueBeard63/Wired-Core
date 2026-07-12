using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Models;

namespace Wired.WiredInteractables;

public class LogicGate : MonoBehaviour, IWiredInteractable
{
    public Interactable interactable { get; private set; }
    public uint InstanceID => GetComponent<IElectricNode>().InstanceID;
    public LogicGateType Type { get; set; }
    private GateNode _gateNode;

    public LogicGateSubnode Input0;
    public LogicGateSubnode Input1;
    public Vector3 Input0Position => Input0.transform.position;
    public Vector3 Input1Position => Input1.transform.position;

    public bool IsOn { get; }

    public void SetPowered(bool state)
    {
        if (!state)
        {
            if (_gateNode.AllowPowerThrough)
                _gateNode.Switch(false, recalculateAfter: false);
            return;
        }
        bool logic = false;

        switch (Type)
        {
            case LogicGateType.AND:
                logic = Input0.IsPowered && Input1.IsPowered;
                break;
            case LogicGateType.OR:
                logic = Input0.IsPowered || Input1.IsPowered;
                break;
            case LogicGateType.NAND:
                logic = !(Input0.IsPowered && Input1.IsPowered);
                break;
            case LogicGateType.NOR:
                logic = !(Input0.IsPowered || Input1.IsPowered);
                break;
            case LogicGateType.XNOR:
                logic = Input0.IsPowered == Input1.IsPowered;
                break;
            case LogicGateType.XOR:
                logic = Input0.IsPowered != Input1.IsPowered;
                break;
            case LogicGateType.NOT:
                logic = !Input0.IsPowered;
                break;
            default:
                break;
        }
        if(_gateNode.AllowPowerThrough != logic)
            _gateNode.Switch(logic, recalculateAfter: false);
    }
    private void Awake()
    {
        _gateNode = this.transform.GetComponent<GateNode>();
        if(this.transform.Find("Input_0") != null)
        {
            Input0 = this.transform.Find("Input_0").gameObject.AddComponent<LogicGateSubnode>();
            Input0.ParentNode = this;
            Input0.barricade = this._gateNode.barricade;
        }
        if(this.transform.Find("Input_1") != null)
        {
            Input1 = this.transform.Find("Input_1").gameObject.AddComponent<LogicGateSubnode>();
            Input1.ParentNode = this;
            Input1.barricade = this._gateNode.barricade;
        }
    }

    public void Uninitialize()
    {
        Input0?.Uninitialize();
        Input1?.Uninitialize();
        Destroy(this);
    }
}

public enum LogicGateType
{
    AND,
    OR,
    XOR,
    NAND,
    NOR,
    XNOR,
    NOT
}