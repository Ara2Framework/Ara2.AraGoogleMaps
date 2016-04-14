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
    public class AraGoogleMapsCircle
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsCircle(AraGoogleMaps vAraGoogleMaps, string vId)
        {
            AraGoogleMaps = vAraGoogleMaps;
            Id = vId;
        }

        public AraGoogleMapsCircle(AraGoogleMaps vAraGoogleMaps, string vId, decimal vRaio, decimal vX, decimal vY) :
            this(vAraGoogleMaps, vId)
        {
            Raio = vRaio;
            X = vX;
            Y = vY;
        }


        public string Id;
        public string BColor="#ff0000";
        public decimal BOpacity = (decimal)0.4;
        public string LColor="#669933";
        public decimal LSize=5;
        public decimal LOpacity = (decimal)0.7;
        public string OpenInfoWindowHtml = "";

        public decimal Raio;
        public decimal X;
        public decimal Y;
        
        internal void SendScript()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.CircleCreate('" + AraTools.StringToStringJS(Id) + "'," + Raio.ToString().Replace(",", ".") + "," + X.ToString().Replace(",", ".") + "," + Y.ToString().Replace(",", ".") + ",'" + AraTools.StringToStringJS(OpenInfoWindowHtml) + "','" + AraTools.StringToStringJS(BColor) + "'," + BOpacity.ToString().Replace(",", ".") + ",'" + AraTools.StringToStringJS(LColor) + "'," + LSize.ToString().Replace(",", ".") + "," + LOpacity.ToString().Replace(",", ".") + ");\n");
        }
    }
}
