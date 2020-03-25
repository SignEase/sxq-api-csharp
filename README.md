# 省心签API-C# 接入文档

本项目是Visual Studio工程，包含两个子模块`SxqApiSample`和`SxqSDK`。
主要的使用示例都在[Program.cs](./SxqApiSample/Program.cs)。

## 接入步骤
1. 创建并登录省心签账户，[官网网址](https://sxqian.com)。
2. 在`账户管理`->`基本资料`里申请并获取`AppKey`和`AppSecret`。
3. Clone本工程并集成（或拷贝必要的代码片段）到您的业务系统中。
4. 参考[Program.cs](./SxqApiSample/Program.cs)里的用例进行调试。
5. 请求参数与请求事例参数有出入请以请求参数为准，请求事例含有的参数而请求参数中没有的参数为sdk生成。

## API列表

#### 环境感知

**初始化示例**
```
new SDKClient("您的appKey","您的appSercret", "请求的服务器");
```
请参见 [Program.cs#GetOrCreateClient](./SxqApiSample/Program.cs)

#### 请求地址

|环境          |      HTTPS请求环境地址
|:----         |:-------   
|正式环境      |https://sxqian.com   
|测试环境      |https://mock.sxqian.com

---

####  PING

测试服务器是否连通

##### *请求地址*
```
/api/ping.json
```

##### *请求示例*
```
https://mock.sxqian.com/api/ping.json
```

##### *请求成功*

```
{
    "success": true,
    "message": "服务器连通",
    "data": {
        "serviceTime": "2020-03-12 17:39:46"
    },
}
```

##### *请求失败*

```
{
    "success": false,
    "message": "Connection refused: connect"
}
```

##### *返回参数说明*
|字段          |      注释
|:----         |:-------   
|success      |是否成功，true为成功，false为失败
|message      |返回信息
|data.serviceTime     |服务器时间

##### *示例代码*
请参见 [Program.cs#Ping](./SxqApiSample/Program.cs)

---

#### 电子签约

签约流程：起草签约 -> 获取返回的签约URL -> 浏览器打开URL继续签约

用户发起一次电子签约，签约调用方必须包含甲方。可以有`多签约人`和`多签约方`，目前支持`自动签约`和`手动签约`两种方式。
- 一次电子签约过程中，可以为不同的签约人选择`自动签约`与`手动签约`，即一份签约文件里面可以存在多种签约方式。
- 自动签约：为签约人指定`自动签约`时，将不会发送签约邀请链接，系统会自动为该签约人完成签约并加盖印章。
- 手动签约: 为签约人指定`手动签约`时，系统将会发送一条签约邀请给该签约人。通过邮件或短信进行发送，用户需根据指引完成签约。
- 信息脱敏：可以为用户的姓名和身份证号等敏感信息设置是否使用掩码进行脱敏。`realNameMask-姓名开启掩码`和`certNoMask-身份证开启掩码`:
在[Contract.cs](./SxqSDK/SxqCore/Bean/Contract/Contract.cs)类中配置是全局的，
也可以在[Signatory.cs](./SxqSDK/SxqCore/Bean/Contract/Signatory.cs)类为每个签约人单独配置。

##### *请求地址*
```
/api/signatory.json
```

##### *请求示例*
```
https://mock.sxqian.com/api/signatory.json?pdfFileBase64=demo8.pdf%40PDF文件的base64
&yclSignatoryList[1].certType=ID
&yclSignatoryList[0].groupChar=a
&yclSignatoryList[2].certNo=4355343544353
&yclSignatoryList[1].sealType=PERSONALl
&sign=e393bc5aa0bca81034ce0a59532ef26f
&yclDataStore.storeName=%E3%80%8A%E5%90%88%E5%90%8C%E5%90%8D%E7%A7%B0%E3%80%8B
&yclSignatoryList[1].keywords=%E5%BC%80%E6%88%B7%E9%93%B6%E8%A1%8C
&yclSignatoryList[1].certNo=4355343544353ssss54
&yclSignatoryList[2].signatoryTime=2018-2-28
...
```
##### *请求参数*
请参见 [Contract.cs](./SxqSDK/SxqCore/Bean/Contract/Contract.cs)

|字段|类型|空|默认|注释|
|:----          |:-------       |:---|---|------                                           |
|pdfFileBase64  |string         |否  |   | 文件内容（格式要求为: 文件名 + @ + 文件的Base64编码）   |
|dataStore   |DataStore   |否  |   | 合同基本信息:[DataStore](./SxqSDK/SxqCore/Bean/Contract/DataStore.cs)|
|signatoryList  |List<Signatory>   |否  |   | 签约人信息:[Signatory.cs](./SxqSDK/SxqCore/Bean/Contract/Signatory.cs)|
|signatoryAuto   |string        |是  |   | YES-自动签章， NO-手动签章      |
|allowPreview   |int        |是  |   | 0-关闭登录前预览，1-打开登录前预览      |
|allowPwdSetting   |int        |是  |   | 0-关闭密码设置，1-允许密码设置      |
|realNameMask   |bool        |是  |   | true: 所有签约人姓名打掩码。仅显示姓，其余的显示*号      |
|certNoMask     |bool        |是  |   | true: 所有签约人证件号打掩码。后四位显示*号          |

##### *请求成功*

```
{
    "success": true,
    "message": "保存成功",
    "data": {
        "contractId": 1046570
        "signUrl": "https://mock.sxqian.com/sxq-web/?contractId=1046570"
    }
}
```

##### *请求失败*

```
{
    "success": false,
    "message": "错误原因",
}
```

##### *返回参数说明*

|字段|类型|空|默认|注释|
|:----    |:-------    |:--- |---|------      |
|success    |bool     |否 |  | 是否成功 true 为成功，false 为失败  |
|message |string |否 |    |   描述  |
|data.contractId |long |否 |    |   签约创建后的编号  |
|data.signUrl |string |否 |    |   签约URL，用浏览器打开该URL可继续下一步签约流程  |


##### *示例代码*
请参见 [Program.cs#SignContract](./SxqApiSample/Program.cs)

---

#### 快捷签约

在用户授信情况下，签约人无需手动签章，可以通过调用接口传递签约需要的信息，快捷完成签约。 

##### *请求地址*
```
/api/quickSign.json
```

##### *请求示例*
和电子签约的请求示例一致，请参见: [电子签约-请求示例](#电子签约)

##### *请求参数*
和电子签约的请求参数一致，请参见: [电子签约-请求参数](#电子签约)
唯一不同的是：signatoryAuto被强制设置为"YES"      |
##### *请求成功*

```
{
    "success": true,
    "message": "保存成功",
    "data": {
        "contractId": 1046570
    }
}
```

##### *请求失败*

```
{
    "success": false,
    "message": "错误原因",
}
```

##### *返回参数说明*

|字段|类型|空|默认|注释|
|:----    |:-------    |:--- |---|------      |
|success    |bool     |否 |  | 是否成功 true 为成功，false 为失败  |
|message |string |否 |    |   描述  |
|data.contractId |long |否 |    |   签约创建后的编号  |

可以调用[取回文件接口](#取回文件)，下载该已签署的合同

##### *示例代码*
请参见 [Program.cs#QuickSignContract](./SxqApiSample/Program.cs)

---

#### 取回文件
取回已签约合同或存证的文件，返回的是数据流（将数据流保存成本地的pdf文件即可）。

##### *请求地址*
```
/api/downloadContract.json
```

##### *请求示例*
```
https://mock.sxqian.com/api/downloadContract.json?appKey=%E6%82%A8%E7%9A%84appKey
&appSecret=%E6%82%A8%E7%9A%84appSecret
&contractId=1046570
```

##### *参数*

|字段|类型|可为空|默认|注释|
| ------------ | ------------ | ------------ | ------------ | ------------ |
|contractId   |long   |否   |   |签约创建后的编号   |
|appKey         |string         |否  |   | 用户appkey    |
|appSecret      |string         |否  |   |  用户appSecret   |


##### *示例代码*
请参见 [Program.cs#Download](./SxqApiSample/Program.cs#Download)

