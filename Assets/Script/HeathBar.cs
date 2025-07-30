using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    float maxHP;
    float hp;
    bool initCompalte;
    private Transform target;
    void Update()
    {
        if (initCompalte == false)
        {
            return;
        }
        if (imageFill == null)
        {
            Debug.Log("loi");
        }

        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHP, Time.deltaTime * 5f);
        transform.position = target.position + offset;

    }
    public void OnInit(float maxHP, Transform target)
    {
        this.target = target;
        this.maxHP = maxHP;
        hp = maxHP;
        imageFill.fillAmount = 1;
        if (imageFill == null)
        {

            imageFill = transform.GetChild(1).GetComponent<Image>();
            Debug.Log("Error");


        }
        initCompalte = true;
    }
    public void SetNewHP(float hp)
    {
        this.hp = hp;
    }
}
