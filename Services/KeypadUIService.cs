using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using Wired.Utilities;
using Wired.WiredInteractables;

namespace Wired.Services
{
    public class KeypadUIService
    {
        private List<KeypadUISession> _keypadUISessions = new List<KeypadUISession>();
        private EffectAsset _keypadUIEffect;

        public delegate void KeypadCodeSubmitted(uint keypadInstanceId, string code, CSteamID steamid);
        public static event KeypadCodeSubmitted OnKeypadCodeSubmitted;

        public KeypadUIService()
        {
            _keypadUIEffect = Assets.find(new Guid("caed0530dfe84087a66f60dacab11ae7")) as EffectAsset;
            if(_keypadUIEffect == null)
            {
                WiredLogger.Error("Failed to find Keypad UI Effect Asset!");
                return;
            }
            Plugin.OnKeypadInteractRequested += OnKeypadInteractRequested;
            EffectManager.onEffectButtonClicked += OnEffectButtonClicked;
        }

        public void OpenKeypadUI(CSteamID steamid, uint instanceId)
        {
            var tc = Provider.findTransportConnection(steamid);
            EffectManager.SendUIEffect(_keypadUIEffect, (short)_keypadUIEffect.id, tc, true);
            _keypadUISessions.Add(new KeypadUISession(steamid, "", instanceId));
            UnturnedPlayer.FromCSteamID(steamid).Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, true);
        }

        public void CloseKeypadUI(CSteamID steamid)
        {
            var tc = Provider.findTransportConnection(steamid);
            EffectManager.ClearEffectByGuid(new Guid("caed0530dfe84087a66f60dacab11ae7"), tc);
            _keypadUISessions.RemoveAll(s => s.steamid == steamid);
            UnturnedPlayer.FromCSteamID(steamid).Player.setPluginWidgetFlag(EPluginWidgetFlags.Modal, false);
        }

        private void OnKeypadInteractRequested(Keypad keypad, Player player)
        {
            var bar = BarricadeManager.FindBarricadeByRootTransform(keypad.transform);
            if (bar == null) return;

            if(_keypadUISessions.Any(s => s.steamid == player.channel.owner.playerID.steamID))
            {
                CloseKeypadUI(player.channel.owner.playerID.steamID);
                _keypadUISessions.RemoveAll(s => s.steamid == player.channel.owner.playerID.steamID);
                return;
            }
            OpenKeypadUI(player.channel.owner.playerID.steamID, bar.instanceID);
        }

        private void OnEffectButtonClicked(Player player, string buttonName)
        {
            if(!buttonName.StartsWith("Keypad")) return;

            var session = _keypadUISessions.FirstOrDefault(s => s.steamid == player.channel.owner.playerID.steamID);
            if (session == null) return;

            if (buttonName == "KeypadButton_Submit")
            {
                CloseKeypadUI(player.channel.owner.playerID.steamID);
                OnKeypadCodeSubmitted?.Invoke(session.keypadInstanceId, session.enteredCode, session.steamid);
                return;
            }
            else if (buttonName == "KeypadButton_Erase")
            {
                session.enteredCode = session.enteredCode.Length > 0 ? session.enteredCode.Remove(session.enteredCode.Length-1) : "";
                EffectManager.sendUIEffectText((short)_keypadUIEffect.id, Provider.findTransportConnection(player.channel.owner.playerID.steamID), true, "Background/Keypad_Display", session.enteredCode);
                return;
            }
            else if (buttonName == "KeypadCloseUI")
            {
                CloseKeypadUI(player.channel.owner.playerID.steamID);
                return;
            }
            else
            {
                string number = buttonName.Split('_')[1];
                if (int.TryParse(number, out int _))
                {
                    session.enteredCode += number;
                    EffectManager.sendUIEffectText((short)_keypadUIEffect.id, Provider.findTransportConnection(player.channel.owner.playerID.steamID), true, "Background/Keypad_Display", session.enteredCode);
                    return;
                }
            }
        }

        private class KeypadUISession
        {
            public readonly CSteamID steamid;
            public string enteredCode = "";
            public readonly uint keypadInstanceId;
            public KeypadUISession(Player player, string enteredCode, uint keypadInstanceId)
            {
                this.steamid = player.channel.owner.playerID.steamID;
                this.enteredCode = enteredCode;
                this.keypadInstanceId = keypadInstanceId;
            }
            public KeypadUISession(CSteamID steamid, string enteredCode, uint keypadInstanceId)
            {
                this.steamid = steamid;
                this.enteredCode = enteredCode;
                this.keypadInstanceId = keypadInstanceId;
            }
        }
    }
}
