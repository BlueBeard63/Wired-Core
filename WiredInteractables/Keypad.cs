using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Models;
using Wired.Services;
using Wired.Utilities;

namespace Wired.WiredInteractables;

public class Keypad : MonoBehaviour, IWiredInteractable
{
    public Interactable interactable { get; private set; }
    public bool IsOn => false;
    public float StaysOnSeconds;
    private uint _instanceId;
    public string Code = string.Empty;

    private void Awake()
    {
        _instanceId = BarricadeManager.FindBarricadeByRootTransform(transform)?.instanceID ?? 0;
        KeypadUIService.OnKeypadCodeSubmitted += KeypadUIService_OnKeypadCodeSubmitted;
        transform.GetComponent<GateNode>().Switch(false);
        interactable = GetComponent<InteractableSpot>();
    }

    private void KeypadUIService_OnKeypadCodeSubmitted(uint keypadInstanceId, string code, CSteamID steamid)
    {
        if (keypadInstanceId != _instanceId) return;

        if (Code == string.Empty)
        {
            Code = code;
            UnturnedPlayer.FromCSteamID(steamid).Player.ServerShowHint($"Set keypad code to: {code}", 2f);
        }
        else
        {
            if (Code == code)
            {
                WiredLogger.Info($"Keypad code correct for instance {_instanceId}, activating connected nodes.");
                transform.GetComponent<GateNode>().Switch(true);
                if(interactable != null)
                {
                    BarricadeManager.ServerSetSpotPowered((InteractableSpot)interactable, !((InteractableSpot)interactable).isPowered);
                }
                StartCoroutine(CloseGateAfterDelay());
            }
        }
    }

    private IEnumerator CloseGateAfterDelay()
    {
        yield return new WaitForSeconds(StaysOnSeconds);
        transform.GetComponent<GateNode>().Switch(false);
    }

    public void SetPowered(bool state) { }
    public void Uninitialize()
    {
        KeypadUIService.OnKeypadCodeSubmitted -= KeypadUIService_OnKeypadCodeSubmitted;
        Destroy(this);
    }
}
