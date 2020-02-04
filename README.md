# LauncherSample
自動アップデート機能つきランチャのサンプル

## 使い方
Settings.settingsの下記を変更してお使いください。

|名前|例|使い方|
|----|----|----|
|Title|`Launcher Sample`|ダイアログに表示するアプリケーション名|
|DownloadTitle|`Downloader`|ダウンロード時のダイアログに表示する名前|
|ExecuteFilePath|`app\app.bat`|実行するアプリケーションの相対パス|
|LocalVersionFilePath|`app\version.txt`|現在のバージョンを記述|
|PatchFilename|`sample_patch_{0}_to_{1}.exe`|パッチのファイル名。拡張子はexeにする。{0}には今のバージョン、{1}にはアップデート後のバージョンを指定|
|RegistryKey|`ExePath`|udm差分ツールのキー名に相当|
|RegistryPathKey|`Software\HogeHoge\Sample`|ルートキー＋サブキーを指定|
|ServerVersionFilePath|`https://hogehoge.jp/sample/{0}.txt`|サーバ側のバージョンファイル。{0}には今のバージョンを指定する。中身はアップデート後のバージョンを書く|
|ServerPatchFilePath|`https://hogehoge.jp/sample/sample_patch_{0}_to_{1}.dat`|サーバ側のパッチファイル。{0}には今のバージョン、{1}にはアップデート後のバージョンを指定||
