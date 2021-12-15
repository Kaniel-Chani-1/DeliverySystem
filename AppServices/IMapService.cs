using Google.Maps;
using Google.Maps.Direction;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
  public  interface IMapService
    {
        public LatLng ConvertAddressToPoint(string address);
        public LatLng[] OriginDestinationAndWaypoints(List<LatLng> listPointsAddress);
        public List<LatLng> ConvertAdressListToLatlng(List<Adress> listAdresses);
        public DirectionResponse CreateDirection(List<LatLng> latLngs, LatLng origin, LatLng destination);
        public int GetTimeDirection(DirectionResponse direction);
        public int GetDistanceDirection(DirectionResponse direction);
        public string GetAddressByLongLat(double lat, double lng);
        public double AreaSizePoints(List<LatLng> listPointsAddress);
        public string AddressToString(Adress adress);
        public double DrivingDistancebyAddressAndLngLat(LatLng originAddress, LatLng DestinationAddress);
        public LatLng[] newOriginDestinationAndWaypoints(List<LatLng> listPointsAddress);
        }
}
