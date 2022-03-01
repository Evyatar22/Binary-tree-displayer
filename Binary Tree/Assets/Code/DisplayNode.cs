using System;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DisplayNode : MonoBehaviour
{
    [SerializeField] private Circle CirclePrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform ConnetorsHoldef;

    private BinNode<int> rootNode;
    private TMP_Text[] Rangetxt = null;

    public int levels = 3;
    public float spacing = 55;

    private void Start()
    {
        rootNode = new BinNode<int>(Random.Range(1, 20));

        //BinNode<BG4Class> RangeTree = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));

        //CrateRndBG4Class(3,RangeTree);

        CreateRndTree(levels, rootNode);

        Display(rootNode);

        print(IsMana(rootNode));

        // if (!IsMana(root))
        // {
        //     SceneManager.LoadScene(0);
        // }
    }

    private void Display(BinNode<int> first)
    {
        GenerateTreeUI(first, false);
        ConnetorsHoldef.anchoredPosition = new Vector2(47.2f, 54.7f);
    }

    #region uiFuncs

    private void GenerateTreeUI(BinNode<int> node, bool right)
    {
        var cir = Instantiate(CirclePrefab, Vector2.zero, Quaternion.identity);
        node.SetGO(cir);

        cir.transform.SetParent(canvas.transform);
        cir.Text.text = node.Value.ToString();

        if (node.GetLevel() == 0) // => root
        {
            rootNode = node;
            rootNode.Circle.RectTransform.anchoredPosition = new Vector2(-11, 213);

            cir.Image.color = Color.black;
            cir.Text.color = Color.white;
        }
        else
        {
            cir.RectTransform.anchoredPosition = GetNodePosition(node, right);
        }

        if (node.HasLeft)
        {
            GenerateTreeUI(node.Left, false);
            GenerateWire(cir.RectTransform.anchoredPosition, GetNodePosition(node.Left, false));
        }

        if (node.HasRight)
        {
            GenerateTreeUI(node.Right, true);
            GenerateWire(cir.RectTransform.anchoredPosition, GetNodePosition(node.Right, true));
        }
    }

    private void GenerateWire(Vector2 nodeA, Vector2 nodeB)
    {
        var connector = new GameObject("connector");

        connector.transform.SetParent(ConnetorsHoldef);

        var connectorRT = connector.AddComponent<RectTransform>();
        connectorRT.anchorMin = new Vector2(0, 0);
        connectorRT.anchorMax = new Vector2(0, 0);

        connector.AddComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);

        var dir = (nodeB - nodeA).normalized;
        var distance = Vector2.Distance(nodeA, nodeB);

        connectorRT.sizeDelta = new Vector2(distance, 3f);
        connectorRT.anchoredPosition = nodeA + dir * distance * .5f;

        connectorRT.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private static Vector2 GetNodePosition(BinNode<int> node, bool right)
    {
        var subLevels = node.GetChildLevels();

        var parentRectTransform = node.Parent.Circle.RectTransform;

        return parentRectTransform.anchoredPosition + new Vector2(55 * (Mathf.Pow(2, subLevels)) * (right ? 1 : -1), -55);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private static void CreateRndTree(int levels, BinNode<int> rootNode)
    {
        if (levels < 0)
        {
            return;
        }

        var leftNode = new BinNode<int>(Random.Range(1, 20), rootNode);
        var rightNode = new BinNode<int>(Random.Range(1, 20), rootNode);

        rootNode.SetChildren(leftNode, rightNode);

        CreateRndTree(levels - 1, leftNode);
        CreateRndTree(levels - 1, rightNode);
    }

    public void CrateRndBG4Class(int levels, BinNode<BG4Class> save_node)
    {
        levels--;

        if (levels < 0)
        {
            save_node.SetChildren(null, null);
            return;
        }

        var left = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));
        var right = new BinNode<BG4Class>(new BG4Class(Random.Range(0, 10), Random.Range(10, 20)));
        save_node.SetChildren(left, right);

        CrateRndBG4Class(levels, left);
        CrateRndBG4Class(levels, right);
    }

    #endregion

    #region BG1

    public bool BG1_Upper(BinNode<int> t)
    {
        if (t == null) { return true; }

        var l = true;
        if (t.Left != null)
        {
            if (t.Value < t.Left.Value)
            {
                l = BG1_Upper(t.Left);
            }
            else
            {
                return false;
            }
        }

        if (t.Right != null)
        {
            if (t.Value < t.Right.Value)
            {
                return BG1_Upper(t.Right) && l;
            }

            return false;
        }

        return l;
    }

    #endregion

    #region BG2

    public bool BG2_1(BinNode<int> t, int x)
    {
        if (t != null)
        {
            if (t.Value == x) { return true; }

            var r = BG2_1(t.Right, x);
            var l = BG2_1(t.Left, x);
            if (r || l) { return true; }
        }

        return false;
    }

    public Node<int> BG2_2(BinNode<int> l1, BinNode<int> l2)
    {
        var first = new Node<int>(-1);
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
        var x2 = list;
        var x = list.GetNext();
        var count = 0;
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
            var x1 = new Node<int>(t.Value);
            list.SetNext(x1);
            list = x1;
            BG2_Help1(t.Right, list);
            BG2_Help1(t.Left, list);
        }
    }

    #endregion

    #region BG3

    public bool BG3_1_treeLessThen(BinNode<int> t, int x)
    {
        if (BG3_Help(t, x) == BG3_helpCount(t))
        {
            return true;
        }

        return false;
    }

    public int BG3_Help(BinNode<int> t, int x)
    {
        if (t != null)
        {
            if (x < t.Value)
            {
                return BG3_Help(t.Left, x) + 1 + BG3_Help(t.Right, x) + 1;
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
        if (t != null)
        {
            return BG3_helpCount(t.Right) + 1 + BG3_helpCount(t.Left) + 1;
        }

        return 0;
    }

    #endregion

    #region BG4

    public bool BG4_1(BinNode<BG4Class> t, BinNode<BG4Class> OldT, bool Left)
    {
        if (t == null) { return true; }

        if (OldT.Value.GetLow() == t.Value.GetLow() && Left &&
            OldT.Value.GetHigh() > t.Value.GetHigh())
        {
            return BG4_1(t.Left, t, Left);
        }

        if (OldT.Value.GetLow() < t.Value.GetLow() && !Left &&
            OldT.Value.GetHigh() <= t.Value.GetHigh())
        {
            return BG4_1(t.Right, t, !Left);
        }

        return false;
    }

    #endregion

    #region BG4_MoadB

    public bool BG4_3MoadB(BinNode<int> node)
    {
        var arr = new int[3];
        BG4_2MoadB(node, arr);
        return arr[0] == arr[1] && arr[2] == arr[1];
    }

    public void BG4_2MoadB(BinNode<int> node, int[] arr)
    {
        if (node != null)
        {
            arr[node.Value % 3]++;
            BG4_2MoadB(node.Right, arr);
        }
    }

    #endregion

    #region shalatKita

    public bool IsMana(BinNode<int> node)
    {
        if (node == null)
        {
            return true;
        }

        if (node.Left.Value / node.Right.Value == node.Value)
        {
            if (node.Left.Value % node.Right.Value == 0)
            {
                return IsMana(node.Left) && IsMana(node.Right);
            }

            return false;
        }

        return false;
    }

    public void TryAdd(BinNode<int> t, int num1, int num2)
    {
        if (t != null)
        {
            if (t.Left.Value / t.Right.Value == t.Value)
            {
                if (t.Left.Value % t.Right.Value == 0) { }
            }
        }
    }

    #endregion
}
