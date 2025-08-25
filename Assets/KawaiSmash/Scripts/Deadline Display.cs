using System.Collections;
using UnityEngine;

public class DeadlineDisplay : MonoBehaviour
{
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    private Coroutine checkCoroutine;

    private void Awake()
    {
        GameManager.onGameStatedChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStatedChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            StartCheckingForNearbyFruits();
        }
        else
        {
            StopCheckingForNearbyFruits();
        }
    }

    private void StartCheckingForNearbyFruits()
    {
        if (checkCoroutine == null)
            checkCoroutine = StartCoroutine(CheckforNearbyFruitsCoroutine());
    }

    private void StopCheckingForNearbyFruits()
    {
        HideDeadLine();
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
            checkCoroutine = null;
        }
    }

    IEnumerator CheckforNearbyFruitsCoroutine()
    {
        while (true)
        {
            bool foundNearbyFruit = false;

            for (int i = 0; i < fruitsParent.childCount; i++)
            {
                if (!fruitsParent.GetChild(i).GetComponent<Fruit>().HasCollided() )
                {
                    continue;
                }
                float distance = Mathf.Abs(fruitsParent.GetChild(i).position.y - 
                                           deadLine.transform.position.y);

                if (distance <= 0.2f)
                {
                    ShowDeadLine();
                    foundNearbyFruit = true;
                    break;
                }
            }

            if (!foundNearbyFruit)
                HideDeadLine();

            yield return new WaitForSeconds(1f);
        }
    }

    private void ShowDeadLine() => deadLine.SetActive(true);

    private void HideDeadLine() => deadLine.SetActive(false);
}
