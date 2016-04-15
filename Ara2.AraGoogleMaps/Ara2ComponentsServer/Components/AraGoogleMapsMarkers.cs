// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ara2.Components.GoogleMaps
{
    [Serializable]
    public class AraGoogleMapsMarkers
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsMarkers(AraGoogleMaps vAraGoogleMaps)
        {
            AraGoogleMaps = vAraGoogleMaps;
        }

        public void Clear()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.MarkerClearAll(); \n");

            Markers = new List<AraGoogleMapsMarker>();
        }

        List<AraGoogleMapsMarker> Markers = new List<AraGoogleMapsMarker>();
        public void Add(AraGoogleMapsMarker vMarker)
        {
            Markers.Add(vMarker);
            vMarker.SendScript();
        }

        public int Count
        {
            get
            {
                return Markers.Count;
            }
        }

        public AraGoogleMapsMarker[] ToArray()
        {
            return Markers.ToArray();
        }

        public AraGoogleMapsMarker this[string vId]
        {
            get
            {
                foreach (AraGoogleMapsMarker M in Markers)
                {
                    if (M.Id == vId)
                        return M;
                }

                return null;
            }
        }

        public void SetFocus()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.MarkerCenter();\n");
        }

    }
}
