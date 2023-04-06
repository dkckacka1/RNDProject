﻿//TODO
//=================================================
// 해야 할 것
//=================================================

// 배틀 씬에서 메인 씬으로 돌아올때 게임매니저에 메인씬의 Userinfo 연결해주기
// 외부에서 Userinfo를 불러오는 함수 작성필요
// 임시 함수 수정 필요
// 스킬 시스템 구현
// 장비 UI에 인챈트 없는 버전의 장비를 보여주는 버튼 추가
// 컨트롤러 LINQ로 가져온 후 가까운 T 리턴 함수 만들기
// UI 다듬기

//=================================================
// 버그 수정
//=================================================
// 장비창의 설명 부분 칸 맞추기
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
// Class 역할 정리
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
    1. 수치에 의한 정리
*/

//=================================================
// 함수 이름 정리
//=================================================
// Show...(...) : UI에서 사용한다.(정보에 맞춰서 UI를 업데이트한다.)
// SetUp(...) : 모든 게임 오브젝트에서 사용한다. (한번만 실행된다.)(컴포넌트를 연결시켜준다.)(클래스를 생성 및 초기화)
// Update...(...) : 모든 게임 오브젝트에서 사용한다. (데이터가 업데이트 되었을 때 사용한다.)
// Init(...) : 모든 게임 오브젝트에서 사용한다. (SetActive(true) 되기 전 사용한다.)
// Release...(...) : 모든 게임 오브젝트에서 사용한다. ( SetActive(false) 되기 전 호출한다.)