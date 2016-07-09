- title : やってみようType Provider
- description : やってみようType Provider
- author : pocketberserker
- theme : sky
- transition : default

***

## やってみようType Provider

ML勉強会

***

### 自己紹介

![icon](https://camo.githubusercontent.com/5dbd18d5fc15054677aaab64d647c4a076483af4/68747470733a2f2f646c2e64726f70626f7875736572636f6e74656e742e636f6d2f752f35373437383735382f7062736b2e6a7067)

* なかやん・ゆーき / ぺんぎん / もみあげ
* [@pocketberserker](https://twitter.com/pocketberserker)
* Microsoft MVP for <del>F#</del> Visual Studio and Development Technologies (2015/04/01～ 2017/03/31)
* 仕事はScala

***

### 発表開始の前に

* 発表内容変えました
* `コンピュテーション式とprintfで作るlogging`は特に面白くなかった

***

### FSharpとは

* ML族らしい？
* でも今回はMLらしくない話です（いつもじゃなイカ？）

***

### ところで

* 型レベル自然数やFizzBuzzの記事をよく見かける（要出典）
* でも型を手作業で定義するのはつらい…
* 型なんて 生成すれば いいんだよ

***

### FSharpでの型生成

* Type Provider
* コンパイル時にコード生成に頼らずに型を生成する仕組み
* [FSharp.TypeProviders.StarterPack](https://github.com/fsprojects/FSharp.TypeProviders.StarterPack)を使えば割と楽に型生成できる

***

### Type Provider実用例

* [FSharp.Text.RegexProvider](https://github.com/fsprojects/FSharp.Text.RegexProvider)
* [FSharp.Data](https://github.com/fsharp/FSharp.Data)のJSON Provider他

何らかの値から型を生成するようだ、という雰囲気を感じましょう

***

### 自然数

https://github.com/pocketberserker/MLStudy/blob/9a0a2795d5f8915cab89a5badf65d3abe17b1cf1/src/MLStudy.Core/FizzBuzzProvider.fs#L18

* コンパイル時に計算できるものは計算し、それ以外は式木で処理を定義する
* `Provided~`というもので型やプロパティ、メソッドを定義する
* 今回は以下のような方針
  1. パラメータからmin, maxを取得してリストを用意して
  1. コンパイル中にFizzBuzzを計算して
  1. 型を定義して
  1. プロパティを登録

***

### 例の前に

FizzBuzzをprintする補助関数を用意する。

```fsharp
let inline dump (value: ^n) = (^n: (member FizzBuzz: string) value) |> printfn "%s"
```

`静的に解決された型パラメータ`で`FizzBuzz`プロパティを持つ型のみを受け付ける。

***

### 利用例

```fsharp
type FizzBuzz = FizzBuzzProvider<1, 100>

dump FizzBuzz.``1``
dump FizzBuzz.``2``
dump FizzBuzz.``3``
dump FizzBuzz.``4``
dump FizzBuzz.``5``
```

```
1
2
Fizz
4
Buzz
```

***

### 不満点

* 足し算ができない
* 演算子が使いたい

***

### メソッドを生やす

https://github.com/pocketberserker/MLStudy/commit/aee21c03a3d7e804c48e9c2fc13655c459645e40

* 生成した型のペアを用意する
* 足し算結果の型が生成済みならオペレータ用のメソッドを定義
  * 存在しない数値は用意できないので…

***

### 使ってみる

```fsharp
FizzBuzz.``10`` + FizzBuzz.``5`` |> dump
```

```
FizzBuzz
```

***

### 考察

* 型レベルというかコンパイル時では？
* はい

### まとめ

* F#にはF#なりの<del>遊び道具</del>良い仕組みがある
* MLとは何だったのか

