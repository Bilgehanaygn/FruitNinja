using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private bool slicing;
    private TrailRenderer bladeTrail;
    public Vector3 direction {get; private set;}

    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();   
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    

    // private void OnEnable(){
    //     StartSlicing();
    // }
    // private void OnDisable() {
    //     StopSlicing();    
    // }

    private void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if(slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {   
        //initial position of the blade
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        //update the position of bladeCollider
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        //check if the slicing is valid
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

}
