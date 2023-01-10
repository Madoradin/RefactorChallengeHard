using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text collectablesCountText;

    public void UpdateCollectablesUI(int collectedCollectables, int targetCollectableCount)
    {
        collectablesCountText.text = $"{collectedCollectables}/{targetCollectableCount}";

    }
}
