# 说明

该文件夹下放置了使用`MiCake`特性的实验性程序，它将随每次`MiCake`的更新进行调整。

## ❄️环境条件

+ `.NET Core SDK 3.0 +`
+ 带有`ASP.NET`和Web开发的 `Visual Studio 2019` 或者`Visual Studio Code`

## 🌼使用

本项目使用`Sqlite`作为数据库，因此您无需下载额外的数据库程序。若您需要查看本地`Sqlite`文件中的数据，推荐您下载[`sqlitebrowser`](https://github.com/sqlitebrowser/sqlitebrowser)。

1. 还原数据库

在`Visual Studio`中选择“工具”>“NuGet 包管理器”>“包管理器控制台”，运行：

```powershell
dotnet ef database update
```

完成时，您将看到项目文件夹下生成了一个名为`micake.db`的文件。

2. 运行

在`Visual Studio`调试运行。

## 🌲备注

`MiCakeFeatures`文件夹下放置了部分使用`MiCake`特性的配置，您可以根据它们来查看`MiCake`新特性的使用方法。