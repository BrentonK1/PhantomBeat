using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : MonoBehaviour{

    public static bool CanPressL, CanPressR, CanPressD, CanPressU;
    bool Touch1CPB; //, Touch2CPB; CPB = Can Push Buttons
    float Score; //, UpEnemies, DownEnemies, LeftEnemies, RightEnemies;
    // public GameObject TriggerButtonUp, TriggerButtonDown, TriggerButtonLeft, TriggerButtonRight;
    bool Touch1ButtonUDistance,Touch1ButtonDDistance, Touch1ButtonLDistance, Touch1ButtonRDistance;
    bool Touch1EnemyUDistance, Touch1EnemyDDistance, Touch1EnemyLDistance, Touch1EnemyRDistance;
    const double hitboxRadius = 3.17;

    Dictionary<Direction, GameObject> Button;

    public enum Direction { Up, Left, Right, Down }

    static string buildTagFrom(string prefix, Direction direction) {
        return prefix + direction.ToString();
    }

    public delegate T Factory<T>(Direction direction);

    GameObject[] GetEnemiesFrom(Direction direction) {
        return GameObject.FindGameObjectsWithTag(buildTagFrom("Enemy", direction));
    }

    Dictionary<Direction, int> GetEnemyCounts() {
        return Playing.BuildDictionary<int>(GetEnemyCount);
    }

    static Dictionary <Direction, T> BuildDictionary <T> (Factory<T> factory){
        return new Dictionary<Direction, T>
        {
            { Direction.Down, factory(Direction.Down) },
            { Direction.Left, factory(Direction.Left) },
            { Direction.Right, factory(Direction.Right) },
            { Direction.Up, factory(Direction.Up) }
        };
    }

    int GetEnemyCount (Direction direction) {
        return GetEnemiesFrom(direction).Length;
    }

    static GameObject GetButton (Direction direction) {
        string tag = buildTagFrom("TriggerButton", direction);
        return GameObject.FindGameObjectWithTag(tag);
    }

    void Start()
    {
        Score = 0;
        Touch1CPB = true;
        Button = Playing.BuildDictionary<GameObject>(GetButton);
    }

    void Update() {
        Debug.Log(Score);

        Dictionary<Direction, int> EnemyCount = GetEnemyCounts();

        int upEnemyCount = EnemyCount[Direction.Up];

        if (Input.touchCount > 0)
        {
            Touch touch0 = Input.GetTouch(0);
            touch0.phase = TouchPhase.Began;
            Vector2 TB1pos = Camera.main.ScreenToWorldPoint(touch0.position);

            GameObject TriggerButtonU = GetButton(Direction.Up);
            GameObject TriggerButtonD = GetButton(Direction.Down);
            GameObject TriggerButtonL = GetButton(Direction.Left);
            GameObject TriggerButtonR = GetButton(Direction.Right);


            Touch1ButtonUDistance = Vector2.Distance(TB1pos, TriggerButtonU.transform.position) <= hitboxRadius;
            Touch1ButtonDDistance = Vector2.Distance(TB1pos, TriggerButtonD.transform.position) <= hitboxRadius;
            Touch1ButtonLDistance = Vector2.Distance(TB1pos, TriggerButtonL.transform.position) <= hitboxRadius;
            Touch1ButtonRDistance = Vector2.Distance(TB1pos, TriggerButtonR.transform.position) <= hitboxRadius;

            if (GetEnemyCount(Direction.Up) > 0)
                Touch1EnemyUDistance = Vector2.Distance(TriggerButtonU.transform.position, GameObject.FindGameObjectWithTag("EnemyUp").transform.position) < 2;
            else
                Touch1EnemyUDistance = false;
            if ( GetEnemyCount(Direction.Down) > 0)
                Touch1EnemyDDistance = Vector2.Distance(TriggerButtonD.transform.position, GameObject.FindGameObjectWithTag("EnemyDown").transform.position) < 2;
            else
                Touch1EnemyDDistance = false;
            if (GetEnemyCount(Direction.Left) > 0)
                Touch1EnemyLDistance = Vector2.Distance(TriggerButtonL.transform.position, GameObject.FindGameObjectWithTag("EnemyLeft").transform.position) < 2;
            else
                Touch1EnemyLDistance = false;
            if (GetEnemyCount(Direction.Right) > 0)
                Touch1EnemyRDistance = Vector2.Distance(TriggerButtonR.transform.position, GameObject.FindGameObjectWithTag("EnemyRight").transform.position) < 2;
            else
                Touch1EnemyRDistance = false;

            if (Touch1CPB && Touch1ButtonUDistance && GetEnemyCount(Direction.Up) < 1 || Touch1CPB && Touch1ButtonDDistance && GetEnemyCount(Direction.Down) < 1 || Touch1CPB && Touch1ButtonLDistance && GetEnemyCount(Direction.Left) < 1 || Touch1CPB && Touch1ButtonRDistance && GetEnemyCount(Direction.Right) < 1)
            {
                Score -= 1;
                Touch1CPB = false;
                StartCoroutine(Touch1Check());
            }
                else if (Touch1CPB && Touch1ButtonUDistance && Touch1EnemyUDistance && GetEnemyCount(Direction.Up) > 0|| Touch1CPB && Touch1ButtonDDistance && Touch1EnemyDDistance && GetEnemyCount(Direction.Down) > 0|| Touch1CPB && Touch1ButtonLDistance && Touch1EnemyLDistance && GetEnemyCount(Direction.Left) > 0 || Touch1CPB && Touch1ButtonRDistance && Touch1EnemyRDistance && GetEnemyCount(Direction.Right) > 0)
            {
                Score += 1;
                Touch1CPB = false;
                StartCoroutine(Touch1Check());
            }
                         else if (Touch1CPB && Touch1ButtonUDistance && !Touch1EnemyUDistance && GetEnemyCount(Direction.Up) > 0 || Touch1CPB && Touch1ButtonDDistance && !Touch1EnemyDDistance && GetEnemyCount(Direction.Down) > 0 || Touch1CPB && Touch1ButtonLDistance && !Touch1EnemyLDistance && GetEnemyCount(Direction.Left) > 0 || Touch1CPB && Touch1ButtonRDistance && !Touch1EnemyRDistance && GetEnemyCount(Direction.Right) > 0)
            {
                Score -= 1;
                Touch1CPB = false;
                StartCoroutine(Touch1Check());
            }


        }

    }

    IEnumerator Touch1Check () 
    {
        yield return new WaitForSeconds(0.01f);
        if (Input.GetTouch(0).phase != TouchPhase.Ended)
            StartCoroutine(Touch1Check());
        else
        {
            Touch1CPB = true;
            StopCoroutine(Touch1Check());
        }
    }
}

// int that equals each time that enemy should drop, if song time = float spawn enemy (enemy for each direction integration?)