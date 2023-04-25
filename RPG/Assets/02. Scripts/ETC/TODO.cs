﻿//
//TODO
//=================================================
// 해야 할 것
//=================================================

// 스테이지 슬라이더 조절시 UV값을 조절하여 이미지 반복 순회 만들기
// 전투 결과 UI 재작업
// 전투 시작, 승리, 패배 연출 만들기
// 전투 준비 전 기다리는 씬만들기
// 현재 층 재도전시 소비되는 에너지 표기하고 소비 에너지보다 유저가 가지고 있는 에너지가 적을 경우 버튼 비활성화
// 게임시작 씬 만들기
// 장비 UI에 인챈트 없는 버전의 장비를 보여주는 버튼 추가
// JSON을 통한 저장 및 불러오기 만들기
// 애니메이터 오버라이드를 통해 양손무기 버전 애니메이션 만들고 적용하기

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
// 메인 메뉴에서 리소스가 제대로 표기되지 않는 버그
// 전투 준비 과정 중 설정창 누르면 배틀 스테이트가 계속 진행되는 버그
// 전투중 메인메뉴로 돌아갈 때 얻은 아이템 수치 적용 안되는 버그
// 배틀 텍스트가 겹치는 문제

//=================================================
// 기획안
//=================================================

// 장비는 어떤 구조를 가지고 있는가?
// 장비 : 이름, 등급(노말, 레어, 유니크, 전설), 종류(무기, 머리, 아머, 바지, 장신구)
// 무기 : 공격력, 공격속도, 공격거리, 이동속도, 치명타확률, 치명타피해 증가, 적중률
// 아머 : 방어력, 체력,이동속도, 회피율
// 머리 : 방어력, 체력,치명타 확률 감소, 치명타 피해 감소
// 바지 : 방어력, 체력,이동속도

// 스킬
// 무기 (접두 : 무기 공격시 이벤트 발생, 접미 : 무기 공격 시 이벤트 발생)
// 갑옷 (접두 : 전투시 초당 이벤트 발생, 접미 : 타격받을 시 이벤트 발생)
// 투구 (접두 : 크리티컬 공격시 이벤트 발생 , 접미 : 액티브 스킬)
// 바지 (접두 : 이동시 이벤트 발생, 접미 : 액티브 스킬)

// 대상이 출혈상태면 추가 데미지 들어간다 이런거?
// 공격횟수에 맞춰서
// 내 체력에 맞추는 스테이터스 변화


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


//=================================================
// 공모전
//=================================================

// 접두:
// 타버린, 그을린, 녹슨, 예리한, 피묻은, 더러운, 성실한 ??, 민첩한, 유능한, 초월적인, 
// 이가 빠진, 유일한, 방치된, 튼튼한, 울음의, 사나운, 울부짖는, 끈적한, 끈끈한, 조잡한, 외로운,
// 강운의, 음율의
   
// 접미 : 
// 돌덩이, 꿀벌, 악마, 운명, 늑대, 사자, 표범, 얼음, 이빨, 발톱, 화염, 불덩이, 장벽, 가호, 은총,
// 청개구리, 행운, 나락, 분노, 바보, 검객, 혼돈