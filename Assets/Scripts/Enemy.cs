using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject deathVFX;
    [Header("This is to tidy up explosion vfx in the hierarchy")]
    [SerializeField] private Transform parent;
    void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        //move vfx objects inside a parent folder to clear up the hierarchy 
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}