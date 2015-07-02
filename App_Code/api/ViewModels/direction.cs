﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace api.ViewModels
{
    [DataContract(Name="direction")]
    public class Direction
    {
        [DataMember]
        public String direction_id { get; set; }
        public Direction() { }
        public Direction(Models.direction direction)
        {
            direction_id = direction.direction_id;
        }
    }
    [DataContract(Name="direction")]
    public class DirectionMapInfo
    {
        [DataMember]
        public String direction_id { get; set; }
        [DataMember]
        public Map_Info map_info { get; set; }
        public DirectionMapInfo() { }
        public DirectionMapInfo(Models.direction direction)
        {
            direction_id = direction.direction_id;
            map_info = new Map_Info(direction);
        }
        public DirectionMapInfo(Models.direction direction, Models.service service)
        {
            direction_id = direction.direction_id;
            map_info = new Map_Info(direction, service);
        }
    }
    [DataContract(Name="direction")]
    public class DirectionTrips : Direction
    {
        [DataMember]
        public IEnumerable<Trip> trips { get; set; }
        public DirectionTrips() { }
        public DirectionTrips(Models.direction direction)
            : base(direction)
        {
            trips = direction.trips.Values.Select(trip => new Trip(trip));
        }
        public DirectionTrips(Models.direction direction, Models.service service)
            : base(direction)
        {
            trips = direction.trips.Values.Where(trip => trip.service_id == service.service_id).Select(trip => new Trip(trip));
        }
    }
}