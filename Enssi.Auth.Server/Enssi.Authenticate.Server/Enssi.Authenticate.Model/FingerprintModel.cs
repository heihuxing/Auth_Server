using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enssi.Authenticate.Model
{
    /// <summary>
    /// 指纹模型
    /// </summary>
    public class FingerprintModel
    {
        /// <summary>
        /// int型识别码或顺序号
        /// </summary>
        public int FID { get; set; }
        /// <summary>
        /// Guid型识别码
        /// </summary>
        public Guid GID { get; set; }
        /// <summary>
        /// string型识别码或字段
        /// </summary>
        public string SID { get; set; }
        /// <summary>
        /// string型字段
        /// </summary>
        public string SValue { get; set; }
        /// <summary>
        /// 指纹
        /// </summary>
        public byte[] FingerValue { get; set; }
    }
}
