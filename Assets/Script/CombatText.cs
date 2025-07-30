using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] protected Text hpText;
    public void OnInit(float damage)
    {
        hpText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    public void OnCombatTextFinished()
    {
        // Ẩn hoặc xoá CombatText sau animation
        gameObject.SetActive(false);
    }
}
