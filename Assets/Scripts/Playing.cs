using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : MonoBehaviour{

    // :)

    public static bool CanPressL, CanPressR, CanPressD, CanPressU;
    bool Touch1CPB; //, Touch2CPB; CPB = Can Push Buttons
    float Score; //, UpEnemies, DownEnemies, LeftEnemies, RightEnemies;
    // public GameObject TriggerButtonUp, TriggerButtonDown, TriggerButtonLeft, TriggerButtonRight;
    bool Touch1ButtonUDistance,Touch1ButtonDDistance, Touch1ButtonLDistance, Touch1ButtonRDistance;
    const double TK = 3.17;

    Dictionary<Direction, GameObject> Button;

    public enum Direction { Up, Left, Right, Down }

    string BuildTagFrom(string prefix, Direction direction) {
        return prefix + direction.ToString();
    }

    public delegate T Factory<T>(Direction direction);

    GameObject[] GetEnemiesFrom(Direction direction) {
        return GameObject.FindGameObjectsWithTag(BuildTagFrom("Enemy", direction));
    }

    Dictionary<Direction, int> GetEnemyCounts() {
        return BuildDictionary(GetEnemyCount);
    }

    bool IsColliding(float threshold, GameObject object1, GameObject object2){
        if (object1 == null || object2 == null) return false;
        var object1Position = object1.transform.position;
        var object2Position = object2.transform.position;
        var distance = Vector2.Distance(object1Position, object2Position);
        return distance <= threshold;
    }

    bool IsEnemyOverButton (Direction direction){
        const float hitboxRadius = 2;

        var button = GetButton(direction);
        var enemyTag = BuildTagFrom("Enemy", direction);
        var enemy = GameObject.FindGameObjectWithTag(enemyTag);

        return IsColliding(hitboxRadius, button, enemy);
    }

    //CHANGE ISCOLLIDING SO THAT I CAN USE THE VECTOR2 OF THE TOUCH RATHER THAN JUST THE GAMEOBJECTS
    bool IsTouchOverButton (Direction direction) {
        const float hitboxRadius = 3.17f;

        var button = GetButton(direction);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        return IsColliding(hitboxRadius, button, touchPosition);
    }

    Dictionary <Direction, T> BuildDictionary <T> (Factory<T> factory){
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

    GameObject GetButton (Direction direction) {
        string buttonTag = BuildTagFrom("TriggerButton", direction);
        return GameObject.FindGameObjectWithTag(buttonTag);
    }

    void Start()
    {
        Score = 0;
        Touch1CPB = true;
        Button = BuildDictionary(GetButton);
    }

    void Update() {
        Debug.Log(Score);

        if (Input.touchCount == 0)
            return;

        Touch touch0 = Input.GetTouch(0);
        touch0.phase = TouchPhase.Began;
        Vector2 TB1pos = Camera.main.ScreenToWorldPoint(touch0.position);

        var button = BuildDictionary(GetButton);

        var enemyOverButton = BuildDictionary(IsEnemyOverButton);

        Touch1ButtonUDistance = Vector2.Distance(TB1pos, GetButton(Direction.Up).transform.position) <= TK;
        Touch1ButtonDDistance = Vector2.Distance(TB1pos, GetButton(Direction.Down).transform.position) <= TK;
        Touch1ButtonLDistance = Vector2.Distance(TB1pos, GetButton(Direction.Left).transform.position) <= TK;
        Touch1ButtonRDistance = Vector2.Distance(TB1pos, GetButton(Direction.Right).transform.position) <= TK;

        if (Touch1CPB && Touch1ButtonUDistance && GetEnemyCount(Direction.Up) < 1 || Touch1CPB && Touch1ButtonDDistance && GetEnemyCount(Direction.Down) < 1 || Touch1CPB && Touch1ButtonLDistance && GetEnemyCount(Direction.Left) < 1 || Touch1CPB && Touch1ButtonRDistance && GetEnemyCount(Direction.Right) < 1)
        {
            Score -= 1;
            Touch1CPB = false;
            StartCoroutine(Touch1Check());
        }
        else if (Touch1CPB && Touch1ButtonUDistance && IsEnemyOverButton(Direction.Up) && GetEnemyCount(Direction.Up) > 0|| Touch1CPB && Touch1ButtonDDistance && IsEnemyOverButton(Direction.Down) && GetEnemyCount(Direction.Down) > 0|| Touch1CPB && Touch1ButtonLDistance && IsEnemyOverButton(Direction.Left) && GetEnemyCount(Direction.Left) > 0 || Touch1CPB && Touch1ButtonRDistance && IsEnemyOverButton(Direction.Right) && GetEnemyCount(Direction.Right) > 0)
        {
            Score += 1;
            Touch1CPB = false;
            StartCoroutine(Touch1Check());
        }
        else if (Touch1CPB && Touch1ButtonUDistance && !IsEnemyOverButton(Direction.Up) && GetEnemyCount(Direction.Up) > 0 || Touch1CPB && Touch1ButtonDDistance && !IsEnemyOverButton(Direction.Down) && GetEnemyCount(Direction.Down) > 0 || Touch1CPB && Touch1ButtonLDistance && !IsEnemyOverButton(Direction.Left) && GetEnemyCount(Direction.Left) > 0 || Touch1CPB && Touch1ButtonRDistance && !IsEnemyOverButton(Direction.Right) && GetEnemyCount(Direction.Right) > 0)
        {
            Score -= 1;
            Touch1CPB = false;
            StartCoroutine(Touch1Check());
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