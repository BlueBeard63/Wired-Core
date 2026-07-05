using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Wired.Utilities;

namespace Wired
{
    public class PlayerEvents : MonoBehaviour
    {
        public delegate void onDequipRequested(Player player, ItemAsset asset, ref bool shouldAllow);

        public static event onDequipRequested OnDequipRequested;

        public delegate void onEquipRequested(Player player, ItemAsset asset, ref bool shouldAllow);
        public static event onEquipRequested OnEquipRequested;

        public void Awake()
        {
            Player player = this.gameObject.transform.GetComponent<Player>();
            PlayerEquipment equipment = player.equipment;
            equipment.onDequipRequested = (PlayerDequipRequestHandler)Delegate.Combine(equipment.onDequipRequested, (PlayerDequipRequestHandler)delegate (PlayerEquipment equipment, ref bool shouldAllow)
            {
                OnDequipRequested?.Invoke(player, equipment.asset, ref shouldAllow);
            });
            equipment.onEquipRequested = (PlayerEquipRequestHandler)Delegate.Combine(equipment.onEquipRequested, (PlayerEquipRequestHandler)delegate (PlayerEquipment equipment, ItemJar jar, ItemAsset asset, ref bool shouldAllow)
            {
                OnEquipRequested?.Invoke(player, asset, ref shouldAllow);
            });
        }
    }
}
