using UnityEngine;
public class Fruit : MonoBehaviour {
    private GameObject fruitWhole;
    private GameObject fruitSliced;

    private Rigidbody fruitBody;
    private Collider fruitCollider; 
    private ParticleSystem sliceEffectParticleSystem;

    private void Awake() {
        fruitWhole = transform.GetChild(0).gameObject;
        fruitSliced = transform.GetChild(1).gameObject;
        fruitCollider = GetComponent<Collider>();
        fruitBody = GetComponent<Rigidbody>();
        sliceEffectParticleSystem = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force){
        fruitCollider.enabled = false;

        //sliced pieces box collider should be removed
        // BoxCollider[] sliceColliders = fruitSliced.GetComponentsInChildren<BoxCollider>();
        // foreach(BoxCollider sliceCollider in sliceColliders){
        //     sliceCollider.enabled = false;
        // }

        // particle effect should be active
        sliceEffectParticleSystem.Play();

        fruitWhole.SetActive(false);
        fruitSliced.SetActive(true);

        //rotate the sliced fruit according to the angle of the blade
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fruitSliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = fruitSliced.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody slice in slices){
            slice.velocity = fruitBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

        FindObjectOfType<GameManager>().IncreaseScore();
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Player")){
            Blade blade = other.GetComponent<Blade>();
            //force is given as 5f, but should be calculated by the velocity of blade
            Slice(blade.direction, blade.transform.position, 5f);
        }
    }


}