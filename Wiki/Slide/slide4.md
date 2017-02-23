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
## 4.敵をつくる

----
<!-- page_number: true -->
<!-- template: default -->
# 目的

:arrow_right_hook:**==前回==**
画像を矢印キーで移動させる


:arrow_right:**==今回==**
弾を撃ってくる敵をつくろう

----

##### プログラムの流れ
<div class="center">
<img src="OverView.png">
</div>

----
## もくじ
1. 敵を用意する
2. 弾の挙動を書く
3. 弾を読み込む
4. 自機の現在位置を取得する
5. 弾の生成

----
<!-- template: invert -->

# 1
# 敵を用意する

----
## 敵を用意する

まずは弾を撃ちだす敵が必要

1. えっくちゅをもう一体ProjectウィンドウからSceneビューにD&D

1. Hierarchyで右クリックし、`Rename`で名前を分かり易いものに変える

1. `SpriteRenderer`コンポーネントの`Color`プロパティを赤色に弄る

1. `Transform`コンポーネントの`Scale`をいじり、大きくする

1. それっぽくなる

----
## 敵を用意する

これで敵の準備が出来た
<div class="center">
<img src="ReadyImage.png">
</div>

----

## 弾を準備する
続いて、敵が撃つ弾を準備する

弾のように、ゲーム中にオブジェクトを作る
:arrow_right:事前に`Prefab`というひな形を作る

----

## 弾を準備する

1. 弾の画像をInspectorからFilterModeを`Point`に
1. 画像をProjectからSceneにD&D
1. Hierarchyで選択:arrow_right:Scaleを弄り適切な大きさに
1. Hirarchyで選択&右クリック
:arrow_right:`Rename`で名前を分かり易いものに変更
1. Projectウィンドウで右クリックし
:arrow_right:`Create > Folder`で`Resources`フォルダを作成
2. Hierarchyにある弾を`Resources`にD&D
3. Hierarchyにある弾選択&右クリック:arrow_right:`Delete`

----
## 弾を準備する

### ==**TIPS**==  

:white_check_mark: 何か誤操作してもだいたい`Ctrl+Z`で戻せる

:white_check_mark: 名前つけるの間違えたらF2キーで再設定できる

----
## 弾を準備する
これでPrefabの準備が完了
<div class="center">
<img src="ReadyPrefab.png">
</div>

----

## Prefab:grey_question:
PrefabとGameObjectは違う

Prefab
:arrow_right:GameObject+設定済みのComponent群 

この場合
:arrow_right:弾のPrefabには既に画像が設定されている 
　:arrow_right:このPrefabをゲーム上に生成すれば、
 　　最初から弾の画像が設定されている

----
<!-- template: invert -->

# 2
# 弾の挙動を書く

----
## 弾の挙動を書く
生成された後はひたすら左に向かって進むような挙動

1. Projectウィンドウで右クリック
2. `Create > C# Script`
3. 作られたファイルをダブルクリック

----
## 弾の挙動を書く

```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	float posX, posY;
	void Update () {
        posX = transform.position.x;
        posY = transform.position.y;
	posX -= 0.3f;
	transform.position = new Vector2 (posX, posY);
	}
}
```
これをProjectウィンドウのPrefabにくっつける
:warning:この時点では実行しても特に変化はない

----

## :boom:**注意**  
クラス名とファイル名は常に同一でなければならない
![](FileNameIsClassName.png)  

- プログラムをGameObjectにくっつけられない 
- プログラムコンポーネントに:warning:マークが出ている

:arrow_right:これが原因

----
# 3
# 弾を読み込む
----
## 弾を読み込む
弾を撃ちだす
:arrow_right:「素材候補」のPrefabを読み込む


```CSharp
GameObject bullet = Resources.Load <GameObject>("bullet");
```
:warning:この **==bullet==** はProjectのファイル名
違う名前の場合、書き換える

弾のPrefabを、Hierarchy上に存在できるよう、GameObjectとして読み込む

----
# 4
# 自機の現在位置を取得する
----

## 自機の現在位置を取得する

==**弾の挙動**== 
弾は自機と同じY座標(横線上)に出現させる
そのあと弾はまっすぐ左に進む

:arrow_right:自機の現在位置をつかむ必要がある

----
## 自機の現在位置を取得する

`transform.position`は自分(敵自身)の位置 

自分以外のGameObject情報を知りたい
　　　　　　　　　　　:arrow_down:
```CSharp
GameObject target = GameObject.Find("me");
```
これでHierarchy上にある「me」という名前のオブジェクトが取得できる

----
## 自機の現在位置を取得する

このオブジェクトの`transform`情報を参照したい<br>
　　　　　　　　　　　:arrow_down:

```CSharp
GameObject target = GameObject.Find("me");
float targetPosY = target.transform.position.y;
```
----
# 5
# 弾の生成
----

## 弾の生成
ここまでで弾を生成する準備が整った
:white_check_mark:弾の用意
:white_check_mark:弾の読み込み
:white_check_mark:生成された後の弾の挙動

----
## 弾の生成

GameObjectの生成は以下のように書く
```CSharp
GameObject bullet = Resources.Load <GameObject>("bullet");
Instantiate (bullet);
```
  
1. `Instantiate()`関数に作りたいGameObjectを投げる
2. 作ってくれる
3. おわり

---- 
## 弾の生成

==**問題点**==
このままでは生成したい位置を指定できない

==**解決策**==
実は、`Instantiate()`関数にも返り値がある
:arrow_right:生成したGameObjectを返してくれる

----

## 弾の生成

```CSharp
//弾の読み込み
GameObject bullet = Resources.Load <GameObject>("bullet");

//座標の取得
GameObject target = GameObject.Find("me");
float targetPosY = target.transform.position.y;

//弾の生成
GameObject clone = Instantiate (bullet);
clone.transform.position = new Vector2 (-4f, targetPosY);
```
----
## 弾の生成
==**問題点**==
このまま弾の生成処理を`Update()`に書く
:arrow_right:フレーム毎に弾が作られる
:arrow_right:地獄のような弾幕が作られる

==**解決策**==
そこで、弾の生成に間隔を設ける

フレームごとではなく、実時間で管理したい
:arrow_right:Timeクラス

----
## 弾の生成

Timeクラスは1フレームにかかる時間を教えてくれる
`deltaTime`という変数を持っている
<br>

```CSharp
float time = 0;
void Update(){
	time += 1f * Time.deltaTime;
}
```
このように書くと、
変数`time`は1秒に1だけ増えていく変数となる

----
## 弾の生成

それでは、1秒ごとに1つ弾を生成するプログラムを書いてみましょう。

(資料ページ参照)

これをHierarchyウィンドウから敵にくっつける
:arrow_right:生成した後はPrefabにくっつけたプログラムによって弾はそれぞれ勝手に動く


----
<!-- template: default -->
# まとめ

- ゲーム中でGameObjectをつくる
:arrow_right:Prefabとして事前に準備
:arrow_right:Instantiate()でPrefabを生成

- 他のGameObjectを参照する
:arrow_right:GameObject.Find("")で見つけることが出来る


----
<!-- template: gaia -->
<!-- page_number: false -->

# やってみよう！
###### 分からないことがあったら周囲の先輩に聞いてみよう

