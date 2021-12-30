# PianoCN

Acad打印配置解析库



## 简要说明:

### PianoCN项目:

是用于打开和修改 AutoDesk® plot files(.pc3, .pmp, .stb, .ctb)的库.

"PIA"文件是原作者针对以上四种文件的简写名,

本工程对原作者的文件进行注释和修改了部分错误代码,

由于原作者已经不再更新,同时为了不和原英文项目混淆,特意单独分出这个工程.

并上述项目升级到.net standard工程.



### PianoCN_Form项目:

这里我写了一个WinForm项目,引用了PianoCN工程,然后调用这个工程内部的函数进行查改增删.

给大家提供作为一个调用参考,毕竟单单凭借README.md并不能很好地说明其中的原理.

和github版本不同,此版本升级到了net5,不过受到zip包工程的影响,采取的是兼容性生成.




## 版权

工程已经移植到gitee上面,github的不再维护,延续了原作者的MIT开源协议.

废弃的github地址: https://github.com/liuqihonggit/-_PianoCN

现gitee地址:     https://gitee.com/inspirefunction/PianNoCN



## 技术文章： 

动图演示和其他必须要的说明信息,可以来我的博客浏览 https://www.cnblogs.com/JJBox/p/10909297.html

此工程不包含发布引擎,需要自行编写,而通过.pc3创建.pmp文件部分则是博客中.



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

原作者项目[链接](https://github.com/phusband/PiaNO)

本工程作者: **惊惊**
