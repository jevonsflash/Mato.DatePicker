using System.Collections.Generic;

namespace Mato.AutoComplete.UWP
{
    public interface IClueObject
    {
        /// <summary>
        /// 线索字符串表
        /// </summary>
        List<string> ClueStrings { get; }
    }
}
