﻿//TODO
//=================================================
// 해야 할 것
//=================================================

// EnemyPrefab에 무기 쥐어주기
// 플레이어 전용 HPBarUI 만들기
// 인챈트 만들기
// 오브젝트 풀링 구현 (팩토리 패턴을 사용하기)
// UI구현
// 스킬 시스템 구현
// 컨트롤러 LINQ로 가져온 후 가까운 T 리턴 함수 만들기

//=================================================
// 버그 수정
//=================================================
// 적이 플레이어를 죽였음에도 계속 이동하는 버그

//=================================================
// 기획안
//=================================================

// 장비는 어떤 구조를 가지고 있는가?
// 장비 : 이름, 등급(노말, 레어, 유니크, 전설), 종류(무기, 머리, 아머, 바지, 장신구)
// 무기 : 공격력, 공격속도, 공격거리, 이동속도, 치명타확률, 치명타피해 증가, 적중률
// 아머 : 방어력, 체력,이동속도, 회피율
// 머리 : 방어력, 체력,치명타 확률 감소, 치명타 피해 감소
// 바지 : 방어력, 체력,이동속도
// 장신구 : 반사데미지 ,마법저항력, 체력재생 etx..

// 방치형답게?? (알아서 던전을 도는???)
// 인챈트 ( + 요소와 - 요소의 적절한 결합)
// 후순위 : 강화 선택권( -2 ~ +2 || +0 ~ +1 )


//=================================================
// 역할 정리
//=================================================
// Status의 역할
/*
    1. Data 정리
*/

// Controller의 역할
/*
    1. AI 조건 정리
*/

// State의 역할
/*
    1. Animator 수정
    2. AI의 수행
*/

// 행동의 역할
/*
    1. 수치에 의한 정리+
*/