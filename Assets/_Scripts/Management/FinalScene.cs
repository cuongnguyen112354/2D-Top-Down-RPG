using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    private void Start(){
        GameManager.Instance.FinalScene = true;
    }
}