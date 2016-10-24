using UnityEngine;
using System.Collections;

public interface IViveControlControllable {
    void AttachController(ViveControllerControl viveController);
    void DetachController(ViveControllerControl viveController);
}
