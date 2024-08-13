using UnityEngine;
using UnityEngine.InputSystem;

public class RideManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ride;
    [SerializeField]
    private Transform playerSeat;
    [SerializeField]
    private GameObject player;

    private bool onRide;

    void Update()
    {
        if(!onRide) //Will make sure player is not on ride already
        {
            float dist = Vector3.Distance(player.transform.position, ride.transform.position); //If player is near ride they can get on
            if (Keyboard.current.eKey.wasPressedThisFrame && dist < 2)
            {                
                StartRide();
            }
        }     
    }

    private void StartRide()
    {
        onRide = true;

        var characterController = player.GetComponent<CharacterController>(); //Will get the character controller to turn off
        characterController.enabled = false;

        player.transform.SetParent(ride.transform); //Set players parent and transform
        player.transform.position = playerSeat.position;

        ride.GetComponent<RideAi>().StartRide();
    }
}
