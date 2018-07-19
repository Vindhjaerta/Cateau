using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour {

    public GameObject message;
    public GameObject messagesParent;
    [Range(1,15)]
    public byte maxMessages = 1;

    public void ClearMessages()
    {
        for (int i = 0; i < messagesParent.transform.childCount; i++)
        {
            DestroyImmediate(messagesParent.transform.GetChild(i).gameObject);
        }
    }

    public void AddMessage(Sprite sprite)
    {
        GameObject obj = Instantiate(message, messagesParent.transform);
        UnityEngine.UI.Image img = obj.GetComponent<UnityEngine.UI.Image>();
        if(img != null)
        {
            img.sprite = sprite;
        }

        if (messagesParent.transform.childCount > maxMessages)
        {
            DestroyImmediate(messagesParent.transform.GetChild(0).gameObject);
        }

        float height = 0;

        for (int i = 0; i < messagesParent.transform.childCount; i++)
        {
            messagesParent.transform.GetChild(i).localPosition = new Vector3(0, 0-height, 0);
            height += messagesParent.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().rectTransform.rect.height;
        }


    }
}
