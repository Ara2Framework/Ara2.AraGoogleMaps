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
    public class AraGoogleMapsMarker
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsMarker(AraGoogleMaps vAraGoogleMaps, string vId)
        {
            AraGoogleMaps = vAraGoogleMaps;
            Id = vId;
        }

        public AraGoogleMapsMarker(AraGoogleMaps vAraGoogleMaps,string vId,decimal vX,decimal vY):
            this(vAraGoogleMaps,vId)
        {
            X = vX;
            Y = vY;
        }

        public AraGoogleMapsMarker(AraGoogleMaps vAraGoogleMaps, string vId, decimal vX, decimal vY, string vImageUrl) :
            this(vAraGoogleMaps, vId, vX, vY)
        {
            ImageUrl = vImageUrl;
        }

        public AraGoogleMapsMarker(AraGoogleMaps vAraGoogleMaps, string vId, decimal vX, decimal vY, string vImageUrl, string vOpenInfoWindowHtml) :
            this(vAraGoogleMaps,vId, vX, vY, vImageUrl)
        {
            OpenInfoWindowHtml = vOpenInfoWindowHtml;
        }

        public string Id;
        public decimal X;
        public decimal Y;
        public string ImageUrl = "http://www.google.com/mapfiles/markerA.png";
        public decimal ImageWidth=20;
        public decimal ImageHeight=34;
        public string ImageShadow = "http://www.google.com/mapfiles/shadow50.png";
        public string OpenInfoWindowHtml = "";

        public void SetFocus()
        {
            //AraGoogleMaps.Call();
            AraGoogleMaps.VisionCenter(X, Y);
        }

        public void SetFocus(decimal vZoom)
        {
            //AraGoogleMaps.Call();
            AraGoogleMaps.VisionCenter(X, Y, vZoom);
        }

        internal void SendScript()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.MarkerCreate('" + AraTools.StringToStringJS(Id) + "'," + X.ToString().Replace(",", ".") + "," + Y.ToString().Replace(",", ".") + ",'" + AraTools.StringToStringJS(ImageUrl) + "','" + AraTools.StringToStringJS(ImageShadow) + "','" + AraTools.StringToStringJS(OpenInfoWindowHtml) + "'," + ImageWidth.ToString().Replace(",", ".") + "," + ImageHeight.ToString().Replace(",", ".") + ");\n");
        }
    }
}
