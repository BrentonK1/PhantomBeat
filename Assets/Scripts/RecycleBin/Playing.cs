/* <!> DEPRECATED <!> */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomBeat.CoreLibrary;

public class Playing : MonoBehaviour
{
    public static bool CanPressL, CanPressR, CanPressD, CanPressU;
    bool Touch1CanPressButtons;
    float Score;

    Dictionary<Direction, GameObject> Buttons {
        get { return BuildDictionary(GetButton); }
    }

    Dictionary<Direction, GameObject> Button;

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

    bool IsEnemyColliding(float threshold, GameObject object1, GameObject object2){
        if (object1 == null || object2 == null) return false;
        var object1Position = object1.transform.position;
        var object2Position = object2.transform.position;
        var distance = Vector2.Distance(object1Position, object2Position);
        return distance <= threshold;
    }

    bool IsTouchColliding(float threshold, GameObject object1, Vector2 touchPosition) {
        var object1Position = object1.transform.position;
        Vector2 touch = touchPosition;
        var distance = Vector2.Distance(object1Position, touch);
        return distance <= threshold;
    }

    bool IsTouchOverButton(Direction direction) {
        const float hitboxRadius = 3.17f;

        var button = GetButton(direction);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        return IsTouchColliding(hitboxRadius, button, touchPosition);
    }

    GameObject ClosestEnemy (Direction direction) {
        var enemyTag = BuildTagFrom("Enemy", direction);
        var currentClosestEnemy = GameObject.FindGameObjectWithTag(enemyTag);
        var buttonPosition = GetButton(direction).transform.position;
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (var currentEnemy in enemies){
            var currentClosestDistance = Vector2.Distance(buttonPosition, currentClosestEnemy.transform.position);
            var enemyDistance = Vector2.Distance(buttonPosition, currentEnemy.transform.position);
            if (enemyDistance < currentClosestDistance)
                currentClosestEnemy = currentEnemy;
        }

       return currentClosestEnemy;
    }

    bool IsEnemyOverButton (Direction direction){
        const float hitboxRadius = 2;

        var button = GetButton(direction);
        var enemy = ClosestEnemy(direction);

        return IsEnemyColliding(hitboxRadius, button, enemy);
    }

    Dictionary <Direction, T> BuildDictionary <T> (Factory<T> factory){
        return new Dictionary<Direction, T> {
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

    void Start() {
        Score = 0;
        Touch1CanPressButtons = true;
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

        if (Touch1CanPressButtons && IsTouchOverButton(Direction.Up) && GetEnemyCount(Direction.Up) < 1 || Touch1CanPressButtons && IsTouchOverButton(Direction.Down) && GetEnemyCount(Direction.Down) < 1 || Touch1CanPressButtons && IsTouchOverButton(Direction.Left) && GetEnemyCount(Direction.Left) < 1 || Touch1CanPressButtons && IsTouchOverButton(Direction.Right) && GetEnemyCount(Direction.Right) < 1) {
            Score -= 1;
            Touch1CanPressButtons = false;
            StartCoroutine(Touch1Check());
        }
        else if (Touch1CanPressButtons && IsTouchOverButton(Direction.Up) && IsEnemyOverButton(Direction.Up) && GetEnemyCount(Direction.Up) > 0|| Touch1CanPressButtons && IsTouchOverButton(Direction.Down) && IsEnemyOverButton(Direction.Down) && GetEnemyCount(Direction.Down) > 0|| Touch1CanPressButtons && IsTouchOverButton(Direction.Left) && IsEnemyOverButton(Direction.Left) && GetEnemyCount(Direction.Left) > 0 || Touch1CanPressButtons && IsTouchOverButton(Direction.Right) && IsEnemyOverButton(Direction.Right) && GetEnemyCount(Direction.Right) > 0) {
            Score += 1;
            Touch1CanPressButtons = false;
            StartCoroutine(Touch1Check());
        }
        else if (Touch1CanPressButtons && IsTouchOverButton(Direction.Up) && !IsEnemyOverButton(Direction.Up) && GetEnemyCount(Direction.Up) > 0 || Touch1CanPressButtons && IsTouchOverButton(Direction.Down) && !IsEnemyOverButton(Direction.Down) && GetEnemyCount(Direction.Down) > 0 || Touch1CanPressButtons && IsTouchOverButton(Direction.Left) && !IsEnemyOverButton(Direction.Left) && GetEnemyCount(Direction.Left) > 0 || Touch1CanPressButtons && IsTouchOverButton(Direction.Right) && !IsEnemyOverButton(Direction.Right) && GetEnemyCount(Direction.Right) > 0) {
            Score -= 1;
            Touch1CanPressButtons = false;
            StartCoroutine(Touch1Check());
        }
    }

    IEnumerator Touch1Check () {
        yield return new WaitForSeconds(0.01f);
        if (Input.GetTouch(0).phase != TouchPhase.Ended)
            StartCoroutine(Touch1Check());
        else {
            Touch1CanPressButtons = true;
            StopCoroutine(Touch1Check());
        }
    }
}