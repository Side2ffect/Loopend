using UnityEngine;
using Target;
using System.Collections;

public class Targets : MonoBehaviour, ITargetSpawnPoint
{
    [SerializeField]
    private int _score;
    [SerializeField]
    private TargetSize _size;
    [SerializeField]
    private float _respawnTime;

    [SerializeField]
    private Vector3 rotation;

    [HideInInspector]
    public GameObject target;

    [SerializeField]
    private bool isMoveable;

    private Animator anim;
    [SerializeField]
    private float animatorSpeed;

    public int Score { get; set; }

    public TargetSize Size { get; set; }

    public float RespawnTime { get; set; }

    public void Start() //Sets all information
    {
        Score = _score;
        Size = _size;
        RespawnTime = _respawnTime;
    }
    
    public void SpawnTarget()
    {
        target = (GameObject)Instantiate(Resources.Load(_size.ToString()));
        target.transform.parent = transform;
        target.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(rotation));

        StartMovingAnimation();
    }

    private void StartMovingAnimation()
    {
        if(isMoveable)
        {
            anim = target.GetComponent<Animator>();
            anim.speed = animatorSpeed; 
            anim.Play(_size.ToString());
        }
    }

    public void RespawnTarget()
    {
        StartCoroutine(StartRespawning());
    }

    IEnumerator StartRespawning()
    {
        target.SetActive(false);
        yield return new WaitForSeconds(RespawnTime);
        target.SetActive(true);

        if(isMoveable)
        {
            anim.Play(_size.ToString());
        }      
    }
}
