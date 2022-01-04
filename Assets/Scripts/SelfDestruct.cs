using UnityEngine;

public class SelfDestruct : MonoBehaviour
{   
    [Header("Time until GameObject is destroyed")]
    [SerializeField] float timeToKill;
    //when a vfx is spawned, it will be deleted after timeToKill seconds
    //best used when attached to vfx prefabs
    private void Start()
    {
        Destroy(gameObject, timeToKill);
    }
}
