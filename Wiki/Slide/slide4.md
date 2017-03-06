<!-- $theme: gaia -->
<style>
.center{
 text-align: center;
}
.center img {
	width: 65%;
}

</style>

<!-- template: gaia -->

# Unity講座
## 4. 敵をつくる

----
<!-- page_number: true -->
<!-- template: default -->
# 目的

:arrow_right_hook:**==前回==**
画像(自機)を矢印キーで移動させる


:arrow_right:**==今回==**
弾を撃ってくる敵をつくろう

----

## 仕様
- 敵は動かない
- 敵が弾を作る
- 弾は自機と同じy座標に生成される
- 弾は左側(x軸のマイナス方向)に等速で飛んでいく

----

##### 実行時のプログラムの流れ
<div class="center">
<img src="../Images/4/OverView.png">
</div>

----

## 実装の流れ(目次)

1. **弾のひな形** の用意
1. **弾の挙動(スクリプト)** を書く
1. **敵** の用意
1. **敵の挙動(スクリプト)** を書く

----
<!-- template: invert -->

# ==1. 弾のひな形の用意==
##### 2. 弾の挙動を書く
##### 3. 敵の用意
##### 4. 敵の挙動を書く

----
<!-- template: default -->

### 1. 弾のひな形の用意

弾ではなく **弾のひな形** :arrow_left:ここ重要

なんで？
- 最初から存在するわけではなく途中で出てくる
- 同じものがたくさん出てくる

----
### 1. 弾のひな形の用意

最初から存在するわけではなく途中からたくさん出てくるものは

1. **ひな形** を作る
1. ゲーム中にひな形の **クローン** を作る



----
### 1. 弾のひな形の用意

ひな形 :arrow_right: `Prefab`

弾の`Prefab`をつくる

----

###### 1. 弾のひな形の用意
### > 弾の`Prefab`の作り方(1)

1. 弾に使う画像を用意
1. 弾の画像をInspectorからFilterModeを`Point`に
1. 画像をProjectからSceneにD&D
1. Hierarchyで選択
   :arrow_right:Scaleを弄り適切な大きさに
1. Hirarchyで選択&右クリック
   :arrow_right:`Rename`で名前を分かり易いものに変更

----
###### 1. 弾のひな形の用意
### > 弾の`Prefab`の作り方(2)

1. Projectウィンドウで右クリック
   :arrow_right:`Create > Folder`で`Resources`フォルダを作成
1. Hierarchyにある弾を`Resources`にD&D
1. Hierarchyにある弾選択&右クリック:arrow_right:`Delete`

----
###### 1. 弾のひな形の用意
### ==**TIPS**==
  
:white_check_mark: `Prefab`を置くフォルダの名前は`Resources`以外にしてはいけません。

詳しくはwikiのトップページの下のほうに...  
つまりは、仕様ということです...

----
###### 1. 弾のひな形の用意
### ==**TIPS**==  

:white_check_mark: 何か誤操作してもだいたい`Ctrl+Z`で戻せる

:white_check_mark: 名前つけるの間違えたらF2キーで再設定できる

----
### 1. 弾のひな形の用意
これでPrefabの準備が完了
<div class="center">
<img src="../Images/4/ReadyPrefab.png">
</div>

----
###### 1. 弾のひな形の用意
## > Prefabとは:grey_question:
PrefabとGameObjectは違う

**Prefab**
:arrow_right:GameObject+設定済みのComponent群

この場合
:arrow_right:弾のPrefabには既に画像が設定されている
　:arrow_right:このPrefabをゲーム上に生成すれば、
 　　最初から弾の画像が設定されている

----
<!-- template: invert -->

##### 1. 弾のひな形の用意
# ==2. 弾の挙動を書く==
##### 3. 敵の用意
##### 4. 敵の挙動を書く


----
<!-- template: default -->
## 2. 弾の挙動を書く

>横に進む

>弾は左側(x軸のマイナス方向)に等速で飛んでいく。

このように作っていきます。

----
## 2. 弾の挙動を書く

まずは==スクリプトファイルの作成==

1. Projectウィンドウで右クリック
2. `Create > C# Script`
3. 作られたファイルをダブルクリック

----
###### 2. 弾の挙動を書く
:arrow_down:弾のスクリプト(解説します)

```CSharp
// using 云々(スペースの関係で省略。wikiにはあります)

public class bullet : MonoBehaviour {
    void Update () {
        // 弾の位置情報の抜き出し
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        // 弾の位置の計算
        posX += -5f * Time.deltaTime;
        
        // 更新
        transform.position = new Vector2 (posX, posY);
    }
}
```


----
###### 2. 弾の挙動を書く
## > Time.deltaTimeとは

1フレームの時間間隔は一定ではない。

:arrow_down:これでは等速で移動しない。
```CSharp
posX += -5f;
```

