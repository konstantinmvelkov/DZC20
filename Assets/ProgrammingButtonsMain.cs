using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingButtonsMain : MonoBehaviour
{
    public Button itemPrefabUp;
    public Button itemPrefabLeft;
    public Button itemPrefabRight;

    bool isUp = false;
    bool isLeft = false;
    bool isRight = false;
    bool toDelete = false;

    public int columnCount;
    int maxItems = 36;

    [SerializeField] Button btnUp;
    [SerializeField] Button btnLeft;
    [SerializeField] Button btnRight;
    [SerializeField] Button btnDelete;

    RectTransform containerRectTransform;

    float width = 110;
    float height = 110;
    float x;
    float y;

    int j = 0;
    int i = 0;

    void Start()
    {
        containerRectTransform = gameObject.GetComponent<RectTransform>();

        btnUp.onClick.AddListener(BtnUpPressed);
        btnLeft.onClick.AddListener(BtnLeftPressed);
        btnRight.onClick.AddListener(BtnRightPressed);
        btnDelete.onClick.AddListener(BtnDeletePressed);
    }

    void Update()
    {
        if (isUp || isLeft || isRight)
        {
            if (isUp)
            {
                isUp = false;
                Button newItem = Instantiate(itemPrefabUp) as Button;
                AddButton(newItem);
            }
            else if (isLeft)
            {
                isLeft = false;
                Button newItem = Instantiate(itemPrefabLeft) as Button;
                AddButton(newItem);
            }
            else if (isRight)
            {
                isRight = false;
                Button newItem = Instantiate(itemPrefabRight) as Button;
                AddButton(newItem);
            }
        }

        if (toDelete)
        {
            toDelete = false;
            i = 0;
            j = 0;
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void BtnUpPressed()
    {
        if (i < maxItems)
        {
            isUp = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }
    }

    public void BtnLeftPressed()
    {
        if (i < maxItems)
        {
            isLeft = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }       
    }

    public void BtnRightPressed()
    {
        if (i < maxItems)
        {
            isRight = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }
    }

    public void BtnDeletePressed()
    {
        toDelete = true;
    }

    public void AddButton (Button btn)
    {
        if (i % columnCount == 0)
            j++;

        btn.transform.parent = gameObject.transform;

        //move and size the new item
        RectTransform rectTransform = btn.GetComponent<RectTransform>();

        x = (float)(-containerRectTransform.rect.width / 1.225 + width * (i % columnCount));
        y = (float)(containerRectTransform.rect.height / 1.5 - height * j);
        rectTransform.offsetMin = new Vector2(x, y);
        rectTransform.offsetMin = new Vector2((float)(x / 1.5), (float)(y / 1.5));

        x = rectTransform.offsetMin.x + width;
        y = rectTransform.offsetMin.y + height;
        rectTransform.offsetMax = new Vector2(x, y);

        i++;
    }
}
