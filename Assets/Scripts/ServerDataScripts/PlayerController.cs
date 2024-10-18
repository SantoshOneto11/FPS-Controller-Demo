using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ServerPlayer serverPlayer;
    private CharacterController characterController;

    private float playerSpeed = 10.0f;
    private float movementThreshold = 0.01f;

    private Vector3 previousMovement = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 targetMovement = Vector3.zero;
    //private Vector3 jumpVector;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        float moveX = Input.GetAxis(StaticValues.Horizontal);
        float moveZ = Input.GetAxis(StaticValues.Vertical);

        targetMovement = new Vector3(moveX, 0f, moveZ) * playerSpeed;
        if (targetMovement != Vector3.zero)
        {
            gameObject.transform.forward = targetMovement;
        }

        playerVelocity = targetMovement;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Only send data to server if player has moved significantly
        if (Vector3.Distance(playerVelocity, previousMovement) > movementThreshold)
        {
            HasPlayerMoved(playerVelocity);
            previousMovement = playerVelocity;
        }
        else
        {
            playerVelocity = Vector3.zero;
        }
    }


    private void HasPlayerMoved(Vector3 movement)
    {
        float XMoved = movement.x != previousMovement.x ? movement.x : float.NaN;
        float YMoved = movement.y != previousMovement.y ? movement.y : float.NaN;
        float ZMoved = movement.z != previousMovement.z ? movement.z : float.NaN;

        // Send only the movement that has changed
        serverPlayer.OnPlayerMovement.Invoke(XMoved, YMoved, ZMoved);
        ChangeInBit(XMoved, YMoved, ZMoved, "Send Data ");
    }

    public int ChangeInBit(float value1, float value2, float value3, string msg)
    {
        int bitSize = 0;
        if (!float.IsNaN(value1))
        {
            bitSize = sizeof(float) * 8;
        }

        if (!float.IsNaN(value2))
        {
            bitSize += sizeof(float) * 8;
        }

        if (!float.IsNaN(value3))
        {
            bitSize += sizeof(float) * 8;
        }

        Debug.Log($"({value1}, {value2}, {value3})" + msg + bitSize);
        return bitSize;
    }
}
