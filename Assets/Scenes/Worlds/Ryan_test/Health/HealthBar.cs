using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using static Unity.VisualScripting.Member;
using UnityStandardAssets.ImageEffects;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    Health pl_h;
    Camera cam;
    public Sprite hi;


    void Start()
    {
        pl_h = GameObject.Find("Player").GetComponent<Health>();
        cam = gameObject.GetComponentInChildren<Camera>();

        ReloadHealthBar();
    }


    public void ReloadHealthBar() 
    {
        GameObject hs = GameObject.Find("heartStore");
        if (hs != null)
        {
            Destroy(hs);
        }

        GameObject heartStore = new GameObject("heartStore");
        heartStore.transform.SetParent(cam.transform);
        for (int i = 0; i < pl_h.currHealth; i++)
        {
            Canvas h_cv = new GameObject("Heart Disp").AddComponent<Canvas>();
            h_cv.transform.SetParent(heartStore.transform);
            h_cv.renderMode = RenderMode.ScreenSpaceOverlay;

            Image img = new GameObject("Heart").AddComponent<Image>();
            img.transform.SetParent(h_cv.transform);
            img.sprite = hi;
            img.rectTransform.anchorMin = new Vector2(0, 1);
            img.rectTransform.anchorMax = new Vector2(0, 1);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 24);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 32);
            img.rectTransform.anchoredPosition = new Vector3(44 + (i * 30), -44, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
