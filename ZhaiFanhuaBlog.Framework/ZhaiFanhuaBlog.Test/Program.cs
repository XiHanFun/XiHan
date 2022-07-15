// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:2886e2d9-f017-4e69-83d5-07e28805db92
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-04-10 下午 10:51:26
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Test.Common;

namespace ZhaiFanhuaBlog.Test;

/// <summary>
/// Program
/// </summary>
public class Program
{
    public static Task Main(string[] args)
    {
        Console.WriteLine("Hello ZhaiFanhuaBlog.Test!");
        //TestChinaDate.ChinaDate();
        //await TestCodeFirst.CodeFirst();
        TestDiskInformation.DiskInformation();
        //TestEncryption.Encryption();
        //TestResourceMigration.ResourceMigration();
        Console.ReadKey();
        return Task.CompletedTask;
    }
}