using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMathAlgorithm;

[TestClass]
public class AssemblyInitialize
{
    public static int GlobalNumber { get; set; }

    /// <summary>
    /// 어셈블리 내 모든 테스트가 실행되기 전 한 번 호출됩니다.
    /// </summary>
    /// <param name="context"></param>
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        GlobalNumber = 1;
    }

    /// <summary>
    /// 어셈블리 내 모든 테스트가 실행된 후 한 번 호출됩니다.
    /// </summary>
    /// <param name="context"></param>
    [AssemblyCleanup]
    public static void Cleanup()
    {
        GlobalNumber = -1;
    }
}
