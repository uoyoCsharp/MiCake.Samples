
# MiCake.Uow.Easy

## 介绍

一个简单的工作单元实现。它是米蛋糕中工作单元的超简易版本，并不代表米蛋糕中真正的工作单元，该项目仅供参考使用。

## 构建步骤

+ 设置 **EasyUowApplication** 为项目启动项
+ 在Startup.cs中替换您的数据库链接字符串。并在数据库中创建对应数据库（案例中为mariadb中创建uowexample库）。

````c#
//将数据库链接字符串替换为您的数据库
services.AddDbContext<UowAppDbContext>(options =>
{
    options.UseMySql("Server=localhost;Database=uowexample;User=root;Password=a12345;", mySqlOptions => mySqlOptions
        .ServerVersion(new ServerVersion(new Version(10, 5, 0), ServerType.MariaDb)));
});
````

+ 打开程序包管理控制台(ALT + T + N + O),执行EF 迁移命令 ： **dotnet ef migrations add InitialCreate --project EasyUowApplication**
+ 继续执行Update语句： **dotnet ef database update --project EasyUowApplication**
+ OK,F5运行

## 项目配置项

本例子使用了 asp.net core 3.0 开发，所以在运行前请您确保您已经安装了新版的 Visual Studio 以及对应的.NET CORE SDK。

数据访问部分，使用了EF Core作为ORM框架，对应的数据库选取的是MariaDB（version：10.5.0），如果您愿意使用MariaDb作为测试数据库，请到MariaDb官网下载对应版本( [https://mariadb.org/](https://mariadb.org/) )。如果您愿意使用其他的数据库，比如Sqlserver，您可以修改EF Core中的数据库选择部分。

本例子引用了部分米蛋糕（MiCake）的nuget包，这些包目前还处于测试预览阶段，在此处使用仅仅是为了使用米蛋糕中的部分DDD特性接口。有关米蛋糕的后期介绍可以关注 [句幽的博客园](https://www.cnblogs.com/uoyo/) 。它是一个超轻柔的DDD组件，方便您的项目能够快速使用和进化为DDD模式，目前还处于开发阶段，后期测试完成后也将开源。

## 学习版本

+ 如果您是从 [如何运用领域驱动设计 - 存储库](https://www.cnblogs.com/uoyo/p/12097737.html) 文章跳转过来，请将GitHub的Tag标签选择为**repository**，该版本提供了超级简易的写法供您学习仓储和工作单元。

![branch](https://images.cnblogs.com/cnblogs_com/uoyo/1624074/o_191231032002QQ%E6%88%AA%E5%9B%BE20191231111913.png)

+ 其它情况下，您可以选择目前的master最新版本进行clone。

## 说明

本项目仅仅是《如何运用领域驱动设计》博文中的一个案例Demo，它借鉴了米蛋糕中工作单元的部分实现，并将其进行了极度的精简，为的只是让您更好的理解仓储和工作单元。有关《如何运用领域驱动设计》的文章，可以参考 [句幽的博客园](https://www.cnblogs.com/uoyo/) 。

## 问题

由于该项目仅仅是为了演示使用，虽然它实现了外界调用仓储时能自动完成事务，但是它依旧缺少了很多特性：

+ 一个业务操作（一个API）中没有创建多个工作单元的能力
+ 目前事务的操作来源于EF Core的支持，如果项目存在多种数据访问方式（比如一个EF，一个ADO），它们之间如何依靠工作单元来完成事务
+ 没有识别什么时候需要开启工作单元，如果一个操作仅仅需要获取数据，其实我们是不需要开启工作单元的