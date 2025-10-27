using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private Transform playerVisualTransform;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed;


    private void Update() 
    {
        Vector3 viewDirecrtion = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);

        orientationTransform.forward = viewDirecrtion.normalized;

        float horInp = Input.GetAxisRaw("Horizontal");
        float verInp = Input.GetAxisRaw("Vertical");


        Vector3 inputDirection = orientationTransform.forward * verInp + orientationTransform.right * horInp;

        if(inputDirection != Vector3.zero)
        {
            playerVisualTransform.forward 
            = Vector3.Slerp(playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
        
    }
}
