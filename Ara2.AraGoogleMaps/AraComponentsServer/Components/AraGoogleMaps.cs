// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Ara2;
using Ara2.Components.GoogleMaps;
using Ara2.Dev;

namespace Ara2.Components
{
    [Serializable]
    [AraDevComponent(vConteiner:false)]
    public class AraGoogleMaps : AraComponentVisualAnchor
    {
        public AraGoogleMaps(IAraContainerClient ConteinerFather, string vKey) :
            this(ConteinerFather)
        {
            Key = vKey;
        }

        public AraGoogleMaps(IAraContainerClient ConteinerFather)
            : base(AraObjectClienteServer.Create(ConteinerFather, "div"), ConteinerFather, "AraGoogleMaps")
        {
            Click = new AraComponentEvent<EventHandler>(this, "Click");

            Marker = new AraGoogleMapsMarkers(this);
            Polygons = new AraGoogleMapsPolygons(this);
            Circles = new AraGoogleMapsCircles(this);

            this.SetProperty += this_SetProperty;
            this.EventInternal += this_EventInternal;
        }

        public override void LoadJS()
        {
            Tick vTick = Tick.GetTick();
            vTick.Session.AddJs("https://www.google.com/jsapi");
            vTick.Session.AddJs("Ara2/Components/AraGoogleMaps/AraGoogleMaps.js");
        }

        public Action ActiveGoogleMaps;
        private void this_EventInternal(String vFunction)
        {
            switch (vFunction.ToUpper())
            {
                case "GOOGLEMAPSACTIVE":
                    if (ActiveGoogleMaps != null)
                    {
                        if (ActiveGoogleMaps.GetInvocationList().Length > 0)
                        {
                            ActiveGoogleMaps();
                        }
                    }
                    break;
                case "CLICK":
                    if (Enabled)
                        Click.InvokeEvent(this, new EventArgs());
                    break;
                case "SEARCHPATHRESULT":
                    {
                        this.TickScriptCall();
                        SearchPathEvent(Json.DynamicJson.Parse(Tick.GetTick().Page.Request["result"]));
                    }
                    break;
                case "SEARCHPOINTRESULT":
                    {
                        this.TickScriptCall();
                        SearchPointEvent(Json.DynamicJson.Parse(Tick.GetTick().Page.Request["result"]));
                    }
                    break;
            }
        }

        #region Eventos
        [AraDevEvent]
        public AraComponentEvent<EventHandler> Click;
        #endregion

        private bool _Enabled = true;
        [AraDevProperty(true)]
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetEnabled(" + (_Enabled == true ? "true" : "false") + "); \n");
            }
        }

        private bool _Visible = true;
        [AraDevProperty(true)]
        [PropertySupportLayout]
        public bool Visible
        {
            set
            {
                _Visible = value;
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetVisible(" + (_Visible == true ? "true" : "false") + "); \n");
            }
            get { return _Visible; }
        }


        private void this_SetProperty(String vNome, dynamic vValor) // ?
        {

            switch (vNome)
            {
                case "getLng()":
                    _X = Convert.ToDecimal(vValor);
                    break;
                case "getLat()":
                    _Y = Convert.ToDecimal(vValor);
                    break;
                case "getZoom()":
                    _Zoom = Convert.ToDecimal(vValor);
                    break;
            }

        }

        private string _Key;
        [AraDevProperty("")]
        public string Key
        {
            get { return _Key; }
            set
            {
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.Key = '" + AraTools.StringToStringJS(_Key) + "'; \n");
                _Key = value;
            }
        }

        private decimal _X;
        [AraDevProperty(0)]
        public decimal X
        {
            get { return _X; }
            set { _X = value; }
        }

        private decimal _Y;
        [AraDevProperty(0)]
        public decimal Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        private decimal _Zoom;
        [AraDevProperty(0)]
        public decimal Zoom
        {
            get { return _Zoom; }
            set { _Zoom = value; }
        }

        public delegate void DSearchPathEvent(List<object> vResult);
        private DSearchPathEvent SearchPathEvent = null;
        public void SearchPath(string vSearch, DSearchPathEvent vEvent)
        {
            SearchPathEvent = vEvent;
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.SearchPath('" + AraTools.StringToStringJS(vSearch) + "'); \n");
        }

        public delegate void DSearchPointEvent(List<object> vResult);
        private DSearchPointEvent SearchPointEvent = null;
        public void SearchPoint(decimal vX, decimal vY, DSearchPointEvent vEvent)
        {
            SearchPointEvent = vEvent;
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.SearchPoint(" + vX.ToString().Replace(",", ".") + "," + vY.ToString().Replace(",", ".") + "); \n");
        }

        public void VisionCenter(decimal vX, decimal vY)
        {
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.SetXY(" + vX.ToString().Replace(",", ".") + "," + vY.ToString().Replace(",", ".") + "); \n");
        }

        public void VisionCenter(decimal vX, decimal vY, decimal vZoom)
        {
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.SetXYZOOM(" + vX.ToString().Replace(",", ".") + "," + vY.ToString().Replace(",", ".") + "," + vZoom.ToString().Replace(",", ".") + "); \n");
        }

        public AraGoogleMapsMarkers Marker;
        public AraGoogleMapsPolygons Polygons;
        public AraGoogleMapsCircles Circles;
        
    }
}
