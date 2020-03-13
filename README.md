# 省心签API-C# 接入文档

本项目是Visual Studio工程，包含两个子模块`SxqApiSample`和`SxqSDK`。
主要的使用示例都在`Pro[SxqApiSample.Program.cs](./SxqApiSample/Program.cs)`。

## 接入步骤
1. 创建并登录省心签账户，[官网网址](https://sxqian.com)。
2. 在`账户管理`->`基本资料`里申请并获取`AppKey`和`AppSecret`。
3. Clone本工程并集成（或拷贝必要的代码片段）到您的业务系统中。
4. 参考[Program.cs](./SxqApiSample/Program.cs)里的用例进行调试。
5. 请求参数与请求事例参数有出入请以请求参数为准，请求事例含有的参数而请求参数中没有的参数为sdk生成。

## API列表

#### 环境感知

0-测试&联调环境; 1-线上环境。

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
|data. serviceTime     |服务器时间

##### *示例代码*
请参见 [Program.cs#Ping](./SxqApiSample/Program.cs)

---

#### 快捷签约

用户发起一次电子签约，签约调用方必须包含甲方。可以有`多签约人`和`多签约方`，目前支持`自动签约`和`手动签约`两种方式。
- 一次电子签约过程中，可以为不同的签约人选择`自动签约`与`手动签约`，即一份签约文件里面可以存在多种签约方式。
- 自动签约：为签约人指定`自动签约`时，将不会发送签约邀请链接，系统会自动为该签约人完成签约并加盖印章。
- 手动签约: 为签约人指定`手动签约`时，系统将会发送一条签约邀请给该签约人。通过邮件或短信进行发送，用户需根据指引完成签约。
- 信息脱敏：可以为用户的姓名和身份证号等敏感信息设置是否使用掩码进行脱敏。`realNameMask-姓名开启掩码`和`certNoMask-身份证开启掩码`:
在[QuickContract.cs](./SxqSDK/SxqCore/Bean/Quick/QuickContract.cs)类中配置是全局的，
也可以在[QuickSignatory.cs](./SxqSDK/SxqCore/Bean/Quick/QuickSignatory.cs)类为每个签约人单独配置。

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
&yclSignatoryList[0].realName=%E5%A7%93%E5%90%8D
&yclSignatoryList[1].realName=%E5%A7%93%E5%90%8D
&yclSignatoryList[1].groupChar=b
&yclSignatoryList[2].signatoryAuto=YES
&yclSignatoryList[0].groupName=%E7%94%B2%E6%96%B9
&yclSignatoryList[0].signaturePage=1
&yclSignatoryList[1].signatoryTime=2018-2-28
&yclSignatoryList[1].phone=15123164744
&yclSignatoryList[1].signatoryUserType=PERSONAL
&yclDataStore.isPublic=PUBLIC
&yclSignatoryList[0].sealType=OFFICIAL
&yclSignatoryList[0].signatoryUserType=PERSONAL
&yclSignatoryList[1].groupName=%E4%B9%99%E6%96%B9
&yclSignatoryList[2].signatoryUserType=PERSONAL
&yclSignatoryList[1].signaturePage=1
&yclDataStore.userBizNumber=20200305175927990718
&yclSignatoryList[2].certType=ID
&yclSignatoryList[2].groupChar=b
&yclSignatoryList[2].phone=15123164744
&yclSignatoryList[1].signatoryAuto=YES
&yclSignatoryList[2].sealType=PERSONAL
&yclSignatoryList[0].signatoryTime=2018-2-28
&yclSignatoryList[2].signaturePage=2
&yclSignatoryList[0].email=zjq115097475%40qq.com
&yclSignatoryList[2].signatureY=20.0
&yclSignatoryList[2].groupName=%E4%B9%99%E6%96%B9
&yclSignatoryList[2].signatureX=20.0
&yclSignatoryList[2].realName=%E5%BC%80%E6%88%B7%E9%93%B6%E8%A1%8C
&yclSignatoryList[0].signatoryAuto=YES
&yclDataStore.appSecret=%E6%82%A8%E7%9A%84appSecret
&yclSignatoryList[0].signatureY=100.0
&yclSignatoryList[0].sealPurpose=%E5%90%88%E5%90%8C%E4%B8%93%E7%94%A8%E7%AB%A0
&yclSignatoryList[0].signatureX=100.0
&yclSignatoryList[1].signatureY=100.0
&yclDataStore.appKey=%E6%82%A8%E7%9A%84appKey
&yclSignatoryList[1].signatureX=100.0
```
##### *请求参数*
请参见 [QuickContract.cs](./SxqSDK/SxqCore/Bean/Quick/QuickContract.cs)

|字段|类型|空|默认|注释|
|:----          |:-------       |:---|---|------                                           |
|pdfFileBase64  |string         |否  |   | 文件内容（格式要求为: 文件名 + @ + 文件的Base64编码）   |
|dataStore   |QuickDataStore   |否  |   | 合同基本信息:[QuickDataStore](./SxqSDK/SxqCore/Bean/Quick/QuickDataStore.cs)|
|signatoryList  |List<QuickSignatory>   |否  |   | 签约人信息:[QuickSignatory.cs](./SxqSDK/SxqCore/Bean/Quick/QuickSignatory.cs)|
|realNameMask   |bool        |是  |   | true: 所有签约人姓名打掩码。仅显示姓，其余的显示*号      |
|certNoMask     |bool        |是  |   | true: 所有签约人证件号打掩码。后四位显示*号          |

##### *请求成功*

```
{
    "success": true,
    "message": "保存成功",
    "storeNo": "YC0001046440",
}
```

##### *请求失败*

```
{
    "success": false,
    "message": "错误原因",
    "storeNo": "YC0001046440",
}
```

##### *返回参数说明*

|字段|类型|空|默认|注释|
|:----    |:-------    |:--- |---|------      |
|success    |boolean     |否 |  | 是否成功 true 为成功，false 为失败  |
|message |String |否 |    |   描述  |
|storeNo |String |否 |    |   合同签约后的存证编号  |


##### *示例代码*
请参见 [Program.cs#QuickSignContract](./SxqApiSample/Program.cs)

---

#### 取回文件
取回电子签约或存证的文件，返回的是数据流（将数据流保存成本地的pdf文件即可）。

##### *请求地址*
```
/api/fileNotary.json
```

##### *请求示例*
```
https://mock.sxqian.com/api/fileNotary.json?appKey=%E6%82%A8%E7%9A%84appKey
&appSecret=%E6%82%A8%E7%9A%84appSecret
&storeNo=YC0001046440
```

##### *参数*

|字段|类型|可为空|默认|注释|
| ------------ | ------------ | ------------ | ------------ | ------------ |
|storeNo   |String   |否   |   |成功签约后的存证编号   |
|appKey         |String         |否  |   | 用户appkey    |
|appSecret      |String         |否  |   |  用户appSecret   |


##### *示例代码*
请参见 [Program.cs#Fetch](./SxqApiSample/Program.cs#Fetch)

