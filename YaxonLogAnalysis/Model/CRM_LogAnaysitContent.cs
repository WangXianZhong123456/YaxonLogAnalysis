using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace YaxonLogAnalysis.Model
{
    // [JsonProperty(PropertyName = "Form")]
    public class CRM_LogAnaysitContent
    {
        public int U { get; set; }

        public string D { get; set; }

        public string C { get; set; }

        public string V { get; set; }


    }


    public class D
    {
        public string T { get; set; }
        public string B { get; set; }
        //public int SerialNum { get; set; }
    }
    #region 总的json类
    public class Form
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public int ShopID { get; set; }
        /// <summary>
        /// 进时间
        /// </summary>
        public string GetTime { get; set; }
        /// <summary>
        /// 进的gps
        /// </summary>
        [JsonProperty(PropertyName = "GPS")]
        public List<GPS> InGpsInfo { get; set; }
        /// <summary>
        /// 离开时间
        /// </summary>
        public string LeaveTime { get; set; }
        /// <summary>
        /// 出的gps
        /// </summary>
        //[JsonProperty(PropertyName = "GPS")]
        //public List<GpsInfo> OutGpsInfo { get; set; }

        /// <summary>
        /// 陈列要求登记
        /// </summary>
        public List<DisplayRegister> DisplayRegister { get; set; }

        /// <summary>
        /// 陈列奖励商品登记
        /// </summary>
        public List<DisplayGift> DisplayGift { get; set; }

        /// <summary>
        /// 陈列奖励现金登记
        /// </summary>
        public List<DisplayMoney> DisplayMoney { get; set; }
        /// <summary>
        /// 陈列执行
        /// </summary>
        public List<DisplayIDs> DisplayIDs { get; set; }
        /// <summary>
        /// 订单单头
        /// </summary>
        public List<OrderHead> OrderHead { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public List<OrderDetail> OrderDetail { get; set; }
        /// <summary>
        /// 订单赠品
        /// </summary>
        public List<OrderDetail> Gift { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 备忘
        /// </summary>
        public string GetLeaveRemark { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public List<StrStoreInOut> StrStoreInOut { get; set; }
        /// <summary>
        /// 资产状态变更
        /// </summary>
        public List<AssetChange> AssetChange { get; set; }
        /// <summary>
        /// 资产报修
        /// </summary>
        public List<AssetRepair> AssetRepair { get; set; }
    }
    #endregion

    #region json子类

    //[JsonProperty(PropertyName = "GPS")]
    public class GPS
    {

        /// <summary>
        /// 状态位0：未定位;1：定位; 2 :Gps 2D定位 3:Gps 3D定位 4:GpsOne定位
        /// </summary>
        [JsonProperty(PropertyName = "S")]
        public int State { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty(PropertyName = "X")]
        public float Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty(PropertyName = "Y")]
        public float Latitude { get; set; }
        /// <summary>
        /// 形变前经度
        /// </summary>
        [JsonProperty(PropertyName = "OX")]
        public float OriginLongitude { get; set; }
        /// <summary>
        /// 形变前纬度
        /// </summary>
        [JsonProperty(PropertyName = "OY")]
        public float OriginLatitude { get; set; }


    }

    public class DisplayRegister
    {

        //陈列政策ID&陈列位置&陈列方式&陈列开始时间&陈列结束时间&奖励类型&陈列架编码|
        /// <summary>
        /// 陈列政策ID
        /// </summary>
        public int DisplayID { get; set; }
        /// <summary>
        /// 陈列位置
        /// </summary>
        public string DisplayPlace { get; set; }
        /// <summary>
        /// 陈列方式
        /// </summary>
        public string DisplayMode { get; set; }
        /// <summary>
        /// 陈列开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 陈列结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 奖励类型
        /// </summary>
        public int flag { get; set; }
        /// <summary>
        /// 陈列架编码
        /// </summary>
        public string DisplayCode { get; set; }

        public override string ToString()
        {
            return DisplayID + "&" + DisplayPlace + "&" + DisplayMode + "&" + BeginTime + "&" + EndTime + "&" + flag + "&" + DisplayCode + "|";
        }

    }

    public class DisplayGift
    {

        // 陈列政策ID，商品ID，每月件数，总件数，每月瓶数，总瓶数;
        /// <summary>
        /// 陈列政策ID
        /// </summary>
        public int DisplayID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 每月件数
        /// </summary>
        public int MonthQuantity { get; set; }
        /// <summary>
        /// 总件数
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 每月瓶数
        /// </summary>
        public int MonthSmallQuantity { get; set; }
        /// <summary>
        /// 总瓶数
        /// </summary>
        public int SmallQuantity { get; set; }

        public override string ToString()
        {
            return DisplayID + "," + CommodityID + "," + MonthQuantity + "," + Quantity + "," + MonthSmallQuantity + "," + SmallQuantity + ";";
        }

    }

    public class DisplayMoney
    {
        //陈列政策ID，每月金额，总金额;
        /// <summary>
        /// 陈列政策ID
        /// </summary>
        public int DisplayID { get; set; }
        /// <summary>
        /// 每月金额
        /// </summary>
        public decimal MonthMoney { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Money { get; set; }

        public override string ToString()
        {
            return DisplayID + "," + MonthMoney + "," + Money + ";";
        }
    }

    public class DisplayIDs
    {
        /// <summary>
        /// 陈列政策ID
        /// </summary>
        public int DisplayID { get; set; }
    }

    public class OrderHead
    {
        //经销商ID，配送商ID，配送模式;
        /// <summary>
        /// 经销商ID
        /// </summary>
        public int FranchiserID { get; set; }
        /// <summary>
        /// 配送商ID
        /// </summary>
        public int DeliverID { get; set; }
        /// <summary>
        /// 配送模式
        /// </summary>
        public int DeliverType { get; set; }


        public override string ToString()
        {
            return FranchiserID + "," + DeliverID + "," + DeliverType + ";";
        }
    }
    public class OrderDetail
    {
        //商品ID，件数，瓶数，配送商ID，配送模式;
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 瓶数
        /// </summary>
        public int Small_Quantity { get; set; }
        /// <summary>
        /// 配送商ID
        /// </summary>
        public int DeliverID { get; set; }

        /// <summary>
        /// 配送模式
        /// </summary>
        public int DeliverType { get; set; }

        public override string ToString()
        {
            return CommodityID + "," + Quantity + "," + Small_Quantity + "," + DeliverID + "," + DeliverType + ";";
        }

    }

    public class StrStoreInOut
    {
        //商品ID，库存件数，库存瓶数，竞品库存件数，竞品库存瓶数,是否有库存,生产日期;
        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; }
        /// <summary>
        /// 库存件数
        /// </summary>
        public int StoreQuantity { get; set; }
        /// <summary>
        /// 库存瓶数
        /// </summary>
        public int StoreSmallQuantity { get; set; }
        /// <summary>
        /// 竞品库存件数
        /// </summary>
        public int RivalQuantity { get; set; }
        /// <summary>
        /// 竞品库存瓶数
        /// </summary>
        public int RivalSmallQuantity { get; set; }
        /// <summary>
        /// 是否有库存
        /// </summary>
        public int haveStock { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string ProduceTime { get; set; }

        public override string ToString()
        {
            return CommodityID + "," + StoreQuantity + "," + StoreSmallQuantity + "," + RivalQuantity + "," + RivalSmallQuantity + "," + haveStock + "," + ProduceTime + ";";
        }
    }
    public class AssetChange
    {
        // 申请类型&资产ID&原因&换机资产ID&退库单号｜
        /// <summary>
        /// 申请类型
        /// </summary>
        public int ApplyType { get; set; }
        /// <summary>
        /// 资产ID
        /// </summary>
        public int PropertyID { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 换机资产ID
        /// </summary>
        public int ChangePropertyID { get; set; }
        /// <summary>
        /// 退库单号
        /// </summary>
        public string ReturnOrderNo { get; set; }

        public override string ToString()
        {
            return ApplyType + "&" + PropertyID + "&" + Remark + "&" + ChangePropertyID + "&" + ReturnOrderNo + "|";
        }

    }

    public class AssetRepair
    {
        //报修ID&报修日期&申请类型&资产ID&故障描述&维修地点&维修地址&维修人&联系电话&派工时间&维修状态&维修内容&原因|
        /// <summary>
        /// 报修ID
        /// </summary>
        public int RepairID { get; set; }
        /// <summary>
        ///报修日期
        /// </summary>
        public string RepairTime { get; set; }
        /// <summary>
        /// 申请类型
        /// </summary>
        public int ApplyType { get; set; }
        /// <summary>
        /// 资产ID
        /// </summary>
        public int PropertyID { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string FaultDescription { get; set; }
        /// <summary>
        /// 维修地点
        /// </summary>
        public string RepairAddress { get; set; }
        /// <summary>
        /// 维修地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 维修人
        /// </summary>
        public int RepairMan { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 派工时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; }
        /// <summary>
        ///维修内容 
        /// </summary>
        public string RepairDescription { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Remark { get; set; }

        public override string ToString()
        {
            return RepairID + "&" + RepairTime + "&" + ApplyType + "&" + PropertyID + "&" + FaultDescription + "&" + RepairAddress + "&" + Address + "&" + RepairMan + "&" + Telephone
                + "&" + StartTime + "&" + RepairStatus + "&" + RepairDescription + "&" + Remark + "|";
        }
    }
    #endregion
}
