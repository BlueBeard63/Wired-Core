using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wired.Commands
{
    public class WiredCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "wired";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => [];

        public List<string> Permissions => ["wired"];

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
                return;

            if (command[0] == "delta")
            {
                if(command.Length < 2)
                {
                    UnturnedChat.Say(caller, "Usage: /wired delta <connect/disconnect>", Color.red);
                    return;
                }
                if (command[1] == "connect")
                {
                    UnturnedChat.Say(caller, "Connecting to Wired Delta...", Color.green);
                    Plugin.Instance.Services.WiredDeltaService.Connect(caller);
                }
                else if (command[1] == "disconnect")
                {
                    UnturnedChat.Say(caller, "Disconnecting from Wired Delta...", Color.green);
                    Plugin.Instance.Services.WiredDeltaService.Disconnect(caller);
                }
            }
            if (command[0] == "testwind")
            {
                var wind = Plugin.Instance.Services.WindService.GetWindAt(((UnturnedPlayer)caller).Position);
                UnturnedChat.Say(caller, $"Intensity: {wind.Intensity}, Direction: {wind.Direction}");
            }
        }
    }
}
