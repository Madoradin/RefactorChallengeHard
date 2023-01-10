using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using LongClass;

public class CollectableController : MonoBehaviour
{
    [SerializeField] private int targetCollectableCount;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private Transform collectableSpawnTopRightHandle;
    [SerializeField] private Transform collectableSpawnBottomLeftHandle;

    [SerializeField] private UpdateUI updateUI;

    [SerializeField] private float minCollectableSpawnTime = 3;
    [SerializeField] private float maxCollectableSpawnTime = 6;
    private int collectedCollectables;
    private Coroutine spawnCoroutine;
    

    public void Collect(GameObject collectable)
    {
        Destroy(collectable);
        collectedCollectables++;
        updateUI.UpdateCollectablesUI(collectedCollectables, targetCollectableCount);

        if (collectedCollectables == targetCollectableCount)
        {
            StopCoroutine(spawnCoroutine);
            PlayerController.Instance.enabled = false;
        }
    }

    public void CollectableCoroutine()
    {
        spawnCoroutine = StartCoroutine(SpawnCollectable());
    }

    private IEnumerator SpawnCollectable()
    {
        while (true)
        {
            var topRight = collectableSpawnTopRightHandle.position;
            var bottomLeft = collectableSpawnBottomLeftHandle.position;
            var position = new Vector3(
                Random.Range(bottomLeft.x, topRight.x),
                topRight.y,
                Random.Range(bottomLeft.z, topRight.z));
            Instantiate(collectablePrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(minCollectableSpawnTime, maxCollectableSpawnTime));
        }
    }

    public void UpdateUI()
    {
        updateUI.UpdateCollectablesUI(collectedCollectables, targetCollectableCount);
    }
}
