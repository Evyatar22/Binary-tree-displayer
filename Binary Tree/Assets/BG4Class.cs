using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG4Class 
{
    private int low;
    private int high;

    public BG4Class(int low,int high)
    {
        this.low = low;
        this.high = high;
    }

    public int GetLow()
    {
        return low;
    }
    public void SetLow(int low)
    {
        this.low = low;
    }

    public int GetHigh()
    {
        return high;
    }
    public void SetHigh(int high)
    {
        this.high = high;
    }
}
