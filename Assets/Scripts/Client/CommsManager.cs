using System;
using System.Collections;
using Client;
using UnityEngine;

public class CommsManager : MonoBehaviour
{
    Movement movement;
    ConnectToHost connectToHost;
    ArrayList scheduledMessages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = FindFirstObjectByType<Movement>();
        connectToHost = ConnectToHost.getInstance();
        scheduledMessages = ArrayList.Synchronized(new ArrayList());
    }

    // Update is called once per frame
    void Update()
    {
        // if(Variables.worldGenerated){
        //     Comms();
        // }
    }

    void scheduleMessage(string message){
        scheduledMessages.Add(message);
    }

    void Comms(){
        bool hitting = movement.isHitting();
        bool interacting = movement.isInteracting();
        Vector3 pos = movement.getPlayerPosition();
        Vector3 forward = movement.getCameraForward();
        string message = "pp " + pos.x + " " + pos.y + " " + pos.z + "%" + forward.x + " " + forward.y + " " + forward.z + "%" + (hitting ? "1" : "0") + "%" + (interacting ? "1" : "0") + "@";
        for(int i = 0; i < scheduledMessages.Count; i++){
            message += (string)scheduledMessages[0];
            scheduledMessages.RemoveAt(0);
        }
        connectToHost.SendMessageToHost(message);
    }
}
