using System;
using UnityEngine;

public class ServerPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private CharacterController characterController;

    private Vector3 lastKnownPosition = Vector3.zero;
    private Vector3 lastReceivedMovement = Vector3.zero;

    public Action<float, float, float> OnPlayerMovement;

    void Start()
    {
        OnPlayerMovement = MoveServerPlayer;
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // Continue moving using the last received movement vector
        if (lastReceivedMovement != Vector3.zero)
        {
            characterController.Move(lastReceivedMovement * Time.deltaTime);
        }
        lastKnownPosition = lastReceivedMovement;
    }

    void MoveServerPlayer(float X, float Y, float Z)
    {
        playerController.ChangeInBit(X, Y, Z, "ReceivedData ");
        if (float.IsNaN(X)) X = lastKnownPosition.x;
        if (float.IsNaN(Y)) Y = lastKnownPosition.y;
        if (float.IsNaN(Z)) Z = lastKnownPosition.z;

        Vector3 movement = new Vector3(X, Y, Z);

        if (X == 0 && Y == 0 && Z == 0)
        {
            lastReceivedMovement = Vector3.zero;
            return;
        }
        lastReceivedMovement = movement;
    }   
}
