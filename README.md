# Uni RectTransform Editor

RectTransform の Inspector にリセット、フィット、四捨五入、AddComponent のボタンを追加するエディタ拡張

![](https://img.shields.io/badge/Unity-2018.4%2B-red.svg)
![](https://img.shields.io/badge/.NET-4.x-orange.svg)
[![](https://img.shields.io/github/license/baba-s/uni-recttransform-editor.svg)](https://github.com/baba-s/uni-recttransform-editor/blob/master/LICENSE)

## インストール

```json
"com.baba_s.uni-recttransform-editor": "https://github.com/baba-s/uni-recttransform-editor.git",
```

manifest.json に上記の記述を追加します  

## 使い方

![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130215.png)

|項目|内容|
|:--|:--|
|Reset Rotation|Rotation を ( 0, 0, 0 ) にリセット|
|Reset Scale|Scale を ( 1, 1, 1 ) にリセット|
|Fill|親の描画サイズに合わせる|
|Round|Pos、Rotation、Scale を四捨五入|
|![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130357.png)|CanvasGroup を AddComponent|
|![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130349.png)|HorizontalLayoutGroup を AddComponent|
|![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130355.png)|VerticalLayoutGroup を AddComponent|
|![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130352.png)|GridLayoutGroup を AddComponent|
|![](https://cdn-ak.f.st-hatena.com/images/fotolife/b/baba_s/20190929/20190929130347.png)|ContentSizeFitter を AddComponent|