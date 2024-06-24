using TMPro;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [Header("Size")]
    private int startingSize = 2;
    [SerializeField] private MeshRenderer blockMesh;

    [Header("References")]
    [SerializeField] private GameObject completeBlock;
    [SerializeField] private GameObject brokenBlock;

    private void Awake()
    {
        completeBlock.SetActive(true);
        brokenBlock.SetActive(true);
    }

    public void CheckHit()
    {
        if (GameEvents.instance.playerSize.Value > startingSize)
        {
            GameEvents.instance.playerSize.Value -= startingSize;
            completeBlock.SetActive(false);
            brokenBlock.SetActive(false);
        }
        else
        {
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
        }
    }
}