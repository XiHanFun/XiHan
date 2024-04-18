## XiHan.Common.Utilities

曦寒公共通用组件库

## 如何使用

项目引用此 Nuget 包

依赖注入

```csharp
services.RunModuleInitializers(configuration)
```

## 包含功能

### 加密解密

#### AES加密解密

```csharp
// 加密
AesEncryptionHelper.Encrypt(plainText,password);
// 解密
AesEncryptionHelper.Decrypt(cipherText,password);
```

#### DES加密解密

```csharp
// 加密
DesEncryptionHelper.Encrypt(plainText);
// 解密
DesEncryptionHelper.Decrypt(cipherText);
```

#### MD5加密

```csharp
// 对字符串进行 MD5 生成哈希
Md5HashEncryptionHelper.Encrypt(plainText);

// 对数据流进行 MD5 生成哈希
Md5HashEncryptionHelper.EncryptStream(inputPath);
```

#### RSA加密解密

```csharp
// 加密

// 生成密钥对，并将公钥和私钥存储到文件中
RsaEncryptionHelper.GenerateKeys(publicKeyPath,privateKeyPath);
// 使用公钥加密数据
string result = RsaEncryptionHelper.Encrypt(plainText);

// 解密

// 加载一个已有的 RSA 密钥对
RsaEncryptionHelper.LoadKeys(publicKeyPath,privateKeyPath);
// 使用私钥解密数据
string result = RsaEncryptionHelper.Decrypt(cipherText);
```

#### SHA加密

```csharp
// 生成 SHA1 哈希值
ShaHashEncryptionHelper.Sha1(plainText);

// 生成 SHA256 哈希值
ShaHashEncryptionHelper.Sha256(plainText);

// 生成 SHA384 哈希值
ShaHashEncryptionHelper.Sha384(plainText);

// 生成 SHA512 哈希值
ShaHashEncryptionHelper.Sha512(plainText);
```

### 自定义异常

```csharp
// 创建一个自定义异常
throw new CustomException("自定义异常");
```

### 拓展

#### 控制台输出拓展

```csharp
// 正常信息
inputStr.WriteLineInfo();
// 成功信息
inputStr.WriteLineSuccess();
// 处理、查询信息
inputStr.WriteLineHandle();
// 警告、新增、更新信息
inputStr.WriteLineWarning();
// 错误、删除信息
inputStr.WriteLineError();

```

#### 编码解码拓展

```csharp
// Base32 编码
data.Base32Encode();
// Base32 解码
data.Base32Decode();

// Base64 编码
data.Base64Encode();
// Base64 解码
data.Base64Decode();

// HTML 编码
data.HtmlEncode();
// HTML 解码
data.HtmlDecode();

// URL 编码
data.UrlEncode();
// URL 解码
data.UrlDecode();

// Unicode 编码
data.UnicodeEncode();
// Unicode 解码
data.UnicodeDecode();

// 将字符串转化为二进制
data.BinaryEncode();
// 将二进制转化为字符串
data.BinaryDecode();

// 将字符串转化为文件流
data.ToStream();

```

#### 枚举拓展

```csharp
// 获取枚举的描述
EnumHelper.GetDescription(enumValue);
// 获取枚举的值
EnumHelper.GetEnumValue(enumValue);
// 获取枚举的名称
EnumHelper.GetEnumName(enumValue);
// 获取枚举的键值对
EnumHelper.GetEnumKeyValue(enumValue);
```

