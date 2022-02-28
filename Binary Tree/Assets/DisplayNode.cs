using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayNode : MonoBehaviour
{
    private TMP_Text[] Rangetxt = null;


    private TMP_Text Value = null;
    [SerializeField] private GameObject CirclePrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ConnetorsHoldef = null;
    private RectTransform roottr;
    private GameObject cir;

    private void Start()
    {

        BinNode<int> first;
        first = new BinNode<int>(Random.Range(1, 20));

        //BinNode<BG4Class> RangeTree = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));

        //CrateRndBG4Class(3,RangeTree);

        CrateRndTree(1, first);

        Display(first);

        print(IsMana(first));
        if (!IsMana(first))
        {
            SceneManager.LoadScene(0);
        }

    }
    public void Display(BinNode<int> first)
    {
        GenrateTreeUI(first, false, 0);
        ConnetorsHoldef.GetComponent<RectTransform>().anchoredPosition = new Vector2(47.2f, 54.7f);
    }

    #region uiFuncs
    public void GenrateTreeUI(BinNode<int> t, bool right, int Level)
    {

        if (Level == 0)
        {
            cir = Instantiate(CirclePrefab, new Vector2(0, 0), Quaternion.identity);
            t.SetGO(cir);
            cir.transform.SetParent(canvas.transform);
            cir.GetComponent<Image>().color = Color.black;
            roottr = cir.GetComponent<RectTransform>();
            Value = cir.GetComponentInChildren<TMP_Text>();


            roottr.anchoredPosition = new Vector2(-11, 213);
            Value.text = t.GetValue().ToString();
            Value.color = Color.white;
        }
        else
        {
            if (!right)
            {
                cir = Instantiate(CirclePrefab, new Vector2(0, 0), Quaternion.identity);
                cir.transform.SetParent(canvas.transform);
                t.SetGO(cir);
                cir.GetComponentInChildren<TMP_Text>().text = t.GetValue().ToString();
                cir.GetComponent<RectTransform>().anchoredPosition = new Vector2(roottr.anchoredPosition.x - Level * 55,
                    roottr.anchoredPosition.y - (Level * 55));
            }
            else
            {
                cir = Instantiate(CirclePrefab, new Vector2(0, 0), Quaternion.identity);
                cir.transform.SetParent(canvas.transform);
                t.SetGO(cir);
                cir.GetComponentInChildren<TMP_Text>().text = t.GetValue().ToString();
                cir.GetComponent<RectTransform>().anchoredPosition = new Vector2(roottr.anchoredPosition.x + Level * 55,
                    roottr.anchoredPosition.y - Level * 55);
            }
        }


        if (t.HasLeft())
        {

            GenrateTreeUI(t.GetLeft(), false, Level + 1);
            GenrateWire(cir.GetComponent<RectTransform>().anchoredPosition, GetFutreNode(t.GetLeft(), Level, false));

        }
        if (t.HasRight())
        {

            GenrateTreeUI(t.GetRight(), true, Level + 1);
            GenrateWire(cir.GetComponent<RectTransform>().anchoredPosition, GetFutreNode(t.GetRight(), Level, true));

        }

    }

    public void GenrateWire(Vector2 NodeA, Vector2 NodeB)
    {
        GameObject Connector = new GameObject("connector", typeof(Image));

        Connector.transform.SetParent(ConnetorsHoldef.transform);

        RectTransform ConnectorRT = Connector.GetComponent<RectTransform>();
        ConnectorRT.anchorMin = new Vector2(0,0);
        ConnectorRT.anchorMax = new Vector2(0,0);

        Connector.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);

        Vector2 dir = (NodeB - NodeA).normalized;
        float distance = Vector2.Distance(NodeA, NodeB);

        ConnectorRT.sizeDelta = new Vector2(distance, 3f);
        ConnectorRT.anchoredPosition = NodeA + dir * distance * .5f;

        ConnectorRT.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    public Vector2 GetFutreNode(BinNode<int> next, int level, bool right)
    {
        Vector2 NodeB;
        if (right)
        {
            NodeB = new Vector2(roottr.anchoredPosition.x + level * 55, roottr.anchoredPosition.y - level * 55);
        }
        else
        {
            NodeB = new Vector2(roottr.anchoredPosition.x - level * 55, roottr.anchoredPosition.y - level * 55);
        }
        return NodeB;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void CrateRndTree(int levels, BinNode<int> save_node)
    {
        BinNode<int> newNode = null;
        for (int i = 0; i < levels; i++)
        {
            newNode = new BinNode<int>(Random.Range(1, 20));
            save_node.SetLeft(newNode);
            newNode = new BinNode<int>(Random.Range(1, 20));
            save_node.SetRight(newNode);
            save_node = newNode;
        }
        
    }

    public void CrateRndBG4Class(int levels, BinNode<BG4Class> save_node)
    {
        BinNode<BG4Class> newNode;
        for (int i = 0; i < levels; i++)
        {
            newNode = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));
            save_node.SetLeft(newNode);
            newNode = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));
            save_node.SetRight(newNode);
            save_node = newNode;
        }
        newNode = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));

    }

    
    #endregion
    #region BG1
    public bool BG1_Upper(BinNode<int> t)
    {

        if(t == null) { return true; }
        bool l = true;
        if(t.GetLeft() != null)
        {
            if (t.GetValue() < t.GetLeft().GetValue())
            {
                l = BG1_Upper(t.GetLeft());
            }
            else
            {
                return false;
            }
        }
        if(t.GetRight() != null)
        {
            if (t.GetValue() < t.GetRight().GetValue())
            {
                return BG1_Upper(t.GetRight()) && l  == true;
            }
            else
            {
                return false;
            }
        }
      
        return l;
    }
    #endregion
    #region BG2
    public bool BG2_1(BinNode<int> t, int x)
    {
        if (t != null)
        {
            if (t.GetValue() == x) { return true; }
            bool r = BG2_1(t.GetRight(), x);
            bool l = BG2_1(t.GetLeft(), x);
            if (r || l) { return true; }
        }
        return false;
    }
    public Node<int> BG2_2(BinNode<int> l1, BinNode<int> l2)
    {
        Node<int> first = new Node<int>(-1);
        first = BG2_3(l1, l2, first);
        if (BG2_1(l2, first.GetValue()))
        {
            first = first.GetNext();
        }
        return first;
    }

    public Node<int> BG2_3(BinNode<int> l1, BinNode<int> l2, Node<int> list)
    {
        BG2_Help1(l1, list);
        Node<int> x, x2;
        x2 = list;
        x = list.GetNext();
        int count = 0;
        while (x.HasNext())
        {
            if (count == 0)
            {
                if (BG2_1(l2, x2.GetValue()))
                {
                    x2.SetNext(null);
                }
            }
            else if (BG2_1(l2, x.GetValue()))
            {

                x2.SetNext(x.GetNext());
                x.SetNext(null);

            }
            x2 = x;
            x = x.GetNext();
            count++;

        }
        return list;
    }
    public void BG2_Help1(BinNode<int> t, Node<int> list)
    {
        if (t != null)
        {
            Node<int> x1 = new Node<int>(t.GetValue());
            list.SetNext(x1);
            list = x1;
            BG2_Help1(t.GetRight(), list);
            BG2_Help1(t.GetLeft(), list);
        }
    }
    #endregion
    #region BG3

    public bool BG3_1_treeLessThen(BinNode<int> t, int x)
    {
        if(BG3_Help(t, x) == BG3_helpCount(t))
        {

            return true;
        }
        else { return false; }
    }

    public int BG3_Help(BinNode<int> t, int x)
    {
        
        if(t != null)
        {
            if(x < t.GetValue())
            {
                return BG3_Help(t.GetLeft(), x) + 1 + BG3_Help(t.GetRight(),x) + 1;
            }
          
        }
        else
        {
            return 0;
        }
        return 0;
    }
    public int BG3_helpCount(BinNode<int> t)
    {
        if(t != null)
        {
            return BG3_helpCount(t.GetRight()) + 1 + BG3_helpCount(t.GetLeft()) + 1;
        }
        else
        {
            return 0;
        }
    }
    #endregion
    #region BG4
    
    public bool BG4_1(BinNode<BG4Class> t, BinNode<BG4Class> OldT,bool Left)
    {
        if(t == null) { return true; }
        if(OldT.GetValue().GetLow() == t.GetValue().GetLow() && Left &&
            OldT.GetValue().GetHigh() > t.GetValue().GetHigh())
        {
            return BG4_1(t.GetLeft(),t,Left);
        }
        else if(OldT.GetValue().GetLow() < t.GetValue().GetLow() && !Left &&
            OldT.GetValue().GetHigh() <= t.GetValue().GetHigh())
        {
            return BG4_1(t.GetRight(), t, !Left);
        }
        else
        {
            return false;
        }
    }

    #endregion
    #region BG4_MoadB

    public bool BG4_3MoadB(BinNode<int> t)
    {
        int[] arr = new int[3];
        arr = BG4_2MoadB(t, arr);
        if(arr[0] == arr[1] && arr[2] == arr[1]) { return true; }
        else { return false; }
    }
    public int[] BG4_2MoadB(BinNode<int> t,int[] arr)
    {
        if(t != null)
        {
            arr[t.GetValue() % 3]++;
            arr = BG4_2MoadB(t.GetRight(),arr);
            return BG4_2MoadB(t.GetLeft(), arr);
        }
        else { return arr; }
    }
    #endregion
    #region shalatKita

    public bool IsMana(BinNode<int> t)
    {
        if (t == null)
        {
            return true;
        }
        else
        {
            if(t.GetLeft().GetValue()/t.GetRight().GetValue() == t.GetValue())
            {
                if(t.GetLeft().GetValue() % t.GetRight().GetValue() == 0)
                {
                    return IsMana(t.GetLeft()) && IsMana(t.GetRight());
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public void TryAdd(BinNode<int> t,int num1,int num2)
    {
        if (t != null)
        {
            if (t.GetLeft().GetValue() / t.GetRight().GetValue() == t.GetValue())
            {
                if (t.GetLeft().GetValue() % t.GetRight().GetValue() == 0)
                {
                     
                }

            }
            
        }
       
    }
    #endregion


}
