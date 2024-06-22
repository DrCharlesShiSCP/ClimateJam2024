using StarterAssets;
using UnityEngine;

public class VirtualInput : MonoBehaviour
{
    [Header("Output")]
    public StarterAssetsInputs StarterAssetsInputs;

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        StarterAssetsInputs.MoveInput(virtualMoveDirection);
    }

    public void VirtualCameraRotate(Vector2 virtualLookDirection)
    {
        StarterAssetsInputs.CameraRotateInput(virtualLookDirection);
    }

    public void VirtualJumpInput(bool virtualJumpState)
    {
        StarterAssetsInputs.JumpInput(virtualJumpState);
    }

    public void VirtualSprintInput(bool virtualSprintState)
    {
        StarterAssetsInputs.SprintInput(virtualSprintState);
    }
}
