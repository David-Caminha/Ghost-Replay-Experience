using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class RecordManagerScript : MonoBehaviour {

    public static RecordManagerScript recorder;

    public GameObject shadowPrefab;

    public List<int> currentPlayerJumpTick = new List<int>();
    public List<int> currentPlayerRightTick = new List<int>();
    public List<int> currentPlayerLeftTick = new List<int>();


    void Awake()
    {
        recorder = this;
    }

	void Start ()
    {
        Load();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerSave.dat");

        PlayerRecording data = new PlayerRecording();
        data.jumpTick = currentPlayerJumpTick;
        data.leftTick = currentPlayerLeftTick;
        data.rightTick = currentPlayerRightTick;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.Open);
            PlayerRecording data = (PlayerRecording)bf.Deserialize(file);
            file.Close();

            GameObject shadow = Instantiate(shadowPrefab, new Vector3(2.77f, 1.84f), Quaternion.identity) as GameObject;
            PlayerControllerScript script = shadow.GetComponent<PlayerControllerScript>();
            script.recorded = true;
            script.jumpTick = new List<int>(data.jumpTick);
            script.leftTick = new List<int>(data.leftTick);
            script.rightTick = new List<int>(data.rightTick);
        }
    }
}

[Serializable]
class PlayerRecording
{
    public List<int> jumpTick;
    public List<int> rightTick;
    public List<int> leftTick;
}
