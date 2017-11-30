using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YaxonLogAnalysis.Model
{
    public class UpDefinedVisit
    {
        public int schemeId;
        public int targetType;
        public int relatedVisitId;
        public int isRouteVisit;
        public int shopId;
        public string date;
        public string inTime;
        public POS inPos;
        public string outTime;
        public POS outPos;
        public int isPass;
        public string passReason;
        public List<DefinedField> defined;
        public List<Proc> proc;
        public string LogPrint;
    }
    public class Photo
    {
        public string photoId;
    }
    public class DefinedField
    {
        public string markId;
        public string type;
        public string value;
        public int valueType;
    }

    public class Proc
    {
        public string proc;
        public string value;
        public int stepId;
    }

    public class GpsData
    {
        public int s;
        public string lt;
        //public string t;
        public int x;
        public int y;
        public int v;
        public int d;
    }

    public class GsmData
    {
        public int t;
        public int l;
        public int c;

    }

    public class CdmaData
    {
        public int sid;
        public int nid;
        public int bsid;
    }

    public class BaiduGps
    {
        public string lt;
        public int s;
        public int x;
        public int y;
        public string p;
    }

    public class POS
    {
        public string t;
        public GpsData gps;
        public BaiduGps bd;
        public int gt;
        public GsmData gsm;
        //public CdmaData cdma;

    }
}
