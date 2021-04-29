# 数据绑定练习项目

## 开发目的

- 测试数据绑定

## 总结
列表：ObservableCollection

属性:
``` csharp
if (PropertyChanged != null)
{
    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
}
```

修改值是，使用property，而不是field