using UnityEngine;
using System.Collections;

public interface IViveControlControllable {
    void AttachDevice(SteamVR_Controller.Device device, Transform parentableTransform);
    void DetachDevice(SteamVR_Controller.Device device);
}
