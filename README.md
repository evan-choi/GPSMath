# README

### Feedback ###
develope_e@naver.com


### GPSMath ###
Because the earth is not a circle, calculating distances with a single radius is prone to error.

So we reduced the error range by calculating the distance based on the radius of the current position (approximate).

Many double operations in C # can degrade software performance. 

So we improved the performance by calculating the distance as an integer through position indexing.

### Example ###

**Radius**
```cs
var ll = new LatLng(<Lat>, <Long>);

// The radius of the current position is calculated based on the pole and the equator.
double r1 = GPSMath.Radius(ll);

// Extension method support
double r2 = ll.Radius();
```

**Distance**
```cs
var l1 = new LatLng(<Lat1>, <Long1>);
var l2 = new LatLng(<Lat2>, <Long2>);

// Calculates the distance based on the radius of the current location using latitude
double d1 = GPSMath.Distance(l1, l2);

// Extension method support
double d2 = l1.Distance(l2);
```

**DistanceCeiling**
```cs
var l1 = new LatLng(<Lat1>, <Long1>);
var l2 = new LatLng(<Lat2>, <Long2>);
double range = 4; // Units are meters

// The distance between l1 and l2 is rounded down to mDistance.
double d1 = GPSMath.DistanceCeiling(l1, l2);

// Extension method support
double d2 = l1.DistanceCeiling(l2);
```

**ToDistanceString**
```cs
double distance = 100; // Units are meters

// Converts metric distances into strings of cm, m, km.
string str = GPSMath.ToDistanceString(distance);

// Extension method support
distance.ToDistanceString();
```

**MidPoint**
```cs
var l1 = new LatLng(<Lat1>, <Long1>);
var l2 = new LatLng(<Lat2>, <Long2>);

// Calculate the center point between l1 and l2
double d1 = GPSMath.MidPoint(l1, l2);

// Extension method support
double d2 = l1.MidPoint(l2);
```

**Bearing**
```cs
var l1 = new LatLng(<Lat1>, <Long1>);
var l2 = new LatLng(<Lat2>, <Long2>);

// Calculates l1 and l2 angles.
double d1 = GPSMath.Bearing(l1, l2);

// Extension method support
double d2 = l1.Bearing(l2);
```

**Indexing**
```cs
var ll = new LatLng(<Lat>, <Long>);

// Creates an index of the current position in 4m distance units.
// The error range is about ±0.5m.
double hash1 = GPSMath.ToIndex(ll);

// Extension method support
double hash2 = ll.ToIndex();
```
