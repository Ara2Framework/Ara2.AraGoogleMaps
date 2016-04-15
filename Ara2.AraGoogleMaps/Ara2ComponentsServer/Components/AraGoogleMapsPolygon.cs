// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ara2.Components;

namespace Ara2.Components.GoogleMaps
{
    [Serializable]
    public class AraGoogleMapsPolygon
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsPolygon(AraGoogleMaps vAraGoogleMaps, string vId)
        {
            AraGoogleMaps = vAraGoogleMaps;
            Id = vId;
        }


        public string Id;
        public string BColor="#ff0000";
        public decimal BOpacity = (decimal)0.4;
        public string LColor="#669933";
        public decimal LSize=5;
        public decimal LOpacity = (decimal)0.7;
        public string OpenInfoWindowHtml = "";


        public struct TPolygon
        {
            public decimal X;
            public decimal Y;
        }


        public void AddPolygon(decimal vX, decimal vY)
        {
            TPolygon  P = new TPolygon();
            P.X = vX;
            P.Y = vY;
            AddPolygon(P);
        }

        List<TPolygon> Polygons = new List<TPolygon>();
        public void AddPolygon(TPolygon vPolygon)
        {
            Polygons.Add(vPolygon);
        }

        public void ClearPolygons()
        {
            Polygons = new List<TPolygon>();
        }

        internal void SendScript()
        {
            string StringPolygons = "[";
            foreach(TPolygon P in Polygons)
            {
                StringPolygons += "{X:" + P.X + ",Y:" + P.Y + "},";
            }
            StringPolygons =StringPolygons.Substring(0,StringPolygons.Length-1);
            StringPolygons += "]";

            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.PolygonCreate('" + AraTools.StringToStringJS(Id) + "'," + StringPolygons + ",'" + AraTools.StringToStringJS(OpenInfoWindowHtml) + "','" + AraTools.StringToStringJS(BColor) + "'," + BOpacity.ToString().Replace(",", ".") + ",'" + AraTools.StringToStringJS(LColor) + "'," + LSize.ToString().Replace(",", ".") + "," + LOpacity.ToString().Replace(",", ".") + ");\n");
        }
    }
}
