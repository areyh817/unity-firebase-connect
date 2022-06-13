using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
// Firebase 불러오기

public class GameShow : MonoBehaviour
{
    class Rank
    {
        // 순위 정보를 담는 Rank 클래스
        // Firebase와 동일하게 name, score를 가지게 해야함
        public string name;
        public int score;
        // JSON 형태로 바꿀 때, 프로퍼티는 지원이 안됨. 프로퍼티로 X

        public Rank(string name, int score)
        {
            // 초기화하기 쉽게 생성자 사용
            this.name = name;
            this.score = score;
        }
    }

    public DatabaseReference reference { get; set; }
    // 라이브러리를 통해 불러온 FirebaseDatabase 관련객체를 선언해서 사용

    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fir-test-29574-default-rtdb.firebaseio.com/");
        // 데이터베이스 경로를 설정해 인스턴스를 초기화

        reference = FirebaseDatabase.DefaultInstance.GetReference("rank");
        // 사용하고자 하는 데이터를 reference가 가리킴
        // 여기선 rank 데이터 셋에 접근

        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            { // 성공적으로 데이터를 가져왔으면
                DataSnapshot snapshot = task.Result;
                // 데이터를 출력하고자 할때는 Snapshot 객체 사용함

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    Debug.Log("이름: " + rank["name"] + ", 점수: " + rank["score"]);
                    // JSON은 사전 형태이기 때문에 딕셔너리 형으로 변환
                }
            }
        });
    }
}