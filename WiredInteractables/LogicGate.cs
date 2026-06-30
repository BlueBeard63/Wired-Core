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
    public LogicGateType Type { get; set; }
    private GateNode _gateNode;

    private LogicGateSubnode _input0;
    private LogicGateSubnode _input1;
    public Vector3 Input0 => _input0.transform.position;
    public Vector3 Input1 => _input1.transform.position;

    public bool IsOn { get; }

    public void SetPowered(bool state)
    {
        switch (Type)
        {
            case LogicGateType.AND:
                _gateNode.Switch(_input0.IsPowered && _input1.IsPowered);
                break;
            case LogicGateType.OR:
                _gateNode.Switch(_input0.IsPowered || _input1.IsPowered);
                break;
            case LogicGateType.NAND:
                _gateNode.Switch(!(_input0.IsPowered && _input1.IsPowered));
                break;
            case LogicGateType.NOR:
                _gateNode.Switch(!(_input0.IsPowered || _input1.IsPowered));
                break;
            case LogicGateType.XNOR:
                _gateNode.Switch(_input0.IsPowered == _input1.IsPowered);
                break;
            case LogicGateType.XOR:
                _gateNode.Switch(_input0.IsPowered != _input1.IsPowered);
                break;
            case LogicGateType.NOT:
                    _gateNode.Switch(!_input0.IsPowered);
                break;
            default:
                break;
        }
    }
    private void Awake()
    {
        _gateNode = this.transform.GetComponent<GateNode>();
        if(this.transform.Find("Input_0") != null)
        _input0 = this.transform.Find("Input_0").gameObject.AddComponent<LogicGateSubnode>();
        if(this.transform.Find("Input_1") != null)
            _input1 = this.transform.Find("Input_1").gameObject.AddComponent<LogicGateSubnode>();
    }

    public void Uninitialize()
    {
        
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