using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private Transform waveStartPos;
    [SerializeField] private List<Transform> collectablePos;
    [SerializeField] private GameObject NPC;
    private int minWave;
    private int maxWave;

    public override void Enter(GlobalStateMachine host)
    {
        base.Enter(host);
        minWave = 1;
        maxWave = 3;
        InvokeRepeating("SpawnWave", 3, 50);
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