----
###### 2. 弾の挙動を書く
## > Time.deltaTimeとは

# `Time.deltaTime`
- １フレームの時間間隔の実測値を示す
- `float`型
- 単位は実時間の秒

----
###### 2. 弾の挙動を書く
## > Time.deltaTimeとは

> **(変位) = (速度) × (変化時間)**

変化時間が`Time.deltaTime`にあたる。

<br/>

:arrow_down: 等速運動させる処理
```CSharp
posX += -5f * Time.deltaTime;
```

----
###### 2. 弾の挙動を書く
:arrow_down:弾のスクリプト(解説済み)

```CSharp
// using云々(省略)

public class bullet : MonoBehaviour {
    void Update () {
        // 弾の位置情報の抜き出し
        float posX = transform.position.x;
        float posY = transform.position.y;
        
        // 弾の位置の計算
        posX += -5f * Time.deltaTime;
        
        // 更新
        transform.position = new Vector2 (posX, posY);
    }
}
```

----
###### 2. 弾の挙動を書く

出来上がった弾のスクリプトをProjectウィンドウのPrefabにくっつける

----

## :boom:**注意**  

クラス名とファイル名は常に同一でなければならない
![](../Images/4/FileNameIsClassName.png)  

- プログラムをGameObjectにくっつけられない
- プログラムコンポーネントに:warning:マークが出ている

![](../Images/4/ClassCantLoad.png)  



----
<!-- template: invert -->

##### 1. 弾のひな形の用意
##### 2. 弾の挙動を書く
# ==3. 敵の用意==
##### 4. 敵の挙動を書く

----
<!-- template: default -->
###### 3. 敵の用意
## > 敵の作り方

1. えっくちゅをもう一体ProjectウィンドウからSceneビューにD&D
1. Hierarchyで右クリックし、`Rename`で名前を分かり易いものに変える
1. `SpriteRenderer`コンポーネントの`Color`プロパティを赤色に弄る
1. `Transform`コンポーネントの`Scale`をいじり、大きくする
1. それっぽくなる

----
###### 3. 敵の用意

これで敵の用意が出来た
<div class="center">
<img src="../Images/4/ReadyImage.png">
</div>

----
<!-- template: invert -->

##### 1. 弾のひな形の用意
##### 2. 弾の挙動を書く
##### 3. 敵の用意
# ==4. 敵の挙動を書く==

----
<!-- template: default -->

## 4. 敵の挙動を書く
==仕様==曰く、

> - 敵は動かない
> - 敵が弾を作る
> - 弾は自機と同じy座標に生成される

----
## 4. 敵の挙動を書く

==実行時のプログラムの流れ==曰く、

> `Start()`内で **事前に用意しておいた弾** を読み込む

> `Update()`内で自機の位置を取得して、この情報をもとに **読み込んでおいた弾** をゲーム上に生成する

----

## 4. 敵の挙動を書く
まずは敵用のスクリプトを用意

<br/>

1. Projectウィンドウで右クリック
2. `Create > C# Script`
3. 作られたファイルをダブルクリック

----

###### 4. 敵の挙動を書く
## > 敵の`Start()`を書く

書くべきことは

> **事前に用意しておいた弾** を読み込む

事前に用意しておいた弾 :arrow_right: 弾の`Prefab`

<br/>

:star: `Start()`内では、弾の`Prefab`の読み込みを行う

----
###### 4. 敵の挙動を書く > 敵の`Start()`を書く
## > `Prefab`の読み込み

<br/>

:arrow_down: `Prefab`を(GameObjectとして)読み込む

```CSharp
Resources.Load <GameObject>(  リソースフォルダ内ので名前  );
```

----
###### 4. 敵の挙動を書く > 敵の`Start()`を書く
## > `Prefab`の読み込み

<br/>

:arrow_down: 弾の`Prefab`を読み込む

```CSharp
GameObject bullet = Resources.Load <GameObject>("bullet");
```

<br/>

これを`Start()`内に書いたらOK...ではない

----
###### 4. 敵の挙動を書く > 敵の`Start()`を書く
## > `Prefab`の読み込み


:arrow_down: ダメ

```CSharp
void Start(){
    GameObject bullet;
    bullet = Resources.Load <GameObject>("bullet");
}
```

:arrow_down: こうする

```CSharp
GameObject bullet;
void Start(){
    bullet = Resources.Load <GameObject>("bullet");
}
```

----
###### 4. 敵の挙動を書く > 敵の`Start()`を書く
## > `Prefab`の読み込み

<br/>

:arrow_down: これはだめ
```CSharp
GameObject bullet = Resources.Load <GameObject>("bullet");
void Start(){
}
```

----

###### 4. 敵の挙動を書く
## > 敵の`Start()`を書く


