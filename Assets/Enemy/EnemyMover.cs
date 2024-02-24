using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField][Range(0f, 5f)] float speed = 1f; // have Rrange to avoid having negative value

    List<Node> path = new List<Node>(); // access in inspector

    Enemy enemy;

    GridManager gridManager;
    Pathfinder pathfinder;


    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>(); // requireする        
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates = resetPath ? pathfinder.StartCoordinates : gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false); // reuse in pool
    }

    // Coroutine
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++) // next nodeから計算する
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPerent = 0f; // 0f == start, 1f == end

            transform.LookAt(endPosition); // 向きの調整

            while (travelPerent < 1f)
            {
                travelPerent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPerent); // Lerp to move the trans smoothly
                yield return new WaitForEndOfFrame();
            }
        }

        // reach the last tile
        FinishPath();
    }
}
