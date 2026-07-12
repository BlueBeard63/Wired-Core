using JetBrains.Annotations;
using SDG.Unturned;
using UnityEngine;
using Wired.Models;
using Wired.Utilities;

namespace Wired
{
    public class Raycast(Player player, uint range = 8)
    {
        private readonly Player player = player;
        private readonly uint Range = range;

        public BarricadeDrop GetBarricade(out string colliderName, out float hitDistance, out LogicGateSubnode lgs)
        {
            colliderName = "";
            hitDistance = 0f;
            lgs = null;
            Transform aim = player.look.aim;

            if (!Physics.Raycast(aim.position, aim.forward, out var hit, Range, RayMasks.BARRICADE_INTERACT))
            {
                return null;
            }

            if (Physics.Raycast(aim.position, aim.forward, out _, hit.distance - 0.1f, RayMasks.BLOCK_COLLISION))
            {
                return null;
            }

            colliderName = hit.collider.name;
            Transform targetTransform = hit.transform;

            if (targetTransform.parent != null && targetTransform.parent.name == "Skeleton")
            {
                if (targetTransform.name.Contains("Hinge"))
                {
                    targetTransform = targetTransform.parent.parent;
                }
            }
            if(targetTransform.parent != null && targetTransform.name.StartsWith("Input"))
            {
                if(targetTransform.TryGetComponent(out LogicGateSubnode lgsn))
                {
                    lgs = lgsn;
                    colliderName = targetTransform.name;

                    targetTransform = targetTransform.parent;
                }
            }
            if(targetTransform.parent != null && targetTransform.name == "Clip")
            {
                targetTransform = targetTransform.parent;
            }

            hitDistance = hit.distance;
            return BarricadeManager.FindBarricadeByRootTransform(targetTransform);
        }
        public Vector3 GetPoint()
        {
            Transform aim = player.look.aim;
            if (!Physics.Raycast(aim.position, aim.forward, out var hitInfo, Range, RayMasks.BLOCK_COLLISION))
            {
                return Vector3.zero;
            }
            return hitInfo.point;
        }
    }
}
