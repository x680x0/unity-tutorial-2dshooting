<!-- $theme: gaia -->
<style>
.center{
 text-align: center;
}
</style>

<!-- template: gaia -->

# Unity講座
## 6.タイマーの表示

----
<!-- page_number: true -->
<!-- template: default -->
# 目的

:arrow_right_hook:**==前回==**
- プレイヤーの移動
- 敵が弾を撃つ
- 弾に当たったら死ぬ(消滅)

:arrow_right:**==今回==**
タイマーの表示でGUIに触れる

----
# 1
# uGUIとは
----

## GUI？

- ==グラフィカル・ユーザー・インタフェース==の略
- ゲームとユーザーをつなぐ手段のこと
	- ボタン
	- 説明文
	- 行くべき方向を示すアイコン
	- etc

----

## UnityにおけるGUI

Unityでは`uGUI`というものを利用することで簡単に実装することが出来る

これを利用してタイマーの表示をしていこう

----
# 2
# タイマーの表示
----
## Textの作成
:arrow_right:**==目標==**
画面上部中央に、1秒に1ずつ増えるタイマーを実装

まずは文字を表示するuGUIオブジェクトを作る

uGUIもゲームオブジェクト
:arrow_right:`GameObject  > UI`の中から選んで作る

<div class="center">
<img style="width:75%" src="../../Images/6/CreateuGUIText.png">
</div>

----

## Textの作成
以下の3つのGameObjectがHierarchy上に追加される

- Canvas
    - Text
- EventSystem

----

## Canvas

- uGUIを描くための画面全体に広がるキャンバス  
- これがないとuGUIオブジェクトは描画されない
- ==すべてのuGUIオブジェクトはCanvas下に配置==
	- uGUIオブジェクトは`RectTransform`という`Transform`をGUI向けに特化させたコンポーネントを持つため
- Canvasは==複数設置できる==

----
## Text

- Canvas上に文字を表示するコンポーネント
- 文字の内容やフォントサイズ、文字色の変更などが出来る

:arrow_right:実際にテキストを変更してみよう

----
## EventSystem

- ユーザーとuGUIオブジェクトをつなぐ仲介役
- ゲーム中、ボタンのクリックなどのを受け取り、uGUIオブジェクトを操作する
	- 「Inputクラスから情報をuGUIオブジェクトに与える」といったようなことをする必要はない

----
## タイマーの実装

まずは作ったTextにプログラムをくっつける

----

### コンポーネントの取得

:arrow_right_hook:**==今まで==** 
Transformコンポーネントへのアクセス`gameObject.transform....`

これはTransformがすべてのGameObjectにくっついているから出来た

:arrow_right:**==Tarsnform以外==** 
`GetComponent<T>()`関数を使って取得する必要

----
### コンポーネントの取得

**==<例>==** `Rigidbody2D`コンポーネントの取得

```CSharp
    Rigidbody2D rectTransformComponent = 
    	gameObject.GetComponent<Rigidbody2D> ();
```

`<>`の中に欲しいコンポーネントの種類を書くと、
その前に書いたGameObjectにくっついているコンポーネントが返ってくる

----
### Textコンポーネントの取得

最初のままだとuGUIを使えない
:arrow_right:プログラムでこれからuGUIを使うことを明示

冒頭に以下の一行を追加する

```CSharp
    using UnityEngine.UI;
```

----
### Textコンポーネントの取得
それでは、Textコンポーネントを取得してみよう

```CSharp
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	Text textComponent;
	void Start (){
		textComponent = GetComponent<Text> ();
	}
}
```

:collision:**==注意==**
ゲームが始まってからコンポーネントを取得する
`Start()`あるいは`Update()`内で呼ぶ

----
### タイマーの実装

Timeクラスを使って1秒に1カウントする

```C
float timer = 0;
void Update () {
    timer += 1f * Time.deltaTime;
}
```

----
### タイマーの実装

この値をTextコンポーネントに適用させる  

:collision:**==注意==** 
`float`を`string`に代入することはできない  
:arrow_right:ToString()関数を使うことで文字列型に変換

**==<例>==**`ToString()`関数の使用例

```CS
    string s = (1.14f).ToString();
```
----
### タイマーの実装

更に、タイマーを整数表示にするため、
`ToString()`関数を使ってケタ数の指定をする

```CSharp
    string s = (1.14f).ToString("F0");
```

----
### タイマーの実装

整数にした時間をTextコンポーネントに適用する
Textコンポーネントの文字情報を持つ変数は`text`


```C
float timer = 0;
Text textComponent;
void Start (){
	textComponent = this.GetComponent<Text> ();
}
void Update () {
	timer += 1f * Time.deltaTime;
	textComponent.text = timer.ToString("F0");
}
```

これで1秒ずつ値が増えていくタイマーの表示をすることができた

----

# 3
# RectTransformの基本
----

### Anchor(アンカー)の基本

Textを作っただけだと、位置が不適切  
:arrow_right:Transformと同じように、位置の調整

<div class="center">
<img style="width:100%" src="../../Images/6/AdjustTextPosition.png">
</div>

----
### Anchor(アンカー)の基本

:thumbsdown:**==問題点==**
Gameビューのウィンドウサイズを変更
:arrow_right:テキストの位置が不安定 

:thumbsup:**==解決策==**
RectTransformの持つ`Anchor`(アンカー)
:arrow_right:座標の基準を決める

----
### Anchor(アンカー)の基本

RectTransformコンポーネントの左上
<div class="center">
<img style="width:15%" src="../../Images/6/DefaultAnchor.png">
</div>

:arrow_right:RectTransformの持つ **座標の基準点は中央** であることを表す

**==<例>==** 試しに座標を`(0, 0)`にしてみよう

タイマーの表示を画面上部で固定させたい
:arrow_right:`Anchor`を変えてあげる 

----
### Anchor(アンカー)の基本
Anchor表示の部分をクリック
<div class="center">
<img style="width:40%" src="../../Images/6/AnchorPresets.png">
</div>

----
### Anchor(アンカー)の基本
今回タイマーは上部中央に設置したい
:arrow_right:真ん中上段のAnchorを選ぶ

<div class="center">
<img style="width:50%" src="../../Images/6/TopCenterAnchor.png">
</div>

----
### Anchor(アンカー)の基本

この状態で表示したい位置にTextを移動させよう  
:arrow_right:常に画面上部中央に表示されるように


----
<!-- template: default -->
# まとめ

- uGUI
	- uGUIはCanvas上で描かれる
	- uGUIもGameObjectの一種
	- Transformの代わりにRectTransformを持つ
	- uGUIの座標の基準はAnchor
- コンポーネントの取得は`GetComponent<>()`

----
<!-- template: gaia -->
<!-- page_number: false -->

# やってみよう！
###### 分からないことがあったら周囲の先輩に聞いてみよう






