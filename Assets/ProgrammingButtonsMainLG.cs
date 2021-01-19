using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingButtonsMainLG : MonoBehaviour
{
    public Button itemPrefabAnd;
    public Button itemPrefabOr;
    public Button itemPrefabNand;
    public Button itemPrefabNor;

    bool isAnd = false;
    bool isOr = false;
    bool isNand = false;
    bool isNor = false;
    bool toDelete = false;

    public int columnCount;
    public int maxItems;

    [SerializeField] Button btnAnd;
    [SerializeField] Button btnOr;
    [SerializeField] Button btnNand;
    [SerializeField] Button btnNor;
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

        btnAnd.onClick.AddListener(BtnAndPressed);
        btnOr.onClick.AddListener(BtnOrPressed);
        btnNand.onClick.AddListener(BtnNandPressed);
        btnNor.onClick.AddListener(BtnNorPressed);
        btnDelete.onClick.AddListener(BtnDeletePressed);
    }

    void Update()
    {
        if (isAnd || isOr || isNand || isNor)
        {
            if (isAnd)
            {
                isAnd = false;
                Button newItem = Instantiate(itemPrefabAnd) as Button;
                AddButton(newItem);
            }
            else if (isOr)
            {
                isOr = false;
                Button newItem = Instantiate(itemPrefabOr) as Button;
                AddButton(newItem);
            }
            else if (isNand)
            {
                isNand = false;
                Button newItem = Instantiate(itemPrefabNand) as Button;
                AddButton(newItem);
            }
            else if (isNor)
            {
                isNor = false;
                Button newItem = Instantiate(itemPrefabNor) as Button;
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

    public void BtnAndPressed()
    {
        if (i < maxItems)
        {
            isAnd = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }
    }

    public void BtnOrPressed()
    {
        if (i < maxItems)
        {
            isOr = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }
    }

    public void BtnNandPressed()
    {
        if (i < maxItems)
        {
            isNand = true;
        }
        else
        {
            Debug.Log("No more moves are possible");
        }
    }

    public void BtnNorPressed()
    {
        if (i < maxItems)
        {
            isNor = true;
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

    public void AddButton(Button btn)
    {
        if (i % columnCount == 0)
            j++;

        btn.transform.parent = gameObject.transform;

        //move and size the new item
        RectTransform rectTransform = btn.GetComponent<RectTransform>();

        /* x = (float)(-containerRectTransform.rect.width / 1.225 + width * (i % columnCount));
         y = (float)(containerRectTransform.rect.height / 1.5 - height * j);*/
        x = (float)(-containerRectTransform.rect.width / 4.5 + width * (i % columnCount));
        y = (float)(containerRectTransform.rect.height / 3 - height * j);
        rectTransform.offsetMin = new Vector2(x, y);
        rectTransform.offsetMin = new Vector2((float)(x * 2), (float)(y * 1.5));

        x = rectTransform.offsetMin.x + width;
        y = rectTransform.offsetMin.y + height;
        rectTransform.offsetMax = new Vector2(x, y);

        i++;
    }
}
