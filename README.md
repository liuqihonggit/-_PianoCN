# CadPiaNO_CN

Acad打印配置解析库



## 简要说明:

PianNo工程:

用于打开和修改 AutoDesk® plot files (.pc3, .pmp, .stb, .ctb). 的参考库

PIA文件是原作者针对(.pc3, .pmp, .stb, .ctb)四种文件的简写名,

本工程只是针对原作者进行汉化注释和修改了局部代码.



PianoCN_Form工程:

这里我写了一个winForm,是引用了PiaNOCN工程,然后调用这个工程内部的函数进行查改增删.



汉化版工程已经移植到gitee上面,githun的不再维护.

github地址是: https://github.com/jingjingbox/-_PianoCN

gitee地址是:  https://gitee.com/inspirefunction/PianNoCN



## 技术文章： 

动图演示和其他必须要的说明信息,可以来我的博客浏览 https://www.cnblogs.com/JJBox/p/10909297.html



## 示例用法：

##### 读取和保存绘图仪配置 (.PC3)
```csharp
string supportPath = @"C:\Plot Support";
string configName = Path.Combine(supportPath, "DWG To PDF.pc3");
var pdfConfig = new PlotterConfiguration(configName);

pdfConfig.TruetypeAsText = true;
pdfConfig.SetCustomValue("Include_Layer", false);
pdfConfig.SetCustomValue("Create_Bookmarks", false);

pdfConfig.Write(Path.Combine(supportPath, "DWG To PDF - NoLayersOrBookmarks.pc3"));
```



## 作者信息:

为了不和原本作者代码进行混淆.

原有git工程连接 https://github.com/phusband/PiaNO

汉化版工程作者: **惊惊**
