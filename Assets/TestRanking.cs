using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
// Firebase 불러오기

public class TestRanking : MonoBehaviour
{
    class Rank
    {
        // 순위 정보를 담는 Rank 클래스
        // Firebase와 동일하게 name, score, timestamp를 가지게 해야함
        public string name;
        public int score;
        public int timestamp;
        // JSON 형태로 바꿀 때, 프로퍼티는 지원이 안됨. 프로퍼티로 X

        public Rank(string name, int score, int timestamp)
        {
            // 초기화하기 쉽게 생성자 사용
            this.name = name;
            this.score = score;
            this.timestamp = timestamp;
        }
    }

    public DatabaseReference reference { get; set; }
    // 라이브러리를 통해 불러온 FirebaseDatabase 관련객체를 선언해서 사용

    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fir-test-29574-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        // 데이터베이스 경로를 설정해 인스턴스를 초기화
        // Database의 특정지점을 가리킬 수 있는데, 그 중 RootReference를 가리킴

        Rank rank = new Rank("나일등", 100, 1574940551);
        string json = JsonUtility.ToJson(rank);
        // 데이터를 json형태로 반환

        string key = reference.Child("rank").Push().Key;
        // firebase에 key값은 0 신인용, 1 황제리, 2 김젤리.
        // root의 자식 rank에 key 값을 추가해주는 것임

        reference.Child("rank").Child(key).SetRawJsonValueAsync(json);
        // 생성된 키의 자식으로 json데이터를 삽입
    }
}

