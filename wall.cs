using UnityEngine;

public class RigidbodyTesting : MonoBehaviour
{
    public float moveSpeed = 0.25f;
    public float rotationRate = 15f;
    public Transform target;

    private Rigidbody _Rigidbody;

    // Vector3 to store player inputs.
    private Vector3 moveInput;

    private void Awake()
    {
        // To store the component Rigidbody in the property _Rigidbody
        // so that you can use it anywhere in your code.
        TryGetComponent(out _Rigidbody);
    }

    private void Update()
    {
        /* To add values to the moveInput property you write "new Vector3 (x, y, z)" and fill those
        values with the inputs that you would like to use. In my case I used for the X axis
        Input.GetAxis("Horizontal") to get the input values from the default
        keys A, D, Left Arrow and Right Arrow and for the Z axis Input.GetAxis("Vertical") to
        get the default input values from the keys W, S, Up Arrow and Down Arrow.
        */

        // Try to always get your player inputs in the Update method.
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // Rigidbody actions are handled by Unity's physics engine, so you should always mess with
        // rigidbody stuff inside FixedUpdate, this will guarantee consistent physics behaviour.

        // After this you just simply use your rigidbody position with a += moveInput (like you did) and multiply
        // that Vector3 by a property I called moveSpeed so that you can control how fast your object should move.
        _Rigidbody.position += moveInput * moveSpeed;

        // sqrMagnitude is a value that goes up when any of the buttons from moveInput are pressed,
        // I added this check here to make sure that our object stays on the last registered rotation
        // (just before the player released the buttons). If you want your object to return to a default
        // rotation, remove this if condition.
        if (moveInput.sqrMagnitude > 0)
        {
            // We create a Quaternion, the type of variable we use to represent rotations and
            // we use Quaternion.LookRotation to look at our moveInput vector which always points
            // towards the moving direction, and we say that we want to rotate the Vector3.up (Y axis).
            Quaternion rotation = Quaternion.LookRotation(moveInput, Vector3.up);

            // Then we pass that rotation to our Rigidbody rot using Quaternion.Lerp which is a method
            // to interpolate between two quaternions by a given time. In our case we use as the first
            // Quaternion the _Rigidbody.rotation and as a second Quaternion our previously calculated rotation,
            // then we add time by writing Time.fixedDeltaTime (fixed cuz we are in the method FixedUpdate)
            // and we multiply that by a rotationRate to make it go faster or slower.
            _Rigidbody.rotation = Quaternion.Lerp(_Rigidbody.rotation, rotation, Time.fixedDeltaTime * rotationRate);
        }
    }
}