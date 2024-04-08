#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ReflectionTest
// Guid:50afaf4f-94e9-4574-a506-01c8b5d95565
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/30 7:43:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Utilities.Reflections;

namespace XiHan.Test.Common.Utilities;

/// <summary>
/// ReflectionTest
/// </summary>
public static class ReflectionTest
{
    /// <summary>
    /// Test
    /// </summary>
    public static void Test()
    {
        var assembly1 = ReflectionHelper.GetAllAssemblies();

        var assembly2 = ReflectionHelper.GetAllReferencedAssemblies();
    }
}