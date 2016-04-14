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
    public class AraGoogleMapsPolygons
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsPolygons(AraGoogleMaps vAraGoogleMaps)
        {
            AraGoogleMaps = vAraGoogleMaps;
        }

        public void Clear()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.PolygonClear(); \n");

            Polygons = new List<AraGoogleMapsPolygon>();
        }

        List<AraGoogleMapsPolygon> Polygons = new List<AraGoogleMapsPolygon>();
        public void Add(AraGoogleMapsPolygon vPolygon)
        {
            if (this[vPolygon.Id] == null)
            {
                Polygons.Add(vPolygon);
                vPolygon.SendScript();
            }
            else
                throw new Exception("id Polygon duplicate '" + vPolygon.Id + "'");
        }

        public int Count
        {
            get
            {
                return Polygons.Count ;
            }
        }

        public AraGoogleMapsPolygon[] ToArray()
        {
            return Polygons.ToArray();
        }


        public AraGoogleMapsPolygon this[string vId]
        {
            get
            {
                foreach (AraGoogleMapsPolygon M in Polygons)
                {
                    if (M.Id == vId)
                        return M;
                }

                return null;
            }
        }



    }
}
