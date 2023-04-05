using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogApp 
{
   public static void Trace(object msg, DebugColor color = DebugColor.normal){
        Debug.Log("<color=" + color.ToString() + ">"+  msg.ToString()  +"</color>");
   }
}

public enum DebugColor{
    normal, white, yellow, red
}