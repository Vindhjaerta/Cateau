using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour {

    public GameObject rightMessage;
    public GameObject leftMessage;
    public GameObject messagesParent;
    [Range(1,15)]
    public byte maxMessages = 1;

    private float height;

    private void Awake()
    {
        height = ((RectTransform)rightMessage.transform).rect.height;
    }

    public void ClearMessages()
    {
        for (int i = 0; i < messagesParent.transform.childCount; i++)
        {
            Destroy(messagesParent.transform.GetChild(i).gameObject);
        }
    }

    public void AddMessage(bool leftSide)
    {
        GameObject newMessage = Instantiate(leftSide ? leftMessage : rightMessage, messagesParent.transform);
        newMessage.transform.localPosition = new Vector3(newMessage.transform.localPosition.x, height * (messagesParent.transform.childCount - 1) * -1, newMessage.transform.localPosition.z);
        if(messagesParent.transform.childCount > maxMessages)
        {
            for (int i = 0; i < messagesParent.transform.childCount; i++)
            {
                messagesParent.transform.GetChild(i).localPosition = new Vector3(newMessage.transform.localPosition.x, messagesParent.transform.GetChild(i).localPosition.y + height, newMessage.transform.localPosition.z);
            }
            Destroy(messagesParent.transform.GetChild(0).gameObject);

        }
    }
}
