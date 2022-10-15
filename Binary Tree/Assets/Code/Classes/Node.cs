using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T> 
{
    private Node<T> next;
    private T value;
    private GameObject MyGO;

    public Node<T> Parent { get; }

    public Node(T value, Node<T> parent = null)
    {
        this.value = value;
        this.next = null;
        MyGO = null;
    }

    public T GetValue()
    {
        return value;
    }
    public void SetValue(T value)
    {
        this.value = value;
    }

    public Node<T> GetNext()
    {
        return next;
    }
    public void SetNext(Node<T> left)
    {
        this.next = left;
    }

    public bool HasNext()
    {
        return this.next != null;
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
