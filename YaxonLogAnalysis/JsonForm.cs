using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YaxonLogAnalysis.Common;

namespace YaxonLogAnalysis
{
    public partial class JsonForm : Form
    {
        public JsonForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Object> paramss = JsonHelper.AndroOrIphoneJsonToParams(textBox1.Text);
            if (paramss != null && paramss.Count==26)
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Green;
                string paramsstr = string.Format("@PersonID={0},\r\n@ShopID={1},\r\n@GetTime='{2}',\r\n@GPSState={3},\r\n" +
                                           "@Longitude={4},\r\n@Latitude={5},\r\n@OriginLongitude={6},\r\n@OriginLatitude={7},\r\n" +
                                           "@LeaveTime='{8}',\r\n@LeaveGPSState={9},\r\n@LeaveLongitude={10},\r\n" +
                                           "@LeaveLatitude={11},\r\n@OriginLeaveLongitude={12},\r\n@OriginLeaveLatitude={13},\r\n@DisplayRegister='{14}',\r\n" +
                                           "@DisplayGift='{15}',\r\n@DisplayMoney='{16}',\r\n@DisplayIDs='{17}',\r\n@OrderHead='{18}',\r\n" +
                                           "@OrderDetail='{19}',\r\n@Gift='{20}',\r\n@Remark='{21}',\r\n@GetLeaveRemark='{22}',\r\n" +
                                           "@StrStoreInOut='{23}',\r\n@AssetChange='{24}',\r\n@AssetRepair='{25}'",
                                           paramss[0], paramss[1], paramss[2], paramss[3], paramss[4], paramss[5],
                                           paramss[6], paramss[7], paramss[8], paramss[9], paramss[10], paramss[11], paramss[12],
                                           paramss[13], paramss[14], paramss[15], paramss[16], paramss[17], paramss[18], paramss[19], paramss[20],
                                           paramss[21], paramss[22], paramss[22], paramss[23], paramss[24], paramss[25]);
                textBox2.Text = paramsstr;
            }
            else
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Red;
                textBox2.Text = "转换失败";

            }





        }
    }
}
