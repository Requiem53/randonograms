using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DimensionsControl : MonoBehaviour
{
    public string xInput;
    public string yInput;

    public void ReadStringX(string x)
    {
        xInput = x;
    }
    public void ReadStringY(string y)
    {
        yInput = y;
    }

}



//public int ToIntX()
//{
//    xDimension = int.Parse(xInput);
//    return xDimension;
//}
//public int ToIntY()
//{
//    yDimension = int.Parse(yInput);
//    return yDimension;
//}

//public int ReadStringX(string x)
//{
//    xInput = x;
//    xDimension = int.Parse(xInput);
//    return xDimension;
//}

//public int ReadStringY(string y)
//{
//    yInput = y;
//    yDimension = int.Parse(yInput);
//    return yDimension;
//}