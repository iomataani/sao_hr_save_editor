# Sword Art Online: Hollow Realization Save Editor

Save editor for Sword Art Online: Hollow Realization (PSVita/Switch/PC).

## Introduction

This project is **not** mine. I'm putting on GitHub just for preservation. The real developer is [Falo](https://gbatemp.net/members/falo.310561/) who can contact me if there's a need to remove this repository.

You can find the code and a builded version in this GBATemp [Thread](https://gbatemp.net/threads/is-it-possible-to-transfer-a-save-data-of-sao-hr-de-from-pc-to-nintendo-switch.537976/).

## Important

### PS Vita to PC Users

If you want to convert your save data from PS Vita to PC, you need to edit the save file after the save file conversion because the controller stick won't work.

From [this](https://gbatemp.net/threads/is-it-possible-to-transfer-a-save-data-of-sao-hr-de-from-pc-to-nintendo-switch.537976/post-9833490) post you need to go from address `0x007AE08` to address `0x007AE57` and change to:

```
01 00 00 00 02 00 00 00 00 00 00 00 03 00 00 00 08 00 00 00 09 00 00 00 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00 0C 00 00 00 0E 00 00 00 0F 00 00 00 0D 00 00 00 11 00 00 00 13 00 00 00 10 00 00 00 12 00 00 00 0A 00 00 00 0B 00 00 00
```

This is the result:

```text
0x007AE00 | -- -- -- -- -- -- -- -- 01 00 00 00 02 00 00 00
0x007AE10 | 00 00 00 00 03 00 00 00 08 00 00 00 09 00 00 00
0x007AE20 | 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00
0x007AE30 | 0C 00 00 00 0E 00 00 00 0F 00 00 00 0D 00 00 00
0x007AE40 | 11 00 00 00 13 00 00 00 10 00 00 00 12 00 00 00
0x007AE50 | 0A 00 00 00 0B 00 00 00 -- -- -- -- -- -- -- --
```
