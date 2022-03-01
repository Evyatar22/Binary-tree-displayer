using UnityEngine;

public class BinNode<T>
{
    public T Value { get; }
    public BinNode<T> Parent { get; }
    public BinNode<T> Left { get; private set; }
    public BinNode<T> Right { get; private set; }
    public Circle Circle { get; private set; }
    public bool HasRight => Right != null;
    public bool HasLeft => Left != null;

    public BinNode(T value, BinNode<T> parent = null)
    {
        this.Value = value;
        this.Parent = parent;
    }

    public int GetLevel()
    {
        if (Parent == null)
        {
            return 0;
        }

        return 1 + Parent.GetLevel();
    }

    public int GetChildLevels()
    {
        var rightChildLevels = Right == null ? 0 : 1 + Right.GetChildLevels();
        var leftChildLevels = Left == null ? 0 : 1 + Left.GetChildLevels();

        return Mathf.Max(rightChildLevels, leftChildLevels);
    }

    public void SetChildren(BinNode<T> left, BinNode<T> right)
    {
        Left = left;
        Right = right;
    }

    public void SetGO(Circle circle)
    {
        Circle = circle;
    }
}
