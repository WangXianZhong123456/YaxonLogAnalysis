using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using YaxonLogAnalysis.Model;
using Yaxon.PublicBLL.Public;
using Yaxon.PublicIDAL.Public;

namespace YaxonLogAnalysis.Common
{
        /// <summary>
        /// Json帮助类
        /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        public static List<Object> AndroOrIphoneJsonToParams(string json)
        {
            List<Object> ListParam =null;
            try
            {
                bool flag = false;
                string tempjson = RemoveStr(json);
                string[] arr = new string[] { "D", "B", "Form" };
                ListParam = new List<object>();
                for (int i = 0; i < 5; i++)
                {
                    GeneralJsonConvert JsonPackage = new GeneralJsonConvert(tempjson);
                    switch (i)
                    {
                        case 0:
                            tempjson = RemoveStr(JsonPackage.GetValue(arr[i]));
                            ListParam.Add(JsonPackage.GetValue("U"));//获取personID
                            break;
                        case 1:
                            tempjson = RemoveStr(JsonPackage.GetValue(arr[i])); break;
                        case 2:
                            tempjson = RemoveStr(JsonPackage.GetValue(arr[i])); flag = true; break;
                        default:
                            break;

                    }
                    if (flag)
                    {
                        break;
                    }
                }
                tempjson = AddStr(tempjson);
                GeneralJsonConvert JsonPackages = new GeneralJsonConvert(tempjson);
                List<IGeneralJsonConvert> lstJsonParams = JsonPackages.GetCollection();

                for (int j = 0; j < lstJsonParams.Count; j++)
                {
                    if (lstJsonParams[j].IsValue())
                    {
                        ListParam.Add(lstJsonParams[j].GetValue());
                    }
                    else if (lstJsonParams[j].IsModel())
                    {
                        GeneralJsonConvert JsonPackageGps = new GeneralJsonConvert(lstJsonParams[j].GetValue());
                        GPS gps = DeserializeJsonToObject<GPS>(JsonPackageGps.GetValue("GPS"));
                        ListParam.Add(gps.State);
                        ListParam.Add(gps.Longitude/(float)(3600*1000));
                        ListParam.Add(gps.Latitude/(float)(3600*1000));
                        ListParam.Add(gps.OriginLongitude);
                        ListParam.Add(gps.OriginLatitude);
                    }
                    else
                    {
                        List<IGeneralJsonConvert> lstRows = lstJsonParams[j].GetCollection();
                        string szTmp = "";
                        string szTotal = "";
                        for (int n = 0; n < lstRows.Count; n++)
                        {
                            List<IGeneralJsonConvert> lstCols = lstRows[n].GetCollection();
                            for (int m = 0; m < lstCols.Count; m++)
                            {
                                szTmp = lstCols[m].GetValue().Replace("&", "");
                                szTmp = szTmp.Replace("|", "");
                                szTotal += szTmp;
                                if (m != lstCols.Count - 1) szTotal += "&";
                            }
                            if (n != lstRows.Count - 1) szTotal += "|";
                        }
                        ListParam.Add(szTotal);

                    }
                }
            }
            catch (Exception ex)
            {
                ListParam = null;
                LogUtil.WriteInfo("调用【AndroJsonToParams】方法异常：{0}" + ex.Message);
            }
          
            return ListParam;
        }

        public static string RemoveStr(string str)
        {
            if (str.StartsWith("["))
            {
                str = str.Substring(1, str.Length - 1);
            }
            if (str.EndsWith("]"))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static string AddStr(string str) {
            if (!str.StartsWith("{"))
            {
                str = "{"+str;
            }
            if (!str.EndsWith("}"))
            {
                str = str + "}";
            }
            return str;
        }
    }
}
