#### PianoCN
用于打开和修改 AutoDesk® plot files (.pc3, .pmp, .stb, .ctb). 的参考库

PIA文件是原作者针对(.pc3, .pmp, .stb, .ctb)四种文件的简写名,
本工程只是针对原作者进行汉化注释和修改了局部代码,
为了不和原本作者代码进行混淆,所以单独新建了一个CN中文的工程.
原有git工程连接 https://github.com/phusband/PiaNO

=====
#### PianoCN_Form
这里我写了一个winForm,是引用了PianoCN工程,然后调用这个工程内部的函数进行查改增删,
详情你们可以来我的博客浏览 https://www.cnblogs.com/JJBox/p/10909297.html

=====

#### 示例用法：

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


