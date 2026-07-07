using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wired.Models;

namespace Wired.WiredInteractables
{
    public class Button : MonoBehaviour, IWiredInteractable
    {
        public Interactable interactable { get; private set; }

        public bool IsOn { get; private set; }
        public float StaysPressedSeconds;

        public void SetPowered(bool state)
        {
            IsOn = state;
            if (state)
            {
                StartCoroutine(CloseGateAfterDelay());
            }
        }
        private IEnumerator CloseGateAfterDelay()
        {
            yield return new WaitForSeconds(StaysPressedSeconds);
            transform.GetComponent<GateNode>().Switch(false);
            IsOn = false;
        }
        public void Uninitialize()
        {
            Destroy(this);
        }
    }
}
