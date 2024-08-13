using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    private List<Targets> targetList = new List<Targets>();
    void Start()
    {
        targetList = FindObjectsOfType<Targets>().ToList(); //Gets all targets to list and spawns them
        SpawnTargets();
    }

    private void SpawnTargets()
    {
        for(int i = 0; i < targetList.Count; i++)
        {
            targetList[i].SpawnTarget();
        }
    }

    public void TargetGotHit(GameObject target)
    {
        for(int i = 0; i < targetList.Count; i++)
        {
            if(GameObject.ReferenceEquals(target, targetList[i].target)) //Will find the correct target and starts the respawn process
            {
                targetList[i].RespawnTarget();
                gameManager.UpdateScore(targetList[i].Score);
                break;
            }
        }
    }
}
