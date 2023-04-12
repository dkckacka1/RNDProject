﻿//TODO
//=================================================
// 해야 할 것
//=================================================

// 현재 배틀 상태에 따른 스킬 쿨타임 멈춤
// 전투 시작, 승리, 패배 연출 만들기
// 전투 준비 전 기다리는 씬만들기
// 전투 준비 시 현재 층 표시하기
// 전투 결과 UI에서 현재 층 재도전 만들기
// 스텟창을 스크롤 뷰로 만들기
// 별도의 장비 강화창 만들기
// 메인UI의 버튼 모양 바꾸기 (이미지 활용)
// 게임시작 씬 만들기
// 장비 강화, 인챈트, 뽑기 시 이전에 착용하고 있던 장비, 새롭게 변화된 장비 보여주기
// 장비 UI에 인챈트 없는 버전의 장비를 보여주는 버튼 추가
// JSON을 통한 저장 및 불러오기 만들기

//=================================================
// 해야 할 것 (후순위)
//=================================================
// 속성치추가
// 인챈트 뽑기에 확률 적용
// 강화 수치에 따른 인챈트 변화
// 강화 선택권( -2 ~ +2 || +0 ~ +1 )

//=================================================
// 버그 수정
//=================================================
// 루팅 아이템의 포지션이 이상하게 잡히는 버그
// (다음 스테이지를 출력하면서 바로 다음 에너미의 포지션이 잡혀서 나오는 버그로 추정됨)
// 배틀 텍스트가 겹치는 문제
// 신규 폰트 사용시 배틀 텍스트의 폰트가 깨진는 문제

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

// 스킬
// 무기 (접두 : 무기 공격시 이벤트 발생, 접미 : 무기 공격 시 이벤트 발생)
// 갑옷 (접두 : 전투시 초당 이벤트 발생, 접미 : 타격받을 시 이벤트 발생)
// 투구 (접두 : 크리티컬 공격시 이벤트 발생 , 접미 : 액티브 스킬)
// 바지 (접두 : 이동시 이벤트 발생, 접미 : 액티브 스킬)


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