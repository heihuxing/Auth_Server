//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enssi.Authenticate.Data
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// 审批日志:审批日志
    /// </summary>
    public partial class ApproveLog
    {
    	/// <summary>
    	/// ID
    	/// </summary>
    	public System.Guid ID { get; set; }
    	/// <summary>
    	/// 审批流通用Key
    	/// </summary>
    	public string Keys { get; set; }
    	/// <summary>
    	/// 模板ID
    	/// </summary>
    	public System.Guid TemplateID { get; set; }
    	/// <summary>
    	/// 模板详情ID
    	/// </summary>
    	public System.Guid TemplateDetailID { get; set; }
    	/// <summary>
    	/// 流程环节序号
    	/// </summary>
    	public int Rank { get; set; }
    	/// <summary>
    	/// 环节名称
    	/// </summary>
    	public string RankName { get; set; }
    	/// <summary>
    	/// 第几次
    	/// </summary>
    	public int Count { get; set; }
    	/// <summary>
    	/// 当前审批人编号
    	/// </summary>
    	public System.Guid ApproveUserID { get; set; }
    	/// <summary>
    	/// 当前审批人名称
    	/// </summary>
    	public string ApproveUserName { get; set; }
    	/// <summary>
    	/// 审批结果
    	/// </summary>
    	public int Result { get; set; }
    	public string ResultState { get; set; }
    	/// <summary>
    	/// 原因
    	/// </summary>
    	public string Reason { get; set; }
    	/// <summary>
    	/// 当前审批日期
    	/// </summary>
    	public System.DateTime ApproveTime { get; set; }
    	public string HistoryData { get; set; }
    	public Nullable<int> NextRank { get; set; }
    
        public virtual ApproveTemplate ApproveTemplate { get; set; }
        public virtual ApproveTemplateDetail ApproveTemplateDetail { get; set; }
    }
}