```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    GameObject bullet;

    void Start () {
        bullet = Resources.Load <GameObject>("bullet");
    }

    void Update () {
        // bulletを使った処理
    }
}
```


----
###### 4. 敵の挙動を書く
## > 敵の`Update()`を書く

<br/>

### ==やること==

1. 自機の現在位置を取得
1. その情報をもとにゲーム上に弾を生成


----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 自機の現在位置の取得
「自機の現在位置を取得する」という処理は

- ==ゲーム上に存在する自機を取得する==
- ==取得した自機から位置情報を抜き出す==

からなる

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
###### > 自機の現在位置の取得
### > 自機の取得

<br/>

:arrow_down:ゲーム上に存在するゲームオブジェクトの取得
```CSharp
GameObject.Find(  ここにHierarchy上での名前を書く  );
```

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
###### > 自機の現在位置の取得
### > 自機の取得

<br/>

:arrow_down:`"me"`の取得
```CSharp
GameObject target = GameObject.Find("me");
```

`target`は自機を指す。

<br/>

自機の取得完了！

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
###### > 自機の現在位置の取得
### > 自機の位置情報を抜き出す

<br/>


:arrow_down: `target`のy座標の取り出し方
```CSharp
float targetPosY = target.transform.position.y;
```

<br/>

自機の現在位置の取得が完了

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 自機の現在位置の取得

:arrow_down:今までのところ
```CSharp
void Update () {
    // 自機の取得
    GameObject target = GameObject.Find("me");
    
    // 自機のy座標を抜き出す
    float targetPosY = target.transform.position.y;
}
```


----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成
:arrow_down:GameObjectをゲーム上に生成する

```CSharp
Instantiate (  生成したいGameObject型の変数  );
```

----

###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

:arrow_down:GameObjectをゲーム上に生成する

```CSharp
Instantiate (  生成したいGameObject型の変数  );
```

<br/>

:arrow_down:先ほど読み込んだ`bullet`をゲーム上に生成する

```CSharp
Instantiate (bullet);
```


----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

<br/>

==**問題点**==
生成した弾が **自機と同じy座標にない！**

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

<br/>

==**問題点**==
生成した弾が **自機と同じy座標にない！**

<br/>

==**解決策**==
`Instantiate()`関数の返り値を使う。

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

# `Instantiate()`

- ゲームオブジェクトをゲーム上に生成

- 返り値:
  ==ゲーム上に生成されたゲームオブジェクト==

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

<br/>

:arrow_down: 弾を自機のy座標に生成
```CSharp
// 弾の生成
GameObject clone = Instantiate (bullet);

// 生成された弾(clone)の位置を変更。
clone.transform.position = new Vector2 (4f, targetPosY);
```
----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く


:arrow_down: ここまでの`Update()`の中身

```CShape
void Update () {
    // 自機の取得
    GameObject target = GameObject.Find("me");

    // 自機のy座標を抜き出す
    float targetPosY = target.transform.position.y;
    
    // 弾の生成
    GameObject clone = Instantiate (bullet);

    // 生成された弾(clone)の位置を変更。
    clone.transform.position
        = new Vector2 (4f, targetPosY);
}
```

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

- 一旦実行してみる

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

- 一旦実行してみる

- 弾の生成間隔が短い

----
###### 4. 敵の挙動を書く > 敵の`Update()`を書く
## > 弾の生成

- 一旦実行してみる

- 弾の生成間隔が短い

## :star: ==弾の生成間隔を設ける==
１個 / 秒つくる処理に変更

----


```CSharp
float time;

void Start () {
    // 弾読み込み処理

    // 変数の初期化
    time = 0;
}

void Update () {
    time += Time.deltaTime;
    
    if (time >= 1f) {
        // ここに弾の生成処理(wikiには書いてある)。
        
        // time変数の値を0に戻す。
        time = 0;
    }
}
```

----

<!-- template: invert -->

##### 1. 弾のひな形の用意
##### 2. 弾の挙動を書く
##### 3. 敵の用意
##### 4. 敵の挙動を書く

# ==完成!==


----
<!-- template: default -->

##### 今回新しく学んだこと

- シューティングゲームの弾のように、ゲームの最初からはなく、大量生産されるものには`Prefab`を使う  
  :arrow_right:`Prefab`の作り方

- `Prefab`を読み込んで、ゲーム上に生成する
  :arrow_right:`Resources.Load`関数、`Instantiate`関数
	
- `Hierarchy`上にあるゲームオブジェクトの取得
  :arrow_right:`GameObject.Find`関数
	
- 他のゲームオブジェクトの位置情報の取得
  :arrow_right:`(ゲームオブジェクト).transform.position`


----
<!-- template: gaia -->
<!-- page_number: false -->

# おわり

###### 分からないことがあったら周囲の先輩に聞いてください
###### wikiのほうに自由課題があるのでそちらもどうぞ

