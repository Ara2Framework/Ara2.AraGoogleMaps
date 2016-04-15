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
    public class AraGoogleMapsCircles
    {
        AraGoogleMaps AraGoogleMaps;
        public AraGoogleMapsCircles(AraGoogleMaps vAraGoogleMaps)
        {
            AraGoogleMaps = vAraGoogleMaps;
        }

        public void Clear()
        {
            AraGoogleMaps.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.CircleClear(); \n");

            Circles = new List<AraGoogleMapsCircle>();
        }

        List<AraGoogleMapsCircle> Circles = new List<AraGoogleMapsCircle>();
        public void Add(AraGoogleMapsCircle vCircles)
        {
            if (this[vCircles.Id] == null)
            {
                Circles.Add(vCircles);
                vCircles.SendScript();
            }
            else
                throw new Exception("id circle duplicate '" + vCircles.Id + "'");
        }


        public int Count
        {
            get
            {
                return Circles.Count ;
            }
        }

        public AraGoogleMapsCircle[] ToArray()
        {
            return Circles.ToArray();
        }


        public AraGoogleMapsCircle this[string vId]
        {
            get
            {
                foreach (AraGoogleMapsCircle M in Circles)
                {
                    if (M.Id == vId)
                        return M;
                }

                return null;
            }
        }

    }
}
