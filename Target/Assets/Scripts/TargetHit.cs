using UnityEngine;
using Target;

public class TargetHit : MonoBehaviour, ITarget
{
    [SerializeField]
    private GameObject parent;
    private TargetManager targetManager;
    void Start()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            Hit();
        }
    }
    public void Hit() //Will let manager know they got hit
    {
        targetManager.TargetGotHit(parent);
    }
}
