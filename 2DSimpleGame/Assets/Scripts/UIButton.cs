using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    public Color color = Color.cyan;

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }

    public void OnMouseOver()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if  (sprite != null) {
            GetComponent<SpriteRenderer>().color = color;
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
