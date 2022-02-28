using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinNode<T>
{
    private BinNode<T> left;
    private BinNode<T> right;
    private T value;
    private GameObject MyGO;

    public BinNode(T value)
    {
        this.value = value;
        this.right = null;
        this.left = null;
    }

    public T GetValue()
    {
       return value;
    }
    public void SetValue(T value)
    {
        this.value = value;
    }

    public BinNode<T> GetLeft() 
    {
            return left;
    }
    public void SetLeft(BinNode<T> left)
    {
        this.left = left;
    }

    public BinNode<T> GetRight()
    {
        return right;
    }
    public void SetRight(BinNode<T> right)
    {
        this.right = right;
    }

    public bool HasRight()
    {
        return this.right != null;
    }
    public bool HasLeft()
    {
        return this.left != null;
    }
    public GameObject GetGO()
    {
        return MyGO;
    }
    public void SetGO(GameObject MyGO)
    {
        this.MyGO = MyGO;
    }

}
