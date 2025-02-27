# Boardgame_SET
 Unity를 이용한 "SET"보드게임 개발

## 개발 기간 
- 2024/12/26 ~ 2024/12/28(플레이 가능한 상태로 구현)
- 2025/01월 중 게임 종료 구현

### 게임 설명
> 보드게임 세트란? - 자세한 규칙은 인터넷 검색 추천
>> 4가지의 "속성"이 모두 같거나, 4가지의 "속성"이 모두 다른 경우에 세트. 즉 어중간하게 같거나 다르면 안 되는 것입니다.

### 게임 개발 과정
- 보드게임 세트를 개발하기 위해서는, 이미지 파일이 필요합니다.(figma를 이용하여 직접 디자인)
- ![게임 내에 사용한 이미지]https://github.com/nmmlee/Boardgame_SET/blob/main/Assets/Resources/card/BLUE_DIAMOND_FILLED_1.png
- 그림을 보면, 카드에는 [그림], [색상], [그림 개수], [채워진 타입] 총 4가지의 속성을 가지고 있습니다.
  
- 카드 데이터를 갖고 있는 상태로, 이미지는 unity에서 직접 그리는 방법도 있습니다.
- 하지만 카드 디자인을 고려했을 때, 이미지를 unity 내에서 그리는 것보다 직접 만드는 편이 시각적으로 좋을 것으로 판단하였습니다.
- 용량을 고려한다면 81장의 카드를 만드는 것보다, 코드를 이용해서 구현하는 편도 좋다고 생각합니다.

#### 카드 데이터 가져오는 방법
- 파일명을 모두 "[색상]_[그림]_[채워진 타입]_[색상].png"로 구성하였습니다.
- Resources 폴더에 총 81장의 카드를 넣은 후에, 확장자를 제거한 후 "_"를 기준으로 문자열을 슬라이싱하였습니다.
- 그리고 Card 클래스에서 4가지의 속성을 멤버변수로 갖고, 슬라이싱 한 문자열을 기반으로 초기화하였습니다.
