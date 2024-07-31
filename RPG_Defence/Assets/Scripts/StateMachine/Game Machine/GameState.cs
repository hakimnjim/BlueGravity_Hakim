using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private Transform waveStartPos;
    [SerializeField] private List<CollectableStructPos> collectablePos;
    [SerializeField] private List<GameObject> collectables;
    [SerializeField] private GameObject NPC;
    private int minWave;
    private int maxWave;

    public override void Enter(GlobalStateMachine host)
    {
        base.Enter(host);
        minWave = 1;
        maxWave = 3;
        InvokeRepeating("SpawnWave", 3, 40);
        InvokeRepeating("SpawnCollectable", 5, 5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SpawnWave()
    {
        int waveCount = Random.Range(minWave, maxWave);
        minWave += 3;
        maxWave += 3;
        StartCoroutine(SpawnWaveCoroutine(waveCount));
    }

    private void SpawnCollectable()
    {
        int index = collectablePos.FindIndex(x => x.collectable == null);
        if (index != -1)
        {
            int random = Random.Range(0, collectables.Count);
            GameObject obj = Instantiate(collectables[random], collectablePos[index].collectablePoint.position, Quaternion.identity);
            collectablePos[index].collectable = obj;
        }
    }

    IEnumerator SpawnWaveCoroutine(int waveCount)
    {
        for (int i = 0; i < waveCount; i++)
        {
            Instantiate(NPC, waveStartPos.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}

[System.Serializable]
public class CollectableStructPos
{
    [HideInInspector]
    public GameObject collectable;
    public Transform collectablePoint;
}