using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired.Utilities;
using Wired.WiredAssets;
using Wired.WiredInteractables;

namespace Wired.Services;

public class RemoteToolService
{
    private WiredAssetsService _assets;

    public delegate void RemoteToolSignalTransmitted(Player player, string frequency);
    public static event RemoteToolSignalTransmitted OnRemoteToolSignalTransmitted;
    public RemoteToolService(WiredAssetsService assets)
    {
        _assets = assets;

        NPCEventManager.onEvent += onNPCEventTriggered;
    }

    private void onNPCEventTriggered(Player player, string eventId)
    {
        if (!eventId.StartsWith("Wired:Remote")) return;
        if (player.equipment.asset == null) return;
        if (!_assets.WiredAssets.TryGetValue(player.equipment.asset.GUID, out IWiredAsset value) || value is not RemoteToolAsset asset) return;

        var item = EquipmentToItem(player.equipment);
        if (item == null) return;

        if (item.metadata.Length != 4)
        {
            item.metadata = [0, 0, 0, 0];
        }

        string eventId2 = eventId.Split(':')[1];
        switch (eventId2)
        {
            case "RemoteLeftClick":
                {
                    Raycast raycast = new(player, range: 4);
                    var barricade = raycast.GetBarricade(out _, out _, out _);
                    if (barricade == null || barricade.model.GetComponent<RemoteReceiver>() == null && barricade.model.GetComponent<RemoteTransmitter>() == null)
                    {
                        var freq = BitConverter.ToUInt16(item.metadata, 0);
                        if (freq == 0) return;
                        else
                        {
                            OnRemoteToolSignalTransmitted?.Invoke(player, $"3.{freq}");
                            WiredLogger.Info($"Transmitted remote tool signal on 3.{freq}");
                        }
                        return;
                    }
                    if (barricade.model.TryGetComponent(out RemoteTransmitter transmitter))
                    {
                        if (!ushort.TryParse(transmitter.Frequency.Split('.')[1], out ushort freq)) return;
                        var array = BitConverter.GetBytes(freq);
                        item.metadata[0] = array[0];
                        item.metadata[1] = array[1];

                        player.ServerShowHint($"Set left click frequency to 3.{freq}!", 2);
                    }
                    if (barricade.model.TryGetComponent(out RemoteReceiver receiver))
                    {
                        if (!ushort.TryParse(receiver.Frequency.Split('.')[1], out ushort freq2)) return;
                        var array = BitConverter.GetBytes(freq2);
                        item.metadata[0] = array[0];
                        item.metadata[1] = array[1];

                        player.ServerShowHint($"Set left click frequency to 3.{freq2}!", 2);
                    }
                }
                break;
            case "RemoteRightClick":
                {
                    Raycast raycast = new(player, range: 4);
                    var barricade = raycast.GetBarricade(out _, out _, out _);
                    if (barricade == null || barricade.model.GetComponent<RemoteReceiver>() == null && barricade.model.GetComponent<RemoteTransmitter>() == null)
                    {
                        var freq = BitConverter.ToUInt16(item.metadata, 2);
                        if (freq == 0) return;
                        else
                        {
                            OnRemoteToolSignalTransmitted?.Invoke(player, $"3.{freq}");
                            WiredLogger.Info($"Transmitted remote tool signal on 3.{freq}");
                        }
                        return;
                    }
                    if (barricade.model.TryGetComponent(out RemoteTransmitter transmitter))
                    {
                        if (!ushort.TryParse(transmitter.Frequency.Split('.')[1], out ushort freq)) return;
                        var array = BitConverter.GetBytes(freq);
                        item.metadata[2] = array[0];
                        item.metadata[3] = array[1];

                        player.ServerShowHint($"Set right click frequency to 3.{freq}!", 2);
                    }
                    if (barricade.model.TryGetComponent(out RemoteReceiver receiver))
                    {
                        if (!ushort.TryParse(receiver.Frequency.Split('.')[1], out ushort freq)) return;
                        var array = BitConverter.GetBytes(freq);
                        item.metadata[2] = array[0];
                        item.metadata[3] = array[1];

                        player.ServerShowHint($"Set right click frequency to 3.{freq}!", 2);
                    }
                }
                break;
        }
    }

    private Item EquipmentToItem(PlayerEquipment equipment)
    {
        var eqitem = equipment;

        if (eqitem == null || eqitem.player.inventory == null)
            return null;

        var page = eqitem.equippedPage;
        var x = eqitem.equipped_x;
        var y = eqitem.equipped_y;

        var index = eqitem.player.inventory.getIndex(page, x, y);
        return equipment.player.inventory.getItem(page, index).item;
    }
}
