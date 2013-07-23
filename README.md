# ux - Micro Xylph

**バージョン: v0.1.4-dev**

ux は軽量でシンプルな動作を目標としたソフトウェアシンセサイザです。C# で作られており、Mono 上でも動作します。


## 概要

ux は Xylph (シルフ) の後継として開発されています。Xylph の開発で得られた最低限必要な機能を絞り、なおかつ Xylph よりも軽快に動作するよう設計されています。ux は音楽制作よりも他アプリケーションへの組み込みを想定しています。

ux は モノフォニック、複数パート、ポルタメント、ビブラートなどの機能を持ち、音源として矩形波、16 段三角波、ユーザ波形、線形帰還シフトレジスタによる擬似ノイズ、4 オペレータ FM 音源を搭載しています。

C# から直接曲データを記述できるほか、独自言語 Xylph による記述を予定しています。


## 動作確認
* Mono 2.10.8.1 (Linux Mint 14 64 bit)
* .NET Framework 4.5 (Windows 7 64 bit)
* (内部プロジェクトは互換性を理由に .NET Framework 4.0 をターゲットにしています)


## v0.1.3-devからの主な変更点

* 修正 - エンベロープを高速化
* 修正 - 無音時の処理を修正、高速化
* 修正 - FM音源の処理の見直し、高速化
* 修正 - Gain の最大値を 2.0 に変更
* 修正 - ZeroGate 命令でポルタメントの開始周波数が変更されなかった問題を修正
* 修正 - Master クラスのパート数をコンストラクタで指定できるよう変更
* 修正 - Handle クラスを構造体に変更
* 修正 - Handle クラスのパラメータを整数と実数の組に変更
* 修正 - NoteOn 命令の実数パラメータが Volume 命令 Velocity に適用されるよう変更
* 修正 - 各命令のパラメータ値が範囲制限されていなかった問題を修正
* 修正 - MasterVolume が適用されていなかった問題を修正
* 削除 - PValue 列挙体を削除
* 削除 - HandleType 列挙体の定数値指定を削除
* 削除 - メソッド Handle.Compare を削除
* 削除 - Handle クラスの ID プロパティを削除
* 削除 - FM音源の Amplifier パラメータを削除
* 追加 - FM音源の変調パラメータにエンベロープを追加
* 追加 - Handle クラスのコンストラクタを追加
* 追加 - メソッド Master.PushHandle にオーバーロードを追加
* 追加 - パラメータ用の各種列挙体を追加
* 追加 - 静的メソッド Envelope.CreateConstant を追加
* 追加 - uxMidi プロジェクトの追加
* 追加 - uxConsole プロジェクトの追加

## ライセンス
Copyright &copy; 2013 Tomona Nanase.

MIT ライセンス
