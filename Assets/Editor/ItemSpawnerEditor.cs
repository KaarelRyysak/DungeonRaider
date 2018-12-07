using UnityEngine;
using UnityEditor;

public class ItemSpawnerEditor : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    public float myX;
    public float myY;
    public float myL;
    public float myR;
    public float myRun;
    public float myWait;
    public Object source;

    int selected = 0;
    string[] options = new string[]
    {

        "Dino Stroller", "Dino Walker", "Dino Runner"
    };


    //private Prefab DinoStrollerPrefab = 






    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/ItemSpawnerEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ItemSpawnerEditor window = (ItemSpawnerEditor)EditorWindow.GetWindow(typeof(ItemSpawnerEditor));
        window.Show();
    }

    void OnGUI()
    {

        GUILayout.Label("Enemy Selection", EditorStyles.boldLabel);
        selected = EditorGUILayout.Popup(selected, options);


        GUILayout.Label("Enemy Spawn Location", EditorStyles.boldLabel);
        GUILayout.Label("One Square = 0.5");
        myX = EditorGUILayout.FloatField("EnemyX", myX);
        myY = EditorGUILayout.FloatField("EnemyY", myY);



            if (selected == 0)
        {
            GUILayout.Label("Dino Stroller variables", EditorStyles.boldLabel);
            myL = EditorGUILayout.FloatField("LeftL", myL);
            myR = EditorGUILayout.FloatField("RightL", myR);
            myRun = EditorGUILayout.FloatField("Speed", myRun);
            myWait = EditorGUILayout.FloatField("WaitTime", myWait);

            source = EditorGUILayout.ObjectField(source, typeof(Prefab), true);

            if (GUILayout.Button("Spawn Stroller"))
            {
                //GameObject.Instantiate<DinoStrollerMovement>()
            }
        }

        else if(selected == 1)
        {
            GUILayout.Label("Dino Walker variables", EditorStyles.boldLabel);
            myRun = EditorGUILayout.FloatField("Speed", myRun);
            myWait = EditorGUILayout.FloatField("WaitTime", myWait);
            if (GUILayout.Button("Spawn Walker")) { }
        }
        
        else if(selected == 2)
        {
            GUILayout.Label("Dino Runner variables", EditorStyles.boldLabel);
            myRun = EditorGUILayout.FloatField("Speed", myRun);
            myWait = EditorGUILayout.FloatField("WaitTime", myWait);
            if (GUILayout.Button("Spawn Runner")) { }
        }

        


       



       
    }
}