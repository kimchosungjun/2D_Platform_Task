using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr 
{
    public T LoadResource<T>(string _path) where T : Object
    {
        return Resources.Load<T>(_path);
    }
}
