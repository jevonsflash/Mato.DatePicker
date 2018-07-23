# Mato.DatePicker 

## 说明
1. 这是一个带有农历日历的日期选择Xamarin控件
2. 可以指定初始日期
3. 多选和单选日期

## 引用
1. PCL：https://www.nuget.org/packages/Mato.DatePicker.PCL/
2. Android：https://www.nuget.org/packages/Mato.DatePicker.Android/
3. iOS: https://www.nuget.org/packages/Mato.DatePicker.iOS/
## 用法
1. 在引用iOS安装包时候需要在`AppDelegate.cs`做如下操作：
```
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
...
    global::Xamarin.Forms.Forms.Init();   
    Mato.DatePicker.iOS.ChinaDateServer ssChinaDateServer=new Mato.DatePicker.iOS.ChinaDateServer(); //在此插入这段语句
    LoadApplication(new App());
}
```
同样的，在引用Android安装包后，需要在`MainActivity.cs`做如下操作：
```
protected override void OnCreate(Bundle bundle)
{
...
    global::Xamarin.Forms.Forms.Init(this, bundle);
    DatePicker.Android.ChinaDateServer ssChinaDateServer = new DatePicker.Android.ChinaDateServer(); //在此插入这段语句
    LoadApplication(new App());
}

```
2. 在这个页面中提供了如何使用这一控件https://github.com/MatoApps/Mato.DatePicker/blob/master/Mato.Sample/Mato.Sample/MainPage.xaml
